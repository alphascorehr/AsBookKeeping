using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDGeneral;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.MDGeneral.Controllers
{
    public class EnumsCurrencyController : Csla.Web.Mvc.Controller
    {
        //
        // GET: /MDGeneral/EnumsCurrency/

        public ActionResult Index()
        {
            ViewData.Model = cMDGeneral_Enums_Currency_List.GetcMDGeneral_Enums_Currency_List();
            return View();
        }

        //
        // GET: /MDGeneral/EnumsCurrency/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDGeneral/EnumsCurrency/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDGeneral_Enums_Currency obj;
            if (id > 0)
            {
                obj = cMDGeneral_Enums_Currency.GetMDGeneral_Enums_Currency(id);
            }
            else
            {
                obj = cMDGeneral_Enums_Currency.NewMDGeneral_Enums_Currency();
            }
            ViewData.Model = obj;
            return View();
        } 

        //
        // POST: /MDGeneral/EnumsCurrency/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDGeneral_Enums_Currency obj, FormCollection collection)
        {
            LoadProperty(obj, cMDGeneral_Enums_Currency.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDGeneral_Enums_Currency.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                    if (SaveObject<cMDGeneral_Enums_Currency>(obj, true))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                else
                    if (SaveObject<cMDGeneral_Enums_Currency>(obj, false))
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
                ViewData.Model = obj;
                return View();
            }
        }

        public ActionResult Odustani()
        {
            return RedirectToAction("Index");
        }
        //
        // GET: /MDGeneral/EnumsCurrency/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDGeneral/EnumsCurrency/Edit/5

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
        // GET: /MDGeneral/EnumsCurrency/Delete/5
 
        public ActionResult Delete(int id)
        {
            using (MDGeneralEntities data = new MDGeneralEntities())
            {
                var item = data.MDGeneral_Enums_Currency.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                    item.Inactive = true;

                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /MDGeneral/EnumsCurrency/Delete/5

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

        public ActionResult EnumsCurrencyGridPartail()
        {
            return PartialView("EnumsCurrencyGridPartail");
        }
    }
}
