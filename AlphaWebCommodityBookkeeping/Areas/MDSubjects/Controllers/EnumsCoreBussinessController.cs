using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDSubjects;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.MDSubjects.Controllers
{
    public class EnumsCoreBussinessController : Csla.Web.Mvc.Controller
    {
        //
        // GET: /MDSubjects/EnumsCoreBussiness/

        public ActionResult Index()
        {
            ViewData.Model = cMDSubjects_Enums_CoreBussiness_List.GetcMDSubjects_Enums_CoreBussiness_List();
            return View();
        }

        //
        // GET: /MDSubjects/EnumsCoreBussiness/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDSubjects/EnumsCoreBussiness/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDSubjects_Enums_CoreBussiness obj;
            if (id > 0)
            {
                obj = cMDSubjects_Enums_CoreBussiness.GetMDSubjects_Enums_CoreBussiness(id);
            }
            else
            {
                obj = cMDSubjects_Enums_CoreBussiness.NewMDSubjects_Enums_CoreBussiness();
            }
            ViewData.Model = obj;
            return View();
        } 

        //
        // POST: /MDSubjects/EnumsCoreBussiness/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDSubjects_Enums_CoreBussiness obj, FormCollection collection)
        {
            LoadProperty(obj, cMDSubjects_Enums_CoreBussiness.IdProperty, id);
            if(collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDSubjects_Enums_CoreBussiness.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {
                
                if (obj.Id > 0)
                {
                    if (SaveObject<cMDSubjects_Enums_CoreBussiness>(obj, true))
                    {
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
                    if (SaveObject<cMDSubjects_Enums_CoreBussiness>(obj, false))
                    {
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
            return RedirectToAction("Index");
        }

        //
        // GET: /MDSubjects/EnumsCoreBussiness/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDSubjects/EnumsCoreBussiness/Edit/5

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
        // GET: /MDSubjects/EnumsCoreBussiness/Delete/5
 
        public ActionResult Delete(int id)
        {
            using (MDSubjectsEntities data = new MDSubjectsEntities())
            {
                var item = data.MDSubjects_Enums_CoreBussiness.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                    item.Inactive = true;

                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /MDSubjects/EnumsCoreBussiness/Delete/5

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

        public ActionResult EnumsCoreBussinessGridPartial()
        {
            return PartialView("EnumsCoreBussinessgridPartial");
        }
    }
}
