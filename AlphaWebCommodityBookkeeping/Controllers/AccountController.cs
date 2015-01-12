using System;
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

    //[AllowAnonymous]
    //public ActionResult LogIn(FormCollection collection)
    //{
    //    string message = "";
    //    bool ok = false;
    //    if (MembershipService.ValidateUser(collection["UserName"], collection["Password"]))
    //    {
    //        FormsService.SignIn(collection["UserName"], false);
    //        ok = true;

    //    }
    //    else
    //    {
    //        message = "Korisničko ime i/ili lozinka nisu ispravni.";
    //    }



    //    string[] list = { ok.ToString(), message };
    //    JsonResult result = new JsonResult() { Data = list };
    //    return result;
    //}

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
                              //int lockedUserId = Convert.ToInt32(id.ToString().Split(',')[1].Split(']')[0]);
                              //int lockedUserId = Convert.ToInt32(HttpRuntime.Cache[id.ToString()].ToString().Split(',')[1].Split(']')[0]);
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


              //    var Ids = context.Documents_Document.Where(p => p.CompanyUsingServiceId == test);
              //    foreach (var id in Ids)
              //    {
              //        if (HttpRuntime.Cache[id.ToString()] != null)
              //        {
              //            int lockedUserId = Convert.ToInt32(id.ToString().Split(',')[1].Split(']')[0]);
              //            if (lockedUserId == ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId)
              //            {
              //                HttpRuntime.Cache.Remove(id.ToString());
              //            }
              //        }
              //    }
              }

              return RedirectToAction("Index", "Home");
              //if (Csla.ApplicationContext.User.IsInRole("Admin"))
              //{
              //    return RedirectToAction("Index", "Home");
              //}
              //else
              //{
              //    return RedirectToAction("Index", "KreditniZahtjev");
              //}


           
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
                message = "Korisničko ime i/ili lozinka nisu ispravni!" ;
            }
        


            string[] list = { ok.ToString(), message};
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

      //// clear authentication cookie
      //HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
      //cookie1.Expires = DateTime.Now.AddYears(-1);
      //Response.Cookies.Add(cookie1);

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

        //// clear authentication cookie
        //HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
        //cookie1.Expires = DateTime.Now.AddYears(-1);
        //Response.Cookies.Add(cookie1);

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

        //ViewData.Model = cAuth_Company.GetAuth_Company(((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
        //AlphaWebCommodityBookkeeping.Models.MjestaComboProvider.MjestoId = 
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
                    //int id = 0;
                    //admin.IsAdmin = true;
                    //admin.CompanyUsingServiceId = obj.Id;
                    //admin.UserName = "admin1"; //TODO generirati username
                    //admin.Password = "admin"; //TODO generirati password
                    //admin.FirstName = "admin";
                    //admin.LastName = "neko ime";
                    //admin.Name = "adfasf";

                    //if (admin.IsValid)
                    //{
                    //    try
                    //    {
                    //        cMDSubjects_Employee temp = admin.Clone();
                    //        admin = temp.Save();

                    //        return RedirectToAction("LogOn");
                    //    }
                    //    catch
                    //    {

                    //        ViewData.Model = obj;
                    //        return View();
                    //    }
                    //}
                    //else
                    //{
                    //    ViewData.Model = obj;
                    //    return View();
                    //}
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
        if(!string.IsNullOrEmpty(oib))
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
        //string SmtpTo = SmtpFrom;
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
       //int id = Convert.ToInt32(collection["Id"]);
       //int NumberOfUsers = Convert.ToInt32(collection["NumberOfUsers"]);
       //int NumbnerOfDays = Convert.ToInt32(collection["NumbnerOfDays"]);
       //DateTime ActivationDate = Convert.ToDateTime(collection["ActivationDate"]);

       //int usersOrdered;
       //int NumberOfUsersOrdered;
       //if (Int32.TryParse(collection["NumberOfUsersOrdered"], out usersOrdered))
       //{
       //    NumberOfUsersOrdered = usersOrdered;
       //}
       //else
       //{
       //    NumberOfUsersOrdered = 0;
       //}
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


   //public override string Name { get { return "UploadControl"; } }

   //public ActionResult Index()
   //{
   //    return MultiFileUpload();
   //}


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
               using(DalEf.MDSubjectsEntities context = new MDSubjectsEntities())
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

                    string resultFilePath = AppDomain.CurrentDomain.BaseDirectory + UploadDirectory + "uid-" + ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId.ToString() + "-" + e.UploadedFile.FileName ;
                    e.UploadedFile.SaveAs(resultFilePath);
                    System.Web.HttpContext.Current.Session["CertFilepath"] = resultFilePath;
               }

               //cMDEntities_Product product = (cMDEntities_Product)System.Web.HttpContext.Current.Session["Product"];
               //cMDEntities_Product_PicturesCol productPicsCol = (cMDEntities_Product_PicturesCol)System.Web.HttpContext.Current.Session["ProductPicsCol"];
               //cMDEntities_Product_Pictures prodPic = new cMDEntities_Product_Pictures();
               //if (product != null)
               //{
               //    Stream _st;
               //    Byte[] _bt;
               //    _st = e.UploadedFile.FileContent;
               //    _bt = new Byte[_st.Length];
               //    _st.Read(_bt, 0, _bt.Length);
               //    //comp.InvoiceLogo = _bt;
               //    //item.MarkChild();
               //    prodPic.MarkChild();
               //    prodPic.Picture = _bt;


               //    int maxId = 0;
               //    maxId = product.MDEntities_Product_PicturesCol.OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefault();
               //    prodPic.Id = maxId + 1;

               //    product.MDEntities_Product_PicturesCol.Add(prodPic);
               //    System.Web.HttpContext.Current.Session["Product"] = product;

               //}

               //string resultFilePath = AppDomain.CurrentDomain.BaseDirectory + UploadDirectory + e.UploadedFile.FileName + "-" + ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId.ToString() + "." + Path.GetExtension(e.UploadedFile.FileName);
              
              
           }
       }
   }


      #endregion

  

   public ActionResult FiskalizacijaTest()
   {
       string e = "";
       // http://stackoverflow.com/questions/1131425/send-a-file-via-http-post-with-c-sharp
       //To send the raw file only:
       // using(WebClient client = new WebClient()) {
       //     client.UploadFile(address, filePath);
       // }
       var a = System.Web.HttpContext.Current.Session["Certpass"];
       if (System.Web.HttpContext.Current.Session["Certpass"] == null || System.Web.HttpContext.Current.Session["CertFilepath"] == null)
       {
           Tuple<int, string, string> t = new Tuple<int, string, string>(-123, "Lozinka ili certifikan nije unesen", "");
           JsonResult rett = new JsonResult();
           rett.Data = t;
           return rett;
       }
       if (System.Web.HttpContext.Current.Session["blagajnaId"] == null || System.Web.HttpContext.Current.Session["BlagajnaLocationCode"] == null)
       {
           Tuple<int, string, string> t = new Tuple<int, string, string>(-123, "Molimo, odaberite blagajnu i lokaciju", "");
           JsonResult rett = new JsonResult();
           rett.Data = t;
           return rett;
       }

       //if(System.Web.HttpContext.Current.Session["blagajnaId"] == null)
       //    return false;
       int status = -99;
       string message = "";
       string httpRetCode = "";
       string xmlFilePathNotSigned = AppDomain.CurrentDomain.BaseDirectory + "Content\\Fiscal-" + ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId + ".xml";
       string certFilePath = System.Web.HttpContext.Current.Session["CertFilepath"].ToString();
       try
       {
           
           DalEf.DocumentsEntities context = new DocumentsEntities();
           var obj = context.Documents_Document.OfType<Documents_Invoice>().SingleOrDefault(p => p.Id == 879);
           string OIB = context.vAuth_Company.FirstOrDefault(p => p.Id == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId).OIB;

           int blagajnaId = Convert.ToInt32(System.Web.HttpContext.Current.Session["blagajnaId"]);
           DalEf.MDGeneralEntities contextGeneral = new MDGeneralEntities();
           string onu = contextGeneral.MDGeneral_Enums_CashBox.FirstOrDefault(p => p.Id == blagajnaId).Code;
           //string onu = "4";

           //bool ret = false;
           

           DalEf.MDEntitiesEntities contextEnt = new MDEntitiesEntities();

           DalEf.MDSubjectsEntities contextSub = new MDSubjectsEntities();

           string OibOper = contextSub.Auth_Company.SingleOrDefault(p => p.Id == ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId).OIB;
           string sadVrijeme = DateTime.Now.ToString("dd.MM.yyyyTHH:mm:ss");
           string izracunZasKodVrijeme = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
           string ZastKod = ZastitiniKodIzracun(obj, izracunZasKodVrijeme, OIB);
           string OznSlijed = contextSub.Auth_Company.SingleOrDefault(p => p.Id == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId).FiscalizationConsistenceCode;

           Guid guid = Guid.NewGuid();
           //XmlWriter xmlWriter = XmlWriter.Create(resultFilePath);
           FileStream fileStream = new FileStream(xmlFilePathNotSigned, FileMode.Create);
           StreamWriter sw = new StreamWriter(fileStream);
           XmlTextWriter xmlWriter = new XmlTextWriter(sw);

           XNamespace tns = "http://www.apis-it.hr/fin/2012/types/f73";
           XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
           XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
           XNamespace schemaLocation = "http://www.apis-it.hr/fin/2012/types/f73 ../schema/FiskalizacijaSchema.xsd";

           xmlWriter.Formatting = Formatting.Indented;
           xmlWriter.Indentation = 4;

           xmlWriter.WriteStartDocument();


           xmlWriter.WriteStartElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
           xmlWriter.WriteStartElement("soapenv", "Body", "http://schemas.xmlsoap.org/soap/envelope/");

           xmlWriter.WriteStartElement("tns", "RacunZahtjev", "http://www.apis-it.hr/fin/2012/types/f73");
           //xmlWriter.WriteStartElement("RacunZahtjev");
           xmlWriter.WriteAttributeString("Id", "RacunZahtjev");
           xmlWriter.WriteAttributeString("xmlns", "tns", null, "http://www.apis-it.hr/fin/2012/types/f73");
           xmlWriter.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
           xmlWriter.WriteAttributeString("xsi", "schemaLocation", null, "http://www.apis-it.hr/fin/2012/types/f73 ../schema/FiskalizacijaSchema.xsd");

           xmlWriter.WriteStartElement("tns", "Zaglavlje", "http://www.apis-it.hr/fin/2012/types/f73");
           xmlWriter.WriteElementString("tns", "IdPoruke", "http://www.apis-it.hr/fin/2012/types/f73", guid.ToString());
           xmlWriter.WriteElementString("tns", "DatumVrijeme", "http://www.apis-it.hr/fin/2012/types/f73", sadVrijeme);
           xmlWriter.WriteEndElement();

           xmlWriter.WriteStartElement("tns", "Racun", "http://www.apis-it.hr/fin/2012/types/f73");
           //xmlWriter.WriteElementString("Oib", OIB);
           xmlWriter.WriteElementString("tns", "Oib", "http://www.apis-it.hr/fin/2012/types/f73", OIB);
           xmlWriter.WriteElementString("tns", "USustPdv", "http://www.apis-it.hr/fin/2012/types/f73", "true");
           xmlWriter.WriteElementString("tns", "DatVrijeme", "http://www.apis-it.hr/fin/2012/types/f73", sadVrijeme);
           xmlWriter.WriteElementString("tns", "OznSlijed", "http://www.apis-it.hr/fin/2012/types/f73", "P");

           xmlWriter.WriteStartElement("tns", "BrRac", "http://www.apis-it.hr/fin/2012/types/f73");
           xmlWriter.WriteElementString("tns", "BrOznRac", "http://www.apis-it.hr/fin/2012/types/f73", "1");
           xmlWriter.WriteElementString("tns", "OznPosPr", "http://www.apis-it.hr/fin/2012/types/f73", System.Web.HttpContext.Current.Session["BlagajnaLocationCode"].ToString()); 
           xmlWriter.WriteElementString("tns", "OznNapUr", "http://www.apis-it.hr/fin/2012/types/f73", onu);
           //xmlWriter.WriteElementString("tns", "OznNapUr", "http://www.apis-it.hr/fin/2012/types/f73", "4");
           xmlWriter.WriteEndElement();

           xmlWriter.WriteStartElement("tns", "Pdv", "http://www.apis-it.hr/fin/2012/types/f73");
           decimal PorezStopa;
           decimal izos;
           decimal PorezIznos;
           foreach (var item in context.Documents_ItemsCol.Where(p => p.DocumentId == obj.Id))
           {
               PorezStopa = contextEnt.MDEntities_Enums_TaxRate.SingleOrDefault(p => p.Id == item.TaxRateId).Percentage; //.ToString().Replace(",", ".");
               PorezIznos = item.TaxAmmount;

               izos = obj.TaxValue;
               xmlWriter.WriteStartElement("tns", "Porez", "http://www.apis-it.hr/fin/2012/types/f73");
               xmlWriter.WriteElementString("tns", "Stopa", "http://www.apis-it.hr/fin/2012/types/f73", Math.Round(PorezStopa, 2).ToString().Replace(",", "."));
               xmlWriter.WriteElementString("tns", "Osnovica", "http://www.apis-it.hr/fin/2012/types/f73", "10.00");
               xmlWriter.WriteElementString("tns", "Iznos", "http://www.apis-it.hr/fin/2012/types/f73", Math.Round(PorezIznos, 2 ).ToString().Replace(",", "."));
               xmlWriter.WriteEndElement();

           }
           xmlWriter.WriteEndElement();

           xmlWriter.WriteElementString("tns", "IznosUkupno", "http://www.apis-it.hr/fin/2012/types/f73", Math.Round(obj.RetailValue, 2 ).ToString().Replace(",", "."));
           xmlWriter.WriteElementString("tns", "NacinPlac", "http://www.apis-it.hr/fin/2012/types/f73", "G");
           xmlWriter.WriteElementString("tns", "OibOper", "http://www.apis-it.hr/fin/2012/types/f73", OibOper);
           xmlWriter.WriteElementString("tns", "ZastKod", "http://www.apis-it.hr/fin/2012/types/f73", ZastKod);
           xmlWriter.WriteElementString("tns", "NakDost", "http://www.apis-it.hr/fin/2012/types/f73", "false");
           //xmlWriter.WriteElementString("ParagonBrRac", "http://www.apis-it.hr/fin/2012/types/f73", "Porez na luksuz");
           //xmlWriter.WriteElementString("SpecNamj", "http://www.apis-it.hr/fin/2012/types/f73", "Porez na luksuz");
           //xmlWriter.WriteEndElement();
           xmlWriter.WriteEndElement();
           xmlWriter.WriteEndElement();
           xmlWriter.WriteEndElement();
           xmlWriter.Close();
       }
       catch(Exception ex)
       {
           e = ex.Message;
       }

       if (e == "")
       {
           string sign = signXml(xmlFilePathNotSigned);
           if (sign == "")
           {
               var result = SendSignedXml(System.Web.HttpContext.Current.Session["xmlSigned"].ToString());
               status = result.Item1;
               message = result.Item2;
               httpRetCode = result.Item3;
           }
           else
           {
               message = sign;
           }
       }
       else
       {
           message = e;
       }
     
          
       // status: 0-ok, <0 - err
       // message: ako je ok, jir, ako nije, ex.message
       // httpRetCode: ret code description
       Tuple<int, string, string> tupl = new Tuple<int, string, string>(status, message, httpRetCode);
       JsonResult ret = new JsonResult();
       ret.Data = tupl;
       return ret;
   }

   // primjer iz Fiskalizacija - Tehnicka specifikacija za korisnike_v1.2
   //      \\razvoj\Shared\Fiskalizacija\Fiskalizacija - Tehnicka specifikacija za korisnike_v1.2
   public string ZastitiniKodIzracun(Documents_Invoice obj, string vrijeme, string OIB)
   {
       using (DalEf.MDSubjectsEntities context = new DalEf.MDSubjectsEntities())
       {
           int userId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
           var employee = context.MDSubjects_Subject.FirstOrDefault(p => p.Id == userId);
           if (employee != null)
           {
               int blagajnaId = Convert.ToInt32(System.Web.HttpContext.Current.Session["blagajnaId"]);
               var contextGeneral = new DalEf.MDGeneralEntities();

               //oznaka naplatnog uređaja
               string onu = contextGeneral.MDGeneral_Enums_CashBox.FirstOrDefault(p => p.Id == blagajnaId).Code; 

               // brojcana oznaka racuna
               string bor = "1";

               //oznaka poslovnog prostora
               string opp = System.Web.HttpContext.Current.Session["BlagajnaLocationCode"].ToString();  

               //ukupni iznos računa
               string uir = Math.Round(obj.RetailValue, 2).ToString().Replace(",", ".");

               string medjurez = "";
               medjurez = medjurez + OIB;
               medjurez = medjurez + vrijeme;
               medjurez = medjurez + bor;
               medjurez = medjurez + opp;
               medjurez = medjurez + onu;
               medjurez = medjurez + uir;
               byte[] potpisano = null;
               try
               {
                   X509Certificate2 certifikat = new X509Certificate2(System.Web.HttpContext.Current.Session["CertFilepath"].ToString(), System.Web.HttpContext.Current.Session["Certpass"].ToString());
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

   public string signXml(string xmlFilePath)
   {
       string ret = string.Empty;
       try
       {
           X509Certificate2 certificate = new X509Certificate2(System.Web.HttpContext.Current.Session["CertFilepath"].ToString(), System.Web.HttpContext.Current.Session["Certpass"].ToString());
           string X509Certificate = certificate.GetRawCertDataString();
           string X509IssuerName = certificate.GetIssuerName();
           byte[] X509SerialNumber = certificate.GetSerialNumber();

           RSACryptoServiceProvider rsaKey = (RSACryptoServiceProvider)certificate.PrivateKey;
           XmlDocument xmlDoc = new XmlDocument();

           // Load an XML file into the XmlDocument object.
           xmlDoc.PreserveWhitespace = true;
           xmlDoc.Load(xmlFilePath);
           SignedXml signedXml = new SignedXml(xmlDoc);
           signedXml.SigningKey = rsaKey;
           Reference reference = new Reference();
           reference.Uri = "#RacunZahtjev"; // ne potpisuje se cijeli dokument, vec samo RacunZahtjev element
           XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();

           signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;

           reference.AddTransform(env);
           signedXml.AddReference(reference);
           KeyInfo keyInfo = new KeyInfo();

           KeyInfoX509Data kdata = new KeyInfoX509Data(certificate);
           X509IssuerSerial xserial;

           xserial.IssuerName = certificate.IssuerName.Name;
           xserial.SerialNumber = certificate.SerialNumber;
           kdata.AddIssuerSerial(xserial.IssuerName, xserial.SerialNumber);
           keyInfo.AddClause(kdata);

           signedXml.KeyInfo = keyInfo;
           signedXml.ComputeSignature();
           XmlElement xmlDigitalSignature = signedXml.GetXml();

           XmlElement RacunZahtjev = signedXml.GetIdElement(xmlDoc, "RacunZahtjev");
           RacunZahtjev.AppendChild(xmlDigitalSignature);

           //xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
           string xmlFiscalSignedpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\FiscalSigned-" + ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId + ".xml";
           System.Web.HttpContext.Current.Session["xmlSigned"] = xmlFiscalSignedpath;
           xmlDoc.Save(xmlFiscalSignedpath);
       }
       catch (Exception ex)
       {
           ret = ex.Message;
       }
       return ret;
   }

   public Tuple<int, string,  string> SendSignedXml(string filePath)
   {
       // cuccess/fail, jir/empty, status.description

       // Za demo testiranje, koristi se self signed certifikat, ne provjeravaj ga
       ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
       string url = "https://cistest.apis-it.hr:8449/FiskalizacijaServiceTest";

       //Declare XMLResponse document
        XmlDocument XMLResponse = null;

        //Declare an HTTP-specific implementation of the WebRequest class.
        HttpWebRequest objHttpWebRequest;

        //Declare an HTTP-specific implementation of the WebResponse class
        HttpWebResponse objHttpWebResponse = null;

        //Declare a generic view of a sequence of bytes
        Stream objRequestStream = null;
        Stream objResponseStream = null;

        //Declare XMLReader
        XmlTextReader objXMLReader;

        //Creates an HttpWebRequest for the specified URL.
        objHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            //---------- Start HttpRequest 

            //Set HttpWebRequest properties
            byte[] bytes;
            //bytes = System.Text.Encoding.ASCII.GetBytes(filePath);
            bytes = System.IO.File.ReadAllBytes(filePath);
            objHttpWebRequest.Method = "POST";
            objHttpWebRequest.ContentLength = bytes.Length;
            objHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";
            objHttpWebRequest.Headers.Add("SOAPAction", "https://cistest.apis-it.hr:8449/FiskalizacijaServiceTest?wsdl");

            //Get Stream object 
            objRequestStream = objHttpWebRequest.GetRequestStream();

            //Writes a sequence of bytes to the current stream 
            objRequestStream.Write(bytes, 0, bytes.Length);

            //Close stream
            objRequestStream.Close();

            //---------- End HttpRequest

            //Sends the HttpWebRequest, and waits for a response.
            objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();

            //---------- Start HttpResponse
            if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                //Get response stream 
                objResponseStream = objHttpWebResponse.GetResponseStream();

                //Load response stream into XMLReader
                objXMLReader = new XmlTextReader(objResponseStream);

                //Declare XMLDocument
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(objXMLReader);

                //Set XMLResponse object returned from XMLReader
                XMLResponse = xmldoc;

                XmlNode odgovor = xmldoc.GetElementsByTagName("RacunOdgovor", "http://www.apis-it.hr/fin/2012/types/f73")[0];
                string jir = odgovor.ChildNodes[1].InnerText;

                //xmldoc.Save();
                //Close XMLReader
                objXMLReader.Close();

                Tuple<int, string, string> ret = new Tuple<int, string, string>(0, jir, string.Empty);
                return ret;
            }

            //Close HttpWebResponse
            objHttpWebResponse.Close();
            Tuple<int, string, string> r = new Tuple<int, string, string>(Convert.ToInt32(objHttpWebResponse.StatusCode), string.Empty, objHttpWebResponse.StatusDescription);
            return r;
        }
        catch (WebException ex)
        {
            // Ako uleti u catch, web servis je javio error, ali ipak salje i xml file u kojemu je opis greske
            string err = "";
            WebResponse ipakPristigliErrorXmlResponse = ((WebException)ex).Response;
            if (ipakPristigliErrorXmlResponse != null)
            {

                using (Stream noviResponseStream = ipakPristigliErrorXmlResponse.GetResponseStream())
                {
                    StreamReader responseReader = new StreamReader(noviResponseStream);
                    var OdgovorGreska = new XmlDocument();
                    OdgovorGreska.Load(responseReader);
                    XmlNode odgovor = OdgovorGreska.GetElementsByTagName("Greska", "http://www.apis-it.hr/fin/2012/types/f73")[0];
                    if(odgovor != null)
                        err = odgovor.ChildNodes[1].InnerText;
                    //OdgovorGreska.Save("C:\\Razvoj\\AlphaWebCommodityBookkeepingNEW\\AlphaWebCommodityBookkeeping\\AlphaWebCommodityBookkeeping\\Content\\Response.xml");
                }
            }
            string error = ex.Message;
            if (err != null)
                error = err;
            Tuple<int, string, string> r = new Tuple<int, string, string>(-1, error, string.Empty);
            return r;
        }
        catch (Exception ex)
        {
            Tuple<int, string, string> r = new Tuple<int, string, string>(-99, ex.Message, string.Empty);
            return r;
        }
        
       
//        string sPoruka = @"<soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" 
//            xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
//            xmlns:xsi=""http://www.w3.org/2001/XMLSchemainstance""><soap:Body><tns:EchoRequest 
//            xmlns:tns=""http://www.apis-it.hr/fin/2012/types/f73"" 
//            xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
//            xsi:schemaLocation=""http://www.apisit.hr/fin/2012/types/f73/FiskalizacijaSchema.xsd"">
//            poruka</tns:EchoRequest></soap:Body></soap:Envelope>";
//
//          echo request - za testiranje dostupnosti web servisa
        }
   }
}

