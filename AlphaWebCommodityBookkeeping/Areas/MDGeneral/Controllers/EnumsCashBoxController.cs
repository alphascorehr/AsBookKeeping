using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using BusinessObjects.MDGeneral;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.MDGeneral.Controllers
{
    public class EnumsCashBoxController : Csla.Web.Mvc.Controller
    {
        //
        // GET: /MDGeneral/EnumsCashBox/

        public ActionResult Index()
        {
            return View();
        }

       
        //
        // GET: /MDGeneral/EnumsCurrency/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDGeneral_Enums_CashBox obj;
            if (id > 0)
            {
                obj = cMDGeneral_Enums_CashBox.GetMDGeneral_Enums_CashBox(id);
            }
            else
            {
                obj = cMDGeneral_Enums_CashBox.NewMDGeneral_Enums_CashBox();
            }
            ViewData.Model = obj;
            return View();
        }

        //
        // POST: /MDGeneral/EnumsCurrency/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDGeneral_Enums_CashBox obj, FormCollection collection)
        {
            LoadProperty(obj, cMDGeneral_Enums_CashBox.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDGeneral_Enums_CashBox.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                    if (SaveObject<cMDGeneral_Enums_CashBox>(obj, true))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                else
                    if (SaveObject<cMDGeneral_Enums_CashBox>(obj, false))
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
        // GET: /MDGeneral/EnumsCurrency/Delete/5

        public ActionResult Delete(int id)
        {
            using (MDGeneralEntities data = new MDGeneralEntities())
            {
                var item = data.MDGeneral_Enums_CashBox.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                   // item.Inactive = true;

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

        public ActionResult EnumsCachBoxGidPartail()
        {
            return PartialView("EnumsCachBoxGidPartail");
        }

    }
}
