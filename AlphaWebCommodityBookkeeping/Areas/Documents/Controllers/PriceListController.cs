using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using Csla.Web.Mvc;
using Csla.Data;
using System.Collections;


namespace AlphaWebCommodityBookkeeping.Areas.Documents.Controllers
{
    public class PriceListController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /Documents/PriceList/

        public ActionResult Index(bool id = false)
        {
            if (!CheckAccess())
                return View("../Account/AccessDenied");
            System.Web.HttpContext.Current.Session["isDefaultList"] = ViewData["isDefaultList"] = id;
            return View();
        }

        public bool CheckAccess()
        {
            bool ret = true;
            if (!((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).PriceList)
            {
                ret = false;
                //return View("Index");
            }
            return ret;
        }

        //
        // GET: /Documents/PriceList/Create

        public ActionResult CreateAndEdit(int id)
        {
            if (!CheckAccess())
                return View("../Account/AccessDenied");
            cDocuments_PriceList obj;
            ViewData["Id"] = id;

            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["PriceList"] = obj = cDocuments_PriceList.GetDocuments_PriceList(id);

                /* U cache se trpa id dokumenta koji se editira i userId
                   Ako objekt u cache-u postoji, netko vec esitira doc. 
                   Ako je null, doc nije otvoren, lockaj ga
                 */

                var lockCheck = new Dictionary<int, int>();
                lockCheck.Add(id, ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId);

                if (HttpRuntime.Cache[id.ToString()] == null)
                {
                    System.Web.HttpContext.Current.Cache.Add(id.ToString(),
                            lockCheck, null, DateTime.MaxValue,
                            new TimeSpan(0, 20, 0),
                            System.Web.Caching.CacheItemPriority.Default,
                            null);
                }
                else
                {
                    ViewData["Action"] = "locked"; /* obavjesti odmah usera da je doc zalockan */
                }
               
            }
            else
            {
                System.Web.HttpContext.Current.Session["PriceList"] = obj = cDocuments_PriceList.NewDocuments_PriceList();
            }
            ViewData.Model = obj;
            return View();
        } 

        //
        // POST: /Documents/PriceList/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cDocuments_PriceList obj, FormCollection collection)
        {
            LoadProperty(obj, cDocuments_PriceList.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cDocuments_PriceList.EntityKeyDataProperty, enKey);
            }

                if (id == 0)
            {
                string docNum = "0";
                int num = 0;
                //using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                //{
                //    var lastItem = data.vDocuments.OrderByDescending(p => p.Id).FirstOrDefault(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.DocumentType == (short)BusinessObjects.Common.DocumentType.PriceList);
                //    if (lastItem != null)
                //    {
                //        docNum = lastItem.UniqueIdentifier;
                //        docNum = docNum.Substring(0, docNum.LastIndexOf("/"));
                //        num = Convert.ToInt32(docNum);
                //        num += 1;
                //        docNum = num.ToString() + "/11";
                //    }
                //    else
                //    {
                //        docNum = "1/11";
                //    }
                //}
                obj.OrdinalNumber = num;
                obj.UniqueIdentifier = docNum;
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            if (obj.IsValid)
            {
                if (obj.Id > 0)
                {

                    if (IsDocLocked(obj.Id, ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId))
                    {
                        /* Dokument je zalockan, snimanje je zabranjeno, vrati useru CreateAndEdit view s porukom */
                        ViewData.Model = obj;
                        return RedirectToAction("CreateAndEdit/" + id, new { message = "locked" });
                    }

                    if (SaveObject<cDocuments_PriceList>(obj, true))
                    {

                        System.Web.HttpContext.Current.Session["PriceList"] = null;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                }
                else
                {
                    obj.MDSubjects_Employee_AuthorId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                    obj.CreationDateTime = DateTime.Now;

                    obj.DocumentType = (short)BusinessObjects.Common.DocumentType.PriceList;
                    if (SaveObject<cDocuments_PriceList>(obj, false))
                    {
                        System.Web.HttpContext.Current.Session["PriceList"] = null;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                }
            }
            else
            {
                ViewData.Model = obj;
                return View();
            }
        }

        public Boolean IsDocLocked(int docId, int userId)
        {
            //var lockCheck = (IDictionary)HttpRuntime.Cache[docId.ToString()];
            if (HttpRuntime.Cache[docId.ToString()] == null)
                return false;

            int lockedDocId;
            int lockedUserId;
            bool isDocLocked = false;
            bool isUser = false;
            foreach (var item in (IEnumerable)HttpRuntime.Cache[docId.ToString()])
            {
                lockedDocId = Convert.ToInt32(item.ToString().Split(',')[0].Split('[')[1]);
                lockedUserId = Convert.ToInt32(item.ToString().Split(',')[1].Split(']')[0]);
                if (lockedDocId == docId)
                {
                    isDocLocked = true;
                    if (lockedUserId == userId)
                    {
                        isDocLocked = true;
                        if (userId == lockedUserId)
                        {
                            isUser = true;
                            HttpRuntime.Cache.Remove(docId.ToString());
                        }
                        break;
                    }
                }
            }
            /* Da li je doc zalockan? true/false */
            if ((isDocLocked && isUser) || !isDocLocked)
            {
                /* lock je obrisan (snima ga user koji ga je zalockao) ili doc nije zalockan */
                return false;
            }
            return true;
        }

        public void unlockTheDoc()
        {
            cDocuments_PriceList obj = (cDocuments_PriceList)System.Web.HttpContext.Current.Session["PriceList"];

            if (HttpRuntime.Cache[obj.Id.ToString()] == null)
                return;

            foreach (var item in (IEnumerable)HttpRuntime.Cache[obj.Id.ToString()])
            {
                int lockedDocId = Convert.ToInt32(item.ToString().Split(',')[0].Split('[')[1]);
                int lockedUserId = Convert.ToInt32(item.ToString().Split(',')[1].Split(']')[0]);
                if (lockedDocId == obj.Id)
                {
                    if (lockedUserId == ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId)
                    {
                        HttpRuntime.Cache.Remove(obj.Id.ToString());
                        break;
                    }
                }
            }
        }

        public ActionResult Odustani()
        {
            unlockTheDoc();
            System.Web.HttpContext.Current.Session["PriceList"] = null;
            bool id = Convert.ToBoolean(System.Web.HttpContext.Current.Session["isDefaultList"]);
            return RedirectToAction("Index/" + id);
        }

        public ActionResult PriceListGridPartial()
        {
            ViewData["isDefaultList"] = System.Web.HttpContext.Current.Session["isDefaultList"];
            return PartialView("PriceListGridPartial");
        }


        public ActionResult EntityPriceListGridPartial()
        {
            return PartialView("EntityPriceListGridPartial");
        }
        //
        // GET: /Documents/PriceList/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Documents/PriceList/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        object IModelCreator.CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cDocuments_PriceList)))
                return (cDocuments_PriceList)System.Web.HttpContext.Current.Session["PriceList"];
            else return Activator.CreateInstance(modelType);
        }

        #region PriceListCol

        public ActionResult RefreshPriceListColGridPartial()
        {
            cDocuments_PriceList priceList = (cDocuments_PriceList)System.Web.HttpContext.Current.Session["PriceList"];

            ViewData["controllerName"] = "PriceList";
            return PartialView("PriceListColGridPartial", priceList.Documents_PriceList_ItemsCol);
        }

        [HttpPost]
        public ActionResult AddNewItem(cDocuments_PriceList_Items item)
        {
            cDocuments_PriceList priceList = (cDocuments_PriceList)System.Web.HttpContext.Current.Session["PriceList"];

            if (ModelState.IsValid)
            {
                try
                {
                    item.MarkChild();
                    item.Ordinal = priceList.Documents_PriceList_ItemsCol.Count + 1;
                    priceList.Documents_PriceList_ItemsCol.Add(item);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "PriceList";
            return PartialView("PriceListColGridPartial", priceList.Documents_PriceList_ItemsCol);
        }

        [HttpPost]
        public ActionResult UpdateItem(cDocuments_PriceList_Items item, FormCollection collection)
        {
            cDocuments_PriceList priceList = (cDocuments_PriceList)System.Web.HttpContext.Current.Session["PriceList"];

            if (ModelState.IsValid)
            {
                try
                {
                    if (item.Id != 0)
                    {
                        var target = priceList.Documents_PriceList_ItemsCol.SingleOrDefault(p => p.Id == item.Id);
                        if (target != null)
                        {
                            DataMapper.Map(item, target, "DocumentId");
                        }
                    }
                    else
                    {
                        var target = priceList.Documents_PriceList_ItemsCol.SingleOrDefault(p => p.Ordinal == item.Ordinal);
                        if (target != null)
                        {
                            DataMapper.Map(item, target, "Id", "DocumentId", "EntityKeyData");
                        }
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "PriceList";
            return PartialView("PriceListColGridPartial", priceList.Documents_PriceList_ItemsCol);
        }

        [HttpPost]
        public ActionResult DeleteItem(cDocuments_PriceList_Items item)
        {
            cDocuments_PriceList priceList = (cDocuments_PriceList)System.Web.HttpContext.Current.Session["PriceList"];

            if (item.Id > 0)
            {
                try
                {
                    var target = priceList.Documents_PriceList_ItemsCol.SingleOrDefault(p => p.Id == item.Id);
                    priceList.Documents_PriceList_ItemsCol.Remove(target);

                    int count = 1;
                    foreach (var it in priceList.Documents_PriceList_ItemsCol)
                    {
                        it.Ordinal = count;
                        count += 1;
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                var target = priceList.Documents_PriceList_ItemsCol.SingleOrDefault(p => p.Ordinal == item.Ordinal);
                priceList.Documents_PriceList_ItemsCol.Remove(target);

                int count = 1;
                foreach (var it in priceList.Documents_PriceList_ItemsCol)
                {
                    it.Ordinal = count;
                    count += 1;
                }
            }

            ViewData["controllerName"] = "PriceList";
            return PartialView("PriceListColGridPartial", priceList.Documents_PriceList_ItemsCol);
        }

        #endregion


        public ActionResult SubjectIdChanged(FormCollection collection)
        {
            string Street = "";
            string Number = "";
            string PlaceName = "";
            string Oib = "";


            int SubjectId = Convert.ToInt32(collection["subjectId"]);
            //System.Web.HttpContext.Current.Session["PriceListSubjectId"] = SubjectId;

            using (DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities())
            {
                var itemSubject = data.MDSubjects_Subject.FirstOrDefault(p => p.Id == SubjectId);
                Street = itemSubject.HomeAddress_Street;
                Number = itemSubject.HomeAddress_Number;
                int? PlaceId = itemSubject.HomeAddress_PlaceId;
                Oib = itemSubject.OIB;

                DalEf.MDPlacesEntities context = new DalEf.MDPlacesEntities();
                var place = context.MDPlaces_Enums_Geo_Place.FirstOrDefault(p => p.Id == PlaceId);
                if (place != null)
                {
                    PlaceName = place.Name;
                }

            }

            string[] list = { Oib, Street, Number, PlaceName };

            JsonResult result = new JsonResult() { Data = list };
            return result;
        }

    }
}
