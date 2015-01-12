using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using BusinessObjects.MDSubjects;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.MDSubjects.Controllers
{
    public class EnumsBankController : Csla.Web.Mvc.Controller
    {
        //
        // GET: /MDSubjects/EnumsBank/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MDSubjects/EnumsBank/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDSubjects/EnumsBank/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDSubjects_Enums_Bank obj;
            
            if (id > 0)
            {
                obj = cMDSubjects_Enums_Bank.GetMDSubjects_Enums_Bank(id);
            }
            else
            {
                obj = cMDSubjects_Enums_Bank.NewMDSubjects_Enums_Bank();
            }
            ViewData.Model = obj;
            return View();
        } 

        //
        // POST: /MDSubjects/EnumsBank/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDSubjects_Enums_Bank obj, FormCollection collection)
        {
            LoadProperty(obj, cMDSubjects_Enums_Bank.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDSubjects_Enums_Bank.EntityKeyDataProperty, enKey);
            }
            obj.Number = Convert.ToInt32(collection["aNumber"]);
            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {
 
                if (obj.Id > 0)
                {
                    if (SaveObject<cMDSubjects_Enums_Bank>(obj, true))
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
                    if (SaveObject<cMDSubjects_Enums_Bank>(obj, false))
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
            //System.Web.HttpContext.Current.Session["EnumsBank"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult EnumsBankGridPartial()
        {
            return PartialView("EnumsBankGridPartial");
        }

        //
        // GET: /MDSubjects/EnumsBank/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDSubjects/EnumsBank/Edit/5

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
        // GET: /MDSubjects/EnumsBank/Delete/5
 
        //public ActionResult Delete(int id)
        //{

        //    using (MDSubjectsEntities data = new MDSubjectsEntities())
        //    {
        //        var item = data.MDSubjects_Enums_Bank.SingleOrDefault(p => p.Id == id);
        //        if (item != null)
        //        {
        //            item.Inactive = true;

        //            data.SaveChanges();
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        //
        // POST: /MDSubjects/EnumsBank/Delete/5

        //[HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (MDSubjectsEntities data = new MDSubjectsEntities())
                {
                    var target = data.MDSubjects_Enums_Bank.FirstOrDefault(p => p.Id == id);
                    if (target != null)
                    {
                        data.DeleteObject(target);
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

        public ActionResult MjestaComboPartial(int? Id, FormCollection collection)
        {
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "EnumsBank");
            ViewData.Add("height", 20);
            ViewData.Add("width", 232);
            ViewData.Model = Id;

            return PartialView();
        }
    }
}
