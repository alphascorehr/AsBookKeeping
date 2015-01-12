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
    public class EnumsCompanyTypeController : Csla.Web.Mvc.Controller
    {
        //
        // GET: /MDSubjects/EnumsCompanyType/

        public ActionResult Index()
        {
            ViewData.Model = cMDSubjects_Enums_CompanyType_List.GetcMDSubjects_Enums_CompanyType_List();
            return View();
        }

        //
        // GET: /MDSubjects/EnumsCompanyType/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDSubjects/EnumsCompanyType/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDSubjects_Enums_CompanyType obj;
            if (id > 0)
            {
                obj = cMDSubjects_Enums_CompanyType.GetMDSubjects_Enums_CompanyType(id);
            }
            else
            {
                obj = cMDSubjects_Enums_CompanyType.NewMDSubjects_Enums_CompanyType();
            }
            ViewData.Model = obj;
            return View();
        } 

        //
        // POST: /MDSubjects/EnumsCompanyType/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDSubjects_Enums_CompanyType obj, FormCollection collection)
        {
            LoadProperty(obj, cMDSubjects_Enums_CompanyType.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDSubjects_Enums_CompanyType.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                {
                    if (SaveObject<cMDSubjects_Enums_CompanyType>(obj, true))
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
                    if (SaveObject<cMDSubjects_Enums_CompanyType>(obj, false))
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
        // GET: /MDSubjects/EnumsCompanyType/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDSubjects/EnumsCompanyType/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, cMDSubjects_Enums_CompanyType obj, FormCollection collection)
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
        // GET: /MDSubjects/EnumsCompanyType/Delete/5
 
        public ActionResult Delete(int id)
        {
            using (MDSubjectsEntities data = new MDSubjectsEntities())
            {
                var item = data.MDSubjects_Enums_CompanyType.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                    item.Inactive = true;

                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /MDSubjects/EnumsCompanyType/Delete/5

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

        public ActionResult EnumsCompanyTypeGridPartial()
        {
            return PartialView("EnumsCompanyTypeGridPartial");
        }
    }
}
