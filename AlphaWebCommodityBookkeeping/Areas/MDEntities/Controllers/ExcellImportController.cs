using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.MDEntities;
using Csla.Web.Mvc;
using BusinessObjects.Security;
using Csla.Data;
using DevExpress.Web.Mvc;
using DevExpress.Web.ASPxUploadControl;
using System.Drawing;
using System.Web.UI;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using BusinessObjects.Projects;
using System.Data.OleDb;
using System.Data;
using DalEf;

namespace AlphaWebCommodityBookkeeping.Areas.MDEntities.Controllers
{
    public class ExcellImportController : Csla.Web.Mvc.Controller
    {
        //
        // GET: /MDEntities/ExcellImport/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Callbacks()
        {
            //return DemoView("Callbacks");
            return PartialView("UploadPartial");
        }
        public ActionResult CallbacksServices()
        {
            //return DemoView("Callbacks");
            return PartialView("UploadPartialServices");
        }
        public ActionResult CallbacksImageUpload()
        {
            UploadControlExtension.GetUploadedFiles("ucCallbacks", UploadControlDemosHelper.ValidationSettings, UploadControlDemosHelper.FileUploadComplete);
            return null;
        }

        public ActionResult CallbacksImageUploadServices()
        {
            UploadControlExtension.GetUploadedFiles("ucCallbacksServices", UploadControlDemosHelper.ValidationSettings, UploadControlDemosHelper.FileUploadComplete);
            return null;
        }
        


        public class UploadControlDemosHelper
        {
            public const string UploadDirectory = "Content\\";
            public const string ThumbnailFormat = "Thumbnail{0}{1}{2}{3}";


            public static readonly DevExpress.Web.ASPxUploadControl.ValidationSettings ValidationSettings = new DevExpress.Web.ASPxUploadControl.ValidationSettings
            {
                AllowedFileExtensions = new string[] { ".xlsx", ".xls" },
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





            public static void FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
            {
                if (e.UploadedFile.IsValid)
                {
                    string resultFilePath = UploadDirectory + string.Format(ThumbnailFormat, ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId + "-", ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId, "", Path.GetExtension(e.UploadedFile.FileName));
                    //string resultFilePath = UploadDirectory + string.Format(ThumbnailFormat, "", Path.GetExtension(e.UploadedFile.FileName));
                    //using (Image original = Image.FromStream(e.UploadedFile.FileContent))
                    //using (Image thumbnail = PhotoUtils.Inscribe(original, 100))
                    //{
                    //    PhotoUtils.SaveToJpeg(thumbnail, System.Web.HttpContext.Current.Request.MapPath(resultFilePath));
                    //}
                    IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                    if (urlResolver != null)
                        //e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath);
                        //e.CallbackData = resultFilePath + "?refresh=" + Guid.NewGuid().ToString();
                        //e.CallbackData = resultFilePath;
                        e.UploadedFile.SaveAs(AppDomain.CurrentDomain.BaseDirectory + resultFilePath);
                        e.CallbackData = AppDomain.CurrentDomain.BaseDirectory + resultFilePath;
                    string a = System.Web.HttpContext.Current.Request.MapPath(resultFilePath);
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

        public static void SaveToJpeg(Image image, Stream output)
        {
            image.Save(output, ImageFormat.Jpeg);
        }

        public static void SaveToJpeg(Image image, string fileName)
        {
            image.Save(fileName, ImageFormat.Jpeg);
            
        }


        //}



        public ActionResult ImportProducts(string link)
        {
            bool res = true;
            string resString = "";
            int rowNumbers = 0;
            int importedRowNumbers = 0;
            decimal i = 0;
            bool r = false;
            JsonResult result = new JsonResult();

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + link + ";Extended Properties=Excel 8.0");
            try
            {
                
                con.Open();
                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                DataSet myDataSet = new DataSet();

                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                List<Guid> addedGuids = new List<Guid>(); 

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
                using (MDEntitiesEntities data = new MDEntitiesEntities())
                {
                    foreach (DataRow itemRow in myDataSet.Tables[0].Rows)
                    {

                        var newItem = new MDEntities_Product();

                        if (itemRow["Šifra"].ToString().Trim() != "")
                        {
                            string sif = itemRow["Šifra"].ToString().Trim();
                            var testSifra = data.MDEntities_Entity.OfType<MDEntities_Product>().FirstOrDefault(p => p.Label == sif && p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                            if (testSifra != null)
                            {
                                continue;
                            }
                            else
                            {
                                newItem.Label = itemRow["Šifra"].ToString().Trim();
                            }
                        }
                        else
                        {
                            continue;
                        }

                        if (itemRow["Naziv"].ToString().Trim() != "")
                        {
                            newItem.Name = itemRow["Naziv"].ToString().Trim();
                        }
                        else
                        {
                            continue;
                        }

 
                        r = decimal.TryParse(itemRow["PDV"].ToString(), out i);
                        if (r)
                        {
                            decimal per = Convert.ToDecimal(itemRow["PDV"]);
                            var idData = data.MDEntities_Enums_TaxRate.FirstOrDefault(p => p.Percentage == per && p.IsService == false && ((p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId));
                            if (idData != null)
                            {
                                newItem.TaxRateId = idData.Id;
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

                        if (itemRow["JM"].ToString().Trim() != "")
                        {
                            string jm = itemRow["JM"].ToString().Trim().ToLower();
                            var idData = data.MDEntities_Enums_Unit.FirstOrDefault(p => p.Label.Trim().ToLower() == jm && ((p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId));
                            if (idData != null)
                            {
                                newItem.UnitId = idData.Id;
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


                        r = decimal.TryParse(itemRow["VP Cijena"].ToString(), out i);
                        if (r)
                        {
                            newItem.WholesalePrice = i;
                        }
                        else
                        {
                            newItem.WholesalePrice = 0;
                        }

                        newItem.GUID = Guid.NewGuid();
                        newItem.EntityType = (short)BusinessObjects.Common.EntityType.Product;
                        newItem.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                        newItem.EmployeeWhoLastChanedItUserId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                        newItem.Inactive = false;
                        newItem.LastActivityDate = DateTime.Now;
                        importedRowNumbers += 1;
                        addedGuids.Add(newItem.GUID);
                        data.AddToMDEntities_Entity(newItem);

                    }

                    data.SaveChanges();
                }


                if (addedGuids.Count > 0)
                {
                    using (MDEntitiesEntities data = new MDEntitiesEntities())
                    {
                        DalEf.Documents_PriceList_ForEntities dataList = data.Documents_Document_ForEntities.OfType<DalEf.Documents_PriceList_ForEntities>().FirstOrDefault(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.IsDefault == true);
                        if (dataList == null)
                        {
                            dataList = new DalEf.Documents_PriceList_ForEntities();
                            dataList.MDSubjects_SubjectId = null;
                            dataList.IsDefault = true;
                            dataList.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                            dataList.DateStart = System.DateTime.Now;
                            dataList.LastActivityDate = System.DateTime.Now;
                            dataList.MDSubjects_Employee_AuthorId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                            dataList.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                            dataList.UniqueIdentifier = "";
                            dataList.OrdinalNumber = 0;
                            dataList.OrdinalNumberInYear = 0;
                            dataList.Name = "Osnovna lista";
                            dataList.DocumentType = (short)BusinessObjects.Common.DocumentType.PriceList;
                            dataList.DocumentDateTime = System.DateTime.Now;
                            dataList.CreationDateTime = System.DateTime.Now;
                            dataList.GUID = Guid.NewGuid();

                            data.AddToDocuments_Document_ForEntities(dataList);
                        }
                        foreach (Guid guidNo in addedGuids)
                        {
                            DalEf.Documents_PriceList_ItemsCol_ForEntities dataItem = null;
                           
                            var entityItem = data.MDEntities_Entity.OfType<DalEf.MDEntities_Product>().FirstOrDefault(p => p.GUID == guidNo);
                         
 
                                if (entityItem!= null)
                                {
                                    dataItem = new DalEf.Documents_PriceList_ItemsCol_ForEntities();

                                    dataItem.MDEntities_EntityId = entityItem.Id;
                                    dataItem.WholesalePrice = entityItem.WholesalePrice;
                                    dataItem.CalcualtedWholesalePrice = entityItem.WholesalePrice;
                                    dataItem.Ordinal = dataList.Documents_PriceList_ItemsCol_ForEntities.Count + 1;

                                    dataList.Documents_PriceList_ItemsCol_ForEntities.Add(dataItem); 
                                }
                            
                        }

                        data.SaveChanges();
                    }
                }




                con.Close();

                resString = String.Format("pronađeni broj artikala: {0} broj artikala dodan u bazu: {1}", rowNumbers, importedRowNumbers);
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




        public ActionResult ImportServices(string link)
        {
            bool res = true;
            string resString = "";
            int rowNumbers = 0;
            int importedRowNumbers = 0;
            decimal i = 0;
            bool r = false;
            JsonResult result = new JsonResult();

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + link + ";Extended Properties=Excel 8.0");
            try
            {

                con.Open();
                //Create Dataset and fill with imformation from the Excel Spreadsheet for easier reference
                DataSet myDataSet = new DataSet();

                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                List<Guid> addedGuids = new List<Guid>();

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
                using (MDEntitiesEntities data = new MDEntitiesEntities())
                {
                    foreach (DataRow itemRow in myDataSet.Tables[0].Rows)
                    {

                        var newItem = new MDEntities_Service();

                        if (itemRow["Šifra"].ToString().Trim() != "")
                        {
                            string sif = itemRow["Šifra"].ToString().Trim();
                            var testSifra = data.MDEntities_Entity.OfType<MDEntities_Service>().FirstOrDefault(p => p.Label == sif && p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                            if (testSifra != null)
                            {
                                continue;
                            }
                            else
                            {
                                newItem.Label = itemRow["Šifra"].ToString().Trim();
                            }
                        }
                        else
                        {
                            continue;
                        }

                        if (itemRow["Naziv"].ToString().Trim() != "")
                        {
                            newItem.Name = itemRow["Naziv"].ToString().Trim();
                        }
                        else
                        {
                            continue;
                        }


                        r = decimal.TryParse(itemRow["PDV"].ToString(), out i);
                        if (r)
                        {
                            decimal per = Convert.ToDecimal(itemRow["PDV"]);
                            var idData = data.MDEntities_Enums_TaxRate.FirstOrDefault(p => p.Percentage == per && p.IsService == true && ((p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId));
                            if (idData != null)
                            {
                                newItem.TaxRateId = idData.Id;
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

                        if (itemRow["JM"].ToString().Trim() != "")
                        {
                            string jm = itemRow["JM"].ToString().Trim().ToLower();
                            var idData = data.MDEntities_Enums_Unit.FirstOrDefault(p => p.Label.Trim().ToLower() == jm && ((p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId));
                            if (idData != null)
                            {
                                newItem.UnitId = idData.Id;
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


                        r = decimal.TryParse(itemRow["VP Cijena"].ToString(), out i);
                        if (r)
                        {
                            newItem.WholesalePrice = i;
                        }
                        else
                        {
                            newItem.WholesalePrice = 0;
                        }

                        newItem.GUID = Guid.NewGuid();
                        newItem.EntityType = (short)BusinessObjects.Common.EntityType.Service;
                        newItem.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                        newItem.EmployeeWhoLastChanedItUserId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                        newItem.Inactive = false;
                        newItem.LastActivityDate = DateTime.Now;
                        importedRowNumbers += 1;
                        addedGuids.Add(newItem.GUID);
                        data.AddToMDEntities_Entity(newItem);

                    }

                    data.SaveChanges();
                }


                if (addedGuids.Count > 0)
                {
                    using (MDEntitiesEntities data = new MDEntitiesEntities())
                    {
                        DalEf.Documents_PriceList_ForEntities dataList = data.Documents_Document_ForEntities.OfType<DalEf.Documents_PriceList_ForEntities>().FirstOrDefault(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.IsDefault == true);
                        if (dataList == null)
                        {
                            dataList = new DalEf.Documents_PriceList_ForEntities();
                            dataList.MDSubjects_SubjectId = null;
                            dataList.IsDefault = true;
                            dataList.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                            dataList.DateStart = System.DateTime.Now;
                            dataList.LastActivityDate = System.DateTime.Now;
                            dataList.MDSubjects_Employee_AuthorId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                            dataList.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                            dataList.UniqueIdentifier = "";
                            dataList.OrdinalNumber = 0;
                            dataList.OrdinalNumberInYear = 0;
                            dataList.Name = "Osnovna lista";
                            dataList.DocumentType = (short)BusinessObjects.Common.DocumentType.PriceList;
                            dataList.DocumentDateTime = System.DateTime.Now;
                            dataList.CreationDateTime = System.DateTime.Now;
                            dataList.GUID = Guid.NewGuid();

                            data.AddToDocuments_Document_ForEntities(dataList);
                        }
                        foreach (Guid guidNo in addedGuids)
                        {
                            DalEf.Documents_PriceList_ItemsCol_ForEntities dataItem = null;

                            var entityItem = data.MDEntities_Entity.OfType<DalEf.MDEntities_Service>().FirstOrDefault(p => p.GUID == guidNo);


                            if (entityItem != null)
                            {
                                dataItem = new DalEf.Documents_PriceList_ItemsCol_ForEntities();

                                dataItem.MDEntities_EntityId = entityItem.Id;
                                dataItem.WholesalePrice = entityItem.WholesalePrice;
                                dataItem.CalcualtedWholesalePrice = entityItem.WholesalePrice;
                                dataItem.Ordinal = dataList.Documents_PriceList_ItemsCol_ForEntities.Count + 1;

                                dataList.Documents_PriceList_ItemsCol_ForEntities.Add(dataItem);
                            }

                        }

                        data.SaveChanges();
                    }
                }




                con.Close();

                resString = String.Format("pronađeni broj artikala: {0} broj artikala dodan u bazu: {1}", rowNumbers, importedRowNumbers);
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



        //
        // GET: /MDEntities/ExcellImport/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDEntities/ExcellImport/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /MDEntities/ExcellImport/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /MDEntities/ExcellImport/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MDEntities/ExcellImport/Edit/5

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
        // GET: /MDEntities/ExcellImport/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MDEntities/ExcellImport/Delete/5

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
    }
}
