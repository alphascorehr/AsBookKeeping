using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.Documents;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using Csla.Data;
using System.Collections;

namespace AlphaWebCommodityBookkeeping.Areas.Documents.Controllers
{
    public class IncomingInvoiceController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /Documents/IncomingInvoice/
        //[OutputCache(Duration = 600, VaryByParam = "None")]
        public ActionResult Index()
        {
            if (!CheckAccess())
                return View("../Account/AccessDenied");
            return View();
        }

        public bool CheckAccess()
        {
            bool ret = true;
            if (!((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).IncomingInvoice)
            {
                ret = false;
                //return View("Index");
            }
            return ret;
        }

        //
        // GET: /Documents/IncomingInvoice/Create
        //[OutputCache(Duration = 600, VaryByParam = "Id")]
        public ActionResult CreateAndEdit(int id)
        {
            if (!CheckAccess())
                return View("../Account/AccessDenied");

            ViewData["Id"] = id;
            cDocuments_IncomingInvoice obj;

            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["IncomingInvoice"] = obj = cDocuments_IncomingInvoice.GetDocuments_IncomingInvoice(id);

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
                System.Web.HttpContext.Current.Session["IncomingInvoice"] = obj = cDocuments_IncomingInvoice.NewDocuments_IncomingInvoice();
            }
            ViewData.Model = obj;

            //string message = Request.QueryString["message"];
            //if (!string.IsNullOrEmpty(message))
            //    ViewData["Action"] = message;


            List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList;

            using (var context = new DalEf.DocumentsEntities())
            {
                System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = paymentsInAdvanceList = context.vPaymentsInAdvance.OrderBy(p => p.UniqueIdentifier).Where(p => p.SubjectBuyerId == obj.SubjectId && (p.AdvancePaymentAmmount ?? 0) != 0).ToList();
            }

            if (obj.MDGeneral_Enums_CurrencyId == null)
                obj.MDGeneral_Enums_CurrencyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).DefaultCurencyId;
            ViewData.Model = obj;
            ViewData["paymentsInAdvanceList"] = paymentsInAdvanceList;
            string message = Request.QueryString["message"];
            if (!string.IsNullOrEmpty(message))
                ViewData["Action"] = message;
            return View();

        } 

        //
        // POST: /Documents/IncomingInvoice/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cDocuments_IncomingInvoice obj, FormCollection collection)
        {
            string Action = collection["HiddenValueAction"];
            LoadProperty(obj, cDocuments_IncomingInvoice.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cDocuments_IncomingInvoice.EntityKeyDataProperty, enKey);
            }

            if (id == 0)
            {
                string docNum = "0";
                int num = 0;
                //using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                //{
                //    var lastItem = data.vDocuments.OrderByDescending(p => p.Id).FirstOrDefault(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.DocumentType == (short)BusinessObjects.Common.DocumentType.IncomingInvoice);
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
            obj.Status = 0;

            obj.RetailValue = obj.Documents_ItemsCol.Sum(p => p.WholesaleValue + p.TaxAmmount);
            obj.WholesaleValue = obj.Documents_ItemsCol.Sum(p => p.WholesaleValue);
            obj.TaxValue = obj.Documents_ItemsCol.Sum(p => p.TaxAmmount);
            obj.RabateValue = obj.Documents_ItemsCol.Sum(p => p.RabateAmmount);
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

                    if (SaveObject<cDocuments_IncomingInvoice>(obj, true))
                    {

                        System.Web.HttpContext.Current.Session["IncomingInvoice"] = null;
                        //return RedirectToAction("Index");
                        if (Action != "")
                        {
                            if (Action.Contains("gen"))
                            {
                                int Employeeid = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                                using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                                {
                                    if (Action == "genCopy")
                                    {
                                        var res = data.uspIncomingInvoice2IncommingInvoice(id, Employeeid).SingleOrDefault();
                                        //Action = String.Format("{0}_{1}", Action, Convert.ToInt32(res));
                                        Action = "genCopy_100";
                                    }
                                    else 
                                    {
                                        var res = data.uspCreateVirman309(id, Employeeid).SingleOrDefault();
                                        //Action = String.Format("{0}_{1}", Action, Convert.ToInt32(res));
                                        Action = "genVirman";
                                        System.Web.HttpContext.Current.Session["virmanId"] = res;
                                    }
                                }
                            }
                            return RedirectToAction("CreateAndEdit/" + id, new { message = Action });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                        
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

                    obj.DocumentType = (short)BusinessObjects.Common.DocumentType.IncomingInvoice;
                    if (SaveObject<cDocuments_IncomingInvoice>(obj, false))
                    {
                        System.Web.HttpContext.Current.Session["IncomingInvoice"] = null;
                        //return RedirectToAction("Index");
                        if (Action != "")
                        {
                            if (Action.Contains("gen"))
                            {
                                int Employeeid = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                                using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                                {
                                    if (Action == "genCopy")
                                    {
                                        var res = data.uspIncomingInvoice2IncommingInvoice(((cDocuments_Invoice)ViewData.Model).Id, Employeeid).SingleOrDefault();
                                        //Action = String.Format("{0}_{1}", Action, Convert.ToInt32(res));
                                        Action = "genCopy_100";
                                    }
                                    else
                                    {
                                        var res = data.uspCreateVirman309(((cDocuments_Invoice)ViewData.Model).Id, Employeeid).SingleOrDefault();
                                        Action = "genVirman";
                                        System.Web.HttpContext.Current.Session["virmanId"] = res;
                                    }
                                }
                            }
                            return RedirectToAction("CreateAndEdit/" + ((cDocuments_Invoice)ViewData.Model).Id, new { message = Action });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
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
            cDocuments_IncomingInvoice obj = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];

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
            System.Web.HttpContext.Current.Session["IncomingInvoice"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult MjestaComboPartial3(int? Id, FormCollection collection)
        {

            //string bindProperty = collection["DXCallbackName"].Substring(1, collection["DXCallbackName"].Length - 1);
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "IncomingInvoice");
            ViewData.Add("height", 20);
            ViewData.Add("width", 232);
            ViewData.Model = Id;


            return PartialView();
        }
        //[OutputCache(Duration = 600, VaryByParam = "None")]
        public ActionResult IncomingInvoiceGridPartial()
        {
            return PartialView("IncomingInvoiceGridPartial");
        }

        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cDocuments_IncomingInvoice)))
                return (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];
            else return Activator.CreateInstance(modelType);
        }



        #region itemsCol

        public ActionResult RefreshDocumentItemsColGridPartial()
        {
            cDocuments_IncomingInvoice invoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];

            ViewData["controllerName"] = "IncomingInvoice";
            return PartialView("DocumentItemsColGridPartial", invoice.Documents_ItemsCol);
        }

        [HttpPost]
        public ActionResult AddNewItem(cDocuments_Items item)
        {
            cDocuments_IncomingInvoice invoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];

            if (ModelState.IsValid)
            {
                try
                {
                    item.MarkChild();
                    item.Ordinal = invoice.Documents_ItemsCol.Count + 1;

                    item.RabateAmmount = (System.Math.Round((item.Price * item.Quantity), 2, MidpointRounding.AwayFromZero) - item.WholesaleValue);

                    decimal tax = 0;
                    using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
                    {
                        var taxItem = data.MDEntities_Enums_TaxRate.SingleOrDefault(p => p.Id == item.TaxRateId);
                        if (taxItem != null)
                            tax = System.Math.Round(item.WholesaleValue * (taxItem.Percentage / 100), 2, MidpointRounding.AwayFromZero);
                    }

                    item.TaxAmmount = tax;

                    invoice.Documents_ItemsCol.Add(item);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "IncomingInvoice";
            return PartialView("DocumentItemsColGridPartial", invoice.Documents_ItemsCol);
        }

        [HttpPost]
        public ActionResult UpdateItem(cDocuments_Items item, FormCollection collection)
        {
            cDocuments_IncomingInvoice invoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];

            if (ModelState.IsValid)
            {
                try
                {
                    if (item.Id != 0)
                    {
                        var target = invoice.Documents_ItemsCol.SingleOrDefault(p => p.Id == item.Id);
                        if (target != null)
                        {
                            DataMapper.Map(item, target, "Id", "DocumentId", "PriceListId", "PriceFromPriceList", "EntityKeyData");

                            target.RabateAmmount = System.Math.Round(target.Price * target.Quantity, 2, MidpointRounding.AwayFromZero) - target.WholesaleValue;

                            decimal tax = 0;
                            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
                            {
                                var taxItem = data.MDEntities_Enums_TaxRate.SingleOrDefault(p => p.Id == target.TaxRateId);
                                if (taxItem != null)
                                    tax = System.Math.Round(target.WholesaleValue * (taxItem.Percentage / 100), 2, MidpointRounding.AwayFromZero);
                            }

                            target.TaxAmmount = tax;
                        }
                    }
                    else
                    {
                        var target = invoice.Documents_ItemsCol.SingleOrDefault(p => p.Ordinal == item.Ordinal);
                        if (target != null)
                        {
                            DataMapper.Map(item, target, "Id", "DocumentId", "PriceListId", "PriceFromPriceList", "EntityKeyData");

                            target.RabateAmmount = System.Math.Round(target.Price * target.Quantity, 2, MidpointRounding.AwayFromZero) - target.WholesaleValue;

                            decimal tax = 0;
                            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
                            {
                                var taxItem = data.MDEntities_Enums_TaxRate.SingleOrDefault(p => p.Id == target.TaxRateId);
                                if (taxItem != null)
                                    tax = System.Math.Round(target.WholesaleValue * (taxItem.Percentage / 100), 2, MidpointRounding.AwayFromZero);
                            }

                            target.TaxAmmount = tax;
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

            ViewData["controllerName"] = "IncomingInvoice";
            return PartialView("DocumentItemsColGridPartial", invoice.Documents_ItemsCol);
        }

        [HttpPost]
        public ActionResult DeleteItem(cDocuments_Items item)
        {
            cDocuments_IncomingInvoice invoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];

            if (item.Id > 0)
            {
                try
                {
                    var target = invoice.Documents_ItemsCol.SingleOrDefault(p => p.Id == item.Id);
                    invoice.Documents_ItemsCol.Remove(target);

                    int count = 1;
                    foreach (var it in invoice.Documents_ItemsCol)
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
                var target = invoice.Documents_ItemsCol.SingleOrDefault(p => p.Ordinal == item.Ordinal);
                invoice.Documents_ItemsCol.Remove(target);

                int count = 1;
                foreach (var it in invoice.Documents_ItemsCol)
                {
                    it.Ordinal = count;
                    count += 1;
                }
            }

            ViewData["controllerName"] = "IncomingInvoice";
            return PartialView("DocumentItemsColGridPartial", invoice.Documents_ItemsCol);
        }
        
        #endregion

        public ActionResult TecajChanged(FormCollection collection)
        {
            cDocuments_IncomingInvoice incomingInvoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];
            int count = incomingInvoice.Documents_ItemsCol.Count;
            JsonResult result = new JsonResult();

            if (count == 0)
            {
                decimal val = 0;
                string[] list = { val.ToString(("n2")), val.ToString(("n2")), val.ToString(("n2")), val.ToString(("n2")) };
                result.Data = list;
                return result;
            }

            decimal Ukupno = 0;
            decimal Pdv = 0;
            decimal ZaPlatiti = 0;
            decimal PdvPercentage = 0;
            decimal RabatTotal = 0;
            decimal NovaCIjena = 0;
            decimal NovaWholesaleValue = 0;
            decimal NovaJedCijena = 0;


            decimal tecaj = Convert.ToDecimal(collection["Tecaj"].Replace(".", ","));
            decimal Cijena = 0;

            decimal PrevTecaj = Convert.ToDecimal(collection["PrevTecaj"].Replace(".", ","));


            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
            {
                for (int i = 0; i < count; i++)
                {

                    Cijena = System.Math.Round(Convert.ToDecimal(incomingInvoice.Documents_ItemsCol[i].Price) * PrevTecaj, 2, MidpointRounding.AwayFromZero);

                    NovaCIjena = System.Math.Round(Cijena / tecaj, 2, MidpointRounding.AwayFromZero);
                    NovaJedCijena = System.Math.Round(Convert.ToDecimal(NovaCIjena - (NovaCIjena / 100 * incomingInvoice.Documents_ItemsCol[i].RabatePercentage)), 2, MidpointRounding.AwayFromZero);
                    NovaWholesaleValue = System.Math.Round(incomingInvoice.Documents_ItemsCol[i].Quantity * NovaJedCijena, 2, MidpointRounding.AwayFromZero);

                    Ukupno += NovaWholesaleValue;
                    int SessionPdv = incomingInvoice.Documents_ItemsCol[i].TaxRateId;

                    var itemPdvPerc = data.MDEntities_Enums_TaxRate.FirstOrDefault(p => p.Id == SessionPdv);
                    PdvPercentage = itemPdvPerc.Percentage;
                    incomingInvoice.Documents_ItemsCol[i].TaxAmmount = System.Math.Round(Convert.ToDecimal(NovaWholesaleValue) * PdvPercentage / 100, 2, MidpointRounding.AwayFromZero);
                    Pdv += System.Math.Round(Convert.ToDecimal(NovaWholesaleValue) * PdvPercentage / 100, 2, MidpointRounding.AwayFromZero);

                    if (Convert.ToDecimal(incomingInvoice.Documents_ItemsCol[i].RabatePercentage) != 0)
                    {
                        RabatTotal += System.Math.Round(NovaCIjena * Convert.ToDecimal(incomingInvoice.Documents_ItemsCol[i].Quantity) - NovaWholesaleValue, 2, MidpointRounding.AwayFromZero);
                        incomingInvoice.Documents_ItemsCol[i].RabateAmmount = System.Math.Round(NovaCIjena * Convert.ToDecimal(incomingInvoice.Documents_ItemsCol[i].Quantity) - NovaWholesaleValue, 2, MidpointRounding.AwayFromZero);
                    }


                    incomingInvoice.Documents_ItemsCol[i].Price = NovaCIjena;
                    incomingInvoice.Documents_ItemsCol[i].Ammount = NovaJedCijena;
                    incomingInvoice.Documents_ItemsCol[i].WholesaleValue = NovaWholesaleValue;
                }
                ZaPlatiti += Ukupno + Pdv;
            }

            string[] llist = { Ukupno.ToString(("n2")), Pdv.ToString(("n2")), ZaPlatiti.ToString(("n2")), RabatTotal.ToString(("n2")) };

            result.Data = llist;
            return result;
        }

        public ActionResult ProductIdChanged(FormCollection collection)
        {
            int SubjectId = Convert.ToInt32(System.Web.HttpContext.Current.Session["IncomingInvoiceSubjectId"]);
            int ProizvoId = Convert.ToInt32(collection["proizvodId"]);

            decimal rezPrice = 0;
            decimal rezPriceOrig = 0;
            var unitid = 0;
            var taxrateId = 0;

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                var itemSubject = data.Documents_Document.OfType<DalEf.Documents_PriceList>().OrderByDescending(p => p.MDSubjects_SubjectId).FirstOrDefault(p => p.MDSubjects_SubjectId == SubjectId || p.MDSubjects_SubjectId == null);
                if (itemSubject != null)
                {
                    var itemProduct = itemSubject.Documents_PriceList_ItemsCol.FirstOrDefault(p => p.MDEntities_EntityId == ProizvoId);

                    var dataEnt = new DalEf.MDEntitiesEntities();
                    var itemProductEnt = dataEnt.vEntities.FirstOrDefault(p => p.Id == ProizvoId);
                    if (itemProduct != null)
                    {
                        rezPrice = itemProduct.WholesalePrice ?? 0;

                    }
                    else
                    {

                        if (itemProductEnt != null)
                            rezPrice = itemProductEnt.WholesalePrice;
                    }

                    unitid = itemProductEnt.UnitId;
                    taxrateId = itemProductEnt.TaxRateId;
                }
                else
                {
                    var dataEnt = new DalEf.MDEntitiesEntities();

                    var itemProductEnt = dataEnt.vEntities.FirstOrDefault(p => p.Id == ProizvoId);
                    if (itemProductEnt != null)
                    {
                        rezPrice = itemProductEnt.WholesalePrice;
                        unitid = itemProductEnt.UnitId;
                        taxrateId = itemProductEnt.TaxRateId;
                    }
                }
            }

            rezPriceOrig = rezPrice;
            try
            {
                rezPrice = System.Math.Round(rezPrice / Convert.ToDecimal(collection["tecaj"]), 2, MidpointRounding.AwayFromZero);
            }
            catch
            {
            }

            string[] list = { rezPrice.ToString(), unitid.ToString(), taxrateId.ToString(), rezPriceOrig.ToString() };

            JsonResult result = new JsonResult() { Data = list };
            return result;
        }


        public ActionResult Calc(FormCollection collection)
        {
            cDocuments_IncomingInvoice incomingInvoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];
            decimal Ukupno = 0;
            decimal Pdv = 0;
            decimal ZaPlatiti = 0;
            decimal PdvPercentage = 0;
            decimal RabatTotal = 0;
            int Ordinal = 0;

            bool isCallback = Convert.ToBoolean(collection["isCallback"]);

            if (!isCallback)
            {
                Ordinal = Convert.ToInt32(collection["Ordinal"]);
            }
            int count = incomingInvoice.Documents_ItemsCol.Count;

            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
            {
                foreach (var item in incomingInvoice.Documents_ItemsCol)
                {
                    if (Ordinal != 0 && !isCallback)
                    {
                        /* Ordinal != 0 => mjenja se postojeci item u gridu, zbroji sve ostale i dodaj cijenu promjenjenog na kraju
                         * U suprotnom, pozbrajaj sve iz Sessiona i dodaj cijenu novog itema */
                        int SessionOrdinal = item.Ordinal;
                        if (SessionOrdinal == Ordinal)
                            continue;
                    }
                    Ukupno += Convert.ToDecimal(item.WholesaleValue);
                    Pdv += Convert.ToDecimal(item.TaxAmmount);
                    RabatTotal += Convert.ToDecimal(item.RabateAmmount);
                }

                if (!isCallback)
                {
                    int TaxId = Convert.ToInt32(collection["TaxId"]);
                    decimal Rabat = Convert.ToDecimal(collection["Rabat"].Replace(".", ","));
                    decimal Cijena = Convert.ToDecimal(collection["Cijena"].Replace(".", ","));
                    decimal Kolicina = Convert.ToDecimal(collection["Kolicina"].Replace(".", ","));
                    decimal Wsv_r = Convert.ToDecimal(collection["WholeSalePrice"].Replace(".", ","));

                    var itemPdvPercToAdd = data.MDEntities_Enums_TaxRate.FirstOrDefault(p => p.Id == TaxId);
                    if (itemPdvPercToAdd != null)
                        PdvPercentage = itemPdvPercToAdd.Percentage;
                    else
                        PdvPercentage = 0;

                    decimal rabatammount = 0;
                    decimal Wsv = System.Math.Round(Cijena * Kolicina, 2, MidpointRounding.AwayFromZero);

                    if (Rabat != 0)
                    {
                        rabatammount = Wsv - Wsv_r;

                        Pdv += System.Math.Round(Wsv_r * (PdvPercentage / 100), 2, MidpointRounding.AwayFromZero);
                        RabatTotal += rabatammount; //Cijena * Kolicina - Wsp;
                        Ukupno += Wsv_r;
                    }
                    else
                    {
                        Pdv += System.Math.Round(Wsv * PdvPercentage / 100, 2, MidpointRounding.AwayFromZero);
                        Ukupno += Wsv;
                    }

                }
                ZaPlatiti = Ukupno + Pdv;
            }
            string[] list = { Ukupno.ToString(("n2")), Pdv.ToString(("n2")), ZaPlatiti.ToString(("n2")), RabatTotal.ToString(("n2")) };
            JsonResult result = new JsonResult() { Data = list };
            return result;
        }


        public ActionResult SubjectIdChanged(FormCollection collection)
        {
            string Street = "";
            string Number = "";
            string PlaceName = "";
            string Oib = "";


            int SubjectId = Convert.ToInt32(collection["subjectId"]);
            System.Web.HttpContext.Current.Session["IncomingInvoiceSubjectId"] = SubjectId;

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

        //
        // GET: /Documents/IncomingInvoice/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Documents/IncomingInvoice/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Documents/IncomingInvoice/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Documents/IncomingInvoice/Delete/5

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

        public ActionResult AddDesc(FormCollection collection)
        {
            int ordinal = Convert.ToInt32(collection["id"]);
            string desc = collection["desc"];
            bool ret = false;



            cDocuments_IncomingInvoice invoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];
            var item = invoice.Documents_ItemsCol.FirstOrDefault(p => p.Ordinal == ordinal);
            if (item != null)
            {
                item.ProductDescription = desc;
                ret = true;
            }

            JsonResult result = new JsonResult();
            result.Data = ret;
            return result;
        }

        public ActionResult GetDesc(FormCollection collection)
        {

            string ret = "";
            if (!string.IsNullOrEmpty(collection["id"]))
            {
                int ordinal = Convert.ToInt32(collection["id"]);

                cDocuments_IncomingInvoice invoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];
                var item = invoice.Documents_ItemsCol.FirstOrDefault(p => p.Ordinal == ordinal);
                if (item != null)
                {
                    ret = item.ProductDescription;
                }

            }
            JsonResult result = new JsonResult();
            result.Data = ret;
            return result;


        }

        public ActionResult CreateDoc(FormCollection collection)
        {
            int FromDocumentId = Convert.ToInt32(collection["FromDocumentId"]);
            int Employeeid = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            string docType = collection["docType"];

            int? id = 0;
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                if (docType == "genDispatch")
                {
                    var res = data.uspIncomingInvoice2IncommingInvoice(FromDocumentId, Employeeid).SingleOrDefault();
                    id = Convert.ToInt32(res);
                }
                else
                {
                    var res = data.uspCreateVirman309(FromDocumentId, Employeeid).SingleOrDefault();
                    id = Convert.ToInt32(res);
                }
                
                
            }
            JsonResult result = new JsonResult();
            result.Data = id;
            return result;

        }

        public ActionResult CreateTransferOrder(FormCollection collection)
        {
            int FromDocumentId = Convert.ToInt32(collection["FromDocumentId"]);
            int Employeeid = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            //string docType = collection["docType"];

            int? id = 0;
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                var res = data.uspIncomingInvoice2TransferOrder(FromDocumentId, Employeeid).SingleOrDefault();
                id = Convert.ToInt32(res);


            }
            JsonResult result = new JsonResult();
            result.Data = id;
            return result;

        }

        public ActionResult GetStatus(FormCollection collection)
        {
            int id = Convert.ToInt32(collection["id"]);
            bool ret = false;
            int result = 0;
            // get statusId from db
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                var status = data.Documents_Document.SingleOrDefault(p => p.Id == id);
                if (status.Status == null)
                {
                    result = 0;
                }
                else
                {
                    result = Convert.ToInt32(status.Status);
                }
            }
            ret = true;
            Tuple<bool, Int32> it = new Tuple<bool, int>(ret, result);
            JsonResult res = new JsonResult();
            res.Data = it;
            return res;

        }

        public ActionResult SetStatus(FormCollection collection)
        {
            int id = Convert.ToInt32(collection["id"]);
            int statusId = Convert.ToInt32(collection["statusId"]);
            bool ret = false;

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                data.uspUpdateStatus(id, statusId);
            }

            ret = true;
            JsonResult res = new JsonResult();
            res.Data = ret;
            return res;
        }

        public ActionResult RefreshHystoryPaymentsGridPartial()
        {
            cDocuments_IncomingInvoice Invoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];

            //if (person == null)
            //    System.Web.HttpContext.Current.Session["Person"] = person = cMDSubjects_Person_Specific_CRO.GetMDSubjects_Person_Specific_CRO(id);
            ViewData["invoiceId"] = Invoice.Id;
            return PartialView("HystoryPaymentsGridPartial");
        }

        public ActionResult RefreshFreeAvansesGridPartial()
        {
            //cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];

            //if (person == null)
            //    System.Web.HttpContext.Current.Session["Person"] = person = cMDSubjects_Person_Specific_CRO.GetMDSubjects_Person_Specific_CRO(id);
            ViewData.Model = System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];
            return PartialView("FreeAvansesGridPartial");
        }

        public ActionResult UpdateFreeAvans(int key, string value)
        {
            bool refreshGrid = false;
            cDocuments_IncomingInvoice Invoice = (cDocuments_IncomingInvoice)System.Web.HttpContext.Current.Session["IncomingInvoice"];
            List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList = (List<DalEf.vPaymentsInAdvance>)System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];
            DalEf.vPaymentsInAdvance item = paymentsInAdvanceList.SingleOrDefault(p => p.PaymentItemId == key);

            if (item != null)
            {
                decimal invoiceAmmount = Invoice.Documents_ItemsCol.Sum(p => p.WholesaleValue + p.TaxAmmount);
                decimal? historyPaymentAmmount = 0;
                decimal paymentsInAdvanceAmmount = (item.AdvancePaymentAmmount ?? 0);
                decimal paymentsInAdvanceAmmountSum = 0;
                decimal ammountForClosing = 0;

                if (value == "true" && item.UseForClosing == false)
                {
                    refreshGrid = true;
                    if (!Invoice.IsNew)
                    {
                        using (var context = new DalEf.DocumentsEntities())
                        {
                            var historyPaymentAmmountData = context.vInvoicePayments.Where(p => p.InvoiceId == Invoice.Id);
                            if (historyPaymentAmmountData != null)
                            {
                                if (historyPaymentAmmountData.Count() > 0)
                                    historyPaymentAmmount = historyPaymentAmmountData.Sum(p => p.Ammount);
                            }

                        }
                    }

                    //if (paymentsInAdvanceAmmount + historyPaymentAmmount >= invoiceAmmount)
                    //{
                    //    ammountForClosing = invoiceAmmount - historyPaymentAmmount;

                    //    foreach (var itemInAdvance in paymentsInAdvanceList)
                    //    {
                    //        itemInAdvance.UseForClosing = false;
                    //        itemInAdvance.AmmountForClosing = 0;
                    //    }

                    //    if (ammountForClosing > 0)
                    //    {
                    //        item.UseForClosing = true;
                    //        item.AmmountForClosing = ammountForClosing;
                    //    }
                    //}
                    //else
                    //{
                    paymentsInAdvanceAmmountSum = paymentsInAdvanceList.Where(p => (p.UseForClosing ?? false) == true).Sum(p => (p.AmmountForClosing ?? 0));
                    decimal closeTillNow = paymentsInAdvanceAmmountSum + (historyPaymentAmmount ?? 0);
                    if (closeTillNow < invoiceAmmount)
                    {
                        if (paymentsInAdvanceAmmount >= (invoiceAmmount - closeTillNow))
                        {
                            ammountForClosing = invoiceAmmount - closeTillNow;
                        }
                        else
                        {
                            ammountForClosing = paymentsInAdvanceAmmount;
                        }
                    }

                    if (ammountForClosing > 0)
                    {

                        item.UseForClosing = true;
                        item.AmmountForClosing = ammountForClosing;
                    }
                    //}
                }
                else if (value == "false" && item.UseForClosing == true)
                {
                    refreshGrid = true;

                    if (!Invoice.IsNew)
                    {
                        using (var context = new DalEf.DocumentsEntities())
                        {
                            var historyPaymentAmmountData = context.vInvoicePayments.Where(p => p.InvoiceId == Invoice.Id);
                            if (historyPaymentAmmountData != null)
                            {
                                if (historyPaymentAmmountData.Count() > 0)
                                    historyPaymentAmmount = historyPaymentAmmountData.Sum(p => p.Ammount);
                            }
                        }
                    }


                    item.AmmountForClosing = 0;
                    item.UseForClosing = false;
                    paymentsInAdvanceAmmountSum = paymentsInAdvanceList.Where(p => (p.UseForClosing ?? false) == true).Sum(p => (p.AmmountForClosing ?? 0));


                    decimal closeTillNow = paymentsInAdvanceAmmountSum + (historyPaymentAmmount ?? 0);

                    foreach (var itemInAdvance in paymentsInAdvanceList)
                    {
                        if (closeTillNow < invoiceAmmount)
                        {
                            if (itemInAdvance.UseForClosing == true)
                            {
                                if (itemInAdvance.AdvancePaymentAmmount > itemInAdvance.AmmountForClosing)
                                {
                                    if ((itemInAdvance.AdvancePaymentAmmount + (closeTillNow - itemInAdvance.AmmountForClosing)) < (invoiceAmmount))
                                    {
                                        closeTillNow = closeTillNow + (itemInAdvance.AdvancePaymentAmmount ?? 0);
                                        itemInAdvance.AmmountForClosing = (itemInAdvance.AdvancePaymentAmmount ?? 0);

                                    }
                                    else
                                    {
                                        closeTillNow = closeTillNow - (itemInAdvance.AmmountForClosing ?? 0);
                                        itemInAdvance.AmmountForClosing = invoiceAmmount - closeTillNow;
                                        closeTillNow = closeTillNow + (itemInAdvance.AmmountForClosing ?? 0);

                                        if (itemInAdvance.AmmountForClosing <= 0)
                                        {
                                            itemInAdvance.UseForClosing = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = paymentsInAdvanceList;
            }
            //TODO:    Dodati novi item zatvaranja vezan za peyment item avansa (kreirati session i za to);  Provjeriti da iznos svih zatvaranja ne prelazi neplaćeni iznos fakture
            JsonResult rez = new JsonResult() { Data = refreshGrid };
            return rez;
        }
    }
}
