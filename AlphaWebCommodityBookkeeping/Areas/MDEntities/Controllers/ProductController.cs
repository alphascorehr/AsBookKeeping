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

namespace AlphaWebCommodityBookkeeping.Areas.MDEntities.Controllers
{   
    public class ProductController : Csla.Web.Mvc.Controller, IModelCreator
    {
        //
        // GET: /MDEntities/Product/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MDEntities/Product/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MDEntities/Product/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MDEntities/Product/Create

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
        // GET: /MDEntities/Product/Edit/5

        public ActionResult CreateAndEdit(int id)
        {
            cMDEntities_Product obj;
            if (id > 0)
            {
                System.Web.HttpContext.Current.Session["Product"] = obj = cMDEntities_Product.GetMDEntities_Product(id);
            }
            else
            {
                System.Web.HttpContext.Current.Session["Product"] = obj = cMDEntities_Product.NewMDEntities_Product();
            }
            ViewData.Model = obj;
            return View();
        }

        [HttpPost]
        public ActionResult CreateAndEdit(int id, [Bind(Exclude = "EntityKeyData")]cMDEntities_Product obj, FormCollection collection)
        {
            string small = collection["Small"];
            LoadProperty(obj, cMDEntities_Product.IdProperty, id);
            if (collection["EntityKeyData"] != "")
            {
                byte[] enKey = Convert.FromBase64String(collection["EntityKeyData"]);
                LoadProperty(obj, cMDEntities_Product.EntityKeyDataProperty, enKey);
            }

            obj.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            obj.EmployeeWhoLastChanedItUserId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
            obj.LastActivityDate = DateTime.Now;
            if (obj.IsValid)
            {

                if (obj.Id > 0)
                {
                    if (SaveObject<cMDEntities_Product>(obj, true))
                    {
                        UpdatePriceList(obj);
                        
                        System.Web.HttpContext.Current.Session["Product"] = null;
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

                    if (SaveObject<cMDEntities_Product>(obj, false))
                    {
                        UpdatePriceList((cMDEntities_Product)ViewData.Model);

                        System.Web.HttpContext.Current.Session["Product"] = null;
                        
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

        public ActionResult AddProduct(FormCollection collection)
        {
            string Name = collection["Name"];
            string Tax = collection["Tax"];
            string WSprice = collection["WSprice"];
            string Label = collection["Label"];
            string Unit = collection["Unit"];
            JsonResult result = new JsonResult();
            result.Data = -1;
            if (Tax != "" && Name != "" && WSprice != "" && Label != "" && Unit != "")
            {
                cMDEntities_Product p = new cMDEntities_Product();
                p.Name = Name;
                p.TaxRateId = Convert.ToInt32(Tax);
                p.WholesalePrice = Convert.ToDecimal(WSprice);
                p.UnitId = Convert.ToInt32(Unit);
                p.Label = Label;
                p.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                cMDEntities_Product temp = p.Clone();
                p = temp.Save();
                result.Data = p.Id;

                cMDEntities_Product obj = new cMDEntities_Product();
                obj.Id = p.Id;
                obj.WholesalePrice = Convert.ToDecimal(WSprice);
                UpdatePriceList(obj);

            }
            return result;
        }

        private static void UpdatePriceList(cMDEntities_Product obj)
        {
            using (DalEf.DocumentsEntities contextDoc = new DalEf.DocumentsEntities())
            {
                DalEf.Documents_PriceList_ItemsCol dataItem = null;
                DalEf.Documents_PriceList dataList = contextDoc.Documents_Document.OfType<DalEf.Documents_PriceList>().FirstOrDefault(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.IsDefault == true);
                if (dataList != null)
                {
                    dataItem = dataList.Documents_PriceList_ItemsCol.FirstOrDefault(p => p.MDEntities_EntityId == obj.Id);

                    if (dataItem == null)
                    {
                        dataItem = new DalEf.Documents_PriceList_ItemsCol();

                        dataItem.MDEntities_EntityId = obj.Id;
                        dataItem.WholesalePrice = obj.WholesalePrice;
                        dataItem.CalcualtedWholesalePrice = obj.WholesalePrice;
                        dataItem.Ordinal = dataList.Documents_PriceList_ItemsCol.Count + 1;

                        dataList.Documents_PriceList_ItemsCol.Add(dataItem);
                    }
                    else
                    {
                        dataItem.MDEntities_EntityId = obj.Id;
                        dataItem.WholesalePrice = obj.WholesalePrice;
                        dataItem.CalcualtedWholesalePrice = obj.WholesalePrice;
                    }

                }
                else
                {
                    dataList = new DalEf.Documents_PriceList();
                    dataList.MDSubjects_SubjectId = null;
                    dataList.IsDefault = true;
                    dataList.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                    dataList.DateStart = System.DateTime.Now;
                    dataList.LastActivityDate = System.DateTime.Now;
                    dataList.MDSubjects_Employee_AuthorId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                    dataList.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                    dataList.UniqueIdentifier = "1";
                    dataList.OrdinalNumber = 1;
                    dataList.OrdinalNumberInYear = 1;
                    dataList.Name = "Osnovna lista";
                    dataList.DocumentType = (short)BusinessObjects.Common.DocumentType.PriceList;
                    dataList.DocumentDateTime = System.DateTime.Now;
                    dataList.CreationDateTime = System.DateTime.Now;
                    dataList.GUID = Guid.NewGuid();

                    dataItem = new DalEf.Documents_PriceList_ItemsCol();

                    dataItem.MDEntities_EntityId = obj.Id;
                    dataItem.WholesalePrice = obj.WholesalePrice;
                    dataItem.CalcualtedWholesalePrice = obj.WholesalePrice;
                    dataItem.Ordinal = 1;

                    dataList.Documents_PriceList_ItemsCol.Add(dataItem);

                    contextDoc.AddToDocuments_Document(dataList);
                }

                contextDoc.SaveChanges();
            }
        }

        public ActionResult Odustani()
        {
            System.Web.HttpContext.Current.Session["Product"] = null;
            return RedirectToAction("Index");
        }

        //
        // GET: /MDEntities/Product/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MDEntities/Product/Delete/5

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

        public ActionResult ProductGridPartial()
        {
            return PartialView("ProductGridPartial");
        }

        public object CreateModel(Type modelType)
        {
            if (modelType.Equals(typeof(cMDEntities_Product)))
                return (cMDEntities_Product)System.Web.HttpContext.Current.Session["Product"];
            else return Activator.CreateInstance(modelType);
        }

        #region PicturesCol

        public ActionResult RefreshPicturesColGridPartial()
        {
            cMDEntities_Product product = (cMDEntities_Product)System.Web.HttpContext.Current.Session["Product"];

            ViewData["controllerName"] = "Product";
            return PartialView("ProductPicturesColPartial", product.MDEntities_Product_PicturesCol);
        }

        //[HttpPost]
        //public ActionResult AddNewItem(cMDEntities_Product_Pictures item)
        //{
        //    cMDEntities_Product product = (cMDEntities_Product)System.Web.HttpContext.Current.Session["Product"];

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            //item.MarkChild();
        //            product.MDEntities_Product_PicturesCol.Add(item);
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Molim ispravite greške!";

        //    ViewData["controllerName"] = "Product";
        //    return PartialView("ProductPicturesColPartial", product.MDEntities_Product_PicturesCol);
        //}

        //[HttpPost]
        //public ActionResult UpdateItem(cMDEntities_Product_Pictures item)
        //{
        //    cMDEntities_Product product = (cMDEntities_Product)System.Web.HttpContext.Current.Session["Product"];

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var target = product.MDEntities_Product_PicturesCol.SingleOrDefault(p => p.Id == item.Id);
        //            if (target != null)
        //            {
        //                DataMapper.Map(item, target, "ProductId");
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Molim ispravite greške!";

        //    ViewData["controllerName"] = "Product";
        //    return PartialView("ProductPicturesColPartial", product.MDEntities_Product_PicturesCol);
        //}

        [HttpPost]
        public ActionResult DeleteItem(int id)
        {
            cMDEntities_Product product = (cMDEntities_Product)System.Web.HttpContext.Current.Session["Product"];

            if (id > 0)
            {
                try
                {
                    var target = product.MDEntities_Product_PicturesCol.SingleOrDefault(p => p.Id == id);
                    product.MDEntities_Product_PicturesCol.Remove(target);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            
            ViewData["controllerName"] = "Person";
            return PartialView("ProductPicturesColPartial", product.MDEntities_Product_PicturesCol);
        }
        #endregion


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
            //public static void ClearImageCollection()
            //{
            //    UploadControlFilesStorage storage = System.Web.HttpContext.Current.Session["Storage"] as UploadControlFilesStorage;
            //    if (storage != null)
            //        storage.Files.Clear();
            //}

            

            public static void FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
            {
                if (e.UploadedFile.IsValid)
                {
                    cMDEntities_Product product = (cMDEntities_Product)System.Web.HttpContext.Current.Session["Product"];
                    cMDEntities_Product_PicturesCol productPicsCol = (cMDEntities_Product_PicturesCol)System.Web.HttpContext.Current.Session["ProductPicsCol"];
                    cMDEntities_Product_Pictures prodPic = new cMDEntities_Product_Pictures();
                    if (product != null)
                    {
                        Stream _st;
                        Byte[] _bt;
                        _st = e.UploadedFile.FileContent;
                        _bt = new Byte[_st.Length];
                        _st.Read(_bt, 0, _bt.Length);
                        //comp.InvoiceLogo = _bt;
                        //item.MarkChild();
                        prodPic.MarkChild();
                        prodPic.Picture = _bt;
                        

                        int maxId = 0;
                        maxId = product.MDEntities_Product_PicturesCol.OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefault();
                        prodPic.Id = maxId + 1;

                        product.MDEntities_Product_PicturesCol.Add(prodPic);
                        System.Web.HttpContext.Current.Session["Product"] = product;
                    }
                    //string resultFilePath = UploadDirectory + string.Format(ThumbnailFormat, "", Path.GetExtension(e.UploadedFile.FileName));
                    //using (Image original = Image.FromStream(e.UploadedFile.FileContent))
                    //using (Image thumbnail = PhotoUtils.Inscribe(original, 100))
                    //{
                    //    PhotoUtils.SaveToJpeg(thumbnail, System.Web.HttpContext.Current.Request.MapPath(resultFilePath));
                    //}
                    //IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                    //if (urlResolver != null)
                    //    //e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath) + "?refresh=" + Guid.NewGuid().ToString();
                    //    e.CallbackData = resultFilePath + "?refresh=" + Guid.NewGuid().ToString();
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



        public static class PhotoUtils
        {

            public static Image Inscribe(Image image, int size)
            {
                return Inscribe(image, size, size);
            }

            public static Image Inscribe(Image image, int width, int height)
            {
                Bitmap result = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(result))
                {
                    double factor = 1.0 * width / image.Width;
                    if (image.Height * factor < height)
                        factor = 1.0 * height / image.Height;
                    Size size = new Size((int)(width / factor), (int)(height / factor));
                    Point sourceLocation = new Point((image.Width - size.Width) / 2, (image.Height - size.Height) / 2);

                    SmoothGraphics(graphics);
                    graphics.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(sourceLocation, size), GraphicsUnit.Pixel);
                }
                return result;
            }

            static void SmoothGraphics(Graphics g)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            }

            public static void SaveToJpeg(Image image, Stream output)
            {
                image.Save(output, ImageFormat.Jpeg);
            }

            public static void SaveToJpeg(Image image, string fileName)
            {
                image.Save(fileName, ImageFormat.Jpeg);
            }


        }

    }
}
