using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using Csla.Web.Mvc;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.Documents.Controllers
{
    public class EnumsDispatchTypeController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /Documents/EnumsDispatchType/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Documents/EnumsDispatchType/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Documents/EnumsDispatchType/Create

        public ActionResult CreateAndEdit(int id)
        {
            cDocuments_Enums_DispatchType obj;


            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["DispatchType"] = obj = cDocuments_Enums_DispatchType.GetDocuments_Enums_DispatchType(id);
            }
            else
            {
                System.Web.HttpContext.Current.Session["DispatchType"] = obj = cDocuments_Enums_DispatchType.NewDocuments_Enums_DispatchType();
            }
            ViewData.Model = obj;
            return View();
        } 

        //
        // POST: /Documents/EnumsDispatchType/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cDocuments_Enums_DispatchType obj, FormCollection collection)
        {
            LoadProperty(obj, cDocuments_Enums_DispatchType.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cDocuments_Enums_DispatchType.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                {
                    if (SaveObject<cDocuments_Enums_DispatchType>(obj, true))
                    {

                        System.Web.HttpContext.Current.Session["DispatchType"] = null;
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
                    if (SaveObject<cDocuments_Enums_DispatchType>(obj, false))
                    {
                        System.Web.HttpContext.Current.Session["DispatchType"] = null;
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

        public ActionResult Odustani()
        {
            System.Web.HttpContext.Current.Session["DispatchType"] = null;
            return RedirectToAction("Index");
        }


        public ActionResult DispatchTypeGridPartial()
        {
            return PartialView("DispatchTypeGridPartial");
        }

        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cDocuments_Enums_DispatchType)))
                return (cDocuments_Enums_DispatchType)System.Web.HttpContext.Current.Session["DispatchType"];
            else return Activator.CreateInstance(modelType);
        }

        //
        // GET: /Documents/EnumsDispatchType/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Documents/EnumsDispatchType/Edit/5

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
        // GET: /Documents/EnumsDispatchType/Delete/5
 
        //public ActionResult Delete(int id)
        //{
        //    using (DocumentsEntities data = new DocumentsEntities())
        //    {
        //        var item = data.Documents_Enums_DispatchType.SingleOrDefault(p => p.Id == id);
        //        if (item != null)
        //        {
        //            item.Inactive = true;

        //            data.SaveChanges();
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        //
        // POST: /Documents/EnumsDispatchType/Delete/5

        //[HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DocumentsEntities data = new DocumentsEntities())
                {
                    var target = data.Documents_Enums_DispatchType.FirstOrDefault(p => p.Id == id);
                    if (target != null)
                    {
                        data.Documents_Enums_DispatchType.DeleteObject(target);
                        data.SaveChanges();
                    }
                }
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
