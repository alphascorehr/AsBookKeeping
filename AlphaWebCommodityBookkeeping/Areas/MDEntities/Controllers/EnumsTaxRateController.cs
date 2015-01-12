using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDEntities;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.MDEntities.Controllers
{
    public class EnumsTaxRateController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDEntities/EnumsTaxRate/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MDEntities/EnumsTaxRate/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDEntities/EnumsTaxRate/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDEntities_Enums_TaxRate obj;


            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["EnumsTaxRate"] = obj = cMDEntities_Enums_TaxRate.GetMDEntities_Enums_TaxRate(id);
            }
            else
            {
                System.Web.HttpContext.Current.Session["EnumsTaxRate"] = obj = cMDEntities_Enums_TaxRate.NewMDEntities_Enums_TaxRate();
            }
            ViewData.Model = obj;
            return View();
            
        } 

        //
        // POST: /MDEntities/EnumsTaxRate/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDEntities_Enums_TaxRate obj, FormCollection collection)
        {
            LoadProperty(obj, cMDEntities_Enums_TaxRate.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDEntities_Enums_TaxRate.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.EmployeeWhoLastChanedItUserId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                {
                    
                    if (SaveObject<cMDEntities_Enums_TaxRate>(obj, true))
                    {

                        System.Web.HttpContext.Current.Session["EnumsTaxRate"] = null;
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

                    if (SaveObject<cMDEntities_Enums_TaxRate>(obj, false))
                    {
                        System.Web.HttpContext.Current.Session["EnumsTaxRate"] = null;
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
            System.Web.HttpContext.Current.Session["EnumsTaxRate"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult EnumsTaxRateGridPartial()
        {
            return PartialView("EnumsTaxRateGridPartial");
        }


        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDEntities_Enums_TaxRate)))
                return (cMDEntities_Enums_TaxRate)System.Web.HttpContext.Current.Session["EnumsTaxRate"];
            else return Activator.CreateInstance(modelType);
        }

        //
        // GET: /MDEntities/EnumsTaxRate/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDEntities/EnumsTaxRate/Edit/5

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
        // GET: /MDEntities/EnumsTaxRate/Delete/5
 
        public ActionResult Delete(int id)
        {
            using (MDEntitiesEntities data = new MDEntitiesEntities())
            {
                var item = data.MDEntities_Enums_TaxRate.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                    item.Inactive = true;

                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /MDEntities/EnumsTaxRate/Delete/5

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
    }
}
