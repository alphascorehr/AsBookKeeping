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
    public class EnumsKindOfTransportationController : Csla.Web.Mvc.Controller
    {
        //
        // GET: /MDGeneral/EnumsCurrency/

        public ActionResult Index()
        {
            ViewData.Model = cMDGeneral_Enums_KindOfTransportation_List.GetcMDGeneral_Enums_KindOfTransportation_List();
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
            cMDGeneral_Enums_KindOfTransportation obj;
            if (id > 0)
            {
                obj = cMDGeneral_Enums_KindOfTransportation.GetMDGeneral_Enums_KindOfTransportation(id);
            }
            else
            {
                obj = cMDGeneral_Enums_KindOfTransportation.NewMDGeneral_Enums_KindOfTransportation();
            }
            ViewData.Model = obj;
            return View();
        }

        //
        // POST: /MDGeneral/EnumsCurrency/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDGeneral_Enums_KindOfTransportation obj, FormCollection collection)
        {
            LoadProperty(obj, cMDGeneral_Enums_KindOfTransportation.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDGeneral_Enums_KindOfTransportation.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                    if (SaveObject<cMDGeneral_Enums_KindOfTransportation>(obj, true))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                else
                    if (SaveObject<cMDGeneral_Enums_KindOfTransportation>(obj, false))
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
                var item = data.MDGeneral_Enums_KindOfTransportation.SingleOrDefault(p => p.Id == id);
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

        public ActionResult EnumsKindOfTransportationGridPartail()
        {
            return PartialView("EnumsKindOfTransportationGridPartail");
        }
    }
}
