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
    public class EnumsEntityGroupController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDEntities/EnumsEntityGroup/

        public ActionResult Index()
        {
            ViewData.Model = cMDEntities_Enums_EntityGroup_List.GetcMDEntities_Enums_EntityGroup_List();
            return View();
        }

        //
        // GET: /MDEntities/EnumsEntityGroup/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDEntities/EnumsEntityGroup/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDEntities_Enums_EntityGroup obj;

            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["EnumsEntityGroup"] = obj = cMDEntities_Enums_EntityGroup.GetMDEntities_Enums_EntityGroup(id);
            }
            else
            {
                System.Web.HttpContext.Current.Session["EnumsEntityGroup"] = obj = cMDEntities_Enums_EntityGroup.NewMDEntities_Enums_EntityGroup();
            }
            ViewData.Model = obj;
            return View();

        } 

        //
        // POST: /MDEntities/EnumsEntityGroup/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDEntities_Enums_EntityGroup obj, FormCollection collection)
        {
            LoadProperty(obj, cMDEntities_Enums_EntityGroup.IdProperty, id);
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
                    if (SaveObject<cMDEntities_Enums_EntityGroup>(obj, true))
                    {
                        System.Web.HttpContext.Current.Session["EnumsEntityGroup"] = null;
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

                    if (SaveObject<cMDEntities_Enums_EntityGroup>(obj, false))
                    {
                        System.Web.HttpContext.Current.Session["EnumsEntityGroup"] = null;
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

            System.Web.HttpContext.Current.Session["EnumsEntityGroup"] = null;
            return RedirectToAction("Index");
        }
        
        //
        // GET: /MDEntities/EnumsEntityGroup/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDEntities/EnumsEntityGroup/Edit/5

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
        // GET: /MDEntities/EnumsEntityGroup/Delete/5
 
        public ActionResult Delete(int id)
        {
            using (MDEntitiesEntities data = new MDEntitiesEntities())
            {
                var item = data.MDEntities_Enums_EntityGroup.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                    item.Inactive = true;

                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /MDEntities/EnumsEntityGroup/Delete/5

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


        public ActionResult EnumsEntityGroupGridPartial()
        {
            return PartialView("EnumsEntityGroupGridPartial");
        }

        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDEntities_Enums_EntityGroup)))
                return (cMDEntities_Enums_EntityGroup)System.Web.HttpContext.Current.Session["EnumsEntityGroup"];
            else return Activator.CreateInstance(modelType);
        }
    }
}
