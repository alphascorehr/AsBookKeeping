using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDEntities;
using BusinessObjects.Security;
using Csla.Web.Mvc;
using Csla.Data;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using System.Drawing;
using System.Web.UI;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using BusinessObjects.Projects;

namespace AlphaWebCommodityBookkeeping.Areas.MDEntities.Controllers
{
    public class ServiceController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDEntities/Service/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MDEntities/Service/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDEntities/Service/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDEntities_Service obj;
            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["Service"] = obj = cMDEntities_Service.GetMDEntities_Service(id);
            }
            else
            {
                System.Web.HttpContext.Current.Session["Service"] = obj = cMDEntities_Service.NewMDEntities_Service();
            }
            ViewData.Model = obj;
            return View();
        }

        public ActionResult AddService(FormCollection collection)
        {
            string Name = collection["Name"];
            string Tax = collection["Tax"];
            string label = collection["Label"];
            string Wsprice = collection["Wsprice"];
            string Unit = collection["Unit"];
            JsonResult result = new JsonResult();
            result.Data = -1;
            if (Tax != "" && Name != "" && label != "")
            {
                cMDEntities_Service p = new cMDEntities_Service();
                p.Name = Name;
                p.TaxRateId = Convert.ToInt32(Tax);
                p.Label = label;
                p.WholesalePrice = Convert.ToDecimal(Wsprice);
                p.UnitId = Convert.ToInt32(Unit);
                
                p.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                cMDEntities_Service temp = p.Clone();
                p = temp.Save();
                result.Data = p.Id;

                cMDEntities_Service obj = new cMDEntities_Service();
                obj.Id = p.Id;
                obj.WholesalePrice = Convert.ToDecimal(Wsprice);
                UpdatePriceList(obj);
            }
            return result;
        }

       

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDEntities_Service obj, FormCollection collection)
        {
            string small = collection["Small"];
            LoadProperty(obj, cMDEntities_Service.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDEntities_Service.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.EmployeeWhoLastChanedItUserId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                {
                    if (SaveObject<cMDEntities_Service>(obj, true))
                    {
                        UpdatePriceList(obj);

                        System.Web.HttpContext.Current.Session["Service"] = null;
                        return RedirectToAction("../Product/Index");
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                }
                else
                {

                    if (SaveObject<cMDEntities_Service>(obj, false))
                    {
                        UpdatePriceList((cMDEntities_Service)ViewData.Model);

                        System.Web.HttpContext.Current.Session["Service"] = null;
                        return RedirectToAction("../Product/Index");

                        
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

        private static void UpdatePriceList(cMDEntities_Service obj)
        {
            using (DalEf.DocumentsEntities contextDoc = new DalEf.DocumentsEntities())
            {
                DalEf.Documents_PriceList_ItemsCol dataItem = null;
                DalEf.Documents_PriceList dataList = contextDoc.Documents_Document.OfType<DalEf.Documents_PriceList>().FirstOrDefault(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.IsDefault == true);
                if (dataList != null)
                {
                    dataItem = dataList.Documents_PriceList_ItemsCol.FirstOrDefault(p => p.MDEntities_EntityId == obj.Id);

                    if (dataItem == null)
                    {
                        dataItem = new DalEf.Documents_PriceList_ItemsCol();

                        dataItem.MDEntities_EntityId = obj.Id;
                        dataItem.WholesalePrice = obj.WholesalePrice;
                        dataItem.CalcualtedWholesalePrice = obj.WholesalePrice;
                        dataItem.Ordinal = dataList.Documents_PriceList_ItemsCol.Count + 1;

                        dataList.Documents_PriceList_ItemsCol.Add(dataItem);
                    }
                    else
                    {
                        dataItem.MDEntities_EntityId = obj.Id;
                        dataItem.WholesalePrice = obj.WholesalePrice;
                        dataItem.CalcualtedWholesalePrice = obj.WholesalePrice;
                    }

                }
                else
                {
                    dataList = new DalEf.Documents_PriceList();
                    dataList.MDSubjects_SubjectId = null;
                    dataList.IsDefault = true;
                    dataList.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                    dataList.DateStart = System.DateTime.Now;
                    dataList.LastActivityDate = System.DateTime.Now;
                    dataList.MDSubjects_Employee_AuthorId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                    dataList.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                    dataList.UniqueIdentifier = "1";
                    dataList.OrdinalNumber = 1;
                    dataList.OrdinalNumberInYear = 1;
                    dataList.Name = "Osnovna lista";
                    dataList.DocumentType = (short)BusinessObjects.Common.DocumentType.PriceList;
                    dataList.DocumentDateTime = System.DateTime.Now;
                    dataList.CreationDateTime = System.DateTime.Now;
                    dataList.GUID = Guid.NewGuid();

                    dataItem = new DalEf.Documents_PriceList_ItemsCol();

                    dataItem.MDEntities_EntityId = obj.Id;
                    dataItem.WholesalePrice = obj.WholesalePrice;
                    dataItem.CalcualtedWholesalePrice = obj.WholesalePrice;
                    dataItem.Ordinal = 1;

                    dataList.Documents_PriceList_ItemsCol.Add(dataItem);

                    contextDoc.AddToDocuments_Document(dataList);
                }

                contextDoc.SaveChanges();
            }
        }

        public ActionResult Odustani()
        {
            System.Web.HttpContext.Current.Session["Service"] = null;
            //return RedirectToAction("Index");
            return RedirectToAction("../Product/Index");
        }

        public ActionResult ServiceGridPartial()
        {
            return PartialView("ServiceGridPartial");
        }

        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDEntities_Service)))
                return (cMDEntities_Service)System.Web.HttpContext.Current.Session["Service"];
            else return Activator.CreateInstance(modelType);
        }

        //
        // GET: /MDEntities/Service/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDEntities/Service/Edit/5

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
        // GET: /MDEntities/Service/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MDEntities/Service/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("../Product/Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
