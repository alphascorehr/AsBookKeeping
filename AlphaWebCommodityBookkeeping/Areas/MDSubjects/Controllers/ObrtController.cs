using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDSubjects;
using Csla.Web.Mvc;
using Csla.Data;
using BusinessObjects.Security;
using AlphaWebCommodityBookkeeping.Models;

namespace AlphaWebCommodityBookkeeping.Areas.MDSubjects.Controllers
{
    public class ObrtController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDSubjects/Obrt/

        public ActionResult Index()
        {
            return View("../Obrt/Index");
        }

        //
        // GET: /MDSubjects/Obrt/Details/5



        //
        // GET: /MDSubjects/Obrt/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDSubjects_Obrt obj;
            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["Obrt"] = obj = cMDSubjects_Obrt.GetMDSubjects_Obrt(id);
                MjestaComboProvider.MjestoId = obj.HomeAddress_PlaceId;
            }
            else
            {
                System.Web.HttpContext.Current.Session["Obrt"] = obj = cMDSubjects_Obrt.NewMDSubjects_Obrt();
            }
            ViewData.Model = obj;
            return View();

            //if (System.Web.HttpContext.Current.Session["Obrt"] != null)
            //{
            //    if (((cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"]).Id == id)
            //    {
            //        obj = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];
            //        ViewData.Model = obj;
            //        return View();
            //    }
            //}


            //if (id > 0)
            //{
            //    System.Web.HttpContext.Current.Session["Obrt"] = obj = cMDSubjects_Obrt.GetMDSubjects_Obrt(id);
            //}
            //else
            //{
            //    System.Web.HttpContext.Current.Session["Obrt"] = obj = cMDSubjects_Obrt.NewMDSubjects_Obrt();
            //}
            //ViewData.Model = obj;
            //return View();
        } 

        //
        // POST: /MDSubjects/Obrt/Create


        //
        // POST: /MDSubjects/Obrt/Edit/5

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDSubjects_Obrt obj, FormCollection collection)
        {
            LoadProperty(obj, cMDSubjects_Obrt.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDSubjects_Obrt.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.EmployeeWhoLastChanedItId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            if (obj.IsValid)
            {
                if (obj.Id > 0)
                {

                    if (SaveObject<cMDSubjects_Obrt>(obj, true))
                    {
                       
                        System.Web.HttpContext.Current.Session["Obrt"] = null;
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

                    if (SaveObject<cMDSubjects_Obrt>(obj, false))
                    {
                      
                        System.Web.HttpContext.Current.Session["Obrt"] = null;
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
            //LoadProperty(obj, cMDSubjects_Obrt.IdProperty, id);
            //if (collection["EntityKeyData"] != "")
            //{
            //    byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
            //    LoadProperty(obj, cMDSubjects_Obrt.EntityKeyDataProperty, enKey);
            //}
            //if (obj.IsValid)
            //{
            //    if (obj.Id > 0)
            //        if (SaveObject<cMDSubjects_Obrt>(obj, true))
            //        {
            //            System.Web.HttpContext.Current.Session["Person"] = null;
            //            return RedirectToAction("Index");
            //        }
            //        else
            //        {
            //            ViewData.Model = obj;
            //            return View();
            //        }
            //    else
            //        if (SaveObject<cMDSubjects_Obrt>(obj, false))
            //        {
            //            System.Web.HttpContext.Current.Session["Person"] = null;
            //            return RedirectToAction("Index");
            //        }
            //        else
            //        {
            //            ViewData.Model = obj;
            //            return View();
            //        }
            //}
            //else
            //{
            //    ViewData.Model = obj;
            //    return View();
            //}
        }

        //
        // GET: /MDSubjects/Obrt/Delete/5

        public ActionResult Odustani()
        {
            System.Web.HttpContext.Current.Session["Obrt"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MDSubjects/Obrt/Delete/5

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

        public ActionResult ObrtGridPartial()
        {
            return PartialView("ObrtGridPartial");
        }

        #region AccountColGridPartial
        public ActionResult RefreshAccountsColGridPartial()
        {
            cMDSubjects_Obrt obrt;
            obrt = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];

            ViewData["controllerName"] = "Obrt";
            return PartialView("AccountsColGridPartial", obrt.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult AddNewAccount(cMDSubjects_Subject_Accounts account)
        {
            cMDSubjects_Obrt obrt;
            obrt = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];

            if (ModelState.IsValid)
            {
                try
                {
                    account.MarkChild();
                    obrt.MDSubjects_Subject_AccountsCol.Add(account);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else 
            {
                ViewData["EditError"] = "Molimo ispravite greske";
            }
            ViewData["controllerName"] = "Obrt";
            return PartialView("AccountsColGridPartial", obrt.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult UpdateAccount(cMDSubjects_Subject_Accounts account)
        {
            cMDSubjects_Obrt obrt;
            obrt = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];

            if (ModelState.IsValid)
            {
                try
                {
                    var target = obrt.MDSubjects_Subject_AccountsCol.SingleOrDefault(p => p.Id == account.Id);
                    if (target != null)
                    {
                        DataMapper.Map(account, target, "Id", "MDSubjects_SubjectId", "Inactive", "EntityKeyData");
                        target.LastActivityDate = DateTime.Now;
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Molimo ispravire pogresku";
            }
            ViewData["controllerName"] = "Obrt";
            return PartialView("AccountsColGridPartial", obrt.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            cMDSubjects_Obrt obrt;
            obrt = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];

            if (id > 0)
            {
                try
                {
                    var target = obrt.MDSubjects_Subject_AccountsCol.SingleOrDefault(p => p.Id == id);
                    obrt.MDSubjects_Subject_AccountsCol.Remove(target);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            ViewData["controllerName"] = "Obrt";
            return PartialView("AccountsColGridPartial", obrt.MDSubjects_Subject_AccountsCol);
        }
        #endregion

        #region ContactColGridpartial

        public ActionResult RefreshContactsColGridPartial()
        {
            cMDSubjects_Obrt obrt;
            obrt = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];

            ViewData["controllerName"] = "Obrt";
            return PartialView("ContactsColGridPartial", obrt.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult AddNewContact(cMDSubjects_Subject_Contacts contact)
        {
            cMDSubjects_Obrt obrt;
            obrt = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];

            if (ModelState.IsValid)
            {
                try
                {
                    contact.MarkChild();
                    obrt.MDSubjects_Subject_ContactsCol.Add(contact);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Molimo ispravire pogresku";
            }
            ViewData["controllerName"] = "Obrt";
            return PartialView("ContactsColGridPartial", obrt.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult UpdateContact(cMDSubjects_Subject_Contacts contact)
        {
            cMDSubjects_Obrt obrt;
            obrt = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];

            if (ModelState.IsValid)
            {
                try
                {
                    var target = obrt.MDSubjects_Subject_ContactsCol.SingleOrDefault(p => p.Id == contact.Id);
                    if (target != null)
                    {
                        DataMapper.Map(contact, target, "Id", "MDSubjects_SubjectId", "Inactive", "EntityKeyData");
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            { 
                ViewData["EditError"] = "Molimo ispravite greske!";
            }

            ViewData["controllerName"] = "Obrt";
            return PartialView("ContactsColGridPartial", obrt.MDSubjects_Subject_ContactsCol);
        }

        public ActionResult DeleteContact(int id)
        {
            cMDSubjects_Obrt obrt;
            obrt = (cMDSubjects_Obrt)System.Web.HttpContext.Current.Session["Obrt"];

            if (id > 0)
            {
                try
                {
                    var target = obrt.MDSubjects_Subject_ContactsCol.SingleOrDefault(p => p.Id == id);
                    obrt.MDSubjects_Subject_ContactsCol.Remove(target);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Molimo ispravite greske!";
            }
            ViewData["controllerName"] = "Obrt";
            return PartialView("ContactsColGridPartial", obrt.MDSubjects_Subject_ContactsCol);
        }
        #endregion

        [HttpPost]
        public ActionResult MjestaComboPartial(int? Id, FormCollection collection)
        {
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "Obrt");
            ViewData.Add("height", 20);
            ViewData.Add("width", 232);
            ViewData.Model = Id;

            return PartialView();
        }

        public ActionResult MjestaNameTemplate(FormCollection collection)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult SearchMjestaById(string id)
        {
            return AlphaWebCommodityBookkeeping.Models.MjestaComboProvider.SearchMjestaById(System.Convert.ToInt32(id));
        }

        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDSubjects_Person)))
                return (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Obrt"];
            else return Activator.CreateInstance(modelType);
        }
    }
    
    
}
