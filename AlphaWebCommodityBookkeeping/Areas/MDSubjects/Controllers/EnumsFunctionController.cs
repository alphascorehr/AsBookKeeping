using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDSubjects;
using BusinessObjects.Security;
using Csla.Web.Mvc;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.MDSubjects.Controllers
{
    public class EnumsFunctionController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /EnumsFunction/

        public ActionResult Index()
        {
            ViewData.Model = cMDSubjects_Enums_Function_List.GetcMDSubjects_Enums_Function_List();
            return View();
        }

        //
        // GET: /EnumsFunction/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /EnumsFunction/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDSubjects_Enums_Function obj;
            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["Function"] = obj = cMDSubjects_Enums_Function.GetMDSubjects_Enums_Function(id);
            }
            else
            {
                System.Web.HttpContext.Current.Session["Function"] = obj = cMDSubjects_Enums_Function.NewMDSubjects_Enums_Function();
            }
            ViewData.Model = obj;
            return View();
        }

        //
        // POST: /EnumsFunction/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDSubjects_Enums_Function obj, FormCollection collection)
        {
            LoadProperty(obj, cMDSubjects_Enums_Function.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                //byte[] enKey = encoding.GetBytes(collection["CslaEnKey"]);

                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDSubjects_Enums_Function.EntityKeyDataProperty, enKey);
            }
            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            if (obj.IsValid)
            {
                if (obj.Id > 0)
                {
                    if (SaveObject<cMDSubjects_Enums_Function>(obj, true))
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
                    if (SaveObject<cMDSubjects_Enums_Function>(obj, false))
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
        // GET: /EnumsFunction/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /EnumsFunction/Edit/5

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
        // GET: /EnumsFunction/Delete/5

        public ActionResult Delete(int id)
        {
            using (MDSubjectsEntities data = new MDSubjectsEntities())
            {
                var item = data.MDSubjects_Enums_Function.SingleOrDefault(p => p.Id == id);
                if (item != null)
                {
                    item.Inactive = true;

                    data.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /EnumsFunction/Delete/5

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

        public ActionResult EnumsFunctionGridPartial()
        {
            return PartialView("EnumsFunctionGridPartial");
        }


        object IModelCreator.CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDSubjects_Enums_Function)))
                return (cMDSubjects_Enums_Function)System.Web.HttpContext.Current.Session["Function"];
            else return Activator.CreateInstance(modelType);
        }
    }

}
