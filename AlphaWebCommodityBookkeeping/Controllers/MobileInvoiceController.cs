using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObjects.Security;
using BusinessObjects.Common;

namespace AlphaWebCommodityBookkeeping.Controllers
{
    public class MobileInvoiceController : Controller
    {
        //
        // GET: /MobileOffer/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetInvoiceList()
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.DocumentsEntities context = new DalEf.DocumentsEntities();
            var listInvoice = context.vInvoice.OrderByDescending(p => p.Id).Where(p => p.CompanyUsingServiceId == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
            List<string[]> lista = new List<string[]>();

            foreach (var item in listInvoice)
            {
                string[] newItem = new string[] { item.Id.ToString(), item.UniqueIdentifier, item.Name, Convert.ToDateTime(item.DocumentDateTime).ToString("dd.MM.yyyy"), Convert.ToDateTime(item.MaturityDate).ToString("dd.MM.yyyy"), item.RetailValue.ToString() };
                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;
        }


        public ActionResult GetInvoice(FormCollection collection)
        {
            JsonResult rez = new JsonResult();
            DalEf.Documents_Invoice invoice;
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                int Id = Convert.ToInt32(collection["Id"]);
                System.Web.HttpContext.Current.Session["Invoice"] = invoice = data.Documents_Document.OfType<DalEf.Documents_Invoice>().SingleOrDefault(p => p.Id == Id);
                var details = data.vDocumentDetailsWithProductDescription.Where(p => p.DocumentId == Id).OrderBy(p => p.Ordinal);

                if (invoice != null)
                {
                    string[] itemHead = { invoice.SubjectId.ToString(), Convert.ToDateTime(invoice.MaturityDate).ToString("dd.MM.yyyy."), invoice.DispatchTypeId.ToString(), Convert.ToDateTime(invoice.DocumentDateTime).ToString("dd.MM.yyyy."), Convert.ToDateTime(invoice.DispatchDate).ToString("dd.MM.yyyy."), invoice.DispatchCompanyId.ToString(), invoice.WholesaleValue.ToString(), ((invoice.RabateValue ?? 0) + (invoice.WholesaleValue)).ToString(), invoice.RabateValue.ToString(), invoice.TaxValue.ToString(), invoice.RetailValue.ToString() };
                    List<string[]> items = new List<string[]>();
                    if (details != null)
                    {
                        foreach (var item in details)
                        {
                            string[] toAdd = { item.Id.ToString(), item.Ordinal.ToString(), item.ProductName, item.Price.ToString("n2"), item.Label, item.Quantity.ToString("n2"), (item.RabatePercentage ?? 0).ToString("n2"), item.TaxPercentage.ToString("n2"), item.Ammount.ToString("n2"), item.WholesaleValue.ToString("n2") };
                            items.Add(toAdd);
                        }
                    }

                    Tuple<string[], List<string[]>> rezData = new Tuple<string[], List<string[]>>(itemHead, items);
                    rez.Data = rezData;
                }
            }
            return rez;
        }


        public ActionResult GetDispatchTypeList()
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.DocumentsEntities data = new DalEf.DocumentsEntities();

            var listDispatchType = data.Documents_Enums_DispatchType.Where(p => (p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();
            foreach (var item in listDispatchType)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.Name);

                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;

        }

        public ActionResult GetDispatchCompanyList()
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();

            var listDispatchCompany = data.MDSubjects_Subject.Where(p => p.CompanyUsingServiceId == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.SubjectType != (short)BusinessObjects.Common.SubjectType.Employee);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();
            foreach (var item in listDispatchCompany)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.Name);

                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;

        }


        public ActionResult GetSubjectsListFull()
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

        public ActionResult GetUnitsList()
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities();

            var listUnits = data.MDEntities_Enums_Unit.Where(p => (p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
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

        public ActionResult GetTaxsList()
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                JsonResult rez = new JsonResult();
            using (DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities())
            {

                var listUnits = data.MDEntities_Enums_TaxRate.Where(p => (p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
                List<Tuple<string, string,string>> lista = new List<Tuple<string, string, string>>();
                foreach (var item in listUnits)
                {
                    Tuple<string, string, string> newItem = new Tuple<string, string, string>(item.Id.ToString(), item.Name, item.Percentage.ToString());

                    lista.Add(newItem);

                }

        
                rez.Data = lista;
            }

            return rez;

        }














        public ActionResult GetCurrencyList()
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;

            DalEf.MDGeneralEntities data = new DalEf.MDGeneralEntities();

            var listCurrency = data.MDGeneral_Enums_Currency.Where(p => (p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();
            foreach (var item in listCurrency)
            {
                Tuple<string, string> newItem = new Tuple<string, string>(item.Id.ToString(), item.Name);

                lista.Add(newItem);

            }
            JsonResult rez = new JsonResult();
            rez.Data = lista;

            return rez;

        }

        

        
        public ActionResult GetEntityList(FormCollection collection)
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            int index = Convert.ToInt16(collection["index"]);
            string searchText = collection["search"];

            DalEf.MDEntitiesEntities data = new DalEf.MDEntitiesEntities();

            var items = data.vEntities.Where(p => p.CompanyUsingServiceId == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.Inactive == false && p.Name.Contains(searchText)).OrderBy(p=> p.Name).Skip(index).Take(10);
            List<Tuple<string, string>> lista = new List<Tuple<string, string>>();

            foreach (var item in items)
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
            var dataPlaces = data.MDPlaces_Enums_Geo_Place.Where(p => ((p.CompanyUsingServiceId ?? 0) == 0 || p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId) && p.Name.Contains(searchText)).OrderBy(p => p.Name).Skip(index).Take(10);
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

        public ActionResult GetSubjectsListFiltered(FormCollection collection)
        {
            int companyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
            int index = Convert.ToInt16(collection["index"]);
            string searchText = collection["search"];

            DalEf.MDSubjectsEntities data = new DalEf.MDSubjectsEntities();

            var listUnits = data.vCustomers.Where(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId).OrderBy(p => p.Name).Skip(index).Take(10);
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

       


        

        public ActionResult GetOffer(FormCollection collection)
        {
            JsonResult rez = new JsonResult();
            DalEf.Documents_Offer offer;
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                int Id = Convert.ToInt32(collection["Id"]);
                System.Web.HttpContext.Current.Session["Offer"] = offer = data.Documents_Document.OfType<DalEf.Documents_Offer>().SingleOrDefault(p => p.Id == Id);
                var details = data.vDocumentDetailsWithProductDescription.Where(p => p.DocumentId == Id).OrderBy(p=> p.Ordinal);
                
                if (offer != null)
                {
                    string[] itemHead = { offer.SubjectId.ToString(), Convert.ToDateTime(offer.MaturityDate).ToString("dd.MM.yyyy."), offer.DispatchTypeId.ToString(), Convert.ToDateTime(offer.DocumentDateTime).ToString("dd.MM.yyyy."), Convert.ToDateTime(offer.DispatchDate).ToString("dd.MM.yyyy."), offer.DispatchCompanyId.ToString(), offer.WholesaleValue.ToString(), ((offer.RabateValue ?? 0) + (offer.WholesaleValue)).ToString(), offer.RabateValue.ToString(), offer.TaxValue.ToString(), offer.RetailValue.ToString() };
                    List<string[]> items = new List<string[]>();
                    if (details != null)
                    {
                        foreach (var item in details)
                        {
                            string[] toAdd = { item.Id.ToString(), item.Ordinal.ToString(), item.ProductName, item.Price.ToString("n2"),  item.Label, item.Quantity.ToString("n2"), (item.RabatePercentage ?? 0).ToString("n2"), item.TaxPercentage.ToString("n2"), item.Ammount.ToString("n2"), item.WholesaleValue.ToString("n2") };
                            items.Add(toAdd);
                        }
                    }

                    Tuple<string[], List<string[]>> rezData = new Tuple<string[], List<string[]>>(itemHead, items);

                    rez.Data = rezData;

                }
            }

            return rez;

        }

        public ActionResult GetItems(FormCollection collection)
        {
            JsonResult rez = new JsonResult();
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                int Id = Convert.ToInt32(collection["offerId"]);
                var details = data.vDocumentDetailsWithProductDescription.Where(p => p.DocumentId == Id).OrderBy(p => p.Ordinal);

              
                    List<string[]> items = new List<string[]>();
                    if (details != null)
                    {
                        foreach (var item in details)
                        {
                            string[] toAdd = { item.Id.ToString(), item.Ordinal.ToString(), item.ProductName, item.Price.ToString("n2"), item.Label, item.Quantity.ToString("n2"), (item.RabatePercentage ?? 0).ToString("n2"), item.TaxPercentage.ToString("n2"), item.Ammount.ToString("n2"), item.WholesaleValue.ToString("n2") };
                            items.Add(toAdd);
                        }
                    }

                    rez.Data = items;
              
            }

            return rez;

        }

        public ActionResult GetItem(FormCollection collection)
        {
            JsonResult rez = new JsonResult();
            DalEf.Documents_ItemsCol item;
            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                int Id = Convert.ToInt32(collection["Id"]);

            
               item = data.Documents_ItemsCol.SingleOrDefault(p => p.Id == Id);

                if (item != null)
                {
                    string[] itemData = { item.Ordinal.ToString(), item.ProductId.ToString(), item.Price.ToString("n2"), item.PriceFromPriceList.ToString(), item.PriceListId.ToString(), item.UnitId.ToString(), item.Quantity.ToString("n2"), (item.RabatePercentage ?? 0).ToString("n2"), item.TaxRateId.ToString(), item.Ammount.ToString("n2"), item.WholesaleValue.ToString("n2") };

                    rez.Data = itemData;

                }
            }

            return rez;

        }

        public ActionResult SaveItem(FormCollection collection)
        {
            bool ok = false;
            int Id = Convert.ToInt32(collection["id"]);
            int OfferId = Convert.ToInt32(collection["offerId"]);


            try
            {
                using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                {
                    var offer = data.Documents_Document.OfType<DalEf.Documents_Invoice>().SingleOrDefault(p => p.Id == OfferId);

                    if (offer != null)
                    {
                        DalEf.Documents_ItemsCol newItem = null;
                        if (Id > 0)
                        {
                            newItem = offer.Documents_ItemsCol.SingleOrDefault(p => p.Id == Id);

                        }
                        else
                        {
                            newItem = new DalEf.Documents_ItemsCol();

                            newItem.DocumentId = OfferId;
                            newItem.Ordinal = offer.Documents_ItemsCol.Count + 1;

                        }

                        if (newItem != null)
                        {
                            ok = true;


                            int ProductId = Convert.ToInt32(collection["productId"]);
                            int UnitId = Convert.ToInt32(collection["unitId"]);
                            int TaxRateId = Convert.ToInt32(collection["taxRateId"]);
                            decimal Quantity = Convert.ToDecimal(collection["quantity"]);
                            decimal Ammount = Convert.ToDecimal(collection["ammount"]);
                            decimal Price = Convert.ToDecimal(collection["price"]);
                            decimal WholesaleValue = Convert.ToDecimal(collection["wholesaleValue"]);

                            decimal PriceFromPriceList;
                            if (decimal.TryParse(collection["priceFromPriceList"].ToString(), out PriceFromPriceList))
                                newItem.PriceFromPriceList = PriceFromPriceList;
                            else
                                newItem.PriceFromPriceList = null;

                             int PriceListId;
                             if (int.TryParse(collection["priceListId"].ToString(), out PriceListId))
                                 newItem.PriceListId = PriceListId;
                             else
                                 newItem.PriceListId = null;

                            decimal RabatePercentage = Convert.ToDecimal(collection["rabatePercentage"]);

                            

                            newItem.ProductId = ProductId;
                            newItem.UnitId = UnitId;
                            newItem.TaxRateId = TaxRateId;
                            newItem.Quantity = Quantity;
                            newItem.Ammount = Ammount;
                            newItem.Price = Price;
                            newItem.WholesaleValue = WholesaleValue;
                            newItem.RabatePercentage = RabatePercentage;

                            newItem.RabateAmmount = ((newItem.Price * newItem.Quantity) * (newItem.RabatePercentage ?? 0)) / 100;

                            decimal tax = 0;

                            var taxItem = data.MDEntities_Enums_TaxRate_documents.SingleOrDefault(p => p.Id == newItem.TaxRateId);
                                if (taxItem != null)
                                    tax = (newItem.WholesaleValue * taxItem.Percentage) / 100;


                                newItem.TaxAmmount = tax;



                            if (Id == 0)
                                data.AddToDocuments_ItemsCol(newItem);

                            offer.TaxValue = offer.Documents_ItemsCol.Sum(p => p.TaxAmmount);
                            offer.RetailValue = offer.Documents_ItemsCol.Sum(p => p.WholesaleValue) + offer.TaxValue;
                            offer.WholesaleValue = offer.Documents_ItemsCol.Sum(p => p.WholesaleValue);
                            offer.RabateValue = offer.Documents_ItemsCol.Sum(p => p.RabateAmmount);

                            data.SaveChanges();
                            Id = newItem.Id; 
                        }
                    }


                }
            }
            catch
            {

                ok = false;
            }


            JsonResult rez = new JsonResult();
            Tuple<bool, int> rezData = new Tuple<bool, int>(ok, Id);
            rez.Data = rezData;

            return rez;

        }

        public ActionResult DeleteItem(FormCollection collection)
        {
            int count = 0;
            int Id = Convert.ToInt32(collection["id"]);
            int OfferId = Convert.ToInt32(collection["offerId"]);
            bool ok = false;
            try
            {
                if (Id > 0 && OfferId > 0)
                {
                    using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                    {
                        var item = data.Documents_ItemsCol.SingleOrDefault(p => p.Id == Id);
                        if (item != null)
                        {
                            data.DeleteObject(item);
                            data.SaveChanges();
                        }

                        var items = data.Documents_ItemsCol.Where(p => p.DocumentId == OfferId);
                        if (items != null)
                        {
                            foreach (var it in items)
                            {
                                count += 1;
                                it.Ordinal = count;
                            }

                            data.SaveChanges();
                        }
                        var offer = data.Documents_Document.OfType<DalEf.Documents_Invoice>().SingleOrDefault(p => p.Id == OfferId);

                        if (offer != null)
                        {

                                ok = true;

                                offer.TaxValue = offer.Documents_ItemsCol.Sum(p => p.TaxAmmount);
                                offer.RetailValue = offer.Documents_ItemsCol.Sum(p => p.WholesaleValue) + offer.TaxValue;
                                offer.WholesaleValue = offer.Documents_ItemsCol.Sum(p => p.WholesaleValue);
                                offer.RabateValue = offer.Documents_ItemsCol.Sum(p => p.RabateAmmount);

                                data.SaveChanges();
                        }
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

























        public ActionResult SaveOffer(FormCollection collection)
        {
            bool ok = false;
            int count = 0;
            int Id = Convert.ToInt32(collection["offerId"]);
            

            try
            {
                using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
                {


                    DalEf.Documents_Offer newOffer = null;
                    if (Id > 0)
                    {
                        newOffer = data.Documents_Document.OfType<DalEf.Documents_Offer>().SingleOrDefault(p => p.Id == Id);

                    }
                    else
                    {
                        newOffer = new DalEf.Documents_Offer();

                        newOffer.CreationDateTime = System.DateTime.Now;
                        newOffer.CourseValue = 1;
                        newOffer.MDGeneral_Enums_CurrencyId = ((PTIdentity)Csla.ApplicationContext.User.Identity).DefaultCurencyId;
                        newOffer.CompanyUsingServiceId = ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId;
                        newOffer.BillToAddress_Name = "";
                        newOffer.ShipToAddress_Street = "";
                        newOffer.BillToAddress_Street = "";
                        newOffer.GUID = new Guid();
                        newOffer.Inactive = false;
                        newOffer.DocumentType = (short)DocumentType.Offer;
                        newOffer.UniqueIdentifier = "";

                    }

                    if (newOffer != null)
                    {
                        ok = true;


                        int SubjectId = Convert.ToInt32(collection["SubjectId"]);
                        DateTime docDate = Convert.ToDateTime(collection["docDate"]);
                        DateTime matDate = Convert.ToDateTime(collection["matDate"]);
                        DateTime datIsporuke = Convert.ToDateTime(collection["datIsporuke"]);
                        
                        int nac;
                        if (Int32.TryParse((collection["nacinIsporuke"].ToString()), out nac))
                        {
                            newOffer.DispatchTypeId = nac;
                        }
                        else
                        {
                            newOffer.DispatchTypeId = null;
                        }

                        int pri;
                        if (Int32.TryParse((collection["prijevoznik"].ToString()), out pri))
                        {
                            newOffer.DispatchCompanyId = pri;
                        }
                        else
                        {
                            newOffer.DispatchCompanyId = null;
                        }

       



                        newOffer.SubjectId = SubjectId;
                        newOffer.DocumentDateTime = docDate;
                        newOffer.MaturityDate = matDate;
                        newOffer.DispatchDate = datIsporuke;

                       

                        newOffer.MDSubjects_EmployeeWhoChengedId = ((PTIdentity)Csla.ApplicationContext.User.Identity).EmployeeSubjectId;
                        
                        newOffer.LastActivityDate = System.DateTime.Now;

                        if (Id == 0)
                            data.AddToDocuments_Document(newOffer);

                        data.SaveChanges();
                        Id = newOffer.Id;
                    }

                  
                }
            }
            catch
            {

                ok = false;
            }


            JsonResult rez = new JsonResult();
            Tuple<bool, int> rezData = new Tuple<bool, int>(ok, Id);
            rez.Data = rezData;

            return rez;

        }

        public ActionResult GetProductData(FormCollection collection)
        {
            int SubjectId = Convert.ToInt32(collection["subjectId"]);
            int ProizvoId = Convert.ToInt32(collection["productId"]);

            decimal rezPrice = 0;
            int? priceListId = null;
            var unitid = 0;
            var taxrateId = 0;

            using (DalEf.DocumentsEntities data = new DalEf.DocumentsEntities())
            {
                var itemSubject = data.Documents_Document.OfType<DalEf.Documents_PriceList>().OrderByDescending(p => p.MDSubjects_SubjectId).FirstOrDefault(p => p.MDSubjects_SubjectId == SubjectId || p.MDSubjects_SubjectId == null);
                if (itemSubject != null)
                {
                    priceListId = itemSubject.Id;
                    var itemProduct = itemSubject.Documents_PriceList_ItemsCol.FirstOrDefault(p => p.MDEntities_EntityId == ProizvoId);

                    var itemProductEnt = data.vEntitiesForDocuments.FirstOrDefault(p => p.Id == ProizvoId);
                    if (itemProduct != null)
                    {
                        rezPrice = itemProduct.WholesalePrice ?? 0;

                    }
                    else
                    {

                        if (itemProductEnt != null)
                            rezPrice = itemProductEnt.WholesalePrice;
                    }

                    unitid = itemProductEnt.UnitId;
                    taxrateId = itemProductEnt.TaxRateId;
                }
                else
                {

                    var itemProductEnt = data.vEntitiesForDocuments.FirstOrDefault(p => p.Id == ProizvoId);
                    if (itemProductEnt != null)
                    {
                        rezPrice = itemProductEnt.WholesalePrice;
                        unitid = itemProductEnt.UnitId;
                        taxrateId = itemProductEnt.TaxRateId;
                    }
                }
            }

           
            string[] rezData = { rezPrice.ToString(), unitid.ToString(), taxrateId.ToString(), priceListId.ToString() };

            JsonResult rez = new JsonResult() { Data = rezData };
            return rez;
        }
    }
}
