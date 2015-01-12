using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.Documents;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using Csla.Data;
using AlphaWebCommodityBookkeeping.Areas.Documents.Reports;
using DevExpress.XtraPrinting;
using System.IO;
using DevExpress.Web.Mvc;
using System.Net.Mail;
using System.Collections;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AlphaWebCommodityBookkeeping.Areas.Documents.Controllers
{
    public class InvoiceController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /Documents/Invoice/

        public ActionResult Index()
        {
            if (!CheckAccess())
                return View("../Account/AccessDenied");
            System.Web.HttpContext.Current.Session["Report"] = null;
            return View();
        }

        public bool CheckAccess()
        {
            bool ret = true;
            if (!((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).Invoice)
            {
                ret = false;
            }
            return ret;
        }

        //
        // GET: /Documents/Invoice/Create

        public ActionResult CreateAndEdit(int id)
        {
            System.Web.HttpContext.Current.Session["Report"] = null;
            if (!CheckAccess())
                return View("../Account/AccessDenied");

            cDocuments_Invoice obj;
            ViewData["Id"] = id;

            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["Invoice"] = obj = cDocuments_Invoice.GetDocuments_Invoice(id);
                System.Web.HttpContext.Current.Session["InvoiceSubjectId"] = obj.SubjectId;
                AlphaWebCommodityBookkeeping.Models.MjestaComboProvider.MjestoId = obj.BillToAddress_PlaceId;
                AlphaWebCommodityBookkeeping.Models.MjestaComboProvider2.MjestoId = obj.ShipToAddress_PlaceId;
                using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                {
                    /* da lije racun kreiran iz radnog naloga, ako je, da li je potrebno fakturirati put, ako je, ispisi  */
                    var itemWorkOrder = data.Documents_Document.OfType<DalEf.Documents_WorkOrder>().FirstOrDefault(p => p.Documents_InvoiceId == id);
                    if (itemWorkOrder != null)
                    {
                        if (itemWorkOrder.IsBillable == true && itemWorkOrder.Distance != null)
                        {
                            ViewData["WorkOrderId"] = itemWorkOrder.UniqueIdentifier;
                            ViewData["WorkOrderKm"] = itemWorkOrder.Distance;
                        }
                    }
                    var lockCheck = new Dictionary<int, int>();
                    lockCheck.Add(id, ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId);

                    if (HttpRuntime.Cache[id.ToString()] == null)
                    {
                        System.Web.HttpContext.Current.Cache.Add(id.ToString(),
                                lockCheck, null, DateTime.MaxValue,
                                new TimeSpan(0, 20, 0),
                                System.Web.Caching.CacheItemPriority.Default,
                                null);
                    }
                    else
                    {
                        ViewData["Action"] = "locked"; /* obavjesti odmah usera da je doc zalockan */
                    }
                }
                ViewData["InvoiceId"] = id;
                System.Web.HttpContext.Current.Session["Id"] = id;
                ViewData["FiskalizirajRacun"] = Request.QueryString["message"];
            }
            else
            {
                System.Web.HttpContext.Current.Session["Invoice"] = obj = cDocuments_Invoice.NewDocuments_Invoice();
                ViewData["InvoiceId"] = 0;
            }
            List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList;
            using (var context = new DalEf.DocumentsEntities())
            {
                System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = paymentsInAdvanceList = context.vPaymentsInAdvance.OrderBy(p => p.UniqueIdentifier).Where(p => p.SubjectBuyerId == obj.SubjectId && (p.AdvancePaymentAmmount ?? 0) != 0 ).ToList();
            }


            /* U cache se snima id dokumenta koji se editira i userId
                   Ako objekt u cache-u postoji, netko vec esitira doc. 
                   Ako je null, doc nije otvoren, lockaj ga
                 */

            if (obj.MDGeneral_Enums_CurrencyId == null)
                obj.MDGeneral_Enums_CurrencyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).DefaultCurencyId;

            ViewData.Model = obj;
            ViewData["paymentsInAdvanceList"] = paymentsInAdvanceList;
            string message = Request.QueryString["message"];
            if (!string.IsNullOrEmpty(message))
                ViewData["Action"] = message;
            return View();
        }

        public bool PrintDocument(int id)
        {
            System.Web.HttpContext.Current.Session["Report"] = null;

            cDocuments_Invoice obj;

            xrDocuments report = new xrDocuments();

            System.Web.HttpContext.Current.Session["Invoice"] = obj = cDocuments_Invoice.GetDocuments_Invoice(id);

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                /* da lije racun kreiran iz radnog naloga, ako je, da li je potrebno fakturirati put, ako je, ispisi  */
                var itemWorkOrder = data.Documents_Document.OfType<DalEf.Documents_WorkOrder>().FirstOrDefault(p => p.Documents_InvoiceId == id);
                if (itemWorkOrder != null)
                {
                    if (itemWorkOrder.IsBillable == true && itemWorkOrder.Distance != null)
                    {
                        ViewData["WorkOrderId"] = itemWorkOrder.UniqueIdentifier;
                        ViewData["WorkOrderKm"] = itemWorkOrder.Distance;
                    }
                    report.xrLabel9.Text = "Vezni dokument: " + itemWorkOrder.UniqueIdentifier + ".";
                }

                DalEf.vDocuments_ForPrint item = data.vDocuments_ForPrint.SingleOrDefault(p => p.Id == id);
                var itemDetails = data.vDocumentDetails.OrderBy(p => p.Ordinal).Where(p => p.DocumentId == id).ToList();
                var taxDetails = data.vDocumentTaxesByTypes.Where(p => p.Id == id).ToList();
                var payedTillNowData = data.vInvoicePayments.Where(p => p.InvoiceId == id);
                decimal payedTillNow = 0;

                if (payedTillNowData != null)
                {
                    if (payedTillNowData.Count() > 0)
                        payedTillNow = payedTillNowData.Sum(p => p.Ammount) / (obj.CourseValue ?? 1);
                }
                
                report.bindingSource1.DataSource = item;
                report.bindingSource2.DataSource = itemDetails;
                ((xrTaxesCashSub)report.xrSubTaxes.ReportSource).bindingSource1.DataSource = taxDetails;

                var cur = data.MDGeneral_Enums_Currency_documents.SingleOrDefault(p => p.Id == (obj.MDGeneral_Enums_CurrencyId ?? ((PTIdentity)Csla.ApplicationContext.User.Identity).DefaultCurencyId));

                if (cur != null)
                    report.lblZaPlatiti.Text = string.Format("Za platiti ({0})", cur.Label.ToUpper());

                DalEf.vAuth_Company companyItem = data.vAuth_Company.SingleOrDefault(p => p.Id == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                if (companyItem != null)
                {
                    report.lblName.Text = companyItem.Name;
                    report.lblName2.Text = companyItem.Name;
                    report.lblOib.Text = companyItem.OIB;
                    report.lblAddress.Text = companyItem.HomeAddress_StreetAndNumber;
                    report.lblPlace.Text = companyItem.HomeAddress_PlaceName;
                    report.lblAccount.Text = companyItem.Account;
                    report.lblAccount1.Text = companyItem.Account1;
                }


                report.lblPayedTillNow.Text = payedTillNow.ToString();
                report.lblForPayment.Text = (item.RetailValue - payedTillNow).ToString();
            }

            report.CreateDocument();

            System.Web.HttpContext.Current.Session["Report"] = report;
            ViewData["Report"] = report;

            return true;
        }
        //
        // POST: /Documents/Invoice/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cDocuments_Invoice obj, FormCollection collection)
        {
            string Action = collection["HiddenValueAction"];
            LoadProperty(obj, cDocuments_Invoice.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cDocuments_Invoice.EntityKeyDataProperty, enKey);
            }
            string cashboxCodes = System.Web.HttpContext.Current.Session["CashBoxCodes"].ToString(); // locationCode / blagajnaCode
            string locationCode = Regex.Split(cashboxCodes, "/")[0];
            string cashBoxCode = Regex.Split(cashboxCodes, "/")[1];
            obj.CashBoxCode = cashBoxCode;
            obj.LocationCode = locationCode;

            if (id == 0)
            {
                string docNum = "0";
                int num = 0;
                
                obj.OrdinalNumber = num;
                obj.UniqueIdentifier = docNum;
            }

            if (Convert.ToBoolean(System.Web.HttpContext.Current.Session["fsMode"]))
            //if (System.Web.HttpContext.Current.Session["BlagajnaLocationId"] != null)
            {
                CreateFiscalXml(obj);
                obj.FiscalizationCashierId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                obj.FiscalizationTime = DateTime.Now;
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            obj.Status = 0;
            obj.TaxValue = obj.Documents_ItemsCol.Sum(p => p.TaxAmmount);
            obj.RetailValue = obj.Documents_ItemsCol.Sum(p => p.WholesaleValue) + obj.TaxValue;
            obj.WholesaleValue = obj.Documents_ItemsCol.Sum(p => p.WholesaleValue);
            obj.RabateValue = obj.Documents_ItemsCol.Sum(p => p.RabateAmmount);
            if (obj.IsValid)
            {
                if (obj.Id > 0)
                {
                    if (IsDocLocked(obj.Id, ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId))
                    {
                        /* Dokument je zalockan, snimanje je zabranjeno, vrati useru CreateAndEdit view s porukom */
                        ViewData.Model = obj;
                        return RedirectToAction("CreateAndEdit/" + id, new { message = "locked" });
                    }

                    if (SaveObject<cDocuments_Invoice>(obj, true))
                    {
                        int invoiceId = obj.Id;
                        SavePaymentsInAdvance(invoiceId);

                        System.Web.HttpContext.Current.Session["Invoice"] = null;
                        //return RedirectToAction("Index");
                        if (Action != "")
                        {
                            if (Action.Contains("gen"))
                            {
                                int Employeeid = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                                using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                                {
                                    switch (Action)
                                    {
                                        case "genDispatch": // dispatch
                                            var res = data.uspInvoice2Dispatch(id, Employeeid).SingleOrDefault();
                                            break;
                                        case "genCopy":
                                            res = data.uspInvoice2Invoice(id, Employeeid).SingleOrDefault();
                                            //Action = String.Format("{0}_{1}", Action, Convert.ToInt32(res));
                                            Action = "genCopy_100";
                                            break;
                                    }
                                }
                            }
                            return RedirectToAction("CreateAndEdit/" + id, new { message = Action });
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ViewData.Model = obj;
                        return View();
                    }
                }
                else
                {
                    obj.FiscalizationConsistenceCode = ((PTIdentity)Csla.ApplicationContext.User.Identity).FiscalizationConsistenceCode;
                    obj.MDSubjects_Employee_AuthorId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                    obj.CreationDateTime = DateTime.Now;
                    obj.DocumentType = (short)BusinessObjects.Common.DocumentType.Invoice;

                    if (SaveObject<cDocuments_Invoice>(obj, false))
                    {
                        int invoiceId = ((cDocuments_Invoice)ViewData.Model).Id;
                        SavePaymentsInAdvance(invoiceId);

                        System.Web.HttpContext.Current.Session["Invoice"] = null;
                        //return RedirectToAction("Index");
                        if (Action != "")
                        {
                            if (Action.Contains("gen"))
                            {
                                int Employeeid = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                                using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                                {
                                    switch (Action)
                                    {

                                        case "genDispatch": // dispatch
                                            var res = data.uspInvoice2Dispatch(((cDocuments_Invoice)ViewData.Model).Id, Employeeid).SingleOrDefault();
                                            break;
                                        case "genCopy":
                                            res = data.uspInvoice2Invoice(((cDocuments_Invoice)ViewData.Model).Id, Employeeid).SingleOrDefault();
                                            //Action = String.Format("{0}_{1}", Action, Convert.ToInt32(res));
                                            Action = "genCopy_100";
                                            break;
                                    }
                                }
                            }
                            return RedirectToAction("CreateAndEdit/" + ((cDocuments_Invoice)ViewData.Model).Id, new { message = Action });
                        }
                        else
                        {
                            if (collection["PotrebnaFiskalizacija"] != "" && collection["PotrebnaFiskalizacija"] != null)
                                return RedirectToAction("CreateAndEdit/" + ((cDocuments_Invoice)ViewData.Model).Id, new { message = "FiskalizirajRacun" });
                            else
                                return RedirectToAction("Index");
                        }
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


        public bool CreateFiscalXml(cDocuments_Invoice obj)
        {
            bool ret = false;
            string resultFilePath = AppDomain.CurrentDomain.BaseDirectory  +  "Content\\Fiscal-" + ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId + "-" + ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId + ".xml";
            XmlWriter writer = XmlWriter.Create(resultFilePath);
            writer.WriteStartElement("Foo");
            writer.WriteAttributeString("Bar", "Some & value");
            writer.WriteElementString("Nested", "data");
            writer.WriteEndElement();
            writer.Close();
            return ret;
        }

        public string ZastitiniKodIzracun(cDocuments_Invoice obj)
        {
            using (DalEf.MDSubjectsEntities context = new DalEf.MDSubjectsEntities())
            {
                int userId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                var employee = context.MDSubjects_Subject.FirstOrDefault(p => p.Id == userId);
                if (employee != null)
                {
                    string onu = "12234";   //oznaka naplatnog uređaja
                    string bor = "12345";   // brojcana oznaka racuna
                    string opp = "blag01";  //oznaka poslovnog prostora
                    string uir = "12312";   //ukupni iznos računa
                    string medjurez = employee.OIB + DateTime.Now.ToString("dd.mm.yyyy HH:mm:ss") + bor + opp + onu + uir;
                    byte[] potpisano = null;
                    try
                    {
                        X509Certificate2 certifikat = new X509Certificate2("c:\\certifikat.p12", "lozinka");
                        RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certifikat.PrivateKey;
                        byte[] podaci = Encoding.ASCII.GetBytes(medjurez);
                        potpisano = rsa.SignData(podaci, new SHA1CryptoServiceProvider());
                        // rezultatIspis = izračunajMD5(elektronički potpisani medjurezultat)
                        MD5 md5Hash = MD5.Create();
                        string rezultatIspis = GetMd5Hash(md5Hash, potpisano);
                        // kraj
                        return rezultatIspis;
                    }
                    catch (Exception ex)
                    {
                        // greška
                        Console.WriteLine(ex.Message);
                    }
                    
                }
                return string.Empty;
            }
        }

        static string GetMd5Hash(MD5 md5Hash, byte[] input)
        {
            byte[] data = md5Hash.ComputeHash(input);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public Boolean IsDocLocked(int docId, int userId)
        {
            if (HttpRuntime.Cache[docId.ToString()] == null)
                return false;

            int lockedDocId;
            int lockedUserId;
            bool isDocLocked = false;
            bool isUser = false;
            foreach (var item in (IEnumerable)HttpRuntime.Cache[docId.ToString()])
            {
                lockedDocId = Convert.ToInt32(item.ToString().Split(',')[0].Split('[')[1]);
                lockedUserId = Convert.ToInt32(item.ToString().Split(',')[1].Split(']')[0]);
                if (lockedDocId == docId)
                {
                    isDocLocked = true;
                    if (lockedUserId == userId)
                    {
                        isDocLocked = true;
                        if (userId == lockedUserId)
                        {
                            isUser = true;
                            HttpRuntime.Cache.Remove(docId.ToString());
                        }
                        break;
                    }
                }
            }
            /* Da li je doc zalockan? true/false */
            if ((isDocLocked && isUser) || !isDocLocked)
            {
                /* lock je obrisan (snima ga user koji ga je zalockao) ili doc nije zalockan */
                return false;
            }
            return true;
        }

        public void unlockTheDoc()
        {
            cDocuments_Invoice obj = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];

            if (HttpRuntime.Cache[obj.Id.ToString()] == null)
                return;

            foreach (var item in (IEnumerable)HttpRuntime.Cache[obj.Id.ToString()])
            {
                int lockedDocId = Convert.ToInt32(item.ToString().Split(',')[0].Split('[')[1]);
                int lockedUserId = Convert.ToInt32(item.ToString().Split(',')[1].Split(']')[0]);
                if (lockedDocId == obj.Id)
                {
                    if (lockedUserId == ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId)
                    {
                        HttpRuntime.Cache.Remove(obj.Id.ToString());
                        break;
                    }
                }
            }
        }


        private static void SavePaymentsInAdvance(int invoiceId)
        {
            List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList = (List<DalEf.vPaymentsInAdvance>)System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                foreach (var item in paymentsInAdvanceList)
                {
                    if ((item.UseForClosing ?? false) && (item.AmmountForClosing ?? 0) > 0)
                    {
                        var payment = data.Documents_PaymentItemsCol.SingleOrDefault(p =>  p.Id == item.PaymentItemId);
                        if (payment != null)
                        {
                            var paymentItem = data.Documents_PaymentClosureGCol.FirstOrDefault(p => p.PayedInvoiceDocumentId == invoiceId);
                            if (paymentItem != null)
                            {
                                paymentItem.Ammount = paymentItem.Ammount + (item.AmmountForClosing ?? 0);
                            }
                            else
                            {
                                var newPaymentItem = new DalEf.Documents_PaymentClosureGCol();

                                newPaymentItem.Ammount = (item.AmmountForClosing ?? 0);
                                newPaymentItem.PayedInvoiceDocumentId = invoiceId;
                                payment.Documents_PaymentClosureGCol.Add(newPaymentItem);
                                
                            }
                            payment.AdvancePaymentAmmount = payment.Ammount - (payment.Documents_PaymentClosureGCol.Sum(p => p.Ammount));
                        }
  
                    }
                }

                data.SaveChanges();
            }
        }

        public ActionResult Odustani()
        {
            unlockTheDoc();
            System.Web.HttpContext.Current.Session["Invoice"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult InvoiceGridPartial()
        {
            return PartialView("InvoiceGridPartial");
        }




   public ActionResult InvoiceReportPartial(int id)
   {
       ViewData.Model = System.Web.HttpContext.Current.Session["Report"];
       ViewData["invoiceId"] = System.Web.HttpContext.Current.Session["invoiceId"];

       return PartialView("InvoiceReportPartial");
   }

   public ActionResult ReportViewerExportTo(int id)
   {
       xrDocuments report = (xrDocuments)System.Web.HttpContext.Current.Session["Report"];
       return ReportViewerExtension.ExportTo(report);
   }

      public ActionResult CreateItem()
	{
        cDocuments_Items newItem = ((cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"]).Documents_ItemsCol.AddNew();
        newItem.ProductId = 8;
        ViewData["Count"] = ((cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"]).Documents_ItemsCol.Count();
        DalEf.MDEntitiesEntities contextEnt = new DalEf.MDEntitiesEntities();
          IQueryable<DalEf.MDEntities_Entity> items = contextEnt.MDEntities_Entity.Where(p => p.CompanyUsingServiceId == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
          ViewData["productItems"] = items;
        return PartialView("ItemTableRowPartial", newItem);
	}
        //
        // GET: /Documents/Invoice/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Documents/Invoice/Delete/5

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

        public ActionResult RefreshHystoryPaymentsGridPartial()
        {
            cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];
            ViewData["invoiceId"] = Invoice.Id;
            return PartialView("HystoryPaymentsGridPartial");
        }

        public ActionResult RefreshFreeAvansesGridPartial()
        {
            ViewData.Model = System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];
            return PartialView("FreeAvansesGridPartial");
        }

        public ActionResult UpdateFreeAvans(int key, string value)
        {
            bool refreshGrid = false;
            cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];
            List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList = (List <DalEf.vPaymentsInAdvance>)System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];
            DalEf.vPaymentsInAdvance item = paymentsInAdvanceList.SingleOrDefault(p => p.PaymentItemId == key);

            if (item != null)
            {
                decimal invoiceAmmount = Invoice.Documents_ItemsCol.Sum(p => p.WholesaleValue + p.TaxAmmount);
                decimal? historyPaymentAmmount = 0;
                decimal paymentsInAdvanceAmmount = (item.AdvancePaymentAmmount ?? 0);
                decimal paymentsInAdvanceAmmountSum = 0;
                decimal ammountForClosing = 0;

               if (value == "true" && item.UseForClosing == false)
               {
                    refreshGrid = true;
                    if (!Invoice.IsNew)
                    {
                        using (var context = new DalEf.DocumentsEntities())
                        {
                            var historyPaymentAmmountData = context.vInvoicePayments.Where(p => p.InvoiceId == Invoice.Id);
                            if (historyPaymentAmmountData != null)
                            {
                                if (historyPaymentAmmountData.Count() > 0)
                                    historyPaymentAmmount = historyPaymentAmmountData.Sum(p => p.Ammount);
                            }
                           
                        }
                    }

                        paymentsInAdvanceAmmountSum = paymentsInAdvanceList.Where(p => (p.UseForClosing ?? false) == true).Sum(p => (p.AmmountForClosing ?? 0));
                        decimal closeTillNow = paymentsInAdvanceAmmountSum + (historyPaymentAmmount ?? 0);
                        if (closeTillNow < invoiceAmmount)
                        {
                            if (paymentsInAdvanceAmmount >= (invoiceAmmount - closeTillNow))
                            {
                                ammountForClosing = invoiceAmmount - closeTillNow;
                            }
                            else
                            {
                                ammountForClosing = paymentsInAdvanceAmmount;
                            }
                        }

                        if (ammountForClosing > 0)
                        {
                           
                            item.UseForClosing = true;
                            item.AmmountForClosing = ammountForClosing;
                        }
                    //}
               }
               else if (value == "false" && item.UseForClosing == true)
               {
                   refreshGrid = true;

                   if (!Invoice.IsNew)
                   {
                       using (var context = new DalEf.DocumentsEntities())
                       {
                           var historyPaymentAmmountData = context.vInvoicePayments.Where(p => p.InvoiceId == Invoice.Id);
                           if (historyPaymentAmmountData != null)
                           {
                               if (historyPaymentAmmountData.Count() > 0)
                                   historyPaymentAmmount = historyPaymentAmmountData.Sum(p => p.Ammount);
                           }
                       }
                   }


                   item.AmmountForClosing = 0;
                   item.UseForClosing = false;
                   paymentsInAdvanceAmmountSum = paymentsInAdvanceList.Where(p => (p.UseForClosing ?? false) == true).Sum(p => (p.AmmountForClosing ?? 0));
                   

                   decimal closeTillNow = paymentsInAdvanceAmmountSum + (historyPaymentAmmount ?? 0);

                   foreach (var itemInAdvance in paymentsInAdvanceList)
                   {
                       if (closeTillNow < invoiceAmmount)
                       {
                           if (itemInAdvance.UseForClosing == true)
                           {
                               if (itemInAdvance.AdvancePaymentAmmount > itemInAdvance.AmmountForClosing)
                               {
                                   if ((itemInAdvance.AdvancePaymentAmmount + (closeTillNow - itemInAdvance.AmmountForClosing)) < (invoiceAmmount))
                                   {
                                       closeTillNow = closeTillNow + (itemInAdvance.AdvancePaymentAmmount ?? 0);
                                       itemInAdvance.AmmountForClosing = (itemInAdvance.AdvancePaymentAmmount ?? 0);
                                       
                                   }
                                   else
                                   {
                                       closeTillNow = closeTillNow - (itemInAdvance.AmmountForClosing ?? 0);
                                       itemInAdvance.AmmountForClosing = invoiceAmmount - closeTillNow;
                                       closeTillNow = closeTillNow + (itemInAdvance.AmmountForClosing ?? 0);
                                      
                                       if (itemInAdvance.AmmountForClosing <= 0)
                                       {
                                           itemInAdvance.UseForClosing = false;
                                       }
                                   }
                               }
                           }
                       }
                   }

               }
               System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = paymentsInAdvanceList;
            }
            JsonResult rez = new JsonResult() { Data = refreshGrid };
            return rez;
        }
        
        #region DocumentItemsColGridPartial
        public ActionResult RefreshDocumentItemsColGridPartial()
        {
            cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];
            ViewData["controllerName"] = "Invoice";
            return PartialView("DocumentItemsColGridPartial", Invoice.Documents_ItemsCol);
        }

        [HttpPost]
        public ActionResult AddNewItem(cDocuments_Items item)
        {
            cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];

            if (ModelState.IsValid)
            {
                try
                {
                    item.MarkChild();
                    item.Ordinal = Invoice.Documents_ItemsCol.Count + 1;

                    item.RabateAmmount = (System.Math.Round((item.Price * item.Quantity),2,MidpointRounding.AwayFromZero) - item.WholesaleValue);

                    decimal tax = 0;
                    using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
                    {
                        var taxItem = data.MDEntities_Enums_TaxRate.SingleOrDefault(p => p.Id == item.TaxRateId);
                        if (taxItem != null)
                            tax = System.Math.Round(item.WholesaleValue * (taxItem.Percentage / 100),2,MidpointRounding.AwayFromZero);
                    }

                    item.TaxAmmount = tax;

                    Invoice.Documents_ItemsCol.Add(item);

                    List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList = (List<DalEf.vPaymentsInAdvance>)System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];

                    foreach (var itemAdvance in paymentsInAdvanceList)
                    {
                        itemAdvance.AmmountForClosing = 0;
                        itemAdvance.UseForClosing = false;
                    }

                    System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = paymentsInAdvanceList;
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "Invoice";
            return PartialView("DocumentItemsColGridPartial", Invoice.Documents_ItemsCol);
        }

        [HttpPost]
        public ActionResult UpdateItem(cDocuments_Items item, FormCollection collection)
        {
            cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];

            if (ModelState.IsValid)
            {
                try
                {
                    if (item.Id != 0)
                    {
                        var target = Invoice.Documents_ItemsCol.SingleOrDefault(p => p.Id == item.Id);
                        if (target != null)
                        {
                            DataMapper.Map(item, target, "Id", "DocumentId", "PriceListId", "PriceFromPriceList", "EntityKeyData");

                            target.RabateAmmount = System.Math.Round(target.Price * target.Quantity,2,MidpointRounding.AwayFromZero) - target.WholesaleValue;

                            decimal tax = 0;
                            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
                            {
                                var taxItem = data.MDEntities_Enums_TaxRate.SingleOrDefault(p => p.Id == target.TaxRateId);
                                if (taxItem != null)
                                    tax = System.Math.Round(target.WholesaleValue * (taxItem.Percentage / 100),2,MidpointRounding.AwayFromZero);
                            }

                            target.TaxAmmount = tax;
                        }
                    }
                    else
                    {
                        var target = Invoice.Documents_ItemsCol.SingleOrDefault(p => p.Ordinal == item.Ordinal);
                        if (target != null)
                        {
                            DataMapper.Map(item, target, "Id", "DocumentId", "PriceListId", "PriceFromPriceList", "EntityKeyData");

                            target.RabateAmmount = System.Math.Round(target.Price * target.Quantity,2,MidpointRounding.AwayFromZero) - target.WholesaleValue;

                            decimal tax = 0;
                            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
                            {
                                var taxItem = data.MDEntities_Enums_TaxRate.SingleOrDefault(p => p.Id == target.TaxRateId);
                                if (taxItem != null)
                                    tax =System.Math.Round(target.WholesaleValue * (taxItem.Percentage / 100),2,MidpointRounding.AwayFromZero);
                            }

                            target.TaxAmmount = tax;
                        }
                    }

                    List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList = (List<DalEf.vPaymentsInAdvance>)System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];

                    foreach (var itemAdvance in paymentsInAdvanceList)
                    {
                        itemAdvance.AmmountForClosing = 0;
                        itemAdvance.UseForClosing = false;
                    }

                    System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = paymentsInAdvanceList;
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "Invoice";
            return PartialView("DocumentItemsColGridPartial", Invoice.Documents_ItemsCol);
        }

        [HttpPost]
        public ActionResult DeleteItem(cDocuments_Items item)
        {
            cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];

            if (item.Id > 0)
            {
                try
                {
                    var target = Invoice.Documents_ItemsCol.SingleOrDefault(p => p.Id == item.Id);
                    Invoice.Documents_ItemsCol.Remove(target);

                    int count = 1;
                    foreach (var it in Invoice.Documents_ItemsCol)
                    {
                        it.Ordinal = count;
                        count += 1;
                    }

                    List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList = (List<DalEf.vPaymentsInAdvance>)System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];

                    foreach (var itemAdvance in paymentsInAdvanceList)
                    {
                        itemAdvance.AmmountForClosing = 0;
                        itemAdvance.UseForClosing = false;
                    }

                    System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = paymentsInAdvanceList;

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
            {
                var target = Invoice.Documents_ItemsCol.SingleOrDefault(p => p.Ordinal == item.Ordinal);
                Invoice.Documents_ItemsCol.Remove(target);

                int count = 1;
                foreach (var it in Invoice.Documents_ItemsCol)
                {
                    it.Ordinal = count;
                    count += 1;
                }

                List<DalEf.vPaymentsInAdvance> paymentsInAdvanceList = (List<DalEf.vPaymentsInAdvance>)System.Web.HttpContext.Current.Session["paymentsInAdvanceList"];

                foreach (var itemAdvance in paymentsInAdvanceList)
                {
                    itemAdvance.AmmountForClosing = 0;
                    itemAdvance.UseForClosing = false;
                }

                System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = paymentsInAdvanceList;
            }

            ViewData["controllerName"] = "Invoice";
            return PartialView("DocumentItemsColGridPartial", Invoice.Documents_ItemsCol);
        }
        #endregion

        object IModelCreator.CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cDocuments_Invoice)))
                return (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];
            else return Activator.CreateInstance(modelType);
        }

        public ActionResult ProductIdChanged(FormCollection collection)
        {
            int SubjectId = Convert.ToInt32((System.Web.HttpContext.Current.Session["InvoiceSubjectId"]??0));
            int ProizvoId;
            if (!Int32.TryParse(collection["proizvodId"], out ProizvoId))
                ProizvoId = 0;

            decimal rezPrice = 0;
            decimal rezPriceOrig = 0;
            var unitid = 0;
            var taxrateId = 0;

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                var itemSubject = data.Documents_Document.OfType<DalEf.Documents_PriceList>()
                    .OrderByDescending(p => p.MDSubjects_SubjectId)
                    .FirstOrDefault(p => p.MDSubjects_SubjectId == SubjectId || p.MDSubjects_SubjectId == null); //Stavljeno privremeno, treba uzeti u obzir datume valjanosti cjenika
                if (itemSubject != null)
                {
                    var itemProduct = itemSubject.Documents_PriceList_ItemsCol.FirstOrDefault(p => p.MDEntities_EntityId == ProizvoId);

                    var dataEnt = new DalEf.MDEntitiesEntities();
                    var itemProductEnt = dataEnt.vEntities.FirstOrDefault(p => p.Id == ProizvoId);
                    
                    if (itemProduct != null)
                        rezPrice = itemProduct.WholesalePrice ?? 0;
                    else
                        if (itemProductEnt != null)
                        {
                            rezPrice = itemProductEnt.WholesalePrice;
                            unitid = itemProductEnt.UnitId;
                            taxrateId = itemProductEnt.TaxRateId;
                        }
                        else
                        {
                            rezPrice = 0;
                            unitid = 0;
                            taxrateId = 0;
                        }

                }
                else
                {
                    var dataEnt = new DalEf.MDEntitiesEntities();
                    
                    var itemProductEnt = dataEnt.vEntities.FirstOrDefault(p => p.Id == ProizvoId);
                    if (itemProductEnt != null)
                    {
                        rezPrice = itemProductEnt.WholesalePrice;
                        unitid = itemProductEnt.UnitId;
                        taxrateId = itemProductEnt.TaxRateId;
                    }
                }
            }

            rezPriceOrig = rezPrice;

            
            try
            {
                rezPrice = System.Math.Round(rezPrice / Convert.ToDecimal(collection["tecaj"]),2,MidpointRounding.AwayFromZero);
            }
            catch 
            {
            }
            string[] list = { rezPrice.ToString(), unitid.ToString(), taxrateId.ToString(), rezPriceOrig.ToString() };

            JsonResult result = new JsonResult() { Data = list };
            return result;
        }

        public ActionResult TecajChanged(FormCollection collection)
        {
            cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];
            int count = Invoice.Documents_ItemsCol.Count;
            JsonResult result = new JsonResult();

            if (count == 0)
            {
                decimal val = 0;
                string[] list = { val.ToString(("n2")), val.ToString(("n2")), val.ToString(("n2")), val.ToString(("n2")) };
                result.Data = list;
                return result;
            }

            decimal Ukupno = 0;
            decimal Pdv = 0;
            decimal ZaPlatiti = 0;
            decimal PdvPercentage = 0;
            decimal RabatTotal = 0;
            decimal NovaCIjena = 0;
            decimal NovaWholesaleValue = 0;
            decimal NovaJedCijena = 0;


            decimal tecaj = Convert.ToDecimal(collection["Tecaj"].Replace(".", ","));
            decimal Cijena = 0;

            decimal PrevTecaj = Convert.ToDecimal(collection["PrevTecaj"].Replace(".", ","));

            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
            {
                for (int i = 0; i < count; i++)
                {

                    Cijena = System.Math.Round(Convert.ToDecimal(Invoice.Documents_ItemsCol[i].Price) * PrevTecaj,2,MidpointRounding.AwayFromZero);

                    NovaCIjena = System.Math.Round(Cijena / tecaj, 2, MidpointRounding.AwayFromZero);
                    NovaJedCijena = System.Math.Round(Convert.ToDecimal(NovaCIjena - (NovaCIjena / 100 * Invoice.Documents_ItemsCol[i].RabatePercentage)), 2, MidpointRounding.AwayFromZero);
                    NovaWholesaleValue = System.Math.Round(Invoice.Documents_ItemsCol[i].Quantity * NovaJedCijena,2,MidpointRounding.AwayFromZero);

                    Ukupno += NovaWholesaleValue;
                    int SessionPdv = Invoice.Documents_ItemsCol[i].TaxRateId;

                    var itemPdvPerc = data.MDEntities_Enums_TaxRate.FirstOrDefault(p => p.Id == SessionPdv);
                    PdvPercentage = itemPdvPerc.Percentage;
                    Invoice.Documents_ItemsCol[i].TaxAmmount = System.Math.Round(Convert.ToDecimal(NovaWholesaleValue) * PdvPercentage / 100,2,MidpointRounding.AwayFromZero);
                    Pdv += System.Math.Round(Convert.ToDecimal(NovaWholesaleValue) * PdvPercentage / 100,2,MidpointRounding.AwayFromZero);

                    if (Convert.ToDecimal(Invoice.Documents_ItemsCol[i].RabatePercentage) != 0)
                    {
                        RabatTotal += System.Math.Round(NovaCIjena * Convert.ToDecimal(Invoice.Documents_ItemsCol[i].Quantity) - NovaWholesaleValue, 2, MidpointRounding.AwayFromZero);
                        Invoice.Documents_ItemsCol[i].RabateAmmount = System.Math.Round(NovaCIjena * Convert.ToDecimal(Invoice.Documents_ItemsCol[i].Quantity) - NovaWholesaleValue,2,MidpointRounding.AwayFromZero);
                    }


                    Invoice.Documents_ItemsCol[i].Price = NovaCIjena;
                    Invoice.Documents_ItemsCol[i].Ammount = NovaJedCijena;
                    Invoice.Documents_ItemsCol[i].WholesaleValue = NovaWholesaleValue;
                }
                ZaPlatiti += Ukupno + Pdv;
            }

            string[] llist = { Ukupno.ToString(("n2")), Pdv.ToString(("n2")), ZaPlatiti.ToString(("n2")), RabatTotal.ToString(("n2")) };
            
            result.Data = llist;
            return result;
        }

        public ActionResult Calc(FormCollection collection)
        {
            cDocuments_Invoice Invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];
            decimal Ukupno = 0;
            decimal Pdv = 0;
            decimal ZaPlatiti = 0;
            decimal PdvPercentage = 0;
            decimal RabatTotal = 0;
            int Ordinal = 0;

            bool isCallback = Convert.ToBoolean(collection["isCallback"]);

            if (!isCallback)
            {
                Ordinal = Convert.ToInt32(collection["Ordinal"]);
            }
            int count = Invoice.Documents_ItemsCol.Count;

            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
            {
                
                foreach (var item in Invoice.Documents_ItemsCol)
                {
                    if (Ordinal != 0 && !isCallback)
                    {
                        /* Ordinal != 0 => mjenja se postojeci item u gridu, zbroji sve ostale i dodaj cijenu promjenjenog na kraju
                         * U suprotnom, pozbrajaj sve iz Sessiona i dodaj cijenu novog itema */
                        int SessionOrdinal = item.Ordinal;
                        if (SessionOrdinal == Ordinal)
                            continue;
                    }
                    Ukupno += Convert.ToDecimal(item.WholesaleValue);
                    Pdv += Convert.ToDecimal(item.TaxAmmount);
                    RabatTotal += Convert.ToDecimal(item.RabateAmmount);
                }
                
                if (!isCallback)
                {
                    int TaxId = Convert.ToInt32(collection["TaxId"]);
                    decimal Rabat = Convert.ToDecimal(collection["Rabat"].Replace(".", ","));
                    decimal Cijena = Convert.ToDecimal(collection["Cijena"].Replace(".", ","));
                    decimal Kolicina = Convert.ToDecimal(collection["Kolicina"].Replace(".", ","));
                    decimal Wsv_r = Convert.ToDecimal(collection["WholeSalePrice"].Replace(".", ","));

                    var itemPdvPercToAdd = data.MDEntities_Enums_TaxRate.FirstOrDefault(p => p.Id == TaxId);
                    if (itemPdvPercToAdd != null)
                        PdvPercentage = itemPdvPercToAdd.Percentage;
                    else
                        PdvPercentage = 0;

                    decimal rabatammount = 0;
                    decimal Wsv = System.Math.Round(Cijena * Kolicina, 2, MidpointRounding.AwayFromZero);

                    if (Rabat != 0)
                    {
                        rabatammount = Wsv - Wsv_r;

                        Pdv += System.Math.Round(Wsv_r * (PdvPercentage / 100), 2, MidpointRounding.AwayFromZero);
                        RabatTotal += rabatammount; //Cijena * Kolicina - Wsp;
                        Ukupno += Wsv_r;
                    }
                    else
                    {
                        Pdv += System.Math.Round(Wsv * PdvPercentage / 100, 2, MidpointRounding.AwayFromZero);
                        Ukupno += Wsv;
                    }

                }
                ZaPlatiti = Ukupno + Pdv;
            }
            string[] list = { Ukupno.ToString(("n2")), Pdv.ToString(("n2")), ZaPlatiti.ToString(("n2")), RabatTotal.ToString(("n2")) };
            JsonResult result = new JsonResult() { Data = list };
            return result;
        }


        public ActionResult SubjectIdChanged(FormCollection collection)
        {
            string Street = "";
            string PlaceName = "";
            string Oib = "";

            string BillToAddr = "";
            int? BillToPlaceId = 0;
            string BillToPlaceName = "";

            int SubjectId = Convert.ToInt32(collection["subjectId"]);
            System.Web.HttpContext.Current.Session["InvoiceSubjectId"] = SubjectId;

            using (var context = new DalEf.DocumentsEntities())
            {
                System.Web.HttpContext.Current.Session["paymentsInAdvanceList"] = context.vPaymentsInAdvance.OrderBy(p => p.UniqueIdentifier).Where(p => p.SubjectBuyerId == SubjectId && (p.AdvancePaymentAmmount ?? 0) != 0).ToList();
            }

            using (DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities())
            {
                var itemSubject = data.MDSubjects_Subject.FirstOrDefault(p => p.Id == SubjectId);
                Street = itemSubject.HomeAddress_Street;
                int? PlaceId = itemSubject.HomeAddress_PlaceId;
                Oib = itemSubject.OIB;
                int SubjectType = itemSubject.SubjectType;
                 

                DalEf.MDPlacesEntities context = new DalEf.MDPlacesEntities();
                var place = context.MDPlaces_Enums_Geo_Place.FirstOrDefault(p => p.Id == PlaceId);
                if (place != null)
                {
                    PlaceName = place.Name;
                }

                if (SubjectType == (short)BusinessObjects.Common.SubjectType.Company || SubjectType == (short)BusinessObjects.Common.SubjectType.Obrt || SubjectType == (short)BusinessObjects.Common.SubjectType.SoleProprietor)
                {
                    var item = data.MDSubjects_Subject.OfType<DalEf.MDSubjects_Company>().FirstOrDefault(p => p.Id == SubjectId);
                    if (item != null)
                    {
                        if (item.BillToAddress_PlaceId != null)
                        {
                            BillToPlaceId = item.BillToAddress_PlaceId;

                            DalEf.MDPlacesEntities dataPlaces = new DalEf.MDPlacesEntities();
                            var itemPlace = dataPlaces.MDPlaces_Enums_Geo_Place.FirstOrDefault(p => p.Id == BillToPlaceId);
                            if (itemPlace != null)
                            {
                                BillToPlaceName = itemPlace.Name;
                            }
                        }
                        BillToAddr = item.BillToAddress_Street;
                    }
                }
            }

            string[] list = { Oib, Street, PlaceName, BillToAddr, BillToPlaceName, BillToPlaceId.ToString() };

            JsonResult result = new JsonResult() { Data = list };
            return result;
        }


        public ActionResult FindEmailById(FormCollection collection)
        { 
            string emailFrom = "dino@demo.hr";
            JsonResult result = new JsonResult();
            result.Data = emailFrom;
            return result;
        }

        public ActionResult SendReportByEmail(FormCollection collection)
        {
            string ret = "";
            int id = Convert.ToInt32(collection["Id"]);
            string SendTo = collection["SendTo"];
            string SendToCC = collection["SendToCC"];
            string Subject = collection["Subject"];
            string Body = collection["Body"];
            string ClientName = collection["ClientName"];
            if (Body == "null")
                Body = "";
            string SmtpFrom = System.Configuration.ConfigurationManager.AppSettings["SmtpFrom"];
            string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
            string SmtpAuth = System.Configuration.ConfigurationManager.AppSettings["SmtpAuth"];
            string SmtpUname = System.Configuration.ConfigurationManager.AppSettings["SmtpUname"];
            string SmtpPass = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"];
            
            if (System.Web.HttpContext.Current.Session["Report"] == null)
                PrintDocument(id);
                
            xrDocuments report = (xrDocuments)System.Web.HttpContext.Current.Session["Report"];
            MemoryStream mem = new MemoryStream();
            report.ExportToPdf(mem);

            string FileName = String.Format("Račun {0}.pdf", ClientName);

            try
            {
                MailMessage message = new MailMessage(SmtpFrom, SendTo, Subject, Body);
 
                mem.Seek(0, System.IO.SeekOrigin.Begin);
                Attachment attach = new Attachment(mem, FileName, "application/pdf");
                message.Attachments.Add(attach);
 
                if (SendToCC != "null" && SendToCC != null)
                {
                    message.CC.Add(SendToCC);
                }
                
                SmtpClient smtpClient = new SmtpClient(SmtpServer);
                if (Convert.ToBoolean(SmtpAuth))
                {
                    System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(SmtpUname, SmtpPass);
                    smtpClient.Credentials = SMTPUserInfo;
                }
                smtpClient.Send(message);
                
                // Close the memory stream.
                mem.Close();
                mem.Flush();

                ret = "Ok";
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
          
            JsonResult result = new JsonResult();
            result.Data = ret;
            return result;
        }

        public ActionResult Print(int id)
        {
            xrDocuments report = new xrDocuments();

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                DalEf.vDocuments_ForPrint item = data.vDocuments_ForPrint.SingleOrDefault(p => p.Id == id);
                var itemDetails = data.vDocumentDetails.OrderBy(p => p.Ordinal).Where(p => p.DocumentId == id).ToList();
                var taxDetails = data.vDocumentTaxesByTypes.Where(p => p.Id == id).ToList();
                var payedTillNowData = data.vInvoicePayments.Where(p => p.InvoiceId == id);
                decimal payedTillNow = 0;
                if (payedTillNowData != null)
                {
                    if (payedTillNowData.Count()>0)
                    payedTillNow = payedTillNowData.Sum(p => p.Ammount);
                }
                report.bindingSource1.DataSource = item;
                report.bindingSource2.DataSource = itemDetails;
                ((xrTaxesCashSub)report.xrSubTaxes.ReportSource).bindingSource1.DataSource = taxDetails;

                DalEf.vAuth_Company companyItem = data.vAuth_Company.SingleOrDefault(p => p.Id == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                if (companyItem != null)
                {
                    report.lblName.Text = companyItem.Name;
                    report.lblName2.Text = companyItem.Name;
                    report.lblOib.Text = companyItem.OIB;
                    report.lblAddress.Text = companyItem.HomeAddress_StreetAndNumber;
                    report.lblPlace.Text = companyItem.HomeAddress_PlaceName;
                }


                report.lblPayedTillNow.Text = payedTillNow.ToString();
                report.lblForPayment.Text = (item.RetailValue - payedTillNow).ToString();
            }

            report.CreateDocument();

            System.Web.HttpContext.Current.Session["Report"] = report;
            System.Web.HttpContext.Current.Session["invoiceId"] = id;
            JsonResult res = new JsonResult();
            res.Data = id;
            return res;
        }

        public ActionResult CreateDoc(FormCollection collection)
        {
            int FromDocumentId = Convert.ToInt32(collection["FromDocumentId"]);
            int Employeeid = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            string docType = collection["docType"];  

            int? id = 0;
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                switch (docType)
                {
                    case "genDispatch": // dispatch
                        var res = data.uspInvoice2Dispatch(FromDocumentId, Employeeid).SingleOrDefault();
                        id = Convert.ToInt32(res);
                        break;
                    case "genCopy":
                        res = data.uspInvoice2Invoice(FromDocumentId, Employeeid).SingleOrDefault();
                        id = Convert.ToInt32(res);
                        //id = 100;
                        break;
                }
            }
            JsonResult result = new JsonResult();
            result.Data = id;
            return result;

        }

        [HttpPost]
        public ActionResult MjestaComboPartial(int? Id, FormCollection collection)
        {
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "Invoice");
            ViewData.Add("height", 20);
            ViewData.Add("width", 232);
            ViewData.Model = Id;

            return PartialView();
        }

        [HttpPost]
        public ActionResult MjestaComboPartial2(int? Id, FormCollection collection)
        {
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "Invoice");
            ViewData.Add("height", 20);
            ViewData.Add("width", 232);
            ViewData.Model = Id;

            return PartialView();
        }

        [HttpPost]
        public ActionResult MjestaComboPartial3(int? Id, FormCollection collection)
        {
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "Invoice");
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

        public ActionResult AddDesc(FormCollection collection)
        {
            int ordinal = Convert.ToInt32(collection["id"]);
            string desc = collection["desc"];
            bool ret = false;



            cDocuments_Invoice invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];
            var item = invoice.Documents_ItemsCol.FirstOrDefault(p => p.Ordinal == ordinal);
            if (item != null)
            {
                item.ProductDescription = desc;
                ret = true;
            }

            JsonResult result = new JsonResult();
            result.Data = ret;
            return result;
        }

        public ActionResult GetDesc(FormCollection collection)
        {

            string ret = "";
            if (!string.IsNullOrEmpty(collection["id"]))
            {
                int ordinal = Convert.ToInt32(collection["id"]);

                cDocuments_Invoice invoice = (cDocuments_Invoice)System.Web.HttpContext.Current.Session["Invoice"];
                var item = invoice.Documents_ItemsCol.FirstOrDefault(p => p.Ordinal == ordinal);
                if (item != null)
                {
                    ret = item.ProductDescription;
                }

            }
            JsonResult result = new JsonResult();
            result.Data = ret;
            return result;


        }

        public ActionResult FindIndividualCustomer()
        {
            JsonResult result = new JsonResult();
            DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();
            var icustomer = data.MDSubjects_Subject.FirstOrDefault(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.IndividualCustomer == true);
            if(icustomer != null)
            {
                result.Data = icustomer.Id;
            }
            else
            {
                DalEf.MDSubjects_Subject item = new DalEf.MDSubjects_Subject() { Name = "IndividualCustomer", GUID = new Guid(), SubjectType = 0, IndividualCustomer = true, CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId, EmployeeWhoLastChanedItId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId, LastActivityDate = DateTime.Now };
                    data.AddToMDSubjects_Subject(item);
                    data.SaveChanges();

                   int id = item.Id;
                   result.Data = id;
            
            }
            return result;
        }

   

        public ActionResult GetStatus(FormCollection collection)
        {
            int id = Convert.ToInt32(collection["id"]);
            bool ret = false;
            int result = 0;
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                var status = data.Documents_Document.SingleOrDefault(p => p.Id == id);
                if (status.Status != null)
                    result = Convert.ToInt32(status.Status);
            }
            ret = true;
            Tuple<bool, Int32> it = new Tuple<bool, int>(ret, result);
            JsonResult res = new JsonResult();
            res.Data = it;
            return res;

        }

        public ActionResult SetStatus(FormCollection collection)
        {
            int id = Convert.ToInt32(collection["id"]);
            int statusId = Convert.ToInt32(collection["statusId"]);
            bool ret = false;

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                data.uspUpdateStatus(id, statusId);
            }

            ret = true;
            //Tuple<bool> it = new Tuple<bool>(ret);
            JsonResult res = new JsonResult();
            res.Data = ret;
            return res;
        }

        public ActionResult CashBoxChanged(string cashbox)
        {
            System.Web.HttpContext.Current.Session["CashBoxCodes"] = cashbox;
            JsonResult result = new JsonResult();
            result.Data = true;
            return result;
        }
    }
}


     