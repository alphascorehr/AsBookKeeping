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
    public class PersonController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDSubjects/Person/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MDSubjects/Person/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDSubjects/Person/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDSubjects_Person obj;
            
            //if (System.Web.HttpContext.Current.Session["Person"] != null)
            //{
            //    if (((cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"]).Id == id)
            //    {
            //        obj = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];
            //        ViewData.Model = obj;
            //        return View();
            //    }
            //}
          
               
            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["Person"] = obj = cMDSubjects_Person.GetMDSubjects_Person(id);
                MjestaComboProvider.MjestoId = obj.HomeAddress_PlaceId;
            }
            else
            {
                System.Web.HttpContext.Current.Session["Person"] = obj = cMDSubjects_Person.NewMDSubjects_Person();
            }
            ViewData.Model = obj;
            return View();
            
        } 

        //
        // POST: /MDSubjects/Person/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDSubjects_Person obj, FormCollection collection)
        {
           LoadProperty(obj, cMDSubjects_Person.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                //System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                //byte[] enKey = encoding.GetBytes(collection["CslaEnKey"]);

                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDSubjects_Person.EntityKeyDataProperty, enKey);
            }

            //obj.Name = String.Format("{0} {1}", obj.SurName, obj.FirstName);
            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.EmployeeWhoLastChanedItId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            obj.Name = obj.FirstName;
            obj.SurName = obj.FirstName;
            if (obj.IsValid)
            {
                if (obj.Id > 0)
                {
                    if (SaveObject<cMDSubjects_Person>(obj, true))
                    {
                       
                        System.Web.HttpContext.Current.Session["Person"] = null;
                        //return RedirectToAction("Index");
                        return RedirectToAction("../Company/Index");
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                }
                else
                {
                
                    if (SaveObject<cMDSubjects_Person>(obj, false))
                    {
                        System.Web.HttpContext.Current.Session["Person"] = null;
                        //return RedirectToAction("Index");
                        return RedirectToAction("../Company/Index");
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
            System.Web.HttpContext.Current.Session["Person"] = null;
            //return RedirectToAction("Index");
            return RedirectToAction("../Company/Index");
        }

        
        //
        // GET: /MDSubjects/Person/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MDSubjects/Person/Delete/5

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

    
        public ActionResult PersonGridPartial()
        {
            return PartialView("PersonGridPartial");
        }

        #region AccountsColGridPartial
        public ActionResult RefreshAccountsColGridPartial()
        {
            cMDSubjects_Person person;
            person = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];

            //if (person == null)
            //    System.Web.HttpContext.Current.Session["Person"] = person = cMDSubjects_Person_Specific_CRO.GetMDSubjects_Person_Specific_CRO(id);
            ViewData["controllerName"] = "Person";
            return PartialView("AccountsColGridPartial", person.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult AddNewAccount(cMDSubjects_Subject_Accounts account)
        {
            cMDSubjects_Person person;
            person = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];

            if (ModelState.IsValid)
            {
                try
                {
                    account.MarkChild();
                    person.MDSubjects_Subject_AccountsCol.Add(account);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "Person";
            return PartialView("AccountsColGridPartial", person.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult UpdateAccount(cMDSubjects_Subject_Accounts account)
        {
            cMDSubjects_Person person;
            person = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];

            if (ModelState.IsValid)
            {
                try
                {
                    var target = person.MDSubjects_Subject_AccountsCol.SingleOrDefault(p => p.Id == account.Id);
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

            ViewData["controllerName"] = "Person";
            return PartialView("AccountsColGridPartial", person.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            cMDSubjects_Person person;
            person = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];

            if (id > 0)
            {
                try
                {
                    var target = person.MDSubjects_Subject_AccountsCol.SingleOrDefault(p => p.Id == id);
                    person.MDSubjects_Subject_AccountsCol.Remove(target);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewData["controllerName"] = "Person";
            return PartialView("AccountsColGridPartial", person.MDSubjects_Subject_AccountsCol);
        } 
        #endregion

        #region ContactsColGridPartial
        public ActionResult RefreshContactsColGridPartial()
        {
            cMDSubjects_Person person;
            person = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];

            //if (person == null)
            //    System.Web.HttpContext.Current.Session["Person"] = person = cMDSubjects_Person_Specific_CRO.GetMDSubjects_Person_Specific_CRO(id);
            ViewData["controllerName"] = "Person";
            return PartialView("ContactsColGridPartial", person.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult AddNewContact(cMDSubjects_Subject_Contacts contact)
        {
            cMDSubjects_Person person;
            person = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];

            if (ModelState.IsValid)
            {
                try
                {
                    contact.MarkChild();
                    person.MDSubjects_Subject_ContactsCol.Add(contact);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "Person";
            return PartialView("ContactsColGridPartial", person.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult UpdateContact(cMDSubjects_Subject_Contacts contact)
        {
            cMDSubjects_Person person;
            person = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];

            if (ModelState.IsValid)
            {
                try
                {
                    var target = person.MDSubjects_Subject_ContactsCol.SingleOrDefault(p => p.Id == contact.Id);
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

            ViewData["controllerName"] = "Person";
            return PartialView("ContactsColGridPartial", person.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult DeleteContact(int id)
        {
            cMDSubjects_Person person;
            person = (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];

            if (id > 0)
            {
                try
                {
                    var target = person.MDSubjects_Subject_ContactsCol.SingleOrDefault(p => p.Id == id);
                    person.MDSubjects_Subject_ContactsCol.Remove(target);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewData["controllerName"] = "Person";
            return PartialView("ContactsColGridPartial", person.MDSubjects_Subject_ContactsCol);
        }
        #endregion

        public ActionResult MjestaComboPartial(int? Id, FormCollection collection)
        {

            //string bindProperty = collection["DXCallbackName"].Substring(1, collection["DXCallbackName"].Length - 1);
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "Person");
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
                return (cMDSubjects_Person)System.Web.HttpContext.Current.Session["Person"];
            else return Activator.CreateInstance(modelType);
        }
    }
}
