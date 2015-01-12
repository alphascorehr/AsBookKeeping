using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDSubjects;
using Csla.Data;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using AlphaWebCommodityBookkeeping.Models;

namespace AlphaWebCommodityBookkeeping.Areas.MDSubjects.Controllers
{
    public class SoleProprietorController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDSubjects/SoleProprietor/

        public ActionResult Index()
        {
            return View("../Company/Index");
        }

        //
        // GET: /MDSubjects/SoleProprietor/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDSubjects/SoleProprietor/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDSubjects_SoleProprietor obj;
            
            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["SoleProprietor"] = obj = cMDSubjects_SoleProprietor.GetMDSubjects_SoleProprietor(id);
                MjestaComboProvider.MjestoId = obj.HomeAddress_PlaceId;
            }
            else
            {
                System.Web.HttpContext.Current.Session["SoleProprietor"] = obj = cMDSubjects_SoleProprietor.NewMDSubjects_SoleProprietor();
            }
            ViewData.Model = obj;
            return View();
        } 

        //
        // POST: /MDSubjects/SoleProprietor/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDSubjects_SoleProprietor obj, FormCollection collection)
        {
            LoadProperty(obj, cMDSubjects_SoleProprietor.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                //byte[] enKey = encoding.GetBytes(collection["CslaEnKey"]);

                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDSubjects_SoleProprietor.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.EmployeeWhoLastChanedItId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            if (obj.IsValid)
            {
                if (obj.Id > 0)
                {

                    if (SaveObject(obj, true))
                    {

                        System.Web.HttpContext.Current.Session["SoleProprietor"] = null;
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

                    if (SaveObject(obj, false))
                    {


                        System.Web.HttpContext.Current.Session["SoleProprietor"] = null;
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
            System.Web.HttpContext.Current.Session["SoleProprietor"] = null;
            //return RedirectToAction("Index");
            return RedirectToAction("../Company/Index");
        }

        //
        // GET: /MDSubjects/SoleProprietor/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MDSubjects/SoleProprietor/Delete/5

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

        public ActionResult SoleProprietorGridPartial()
        {
            return PartialView("SoleProprietorGridPartial");
        }

        #region AccountsColGridPartial
        public ActionResult RefreshAccountsColGridPartial()
        {
            cMDSubjects_SoleProprietor SoleProprietor = (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];

            //if (SoleProprietor == null)
            //    System.Web.HttpContext.Current.Session["SoleProprietor"] = SoleProprietor = cMDSubjects_SoleProprietor_Specific_CRO.GetMDSubjects_SoleProprietor_Specific_CRO(id);
            ViewData["controllerName"] = "SoleProprietor";
            return PartialView("AccountsColGridPartial", SoleProprietor.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult AddNewAccount(cMDSubjects_Subject_Accounts account)
        {
            cMDSubjects_SoleProprietor SoleProprietor = (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];

            if (ModelState.IsValid)
            {
                try
                {
                    account.MarkChild();
                    SoleProprietor.MDSubjects_Subject_AccountsCol.Add(account);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "SoleProprietor";
            return PartialView("AccountsColGridPartial", SoleProprietor.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult UpdateAccount(cMDSubjects_Subject_Accounts account)
        {
            cMDSubjects_SoleProprietor SoleProprietor = (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];

            if (ModelState.IsValid)
            {
                try
                {
                    var target = SoleProprietor.MDSubjects_Subject_AccountsCol.SingleOrDefault(p => p.Id == account.Id);
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
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "SoleProprietor";
            return PartialView("AccountsColGridPartial", SoleProprietor.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            cMDSubjects_SoleProprietor SoleProprietor = (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];

            if (id > 0)
            {
                try
                {
                    var target = SoleProprietor.MDSubjects_Subject_AccountsCol.SingleOrDefault(p => p.Id == id);
                    SoleProprietor.MDSubjects_Subject_AccountsCol.Remove(target);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewData["controllerName"] = "SoleProprietor";
            return PartialView("AccountsColGridPartial", SoleProprietor.MDSubjects_Subject_AccountsCol);
        }
        #endregion

        #region ContactsColGridPartial
        public ActionResult RefreshContactsColGridPartial()
        {
            cMDSubjects_SoleProprietor SoleProprietor = (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];

            //if (SoleProprietor == null)
            //    System.Web.HttpContext.Current.Session["SoleProprietor"] = SoleProprietor = cMDSubjects_SoleProprietor_Specific_CRO.GetMDSubjects_SoleProprietor_Specific_CRO(id);
            ViewData["controllerName"] = "SoleProprietor";
            return PartialView("ContactsColGridPartial", SoleProprietor.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult AddNewContact(cMDSubjects_Subject_Contacts contact)
        {
            cMDSubjects_SoleProprietor SoleProprietor = (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];

            if (ModelState.IsValid)
            {
                try
                {
                    contact.MarkChild();
                    SoleProprietor.MDSubjects_Subject_ContactsCol.Add(contact);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "SoleProprietor";
            return PartialView("ContactsColGridPartial", SoleProprietor.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult UpdateContact(cMDSubjects_Subject_Contacts contact)
        {
            cMDSubjects_SoleProprietor SoleProprietor = (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];

            if (ModelState.IsValid)
            {
                try
                {
                    var target = SoleProprietor.MDSubjects_Subject_ContactsCol.SingleOrDefault(p => p.Id == contact.Id);
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
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "SoleProprietor";
            return PartialView("ContactsColGridPartial", SoleProprietor.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult DeleteContact(int id)
        {
            cMDSubjects_SoleProprietor SoleProprietor = (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];

            if (id > 0)
            {
                try
                {
                    var target = SoleProprietor.MDSubjects_Subject_ContactsCol.SingleOrDefault(p => p.Id == id);
                    SoleProprietor.MDSubjects_Subject_ContactsCol.Remove(target);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewData["controllerName"] = "SoleProprietor";
            return PartialView("ContactsColGridPartial", SoleProprietor.MDSubjects_Subject_ContactsCol);
        }
        #endregion

        [HttpPost]
        public ActionResult MjestaComboPartial(int? Id, FormCollection collection)
        {

            //string bindProperty = collection["DXCallbackName"].Substring(1, collection["DXCallbackName"].Length - 1);
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "SoleProprietor");
            ViewData.Add("height", 20);
            ViewData.Add("width", 150);
            ViewData.Model = Id;


            return PartialView();
        }

        object IModelCreator.CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDSubjects_SoleProprietor)))
                return (cMDSubjects_SoleProprietor)System.Web.HttpContext.Current.Session["SoleProprietor"];
            else return Activator.CreateInstance(modelType);
        }
    }
}
