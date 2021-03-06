﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AlphaWebCommodityBookkeeping.Models;
using BusinessObjects.MDSubjects;
using Csla.Security;
using Csla.Data;
using DalEf;
using System.Net.Mail;
using BusinessObjects.Security;
using System.Collections;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using System.Drawing;
using System.Web.UI;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Csla.Web.Mvc;
using BusinessObjects.Documents;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml.Linq;
using System.Net;


namespace AlphaWebCommodityBookkeeping.Controllers
{
    public class AccountController : Csla.Web.Mvc.Controller, IModelCreator
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        [AllowAnonymous]
        public ActionResult LogIn(FormCollection collection)
        {

            ViewData.Model = new LogOnModel();
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();
                        int companyId = ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                        var itemCompany = data.Auth_Company.FirstOrDefault(p => p.Id == companyId);
                        if (itemCompany != null)
                        {
                            int numberOfDays = itemCompany.NumbnerOfDays;
                            DateTime activationDate = itemCompany.ActivationDate;

                            DateTime dt = DateTime.Now;
                            DateTime Expirationdate = activationDate.AddDays(numberOfDays);
                            TimeSpan ts = Expirationdate.Subtract(dt);
                            int DaysRemaining = Convert.ToInt32(ts.Days);

                            if (DaysRemaining < 1)
                            {

                                return RedirectToAction("LogOffExpired");
                            }
                        }


                        int test = ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

                        using (DalEf.DocumentsEntities context = new DocumentsEntities())
                        {
                            var Ids = context.uspGetDocIds(test);
                            foreach (var id in Ids)
                            {
                                if (HttpRuntime.Cache[id.ToString()] != null)
                                {
                                    try
                                    {
                                        int lockedDocId = Convert.ToInt32(HttpRuntime.Cache[id.ToString()].ToString().Split(',')[0].Split('[')[1]);
                                        int lockedUserId = Convert.ToInt32(HttpRuntime.Cache[id.ToString()].ToString().Split(',')[1].Split(']')[0]);
                                        if (lockedUserId == ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId)
                                        {
                                            HttpRuntime.Cache.Remove(id.ToString());
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Korisničko ime i/ili lozinka nisu ispravni!");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult MobileLogIn(FormCollection collection)
        {
            string message = "";
            bool ok = false;
            string uname = collection["UserName"];
            string pass = collection["Password"];
            if (MembershipService.ValidateUser(collection["UserName"], collection["Password"]))
            {
                FormsService.SignIn(collection["UserName"], false);
                if (((PTIdentity)Csla.ApplicationContext.User.Identity).WorkOrder)
                {
                    ok = true;
                }
                else
                {
                    message = "Logirani korisnik nema ovlasti za rad na radnim nalozima!";
                }


            }
            else
            {
                message = "Korisničko ime i/ili lozinka nisu ispravni!";
            }



            string[] list = { ok.ToString(), message };
            JsonResult result = new JsonResult() { Data = list };
            return result;
        }
        // **************************************
        // URL: /Account/LogOff
        // **************************************

        [AllowAnonymous]
        public ActionResult SendUserAcc(FormCollection collection)
        {
            string Email = "";
            bool ok = false;
            if (collection["email"] != null && Convert.ToString(collection["email"]) != "")
            {
                Email = collection["email"];
                DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();
                var itemCompany = data.Auth_Company.FirstOrDefault(p => p.Email == Email);
                if (itemCompany != null)
                {
                    var itemSubject = data.MDSubjects_Subject.OfType<DalEf.MDSubjects_Employee>().FirstOrDefault(p => p.CompanyUsingServiceId == itemCompany.Id && p.IsAdmin == true);
                    if (itemSubject != null)
                    {
                        string userName = itemSubject.UserName;
                        string password = itemSubject.Password;
                        string companyName = itemCompany.Name;

                        string Subject = System.Configuration.ConfigurationManager.AppSettings["subjectPodaci"];

                        string SmtpFrom = System.Configuration.ConfigurationManager.AppSettings["SmtpFrom"];
                        string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
                        string SmtpAuth = System.Configuration.ConfigurationManager.AppSettings["SmtpAuth"];
                        string SmtpUname = System.Configuration.ConfigurationManager.AppSettings["SmtpUname"];
                        string SmtpPass = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"];

                        string body = "Podaci o korisničkom računu:\n\nKorisnicko ime:    " + userName + "\nLozinka:     " + password;

                        try
                        {
                            MailMessage message = new MailMessage(SmtpFrom, Email, Subject, body);

                            SmtpClient smtpClient = new SmtpClient(SmtpServer);
                            if (Convert.ToBoolean(SmtpAuth))
                            {
                                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(SmtpUname, SmtpPass);
                                smtpClient.Credentials = SMTPUserInfo;
                            }
                            smtpClient.Send(message);
                            ok = true;
                        }
                        catch (Exception ex)
                        {
                            //ret = ex.Message;
                        }
                    }
                }

            }
            JsonResult result = new JsonResult();
            result.Data = ok;
            return result;
        }


        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsService.SignOut();

            Session.Abandon();

            return RedirectToAction("LogIn", "Account");
        }

        public ActionResult LogOffExpired()
        {
            FormsService.SignOut();
            Session.Abandon();
            return View();
        }

        [AllowAnonymous]
        public ActionResult MobileLogOff()
        {
            FormsService.SignOut();

            Session.Abandon();

            JsonResult result = new JsonResult() { Data = true };
            return result;
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        [AllowAnonymous]
        public ActionResult Register()
        {

            //mod.HomeAddress_PlaceId = 6525;
            //MjestaComboProvider.MjestoId=6525;
            cAuth_Company obj;
            System.Web.HttpContext.Current.Session["Auth_Company"] = obj = cAuth_Company.NewAuth_Company();
            ViewData.Model = obj;
            return View();
        }

        public ActionResult CompanyInfo()
        {
            cAuth_Company obj;
            System.Web.HttpContext.Current.Session["Auth_Company"] = obj = cAuth_Company.GetAuth_Company(((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
            AlphaWebCommodityBookkeeping.Models.MjestaComboProvider.MjestoId = obj.HomeAddress_PlaceId;
            if (obj.FirstNumberDispatch == null)
                obj.FirstNumberDispatch = 1;
            if (obj.FirstNumberIncomingInvoice == null)
                obj.FirstNumberIncomingInvoice = 1;
            if (obj.FirstNumberInvoice == null)
                obj.FirstNumberInvoice = 1;
            if (obj.FirstNumberOffer == null)
                obj.FirstNumberOffer = 1;
            if (obj.FirstNumberOrderForm == null)
                obj.FirstNumberOrderForm = 1;
            if (obj.FirstNumberPayment == null)
                obj.FirstNumberPayment = 1;
            if (obj.FirstNumberPayoff == null)
                obj.FirstNumberPayoff = 1;
            if (obj.FirstNumberPriceList == null)
                obj.FirstNumberPriceList = 1;
            if (obj.FirstNumberQuote == null)
                obj.FirstNumberQuote = 1;
            if (obj.FirstNumberTakeoverOfGoods == null)
                obj.FirstNumberTakeoverOfGoods = 1;
            if (obj.FirstNumberTravelOrder == null)
                obj.FirstNumberTravelOrder = 1;
            if (obj.FirstNumberWorkOrder == null)
                obj.FirstNumberWorkOrder = 1;

            string message = Request.QueryString["message"];
            if (!string.IsNullOrEmpty(message))
                ViewData["activeTab"] = message;
            ViewData.Model = obj;
            return View();
        }

        [HttpPost]
        public ActionResult CompanyInfo(int id, [Bind(Exclude = "EntityKeyData")]cAuth_Company obj, FormCollection collection)
        {
            string activeTab = collection["activeTab"];


            LoadProperty(obj, cAuth_Company.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cAuth_Company.EntityKeyDataProperty, enKey);
            }
            if (obj.IsValid)
            {

                if (SaveObject<cAuth_Company>(obj, true))
                {
                    if (activeTab != "")
                    {
                        return RedirectToAction("CompanyInfo/" + id, new { message = activeTab });
                    }
                    else
                    {
                        return RedirectToAction("CompanyInfo");
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
                ViewData.Model = obj;
                return View();
            }

        }

        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cAuth_Company)))
                return (cAuth_Company)System.Web.HttpContext.Current.Session["Auth_Company"];
            else return Activator.CreateInstance(modelType);
        }

        public ActionResult CompanyInfoGridPartail()
        {
            return PartialView("CompanyInfoGridPartail");
        }

        [AllowAnonymous]
        public ActionResult MjestaComboPartial(int? Id, FormCollection collection)
        {
            //string bindProperty = collection["DXCallbackName"].Substring(1, collection["DXCallbackName"].Length - 1);
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "Account");
            ViewData.Add("height", 20);
            ViewData.Add("width", 232);
            ViewData.Model = Id;

            //var prin = Csla.
            return PartialView();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(cAuth_Company obj)
        {
            if (obj.IsValid)
            {
                obj.NumbnerOfDays = 30;
                if (SaveObject<cAuth_Company>(obj, false))
                {

                    return RedirectToAction("RegisterSuccess");
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

        [AllowAnonymous]
        public ActionResult RegisterSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult UvjetiKoristenja()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Cjeniknadogradnje()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Kontakt()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Copyright()
        {
            return View();
        }


        [AllowAnonymous]
        public JsonResult IsOibUnique(string oib)
        {
            ////return Json(result, JsonRequestBehavior.AllowGet);
            //return result;
            bool res = false;
            if (!string.IsNullOrEmpty(oib))
            {
                DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();
                var itemCompany = data.Auth_Company.FirstOrDefault(p => p.OIB == oib);
                if (itemCompany == null)
                    res = true;
            }

            JsonResult result = new JsonResult();
            result.Data = res;
            return result;
        }



        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData.Model = new ChangePasswordModel();
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }


        public FileResult GetImage()
        {
            cAuth_Company comp = (cAuth_Company)System.Web.HttpContext.Current.Session["Auth_Company"];
            byte[] img = comp.InvoiceLogo;

            return File(img, "pic");
        }


        #region CompanyInfoUpload



        public ActionResult Callbacks()
        {
            //return DemoView("Callbacks");
            return PartialView("UploadPartial");
        }
        public ActionResult CallbacksImageUpload()
        {
            UploadControlExtension.GetUploadedFiles("ucCallbacks", UploadControlDemosHelper.ValidationSettings, UploadControlDemosHelper.FileUploadComplete);
            return null;
        }


        //public override string Name { get { return "UploadControl"; } }

        //public ActionResult Index()
        //{
        //    return MultiFileUpload();
        //}


        public class UploadControlDemosHelper
        {
            public const string UploadDirectory = "/Content/";
            public const string ThumbnailFormat = "Thumbnail{0}{1}";


            public static readonly DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings
            {
                AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif" },
                MaxFileSize = 20971520
            };

            public static List<string> Files
            {
                get
                {
                    UploadControlFilesStorage storage = System.Web.HttpContext.Current.Session["Storage"] as UploadControlFilesStorage;
                    if (storage != null)
                        return storage.Files;
                    return new List<string>();
                }
            }
            public static int FileInputCount
            {
                get
                {
                    UploadControlFilesStorage storage = System.Web.HttpContext.Current.Session["Storage"] as UploadControlFilesStorage;
                    if (storage != null)
                        return storage.FileInputCount;
                    return 2;
                }
            }

            //public static void AddImagesToCollection(UploadedFile[] files)
            //{

            //    UploadControlFilesStorage storage = System.Web.HttpContext.Current.Session["Storage"] as UploadControlFilesStorage;
            //    if (storage != null)
            //    {
            //        for (int i = 0; i < files.Length; i++)
            //        {
            //            if (files[i].FileBytes.Length > 0 && files[i].IsValid)
            //            {
            //                if (!storage.Files.Contains(files[i].FileName))
            //                {
            //                    string filePath = UploadDirectory + string.Format(ThumbnailFormat, storage.Files.Count, Path.GetExtension(files[i].FileName));
            //                    files[i].SaveAs(System.Web.HttpContext.Current.Request.MapPath(filePath));
            //                    storage.Files.Add(files[i].FileName);
            //                }
            //            }
            //        }
            //        storage.FileInputCount = files.Length;
            //    }
            //}
            public static void ClearImageCollection()
            {
                UploadControlFilesStorage storage = System.Web.HttpContext.Current.Session["Storage"] as UploadControlFilesStorage;
                if (storage != null)
                    storage.Files.Clear();
            }



            public static void FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
            {
                if (e.UploadedFile.IsValid)
                {
                    cAuth_Company comp = (cAuth_Company)System.Web.HttpContext.Current.Session["Auth_Company"];
                    if (comp != null)
                    {
                        Stream _st;
                        Byte[] _bt;
                        _st = e.UploadedFile.FileContent;
                        _bt = new Byte[_st.Length];
                        _st.Read(_bt, 0, _bt.Length);
                        comp.InvoiceLogo = _bt;

                        System.Web.HttpContext.Current.Session["Auth_Company"] = comp;
                    }
                    //string resultFilePath = UploadDirectory + string.Format(ThumbnailFormat, "", Path.GetExtension(e.UploadedFile.FileName));
                    //using (Image original = Image.FromStream(e.UploadedFile.FileContent))
                    //using (Image thumbnail = PhotoUtils.Inscribe(original, 100))
                    //{

                    //    PhotoUtils.SaveToJpeg(thumbnail, System.Web.HttpContext.Current.Request.MapPath(resultFilePath));
                    //}
                    //IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                    //if (urlResolver != null)
                    //    e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath) + "?refresh=" + Guid.NewGuid().ToString();
                    //    //e.CallbackData = resultFilePath + "?refresh=" + Guid.NewGuid().ToString();
                }
            }
        }

        public class UploadControlFilesStorage
        {
            List<string> files;

            public UploadControlFilesStorage()
            {
                this.files = new List<string>();
                FileInputCount = 2;
            }

            public int FileInputCount { get; set; }
            public List<string> Files { get { return files; } }
        }



        //public static class PhotoUtils
        //{

        //    public static Image Inscribe(Image image, int size)
        //    {
        //        return Inscribe(image, size, size);
        //    }

        //    public static Image Inscribe(Image image, int width, int height)
        //    {
        //        Bitmap result = new Bitmap(width, height);
        //        using (Graphics graphics = Graphics.FromImage(result))
        //        {
        //            double factor = 1.0 * width / image.Width;
        //            if (image.Height * factor < height)
        //                factor = 1.0 * height / image.Height;
        //            Size size = new Size((int)(width / factor), (int)(height / factor));
        //            Point sourceLocation = new Point((image.Width - size.Width) / 2, (image.Height - size.Height) / 2);

        //            SmoothGraphics(graphics);
        //            graphics.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(sourceLocation, size), GraphicsUnit.Pixel);
        //        }
        //        return result;
        //    }

        //    static void SmoothGraphics(Graphics g)
        //    {
        //        g.SmoothingMode = SmoothingMode.AntiAlias;
        //        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //    }

        //    public static void SaveToJpeg(Image image, Stream output)
        //    {
        //        image.Save(output, ImageFormat.Jpeg);
        //    }

        //    public static void SaveToJpeg(Image image, string fileName)
        //    {
        //        image.Save(fileName, ImageFormat.Jpeg);
        //    }

        //}
        #endregion

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Kontakt(FormCollection collection)
        {
            string Body = collection["Body"];
            string therest = "";
            string companyName = "";
            string Email = "";
            if (!Request.IsAuthenticated)
            {
                Email = collection["emailAddr"];
                therest = "Sender nije korisnik servisa, email: " + Email;
            }
            else
            {
                DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();
                var itemSubject = data.Auth_Company.SingleOrDefault(p => p.Id == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                companyName = " - " + itemSubject.Name;
            }

            string ret = "err";
            string body = Body + "\n\n" + therest;
            string Subject = "AlphaOnline contact request " + companyName;

            string SmtpFrom = System.Configuration.ConfigurationManager.AppSettings["SmtpFrom"];
            string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
            string SmtpAuth = System.Configuration.ConfigurationManager.AppSettings["SmtpAuth"];
            string SmtpUname = System.Configuration.ConfigurationManager.AppSettings["SmtpUname"];
            string SmtpPass = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"];
            string SmtpTo = "dino@alphascore.hr";

            try
            {
                MailMessage message = new MailMessage(SmtpFrom, SmtpTo, Subject, body);

                SmtpClient smtpClient = new SmtpClient(SmtpServer);
                if (Convert.ToBoolean(SmtpAuth))
                {
                    System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(SmtpUname, SmtpPass);
                    smtpClient.Credentials = SMTPUserInfo;
                }
                smtpClient.Send(message);
                ret = "ok";
            }
            catch (Exception ex)
            {
                //ret = ex.Message;
            }
            ViewData["Ret"] = ret;
            return View("Kontakt");
        }


        public ActionResult AccessDenied()
        {
            return View();
        }



        [Authorize(Roles = "SuperAdmin")]
        public ActionResult SuperAdmin()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult SuperAdminGridPartial()
        {
            return PartialView("SuperAdminGridPartial");
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult UpdateAccount(int id, DalEf.Auth_Company formData, FormCollection collection)
        {

            using (DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities())
            {
                var comp = data.Auth_Company.SingleOrDefault(p => p.Id == id);
                comp.NumberOfUsers = formData.NumberOfUsers;
                comp.NumberOfUsersOrdered = formData.NumberOfUsersOrdered;
                comp.NumbnerOfDays = formData.NumbnerOfDays;
                comp.ActivationDate = formData.ActivationDate;

                data.SaveChanges();
            }

            return PartialView("SuperAdminGridPartial");
        }




        #region CertUpload

        public ActionResult CertUpload()
        {
            //return DemoView("Callbacks");
            return PartialView("UploadPartial");
        }
        public ActionResult CallbacksCertificateUpload(FormCollection col)
        {
            if (col["pass"] != "")
                System.Web.HttpContext.Current.Session["Certpass"] = col["pass"];
            UploadControlExtension.GetUploadedFiles("ucUploadCert", UploadControlCertHelper.ValidationSettingsCert, UploadControlCertHelper.FileUploadComplete);
            return null;
        }



        public class UploadControlCertHelper
        {
            public const string UploadDirectory = "/Content/";
            public const string ThumbnailFormat = "Cert{0}{1}";


            public static readonly DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettingsCert = new DevExpress.Web.ASPxUploadControl.ValidationSettings
            {
                AllowedFileExtensions = new string[] { ".pfx" },
                MaxFileSize = 20971520
            };

            public static List<string> Files
            {
                get
                {
                    UploadControlFilesStorage storage = System.Web.HttpContext.Current.Session["StorageCert"] as UploadControlFilesStorage;
                    if (storage != null)
                        return storage.Files;
                    return new List<string>();
                }
            }
            public static int FileInputCount
            {
                get
                {
                    UploadControlFilesStorage storage = System.Web.HttpContext.Current.Session["StorageCert"] as UploadControlFilesStorage;
                    if (storage != null)
                        return storage.FileInputCount;
                    return 2;
                }
            }




            public static void FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
            {
                if (e.UploadedFile.IsValid)
                {
                    using (DalEf.MDSubjectsEntities context = new MDSubjectsEntities())
                    {

                        var company = context.Auth_Company.SingleOrDefault(p => p.Id == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                        if (company != null && System.Web.HttpContext.Current.Session["Certpass"] != null)
                        {
                            string pass = System.Web.HttpContext.Current.Session["Certpass"].ToString();
                            Stream _st;
                            Byte[] _bt;
                            _st = e.UploadedFile.FileContent;
                            _bt = new Byte[_st.Length];
                            _st.Read(_bt, 0, _bt.Length);
                            company.Certificate = _bt;
                            company.Password = pass;
                            context.SaveChanges();
                        }

                        string resultFilePath = AppDomain.CurrentDomain.BaseDirectory + UploadDirectory + "uid-" + ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId.ToString() + "-" + e.UploadedFile.FileName;
                        e.UploadedFile.SaveAs(resultFilePath);
                        System.Web.HttpContext.Current.Session["CertFilepath"] = resultFilePath;
                    }
                }
            }
        }
        #endregion
    }
}

