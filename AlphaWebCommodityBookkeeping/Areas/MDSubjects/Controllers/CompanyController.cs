using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDSubjects;
using BusinessObjects.Security;
using Csla.Web.Mvc;
using AlphaWebCommodityBookkeeping.Models;
using Csla.Data;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using System.Web.UI;
using System.Data.OleDb;
using System.Data;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.MDSubjects.Controllers
{
    public class CompanyController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDSubjects/Company/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ClientsGridPartial()
        {
            return PartialView("ClientsGridPartial");
        }

        public ActionResult AddClient(FormCollection collection)
        {
            string Name = collection["Name"];
            string OIB = collection["OIB"];
            string SubjectType = collection["SubjectType"];
            string MjestoId = collection["Mjesto"];
            string UlicaBroj = collection["UlicaBroj"];
            JsonResult result = new JsonResult();
            result.Data = -1;
            int Id = -1;
            if (MjestoId != "" && Name != "" && SubjectType != "" && UlicaBroj != "")
            {
                
                //using (DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities())
                //{
                //    var item = data.MDSubjects_Subject.OfType<DalEf.MDSubjects_Company>();
                //    DalEf.MDSubjects_Company company = new DalEf.MDSubjects_Company() { Name = Name, OIB = OIB, SubjectType = Convert.ToInt16(SubjectType) };
                //    data.AddToMDSubjects_Subject(company);
                //    data.SaveChanges();

                //    data.AddObject("MDSubjects_Company", company);
                //    data.SaveChanges();
                //}

                if (Convert.ToInt32(SubjectType) == (short)BusinessObjects.Common.SubjectType.Person)
                {

                    cMDSubjects_Person pp = new cMDSubjects_Person();
                    pp.Name = Name;
                    if (OIB != "")
                        pp.OIB = OIB;
                    pp.SubjectType = Convert.ToInt16(SubjectType);
                    pp.HomeAddress_PlaceId = Convert.ToInt32(MjestoId);
                    pp.HomeAddress_Street = UlicaBroj;
                    pp.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

                    cMDSubjects_Person temppp = pp.Clone();
                    pp = temppp.Save();

                    result.Data = pp.Id;
                }
                else
                {
                    cMDSubjects_Company c = new cMDSubjects_Company();
                    c.Name = Name;
                    if (OIB != "")
                        c.OIB = OIB;
                    c.HomeAddress_PlaceId = Convert.ToInt32(MjestoId);
                    c.HomeAddress_Street = UlicaBroj;
                    c.SubjectType = Convert.ToInt16(SubjectType);
                    c.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                    cMDSubjects_Company temp = c.Clone();
                    c = temp.Save();

                    result.Data = c.Id;
                }

            //    switch(Convert.ToInt32(SubjectType))
            //    {
            //        case((short)BusinessObjects.Common.SubjectType.Company):
            //            cMDSubjects_Company c = new cMDSubjects_Company();
            //            c.Name = Name;
            //            c.OIB = OIB;
            //            c.SubjectType = Convert.ToInt16(SubjectType);

            //            cMDSubjects_Company temp = c.Clone();
            //            c = temp.Save();

            //            result.Data = c.Id;
            //            break;

            //        case ((short)BusinessObjects.Common.SubjectType.Obrt):
            //            cMDSubjects_Obrt o = new cMDSubjects_Obrt();
            //            o.Name = Name;
            //            o.OIB = OIB;
            //            o.SubjectType = Convert.ToInt16(SubjectType);

            //            cMDSubjects_Obrt tempo = o.Clone();
            //            o = tempo.Save();

            //            result.Data = o.Id;
            //            break;

            //        case ((short)BusinessObjects.Common.SubjectType.SoleProprietor):
            //            cMDSubjects_SoleProprietor p = new cMDSubjects_SoleProprietor();
            //            p.Name = Name;
            //            p.OIB = OIB;
            //            p.SubjectType = Convert.ToInt16(SubjectType);

            //            cMDSubjects_SoleProprietor tempp = p.Clone();
            //            p = tempp.Save();

            //            result.Data = p.Id;
            //            break;

            //        case ((short)BusinessObjects.Common.SubjectType.Person):
            //            cMDSubjects_Person pp = new cMDSubjects_Person();
            //            pp.Name = Name;
            //            pp.OIB = OIB;
            //            pp.SubjectType = Convert.ToInt16(SubjectType);

            //            cMDSubjects_Person temppp = pp.Clone();
            //            pp = temppp.Save();

            //            result.Data = pp.Id;
            //            break;
            //}

            }
            return result;
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDSubjects/Company/Create

        public ActionResult CreateAndEdit(int id)
        {
            cMDSubjects_Company obj;
            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["Company"] = obj = cMDSubjects_Company.GetMDSubjects_Company(id);
                MjestaComboProvider.MjestoId = obj.HomeAddress_PlaceId;
                MjestaComboProvider2.MjestoId = obj.BillToAddress_PlaceId;
            }
            else
            {
                System.Web.HttpContext.Current.Session["Company"] = obj = cMDSubjects_Company.NewMDSubjects_Company();
            }
            ViewData.Model = obj;
            return View();
        } 

        //
        // POST: /MDSubjects/Company/Create

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDSubjects_Company obj, FormCollection collection)
        {
            string small = collection["Small"];
            LoadProperty(obj, cMDSubjects_Company.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDSubjects_Company.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.EmployeeWhoLastChanedItId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            if (obj.IsValid)
            {
                if (obj.Id > 0)
                {
               
                    if (SaveObject<cMDSubjects_Company>(obj, true))
                    {

                        System.Web.HttpContext.Current.Session["Company"] = null;
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
                    if (SaveObject<cMDSubjects_Company>(obj, false))
                    {
                        System.Web.HttpContext.Current.Session["Company"] = null;
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
            System.Web.HttpContext.Current.Session["Company"] = null;
            //return RedirectToAction("Index");
            return RedirectToAction("../Company/Index");
        }

        public ActionResult CompanyGridPartial()
        {
            return PartialView("CompanyGridPartial");
        }

        [HttpPost]
        public ActionResult MjestaComboPartial(int? Id, FormCollection collection)
        {

            //string bindProperty = collection["DXCallbackName"].Substring(1, collection["DXCallbackName"].Length - 1);
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "Company");
            ViewData.Add("height", 20);
            ViewData.Add("width", 232);
            ViewData.Model = Id;


            return PartialView();
        }

        [HttpPost]
        public ActionResult MjestaComboPartial2(int? Id, FormCollection collection)
        {

            //string bindProperty = collection["DXCallbackName"].Substring(1, collection["DXCallbackName"].Length - 1);
            ViewData.Add("cmbMjestaName", collection["DXCallbackName"]);
            ViewData.Add("controllerName", "Company");
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


        #region AccountsColGridPartial
        public ActionResult RefreshAccountsColGridPartial()
        {
            cMDSubjects_Company company;
            company = (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];

            
            ViewData["controllerName"] = "Company";
            return PartialView("AccountsColGridPartial", company.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult AddNewAccount(cMDSubjects_Subject_Accounts account)
        {
            cMDSubjects_Company company;
            company = (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];

            if (ModelState.IsValid)
            {
                try
                {
                    account.MarkChild();
                    company.MDSubjects_Subject_AccountsCol.Add(account);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "Company";
            return PartialView("AccountsColGridPartial", company.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult UpdateAccount(cMDSubjects_Subject_Accounts account)
        {
            cMDSubjects_Company company;
            company = (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];

            if (ModelState.IsValid)
            {
                try
                {
                    var target = company.MDSubjects_Subject_AccountsCol.SingleOrDefault(p => p.Id == account.Id);
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

            ViewData["controllerName"] = "Company";
            return PartialView("AccountsColGridPartial", company.MDSubjects_Subject_AccountsCol);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id)
        {
            cMDSubjects_Company company;
            company = (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];

            if (id > 0)
            {
                try
                {
                    var target = company.MDSubjects_Subject_AccountsCol.SingleOrDefault(p => p.Id == id);
                    company.MDSubjects_Subject_AccountsCol.Remove(target);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewData["controllerName"] = "Company";
            return PartialView("AccountsColGridPartial", company.MDSubjects_Subject_AccountsCol);
        }
        #endregion

        #region ContactsColGridPartial
        public ActionResult RefreshContactsColGridPartial()
        {
            cMDSubjects_Company company;
            company = (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];

            
            ViewData["controllerName"] = "Company";
            return PartialView("ContactsColGridPartial", company.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult AddNewContact(cMDSubjects_Subject_Contacts contact)
        {
            cMDSubjects_Company company;
            company = (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];

            if (ModelState.IsValid)
            {
                try
                {
                    contact.MarkChild();
                    company.MDSubjects_Subject_ContactsCol.Add(contact);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Molim ispravite greške!";

            ViewData["controllerName"] = "Company";
            return PartialView("ContactsColGridPartial", company.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult UpdateContact(cMDSubjects_Subject_Contacts contact)
        {
            cMDSubjects_Company company;
            company = (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];

            if (ModelState.IsValid)
            {
                try
                {
                    var target = company.MDSubjects_Subject_ContactsCol.SingleOrDefault(p => p.Id == contact.Id);
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

            ViewData["controllerName"] = "Company";
            return PartialView("ContactsColGridPartial", company.MDSubjects_Subject_ContactsCol);
        }

        [HttpPost]
        public ActionResult DeleteContact(int id)
        {
            cMDSubjects_Company company;
            company = (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];

            if (id > 0)
            {
                try
                {
                    var target = company.MDSubjects_Subject_ContactsCol.SingleOrDefault(p => p.Id == id);
                    company.MDSubjects_Subject_ContactsCol.Remove(target);

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            ViewData["controllerName"] = "Company";
            return PartialView("ContactsColGridPartial", company.MDSubjects_Subject_ContactsCol);
        }
        #endregion


        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDSubjects_Company)))
                return (cMDSubjects_Company)System.Web.HttpContext.Current.Session["Company"];
            else return Activator.CreateInstance(modelType);
        }

        //
        // GET: /MDSubjects/Company/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDSubjects/Company/Edit/5

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
        // GET: /MDSubjects/Company/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MDSubjects/Company/Delete/5

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

        public ActionResult CompanyUpload()
        {
            return View();
        }


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


   


        public class UploadControlDemosHelper
        {
            public const string UploadDirectory = "Content\\";
            public const string ThumbnailFormat = "Company{0}{1}{2}{3}";


            public static readonly DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings
            {
                AllowedFileExtensions = new string[] { ".xls", ".xlsx" },
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

            public static void AddImagesToCollection(UploadedFile[] files)
            {
                UploadControlFilesStorage storage = System.Web.HttpContext.Current.Session["Storage"] as UploadControlFilesStorage;
                if (storage != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].FileBytes.Length > 0 && files[i].IsValid)
                        {
                            if (!storage.Files.Contains(files[i].FileName))
                            {
                                string filePath = UploadDirectory + string.Format(ThumbnailFormat, storage.Files.Count, Path.GetExtension(files[i].FileName));
                                files[i].SaveAs(System.Web.HttpContext.Current.Request.MapPath(filePath));
                                storage.Files.Add(files[i].FileName);
                            }
                        }
                    }
                    storage.FileInputCount = files.Length;
                }
            }
      



            public static void FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
            {
                if (e.UploadedFile.IsValid)
                {
                    string resultFilePath = UploadDirectory + string.Format(ThumbnailFormat, ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId + "-", ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId, "", Path.GetExtension(e.UploadedFile.FileName));
                    //string resultFilePath = UploadDirectory + string.Format(ThumbnailFormat, "", Path.GetExtension(e.UploadedFile.FileName));
                    IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                    if (urlResolver != null)
                        //e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath);
                        //e.CallbackData = resultFilePath + "?refresh=" + Guid.NewGuid().ToString();
                        //e.CallbackData = resultFilePath;
                        e.UploadedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + resultFilePath);
                    e.CallbackData = AppDomain.CurrentDomain.BaseDirectory + resultFilePath;
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

        public ActionResult ImportCompanies(string link)
        {
            bool res = true;
            string resString = "";
            int rowNumbers = 0;
            int importedRowNumbers = 0;
            int i = 0;
            bool r = false;
            JsonResult result = new JsonResult();

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + link + ";Extended Properties=Excel 8.0");
            try
            {

                con.Open();
                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                DataSet myDataSet = new DataSet();

                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    res = false;
                    resString = " xml file nije ispravan!";
                    string[] resultData = { res.ToString(), resString };
                    result.Data = resultData;
                    return result;
                }

                else
                {

                    List<string> sheets = new List<string>();
                    foreach (var dataRow in dt.Rows)
                    {
                        sheets.Add(((DataRow)(dataRow))["TABLE_NAME"].ToString());
                    }

                    if (!sheets.Contains("Sheet1$"))
                    {
                        res = false;
                        resString = " ime tablice promjenjeno!";
                        string[] resultData = { res.ToString(), resString };
                        result.Data = resultData;
                        return result;
                    }


                }


                OleDbDataAdapter myCommand = new OleDbDataAdapter(" SELECT * FROM [Sheet1$]", con);
                myCommand.Fill(myDataSet);
                rowNumbers = myDataSet.Tables[0].Rows.Count;
                using (MDSubjectsEntities data = new MDSubjectsEntities())
                {
                    foreach (DataRow itemRow in myDataSet.Tables[0].Rows)
                    {

                        var newItem = new MDSubjects_Company();

                        if (itemRow["OIB"].ToString().Trim() != "")
                        {
                            string oib = itemRow["OIB"].ToString().Trim();
                            var testOib = data.MDSubjects_Subject.OfType<MDSubjects_Company>().FirstOrDefault(p => p.OIB == oib && p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                            if (testOib != null)
                            {
                                continue;
                            }
                            else
                            {
                                newItem.OIB = itemRow["OIB"].ToString().Trim();
                            }
                        }
                        else
                        {
                            continue;
                        }

                        if (itemRow["Naziv tvrtke"].ToString().Trim() != "")
                        {
                            newItem.Name = itemRow["Naziv tvrtke"].ToString().Trim();
                        }
                        else
                        {
                            continue;
                        }


                        if (itemRow["Poštanski broj"].ToString().Trim() != "")
                        {
                            int idMjesto = 0;
                            string zip = itemRow["Poštanski broj"].ToString().Trim();
                            var mjesto = data.MDPlaces_Enums_Geo_PlaceForSubjects.FirstOrDefault(p => p.ZIPCode == zip);
                            if (mjesto == null)
                            {
                                continue;
                            }
                            else
                            {
                                idMjesto = mjesto.Id;
                                if (idMjesto != 0)
                                    newItem.HomeAddress_PlaceId = idMjesto;
                                else
                                    continue;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        if (itemRow["Ulica i broj"].ToString().Trim() != "")
                        {
                            newItem.HomeAddress_Street = itemRow["Ulica i broj"].ToString().Trim();
                        }
                        else
                        {
                            continue;
                        }

                        if (itemRow["R1 ili R2"].ToString().Trim() != "")
                        {
                            if (itemRow["R1 ili R2"].ToString().Trim() == "R1")
                            {
                                newItem.PDVType = 0;
                            }
                            else if (itemRow["R1 ili R2"].ToString().Trim() == "R2")
                            {
                                newItem.PDVType = 1;
                            }
                            else
                            {
                                continue;
                            }
                                
                           
                        }
                        else
                        {
                            continue;
                        }

                        //if (itemRow["MBR"].ToString().Trim() != "")
                        //{
                        //    newItem.CRO_MBR = itemRow["MBR"].ToString().Trim();
                        //}


                        //r = int.TryParse(itemRow["Broj djelatnika"].ToString().Trim(), out i);
                        //if (r)
                        //{
                        //    newItem.NumberOfEmployees = i;
                        //}
                        //else
                        //{
                        //    newItem.NumberOfEmployees = null;
                        //}

                        newItem.GUID = Guid.NewGuid();
                        newItem.SubjectType = (short)BusinessObjects.Common.SubjectType.Company;
                        newItem.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                        newItem.EmployeeWhoLastChanedItId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                        newItem.IsCustomer = true;
                        newItem.Inactive = false;
                        newItem.LastActivityDate = DateTime.Now;
                        importedRowNumbers += 1;
                        data.AddToMDSubjects_Subject(newItem);

                    }

                    data.SaveChanges();
                }

                con.Close();

                resString = String.Format("Pronađeni broj klijenata: {0} broj klijenata dodan u bazu: {1}", rowNumbers, importedRowNumbers);
            }
            catch (Exception ex)
            {
                res = false;
                resString = ex.Message;
                //MessageBox.Show(ex.ToString());
                //Thread.Sleep(15000);
            }
            finally
            {
                con.Close();

            }

            string[] resultDataFinal = { res.ToString(), resString };
            result.Data = resultDataFinal;

            return result;

        }
    }
}
