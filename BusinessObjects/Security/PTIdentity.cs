using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Serialization;
using Csla.Security;
using BusinessObjects.MDSubjects;
using Csla.Data;
using DalEf;

namespace BusinessObjects.Security
{
    [Serializable]
    public partial class PTIdentity : CslaIdentityBase<PTIdentity>
    {
         private static readonly PropertyInfo<bool> incomingInvoiceProperty = RegisterProperty<bool>(p => p.IncomingInvoice, string.Empty, false);
        public bool IncomingInvoice
        {
            get { return GetProperty(incomingInvoiceProperty); }
            private set { LoadProperty(incomingInvoiceProperty, value); }
        }

        private static readonly PropertyInfo<bool> invoiceProperty = RegisterProperty<bool>(p => p.Invoice, string.Empty, false);
        public bool Invoice
        {
            get { return GetProperty(invoiceProperty); }
            private set { LoadProperty(invoiceProperty, value); }
        }

        private static readonly PropertyInfo<bool> offerProperty = RegisterProperty<bool>(p => p.Offer, string.Empty, false);
        public bool Offer
        {
            get { return GetProperty(offerProperty); }
            private set { LoadProperty(offerProperty, value); }
        }

        private static readonly PropertyInfo<bool> quoteProperty = RegisterProperty<bool>(p => p.Quote, string.Empty, false);
        public bool Quote
        {
            get { return GetProperty(quoteProperty); }
            private set { LoadProperty(quoteProperty, value); }
        }

        private static readonly PropertyInfo<bool> travelOrderProperty = RegisterProperty<bool>(p => p.TravelOrder, string.Empty, false);
        public bool TravelOrder
        {
            get { return GetProperty(travelOrderProperty); }
            private set { LoadProperty(travelOrderProperty, value); }
        }

        private static readonly PropertyInfo<bool> workOrderProperty = RegisterProperty<bool>(p => p.WorkOrder, string.Empty, false);
        public bool WorkOrder
        {
            get { return GetProperty(workOrderProperty); }
            private set { LoadProperty(workOrderProperty, value); }
        }

        private static readonly PropertyInfo<bool> priceListProperty = RegisterProperty<bool>(p => p.PriceList, string.Empty, false);
        public bool PriceList
        {
            get { return GetProperty(priceListProperty); }
            private set { LoadProperty(priceListProperty, value); }
        }

        private static readonly PropertyInfo<bool> paymentProperty = RegisterProperty<bool>(p => p.Payment, string.Empty, false);
        public bool Payment
        {
            get { return GetProperty(paymentProperty); }
            private set { LoadProperty(paymentProperty, value); }
        }

        private static readonly PropertyInfo<bool> compensationProperty = RegisterProperty<bool>(p => p.Compensation, string.Empty, false);
        public bool Compensation
        {
            get { return GetProperty(compensationProperty); }
            private set { LoadProperty(compensationProperty, value); }
        }

        private static readonly PropertyInfo<bool> transferOrderProperty = RegisterProperty<bool>(p => p.TransferOrder, string.Empty, false);
        public bool TransferOrder
        {
            get { return GetProperty(transferOrderProperty); }
            private set { LoadProperty(transferOrderProperty, value); }
        }

        private static readonly PropertyInfo<bool> firstPageProperty = RegisterProperty<bool>(p => p.FirstPage, string.Empty, false);
        public bool FirstPage
        {
            get { return GetProperty(firstPageProperty); }
            private set { LoadProperty(firstPageProperty, value); }
        }

        public static readonly PropertyInfo<int> companyIdProperty = RegisterProperty<int>(c => c.CompanyId);
        public int CompanyId
        {
            get { return GetProperty(companyIdProperty); }
            private set { LoadProperty(companyIdProperty, value); } 
        }

        public static readonly PropertyInfo<int> employeeSubjectIdProperty = RegisterProperty<int>(c => c.EmployeeSubjectId);
        public int EmployeeSubjectId
        {
            get { return GetProperty(employeeSubjectIdProperty); }
            private set { LoadProperty(employeeSubjectIdProperty, value); }
        }

        public static readonly PropertyInfo<int> defaultCurencyIdProperty = RegisterProperty<int>(c => c.DefaultCurencyId);
        public int DefaultCurencyId
        {
            get { return GetProperty(defaultCurencyIdProperty); }
            private set { LoadProperty(defaultCurencyIdProperty, value); }
        }

        private static readonly PropertyInfo<bool> fiscalModeProperty = RegisterProperty<bool>(p => p.FiscalMode, string.Empty, false);
        public bool FiscalMode
        {
            get { return GetProperty(fiscalModeProperty); }
            private set { LoadProperty(fiscalModeProperty, value); }
        }

        private static readonly PropertyInfo<string> fiscalizationConsistenceCodeProperty = RegisterProperty<string>(p => p.FiscalizationConsistenceCode);
        public string FiscalizationConsistenceCode
        {
            get { return GetProperty(fiscalizationConsistenceCodeProperty); }
            private set { LoadProperty(fiscalizationConsistenceCodeProperty, value); }
        }

        public static void GetPTIdentity(string username, string password, EventHandler<DataPortalResult<PTIdentity>> callback)
        {
            DataPortal.BeginFetch<PTIdentity>(new UsernameCriteria(username, password), callback);
        }

#if !SILVERLIGHT
        public static PTIdentity GetPTIdentity(string username, string password)
        {
                return DataPortal.Fetch<PTIdentity>(new UsernameCriteria(username, password));
        }

        private void DataPortal_Fetch(UsernameCriteria criteria)
        {

            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var korisnik = (from r in ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Employee>()
                            where r.UserName == criteria.Username && r.Password == criteria.Password
                            select r).FirstOrDefault();
                if (korisnik != null)
                {
                    base.Name = korisnik.UserName;
                    base.IsAuthenticated = true;
                    base.AuthenticationType = "Membership";
                    base.Roles = new Csla.Core.MobileList<string>();
                    CompanyId = korisnik.CompanyUsingServiceId;
                    EmployeeSubjectId = korisnik.Id;
                    IncomingInvoice = korisnik.IncomingInvoice ?? false;
                    Invoice = korisnik.Invoice ?? false;
                    Offer = korisnik.Offer ?? false;
                    Quote = korisnik.Quote ?? false;
                    TravelOrder = korisnik.TravelOrder ?? false;
                    WorkOrder = korisnik.WorkOrder ?? false;
                    PriceList = korisnik.PriceList ?? false;
                    Payment = korisnik.Payment ?? false;
                    FirstPage = korisnik.FirstPage ?? false;
                    Compensation = korisnik.Compensation ?? false;
                    TransferOrder = korisnik.TransferOrder ?? false;
                    FiscalMode = korisnik.FiscalMode ?? false;

                    if (CompanyId == 0)
                    {
                        base.Roles.Add("SuperAdmin");
                    }
                    else
                    {
                        if (korisnik.IsAdmin ?? false)
                            base.Roles.Add("Admin");
                        else
                            base.Roles.Add("Staff");

                    }

                    var curency = ctx.ObjectContext.MDGeneral_Enums_Currency_subjects.FirstOrDefault(p => p.DefaultCurrency == true);

                    if (curency != null)
                    {
                        DefaultCurencyId = curency.Id;
                    }

                    string FiscalizationConsistenceCode = "";
                    var item = ctx.ObjectContext.Auth_Company.FirstOrDefault(p => p.Id == CompanyId);
                    if(item != null)
                        FiscalizationConsistenceCode = item.FiscalizationConsistenceCode;


                }
                else
                {
                    base.Name = string.Empty;
                    base.IsAuthenticated = false;
                    base.AuthenticationType = string.Empty;
                    base.Roles = new Csla.Core.MobileList<string>();
                }

            }
        }
#endif
    }
}
