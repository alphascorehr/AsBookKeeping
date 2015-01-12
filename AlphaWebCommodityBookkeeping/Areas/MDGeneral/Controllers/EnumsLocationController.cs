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
    public class EnumsLocationController : Csla.Web.Mvc.Controller
    {
        //
        // GET: /MDGeneral/EnimsLocation/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MDGeneral/EnumsCurrency/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDGeneral_Enums_Location obj;
            if (id > 0)
            {
                obj = cMDGeneral_Enums_Location.GetMDGeneral_Enums_Location(id);
            }
            else
            {
                obj = cMDGeneral_Enums_Location.NewMDGeneral_Enums_Location();
            }
            ViewData.Model = obj;
            return View();
        }

        //
        // POST: /MDGeneral/EnumsCurrency/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDGeneral_Enums_Location obj, FormCollection collection)
        {
            LoadProperty(obj, cMDGeneral_Enums_Location.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDGeneral_Enums_Location.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                    if (SaveObject<cMDGeneral_Enums_Location>(obj, true))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                else
                    if (SaveObject<cMDGeneral_Enums_Location>(obj, false))
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
                var item = data.MDGeneral_Enums_Location.SingleOrDefault(p => p.Id == id);
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

        public ActionResult EnumsLocationsGridPartial()
        {
            return PartialView("EnumsLocationsGridPartial");
        }

    }
}
