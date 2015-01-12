using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using BusinessObjects.Security;
using AlphaWebCommodityBookkeeping.Areas.Documents.Reports;
using System.IO;
using System.Net.Mail;


namespace AlphaWebCommodityBookkeeping.Controllers
{
    public class MobileController : Controller
    {
        //
        // GET: /Mobile/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MobileApp()
        {
            return View();

        }
        public ActionResult SendMail(FormCollection collection)
        {
            bool res = false;
            string email = collection["email"];
            bool wo;
            bool off;
            

            if (!string.IsNullOrEmpty(email))
            {
                string SmtpFrom = System.Configuration.ConfigurationManager.AppSettings["SmtpFrom"];
                string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
                string SmtpAuth = System.Configuration.ConfigurationManager.AppSettings["SmtpAuth"];
                string SmtpUname = System.Configuration.ConfigurationManager.AppSettings["SmtpUname"];
                string SmtpPass = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"];

                string subject = "Preuzimanje mobilnih aplikacija";
                //string link1 = "<a href=\"http://bi.alphascore.hr/dl/AlphaOffers.apk\">Ponude</a>";
                //string link2 = "<a href=\"http://bi.alphascore.hr/dl/AlphaWorkOrders.apk\">Radni nalozi</a>";
                string body = "Aplikacije se nalaze u privitku. <br><br>Kliknite na pojedinu aplikaciju kako bi je instalirali";


                try
                {
                    MailMessage messge = new MailMessage(SmtpFrom, email, subject, body);
                    messge.IsBodyHtml = true;
                    SmtpClient cl = new SmtpClient(SmtpServer);
                    if (Convert.ToBoolean(SmtpAuth))
                    {
                        System.Net.NetworkCredential creds = new System.Net.NetworkCredential(SmtpUname, SmtpPass);
                        cl.Credentials = creds;
                    }

                    string file = "";
                    if (bool.TryParse(collection["workOrders"], out wo))
                    {
                        if (wo)
                        {
                            file = AppDomain.CurrentDomain.BaseDirectory + "\\Downloads\\Mobile\\AlphaWorkOrders.apk";
                            Attachment attach = new Attachment(file);
                            messge.Attachments.Add(attach);
                        }
                    }
                    if (bool.TryParse(collection["offers"], out off))
                    {
                        if (off)
                        {
                            file = AppDomain.CurrentDomain.BaseDirectory + "\\Downloads\\Mobile\\AlphaOffers.apk";
                            Attachment attach = new Attachment(file);
                            messge.Attachments.Add(attach);
                        }
                    }
                    cl.Send(messge);
                    res = true;
                }
                catch (Exception ex)
                {
                    ViewData["Exception"] = ex.Message;
                }
            }
            JsonResult result = new JsonResult();
            result.Data = res;
            return result;
        }

   
        public ActionResult WorkorderList(FormCollection collection)
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.DocumentsEntities data = new DalEf.DocumentsEntities();

            var list = data.vWorkOrder.Where(p => p.CompanyUsingServiceId == companyId);

            var listNames = (from p in list
                             select new { p.Id, p.Klijent, p.DocumentDateTime, p.VerifierName, p.VerifierPhone, p.CusomerId, p.PlaceId, p.Distance, p.Mjesto, p.IsBillable }).ToList();

            List<Tuple<string, string, string, string, string, string, string, Tuple<string, string, string>>> lista = new List<Tuple<string,string, string, string, string, string, string, Tuple<string, string, string>>>();

            foreach (var item in listNames)
            {
                Tuple<string, string, string, string, string, string, string, Tuple<string, string, string>> newItem = new Tuple<string, string, string, string, string, string, string, Tuple<string, string, string>>(item.Id.ToString(), item.Klijent, Convert.ToDateTime(item.DocumentDateTime).ToString("dd.MM.yyyy"), item.VerifierName.ToString(), item.VerifierPhone.ToString(), item.CusomerId.ToString(), item.Distance.ToString(), new Tuple<string, string, string>(item.PlaceId.ToString(), item.Mjesto, item.IsBillable.ToString()));
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;



        }

        public ActionResult CreateEditWorkorder(FormCollection collection)
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            string docNum = "";
            int num = 0;
            int year = 0;
            bool ok = false;
            int Id = Convert.ToInt32(collection["Id"]);
            int PlaceId = 0;
            if(collection["placeId"] != null)
                PlaceId = Convert.ToInt32(collection["placeId"]);

            decimal? Distance = null;
            if (collection["distance"] != null && (Convert.ToString(collection["distance"])).Trim() != "")
                Distance = Convert.ToDecimal(collection["distance"]);

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                try
                {
                    DalEf.Documents_WorkOrder newWorkOrder = null;
                    if (Id > 0)
                    {
                        newWorkOrder = data.Documents_Document.OfType<DalEf.Documents_WorkOrder>().SingleOrDefault(p => p.Id == Id);
                    }
                    else
                    {
                        //var lastItem = data.vDocuments.OrderByDescending(p => p.Id).FirstOrDefault(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.DocumentType == (short)BusinessObjects.Common.DocumentType.WorkOrder);
                        //if (lastItem != null)
                        //{
                        //    docNum = lastItem.UniqueIdentifier;
                        //    docNum = docNum.Substring(0, docNum.LastIndexOf("/"));
                        //    num = Convert.ToInt32(docNum);
                        //    num += 1;
                        //    docNum = num.ToString() + "/11";
                        //}
                        //else
                        //{
                        //    docNum = "1/11";
                        //    num = 1;
                        //}
                        
                        

                        newWorkOrder = new DalEf.Documents_WorkOrder();
                        newWorkOrder.CompanyUsingServiceId = companyId;
                        newWorkOrder.GUID = System.Guid.NewGuid();
                        newWorkOrder.DocumentType = (byte)BusinessObjects.Common.DocumentType.WorkOrder;
                        
                        newWorkOrder.CreationDateTime = System.DateTime.Now;
                        newWorkOrder.UniqueIdentifier = docNum;
                        newWorkOrder.OrdinalNumber = num;
                        newWorkOrder.OrdinalNumberInYear = 0; //TODO promjeniti kada se odrerdi način generiranja brojeva dokumenata!
                        newWorkOrder.MDSubjects_Employee_AuthorId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                        newWorkOrder.Description = "";
                        newWorkOrder.Inactive = false;
                    }

                    if (newWorkOrder != null)
                    {
                        newWorkOrder.DocumentDateTime = Convert.ToDateTime(collection["documentDate"]);
                        newWorkOrder.LastActivityDate = System.DateTime.Now;
                        newWorkOrder.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;

                        newWorkOrder.MDSubjects_SubjectId = Convert.ToInt32(collection["subjectId"]);
                        newWorkOrder.PlaceId = PlaceId;
                        newWorkOrder.VerifierName = Convert.ToString(collection["verifierName"]);
                        newWorkOrder.VerifierPhone = Convert.ToString(collection["verifierPhone"]);
                        newWorkOrder.IsBillable= Convert.ToBoolean(collection["bill"]);
                        newWorkOrder.Distance = Distance;
                        if (newWorkOrder.VerifierName.Trim() == "")
                        {
                            newWorkOrder.IsVerified = false;
                        }
                        else
                        {
                            newWorkOrder.IsVerified = true;
                        }

                        if (Id == 0)
                        data.AddToDocuments_Document(newWorkOrder);

                        data.SaveChanges();

                        Id = newWorkOrder.Id;

                        ok = true;
                    }
                }
                catch
                {

                    ok = false;
                }
            }
            Tuple<string, string> rezItem = new Tuple<string, string>(ok.ToString(), Id.ToString());
            JsonResult rez= new JsonResult();
            rez.Data = rezItem;

            return rez;
        }

        public ActionResult GetUnitsList()
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities();

            var listUnits = data.MDEntities_Enums_Unit.Where(p => p.CompanyUsingServiceId == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();

            foreach (var item in listUnits)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.Name);
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;

        }

        public ActionResult GetEmployeesList(FormCollection collection)
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();

            var listUnits = data.MDSubjects_Subject.OfType<DalEf.MDSubjects_Employee>().Where(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();

            foreach (var item in listUnits)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.LastName + " " + item.FirstName);
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;

        }

        public ActionResult GetSubjectsList(FormCollection collection)
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();

            var listUnits = data.vCustomers.Where(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();

            foreach (var item in listUnits)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.Name);
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;

        }

        public ActionResult GetProductList(FormCollection collection)
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            int index = Convert.ToInt16(collection["index"]);
            string searchText = collection["search"];

            DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities();

            var listUnits = data.MDEntities_Entity.OfType<DalEf.MDEntities_Product>().Where(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.Name.Contains(searchText)).OrderBy(p => p.Name).Skip(index).Take(10);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();

            foreach (var item in listUnits)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.Name);
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;



        }

        public ActionResult GetServiceList(FormCollection collection)
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            int index = Convert.ToInt16(collection["index"]);
            string searchText = collection["search"];

            DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities();

            var listUnits = data.MDEntities_Entity.OfType<DalEf.MDEntities_Service>().Where(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.Name.Contains(searchText)).OrderBy(p => p.Name).Skip(index).Take(10);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();

            foreach (var item in listUnits)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.Name);
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;
        }

        public ActionResult GetPlaces(FormCollection collection)
        {
            int index = Convert.ToInt16(collection["index"]);
            string searchText = collection["search"];

            DalEf.MDPlacesEntities data = new DalEf.MDPlacesEntities();
            var dataPlaces = data.MDPlaces_Enums_Geo_Place.Where(p => p.Name.Contains(searchText)).OrderBy(p => p.Name).Skip(index).Take(10);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();

            foreach (var item in dataPlaces)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.Name);
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;
        }


        public ActionResult GetMatrialList(FormCollection collection)
        {

            DalEf.DocumentsEntities data = new DalEf.DocumentsEntities();
            int Id = Convert.ToInt32(collection["Id"]);
            var matrialList = data.vWorkOrderMaterial.Where(p => p.Documents_WorkOrderId == Id).ToList();



            List<Tuple<string, string, string, string, string, string>> lista = new List<Tuple<string, string, string, string, string, string>>();

            foreach (var item in matrialList)
            {
                Tuple<string, string, string, string, string, string> newItem = new Tuple<string, string, string, string, string, string>(item.Id.ToString(), item.ProductId.ToString(), item.Materijal, item.JM, (item.Kolicina ?? 0).ToString("n2"), item.Faktuirati);
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;

        }

        public ActionResult GetMatrial(FormCollection collection)
        {

            DalEf.ProjectsEntities data = new DalEf.ProjectsEntities();
            int Id = Convert.ToInt32(collection["Id"]);
            var material = data.Projects_MaterialTrackingLog.SingleOrDefault(p => p.Id == Id);

            JsonResult rez = new JsonResult();
            if (material != null)
            {
                string[] item = { material.ProductId.ToString(), material.ProductUnitId.ToString(), material.ProductAmmount.ToString(), material.IsBillable.ToString() };

                rez.Data = item;
            }
           

            return rez;

        }

        public ActionResult GetTime(FormCollection collection)
        {

            DalEf.ProjectsEntities data = new DalEf.ProjectsEntities();
            int Id = Convert.ToInt32(collection["Id"]);
            var time = data.Projects_TimeTrackingLog.SingleOrDefault(p => p.Id == Id);

            JsonResult rez = new JsonResult();
            if (time != null)
            {
                string[] item = { time.MDEntities_ServiceId.ToString(), time.MDSubjects_SubjectTeamMemberId.ToString(), time.ActivityDate.ToString("d"), time.Hours.ToString(), time.Quantity.ToString(), time.IsBillable.ToString() };

                rez.Data = item;
            }


            return rez;

        }

   
        public ActionResult CreateEditMaterial(FormCollection collection)
        {
            bool ok = false;
            int count = 0;
            int Id = Convert.ToInt32(collection["Id"]);
            int wordOrderId = Convert.ToInt32(collection["workorderId"]);
            try
            {
                if (Id == 0)
                {
                    using (DalEf.DocumentsEntities dataDoc = new DalEf.DocumentsEntities())
                    {
                        count = dataDoc.vWorkOrderMaterial.Where(p => p.Documents_WorkOrderId == wordOrderId).Count();
                    }
                }
                using (DalEf.ProjectsEntities data = new DalEf.ProjectsEntities())
                {

                  
                    DalEf.Projects_MaterialTrackingLog newMaterial = null;
                    if (Id > 0)
                    {
                        newMaterial = data.Projects_MaterialTrackingLog.SingleOrDefault(p => p.Id == Id);

                    }
                    else
                    {
                        newMaterial = new DalEf.Projects_MaterialTrackingLog();
                        newMaterial.OrdinalInWorkOrder = count + 1;
                    }

                    if (newMaterial != null)
                    {
                        ok = true;

                       
                        int productId = Convert.ToInt32(collection["productId"]);
                        int pUnitId = Convert.ToInt32(collection["productUnitId"]);
                        decimal ammount = Convert.ToDecimal(collection["productAmmount"]);
                        bool isBillable = Convert.ToBoolean(collection["isBillable"]);


                        newMaterial.Documents_WorkOrderId = wordOrderId;
                        newMaterial.ProductId = productId;
                        newMaterial.ProductUnitId = pUnitId;
                        newMaterial.ProductAmmount = ammount;
                        newMaterial.IsBillable = isBillable;
                      

                        newMaterial.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                        newMaterial.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                        newMaterial.LastActivityDate = System.DateTime.Now;
                    }

                    if (Id == 0)
                        data.AddToProjects_MaterialTrackingLog(newMaterial);

                    data.SaveChanges();
                }
            }
            catch
            {

                ok = false;
            }


            JsonResult rez = new JsonResult();
            rez.Data = ok;

            return rez;

        }

        public ActionResult DeleteMaterial(FormCollection collection)
        {
            int count = 0;
            int workorderId = 0;
            int Id = Convert.ToInt32(collection["Id"]);
            bool ok = false;
            try
            {
                using (DalEf.ProjectsEntities data = new DalEf.ProjectsEntities())
                {
                    DalEf.Projects_MaterialTrackingLog newMaterial = null;
                    if (Id > 0)
                    {
                        newMaterial = data.Projects_MaterialTrackingLog.SingleOrDefault(p => p.Id == Id);
                        workorderId = (newMaterial.Documents_WorkOrderId ?? 0);
                    }
                    if (newMaterial != null)
                    {
                        ok = true;
                        data.DeleteObject(newMaterial);
                        data.SaveChanges();
                        var newMaterialList = data.Projects_MaterialTrackingLog.OrderBy(p => p.OrdinalInWorkOrder).Where(p => p.Documents_WorkOrderId == workorderId);
                        foreach (var item in newMaterialList)
                        {
                            count += 1;
                            item.OrdinalInWorkOrder = count;
                        }
                        data.SaveChanges();
                    }
                }
              
            }
            catch
            {

                ok = false;
            }


            JsonResult rez = new JsonResult();
            rez.Data = ok;

            return rez;

        }


        

        public ActionResult GetTimeList(FormCollection collection)
        {
            int companyId = 39;

            DalEf.DocumentsEntities data = new DalEf.DocumentsEntities();
            int Id = Convert.ToInt32(collection["Id"]);
            var matrialList = data.vWorkOrderTime.Where(p => p.Work_OrderId == Id).ToList();



            List<Tuple<string, string, string, string, string>> lista = new List<Tuple<string, string, string, string, string>>();

            foreach (var item in matrialList)
            {
                Tuple<string, string, string, string, string> newItem = new Tuple<string, string, string, string, string>(item.Id.ToString(), item.OrdinalInWorkOrder.ToString(), item.Rad, item.Izvrsitelj, item.MDEntities_ServiceId.ToString());
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;

        }

        public ActionResult CreateEditTime(FormCollection collection)
        {
            bool ok = false;
            int count = 0;
            int Id = Convert.ToInt32(collection["Id"]);
            int workorderId = Convert.ToInt32(collection["workorderId"]);
            try
            {
                if (Id == 0)
                {
                    using (DalEf.DocumentsEntities dataDoc = new DalEf.DocumentsEntities())
                    {
                        count = dataDoc.vWorkOrderTime.Where(p => p.Work_OrderId == workorderId).Count();
                    }
                }
                using (DalEf.ProjectsEntities data = new DalEf.ProjectsEntities())
                {

                    DalEf.Projects_TimeTrackingLog newTime = null;
                    if (Id > 0)
                    {
                        newTime = data.Projects_TimeTrackingLog.SingleOrDefault(p => p.Id == Id);

                    }
                    else
                    {
                        newTime = new DalEf.Projects_TimeTrackingLog();
                        newTime.Description = "";
                        newTime.OrdinalInWorkOrder = count + 1;
                    }

                    if (newTime != null)
                    {
                        ok = true;

                        newTime.Work_OrderId = Convert.ToInt32(collection["workorderId"]);
                        newTime.MDEntities_ServiceId = Convert.ToInt32(collection["serviceId"]);
                        newTime.MDSubjects_SubjectTeamMemberId = Convert.ToInt32(collection["subjectTeamMemberId"]);
                        newTime.ActivityDate = Convert.ToDateTime(collection["activityDate"]);
                        newTime.Hours = Convert.ToDecimal(collection["hours"]);
                        newTime.Quantity = Convert.ToDecimal(collection["quantity"]);


                        newTime.IsBillable = Convert.ToBoolean(collection["isBillable"]);

                        newTime.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                        newTime.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                        newTime.LastActivityDate = System.DateTime.Now;
                    }

                    if (Id == 0)
                        data.AddToProjects_TimeTrackingLog(newTime);

                    data.SaveChanges();
                }
            }
            catch
            {

                ok = false;
            }


            JsonResult rez = new JsonResult();
            rez.Data = ok;

            return rez;

        }

   

        public ActionResult DeleteTime(FormCollection collection)
        {
            int count = 0;
            bool ok = false;
            int workorderId = 0;
            try
            {
                using (DalEf.ProjectsEntities data = new DalEf.ProjectsEntities())
                {
                    int Id = Convert.ToInt32(collection["Id"]);
                    DalEf.Projects_TimeTrackingLog newTime = null;
                    if (Id > 0)
                    {
                        newTime = data.Projects_TimeTrackingLog.SingleOrDefault(p => p.Id == Id);
                        workorderId = (newTime.Work_OrderId ?? 0);
                    }
                    if (newTime != null)
                    {
                        ok = true;
                        data.DeleteObject(newTime);
                        data.SaveChanges();
                        var newTimeList = data.Projects_TimeTrackingLog.OrderBy(p => p.OrdinalInWorkOrder).Where(p => p.Work_OrderId == workorderId);
                        foreach (var item in newTimeList)
                        {
                            count += 1;
                            item.OrdinalInWorkOrder = count;
                        }
                        data.SaveChanges();
                    }
                }

            }
            catch
            {
                ok = false;
            }


            JsonResult rez = new JsonResult();
            rez.Data = ok;

            return rez;

        }


        public ActionResult SendReportByEmail(FormCollection collection)
        {
            string ret = "";
            int id = Convert.ToInt32(collection["Id"]);
            string SendTo = collection["SendTo"];
            //string SendToCC = collection["SendToCC"];
            string Subject = collection["Subject"];
            string Body = collection["Body"];
            string ClientName = collection["ClientName"];

            string SmtpFrom = System.Configuration.ConfigurationManager.AppSettings["SmtpFrom"];
            string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
            string SmtpAuth = System.Configuration.ConfigurationManager.AppSettings["SmtpAuth"];
            string SmtpUname = System.Configuration.ConfigurationManager.AppSettings["SmtpUname"];
            string SmtpPass = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"];

            xrWorkOrder report = new xrWorkOrder();

            DalEf.DocumentsEntities data = new DalEf.DocumentsEntities();
            DalEf.vReportWorkOrder item = data.vReportWorkOrder.SingleOrDefault(p => p.Id == id);
            var itemMaterialDetails = data.vWorkOrderMaterial.OrderBy(p => p.Rbr).Where(p => p.Documents_WorkOrderId == id).ToList();

            //DalEf.vWorkOrderTime itemTime = data.vWorkOrderTime.SingleOrDefault(p => p.Id == id);
            var itemTimelDetails = data.vWorkOrderTime.OrderBy(p => p.OrdinalInWorkOrder).Where(p => p.Work_OrderId == id).ToList();

            report.bindingSource1.DataSource = item;
            report.BindingSource2.DataSource = itemMaterialDetails;
            ((xrWorkOrderTimeSub)report.xrSubreport1.ReportSource).BindingSourceTime.DataSource = itemTimelDetails;


         

            MemoryStream mem = new MemoryStream();
            report.ExportToPdf(mem);

            string FileName = String.Format("Radni nalog {0}.pdf", ClientName);

            try
            {
                MailMessage message = new MailMessage(SmtpFrom, SendTo, Subject, Body);

                mem.Seek(0, System.IO.SeekOrigin.Begin);
                Attachment attach = new Attachment(mem, FileName, "application/pdf");
                message.Attachments.Add(attach);

                //if (SendToCC != "null" && SendToCC != null)
                //{
                //    message.CC.Add(SendToCC);
                //}

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

        [AllowAnonymous]
        public FileStreamResult GeneratePdf(FormCollection collection)
        {
            //int id = Convert.ToInt32(collection["Id"]);
            
            int id = 0;
            string colId = Request.QueryString["Id"];
            if (!string.IsNullOrEmpty(colId))
                id = Convert.ToInt32(colId);
           
            xrWorkOrder report = new xrWorkOrder();

            DalEf.DocumentsEntities data = new DalEf.DocumentsEntities();
            DalEf.vReportWorkOrder item = data.vReportWorkOrder.SingleOrDefault(p => p.Id == id);
            var itemMaterialDetails = data.vWorkOrderMaterial.OrderBy(p => p.Rbr).Where(p => p.Documents_WorkOrderId == id).ToList();

            var itemTimelDetails = data.vWorkOrderTime.OrderBy(p => p.OrdinalInWorkOrder).Where(p => p.Work_OrderId == id).ToList();

            report.bindingSource1.DataSource = item;
            report.BindingSource2.DataSource = itemMaterialDetails;
            ((xrWorkOrderTimeSub)report.xrSubreport1.ReportSource).BindingSourceTime.DataSource = itemTimelDetails;

            MemoryStream mem = new MemoryStream();
            //report.ExportToPdf(mem);
            report.ExportToHtml(mem);

            mem.Seek(0, System.IO.SeekOrigin.Begin);
            
            //return File(mem, "application/pdf", "test.pdf");
            //return new FileStreamResult(mem, "application/pdf");
            return new FileStreamResult(mem, "text/html");
            
        }

        //public ActionResult MobileApp()
        //{
        //    return View();
        //}
    }
}
