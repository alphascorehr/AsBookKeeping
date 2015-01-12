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
    public class EnumsUnitController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDEntities/EnumsUnit/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MDEntities/EnumsUnit/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDEntities/EnumsUnit/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDEntities_Enums_Unit obj;

            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["EnumsEntityUnit"] = obj = cMDEntities_Enums_Unit.GetMDEntities_Enums_Unit(id);
            }
            else
            {
                System.Web.HttpContext.Current.Session["EnumsEntityUnit"] = obj = cMDEntities_Enums_Unit.NewMDEntities_Enums_Unit();
            }
            ViewData.Model = obj;
            return View();

        }


        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDEntities_Enums_Unit obj, FormCollection collection)
        {
            LoadProperty(obj, cMDEntities_Enums_Unit.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDEntities_Enums_EntityGroup.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.EmployeeWhoLastChanedItUserId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                {
                    if (SaveObject<cMDEntities_Enums_Unit>(obj, true))
                    {
                        System.Web.HttpContext.Current.Session["EnumsEntityUnit"] = null;
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

                    if (SaveObject<cMDEntities_Enums_Unit>(obj, false))
                    {
                        System.Web.HttpContext.Current.Session["EnumsEntityUnit"] = null;
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
            System.Web.HttpContext.Current.Session["EnumsEntityUnit"] = null;
            return RedirectToAction("Index");
        }
        
        //
        // GET: /MDEntities/EnumsUnit/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDEntities/EnumsUnit/Edit/5

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
        // GET: /MDEntities/EnumsUnit/Delete/5
 
        public ActionResult Delete(int id)
        {
            using (MDEntitiesEntities data = new MDEntitiesEntities())
            {
                var item = data.MDEntities_Enums_Unit.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                    item.Inactive = true;

                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /MDEntities/EnumsUnit/Delete/5

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

        public ActionResult EnumsEntityUnitGridPartial()
        {
            return PartialView("EnumsEntityUnitGridPartial");
        }

        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDEntities_Enums_Unit)))
                return (cMDEntities_Enums_Unit)System.Web.HttpContext.Current.Session["EnumsEntityUnit"];
            else return Activator.CreateInstance(modelType);
        }
    }
}
