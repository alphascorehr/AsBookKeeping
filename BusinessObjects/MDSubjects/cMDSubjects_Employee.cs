using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using Csla.Security;

namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public partial class cMDSubjects_Employee: cMDSubjects_Subject<cMDSubjects_Employee>
	{
		#region Business Methods
        private static readonly PropertyInfo<bool?> isAdminProperty = RegisterProperty<bool?>(p => p.IsAdmin, string.Empty, (bool?)null);
        public bool? IsAdmin
        {
            get { return GetProperty(isAdminProperty); }
            set { SetProperty(isAdminProperty, value); }
        }

        private static readonly PropertyInfo<System.String> firstNameProperty = RegisterProperty<System.String>(p => p.FirstName, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String FirstName
        {
            get { return GetProperty(firstNameProperty); }
            set { SetProperty(firstNameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> lastNameProperty = RegisterProperty<System.String>(p => p.LastName, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String LastName
        {
            get { return GetProperty(lastNameProperty); }
            set { SetProperty(lastNameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> homePhoneProperty = RegisterProperty<System.String>(p => p.HomePhone, string.Empty, (System.String)null);
        public System.String HomePhone
        {
            get { return GetProperty(homePhoneProperty); }
            set { SetProperty(homePhoneProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> mobileProperty = RegisterProperty<System.String>(p => p.Mobile, string.Empty, (System.String)null);
        public System.String Mobile
        {
            get { return GetProperty(mobileProperty); }
            set { SetProperty(mobileProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> mDPlaces_Enums_OgranizationalUnitIdProperty = RegisterProperty<System.Int32?>(p => p.MDPlaces_Enums_OgranizationalUnitId, string.Empty, (System.Int32?)null);
        public System.Int32? MDPlaces_Enums_OgranizationalUnitId
        {
            get { return GetProperty(mDPlaces_Enums_OgranizationalUnitIdProperty); }
            set { SetProperty(mDPlaces_Enums_OgranizationalUnitIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> mDSubjects_Enums_FunctionIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Enums_FunctionId, string.Empty, (System.Int32?)null);
        public System.Int32? MDSubjects_Enums_FunctionId
        {
            get { return GetProperty(mDSubjects_Enums_FunctionIdProperty); }
            set { SetProperty(mDSubjects_Enums_FunctionIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> mDSubjects_Enums_EducationDegreeIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Enums_EducationDegreeId, string.Empty, (System.Int32?)null);
        public System.Int32? MDSubjects_Enums_EducationDegreeId
        {
            get { return GetProperty(mDSubjects_Enums_EducationDegreeIdProperty); }
            set { SetProperty(mDSubjects_Enums_EducationDegreeIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Decimal?> billingRatePerHourProperty = RegisterProperty<System.Decimal?>(p => p.BillingRatePerHour, string.Empty, (System.Decimal?)null);
        public System.Decimal? BillingRatePerHour
        {
            get { return GetProperty(billingRatePerHourProperty); }
            set { SetProperty(billingRatePerHourProperty, value); }
        }

        private static readonly PropertyInfo<System.String> userNameProperty = RegisterProperty<System.String>(p => p.UserName, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        //[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String UserName
        {
            get { return GetProperty(userNameProperty); }
            set { SetProperty(userNameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> passwordProperty = RegisterProperty<System.String>(p => p.Password, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        //[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String Password
        {
            get { return GetProperty(passwordProperty); }
            set { SetProperty(passwordProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<bool?> incomingInvoiceProperty = RegisterProperty<bool?>(p => p.IncomingInvoice, string.Empty, (bool?)null);
        public bool? IncomingInvoice
        {
            get { return GetProperty(incomingInvoiceProperty); }
            set { SetProperty(incomingInvoiceProperty, value); }
        }

        private static readonly PropertyInfo<bool?> invoiceProperty = RegisterProperty<bool?>(p => p.Invoice, string.Empty, (bool?)null);
        public bool? Invoice
        {
            get { return GetProperty(invoiceProperty); }
            set { SetProperty(invoiceProperty, value); }
        }

        private static readonly PropertyInfo<bool?> offerProperty = RegisterProperty<bool?>(p => p.Offer, string.Empty, (bool?)null);
        public bool? Offer
        {
            get { return GetProperty(offerProperty); }
            set { SetProperty(offerProperty, value); }
        }

        private static readonly PropertyInfo<bool?> quoteProperty = RegisterProperty<bool?>(p => p.Quote, string.Empty, (bool?)null);
        public bool? Quote
        {
            get { return GetProperty(quoteProperty); }
            set { SetProperty(quoteProperty, value); }
        }

        private static readonly PropertyInfo<bool?> travelOrderProperty = RegisterProperty<bool?>(p => p.TravelOrder, string.Empty, (bool?)null);
        public bool? TravelOrder
        {
            get { return GetProperty(travelOrderProperty); }
            set { SetProperty(travelOrderProperty, value); }
        }

        private static readonly PropertyInfo<bool?> workOrderProperty = RegisterProperty<bool?>(p => p.WorkOrder, string.Empty, (bool?)null);
        public bool? WorkOrder
        {
            get { return GetProperty(workOrderProperty); }
            set { SetProperty(workOrderProperty, value); }
        }

        private static readonly PropertyInfo<bool?> priceListProperty = RegisterProperty<bool?>(p => p.PriceList, string.Empty, (bool?)null);
        public bool? PriceList
        {
            get { return GetProperty(priceListProperty); }
            set { SetProperty(priceListProperty, value); }
        }

        private static readonly PropertyInfo<bool?> paymentProperty = RegisterProperty<bool?>(p => p.Payment, string.Empty, (bool?)null);
        public bool? Payment
        {
            get { return GetProperty(paymentProperty); }
            set { SetProperty(paymentProperty, value); }
        }

        private static readonly PropertyInfo<bool?> firstPageProperty = RegisterProperty<bool?>(p => p.FirstPage, string.Empty, (bool?)true);
        public bool? FirstPage
        {
            get { return GetProperty(firstPageProperty); }
            set { SetProperty(firstPageProperty, value); }
        }
        // dodano zbog fiskalizacije
        private static readonly PropertyInfo<System.String> CashierCodeProperty = RegisterProperty<System.String>(p => p.CashierCode, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(11, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        //[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String CashierCode
        {
            get { return GetProperty(CashierCodeProperty); }
            set { SetProperty(CashierCodeProperty, (value ?? "").Trim()); }
        }
        private static readonly PropertyInfo<bool?> fiscalModeProperty = RegisterProperty<bool?>(p => p.FiscalMode, string.Empty, (bool?)null);
        public bool? FiscalMode
        {
            get { return GetProperty(fiscalModeProperty); }
            set { SetProperty(fiscalModeProperty, value); }
        }

		#endregion

        #region Factory Methods

        public static cMDSubjects_Employee NewMDSubjects_Employee()
        {
            return DataPortal.Create<cMDSubjects_Employee>();

        }

        public static cMDSubjects_Employee GetMDSubjects_Employee(int uniqueId)
        {
            return DataPortal.Fetch<cMDSubjects_Employee>(new SingleCriteria<cMDSubjects_Employee, int>(uniqueId));
        }

        public static cMDSubjects_Employee GetMDSubjects_Employee(string username)
        {
            return DataPortal.Fetch<cMDSubjects_Employee>(new SingleCriteria<cMDSubjects_Employee, string>(username));
        }

        public static cMDSubjects_Employee GetMDSubjects_Employee(UsernameCriteria criteria)
        {
            return DataPortal.Fetch<cMDSubjects_Employee>(criteria);
        }

        internal static cMDSubjects_Employee GetMDSubjects_Employee(MDSubjects_Employee data)
        {
            return DataPortal.Fetch<cMDSubjects_Employee>(data);
        }

        public static void DeleteMDSubjects_Employee(int uniqueId)
        {
            DataPortal.Delete<cMDSubjects_Employee>(new SingleCriteria<cMDSubjects_Employee, int>(uniqueId));
        }
        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(UsernameCriteria criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Employee>().First(p => p.UserName == criteria.Username && p.Password == criteria.Password);

                Fetch(data);

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(SingleCriteria<cMDSubjects_Employee, string> criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Employee>().First(p => p.UserName == criteria.Value);

                Fetch(data);

                LoadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty, cMDSubjects_Subject_AccountsCol.GetcMDSubjects_Subject_AccountsCol(data.MDSubjects_Subject_AccountsCol));
                LoadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty, cMDSubjects_Subject_ContactsCol.GetcMDSubjects_Subject_ContactsCol(data.MDSubjects_Subject_ContactsCol));
                LoadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty, cMDSubjects_Subject_PermissionsForClientsCol.GetcMDSubjects_Subject_PermissionsForClientsCol(data.MDSubjects_Subject_PermissionsForClientsCol));

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(SingleCriteria<cMDSubjects_Employee, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Employee>().First(p => p.Id == criteria.Value);

                Fetch(data);

                LoadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty, cMDSubjects_Subject_AccountsCol.GetcMDSubjects_Subject_AccountsCol(data.MDSubjects_Subject_AccountsCol));
                LoadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty, cMDSubjects_Subject_ContactsCol.GetcMDSubjects_Subject_ContactsCol(data.MDSubjects_Subject_ContactsCol));
                LoadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty, cMDSubjects_Subject_PermissionsForClientsCol.GetcMDSubjects_Subject_PermissionsForClientsCol(data.MDSubjects_Subject_PermissionsForClientsCol));

                BusinessRules.CheckRules();

            }
        }

        private void Fetch(MDSubjects_Employee data)
        {
            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LoadProperty<Guid>(GUIDProperty, data.GUID);
            LoadProperty<short>(subjectTypeProperty, data.SubjectType);
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<string>(longNameProperty, data.LongName);
            LoadProperty<bool>(residentProperty, data.Resident);
            LoadProperty<bool?>(isCustomerProperty, data.IsCustomer);
            LoadProperty<bool?>(isContractorProperty, data.IsContractor);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
            LoadProperty<int>(employeeWhoLastChanedItIdProperty, data.EmployeeWhoLastChanedItId);
            LoadProperty<int?>(homeAddress_PlaceIdProperty, data.HomeAddress_PlaceId);
            LoadProperty<string>(homeAddress_StreetProperty, data.HomeAddress_Street);
            LoadProperty<string>(homeAddress_NumberProperty, data.HomeAddress_Number);
            LoadProperty<string>(homeAddress_DescriptionProperty, data.HomeAddress_Description);
            LoadProperty<string>(oIBProperty, data.OIB);

            LoadProperty<bool?>(isAdminProperty, data.IsAdmin);
            LoadProperty<string>(firstNameProperty, data.FirstName);
            LoadProperty<string>(lastNameProperty, data.LastName);
            LoadProperty<string>(homePhoneProperty, data.HomePhone);
            LoadProperty<string>(mobileProperty, data.Mobile);
            LoadProperty<int?>(mDPlaces_Enums_OgranizationalUnitIdProperty, data.MDPlaces_Enums_OgranizationalUnitId);
            LoadProperty<int?>(mDSubjects_Enums_FunctionIdProperty, data.MDSubjects_Enums_FunctionId);
            LoadProperty<int?>(mDSubjects_Enums_EducationDegreeIdProperty, data.MDSubjects_Enums_EducationDegreeId);
            LoadProperty<decimal?>(billingRatePerHourProperty, data.BillingRatePerHour);
            LoadProperty<string>(userNameProperty, data.UserName);
            LoadProperty<string>(passwordProperty, data.Password);
            LoadProperty<bool?>(incomingInvoiceProperty, data.IncomingInvoice);
            LoadProperty<bool?>(invoiceProperty, data.Invoice);
            LoadProperty<bool?>(offerProperty, data.Offer);
            LoadProperty<bool?>(quoteProperty, data.Quote);
            LoadProperty<bool?>(travelOrderProperty, data.TravelOrder);
            LoadProperty<bool?>(workOrderProperty, data.WorkOrder);
            LoadProperty<bool?>(priceListProperty, data.PriceList);
            LoadProperty<bool?>(paymentProperty, data.Payment);
            LoadProperty<bool?>(firstPageProperty, data.FirstPage);
            LoadProperty<string>(CashierCodeProperty, data.CashierCode);
            LoadProperty<bool?>(fiscalModeProperty, data.FiscalMode);

            LastChanged = data.LastChanged;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Employee();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.SubjectType = (short)Common.SubjectType.Employee;
                data.Name = ReadProperty<string>(nameProperty);
                data.LongName = ReadProperty<string>(longNameProperty);
                data.Resident = ReadProperty<bool>(residentProperty);
                data.IsCustomer = ReadProperty<bool?>(isCustomerProperty);
                data.IsContractor = ReadProperty<bool?>(isContractorProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItId = ReadProperty<int>(employeeWhoLastChanedItIdProperty);
                data.HomeAddress_PlaceId = ReadProperty<int?>(homeAddress_PlaceIdProperty);
                data.HomeAddress_Street = ReadProperty<string>(homeAddress_StreetProperty);
                data.HomeAddress_Number = ReadProperty<string>(homeAddress_NumberProperty);
                data.HomeAddress_Description = ReadProperty<string>(homeAddress_DescriptionProperty);
                data.OIB = ReadProperty<string>(oIBProperty);

                data.IsAdmin = ReadProperty<bool?>(isAdminProperty);
                data.FirstName = ReadProperty<string>(firstNameProperty);
                data.LastName = ReadProperty<string>(lastNameProperty);
                data.HomePhone = ReadProperty<string>(homePhoneProperty);
                data.Mobile = ReadProperty<string>(mobileProperty);
                data.MDPlaces_Enums_OgranizationalUnitId = ReadProperty<int?>(mDPlaces_Enums_OgranizationalUnitIdProperty);
                data.MDSubjects_Enums_FunctionId = ReadProperty<int?>(mDSubjects_Enums_FunctionIdProperty);
                data.MDSubjects_Enums_EducationDegreeId = ReadProperty<int?>(mDSubjects_Enums_EducationDegreeIdProperty);
                data.BillingRatePerHour = ReadProperty<decimal?>(billingRatePerHourProperty);
                data.UserName = ReadProperty<string>(userNameProperty);
                data.Password = ReadProperty<string>(passwordProperty);
                data.IncomingInvoice = ReadProperty<bool?>(incomingInvoiceProperty);
                data.Invoice = ReadProperty<bool?>(invoiceProperty);
                data.Offer = ReadProperty<bool?>(offerProperty);
                data.Quote = ReadProperty<bool?>(quoteProperty);
                data.TravelOrder = ReadProperty<bool?>(travelOrderProperty);
                data.WorkOrder = ReadProperty<bool?>(workOrderProperty);
                data.PriceList = ReadProperty<bool?>(priceListProperty);
                data.Payment = ReadProperty<bool?>(paymentProperty);
                data.FirstPage = ReadProperty<bool?>(firstPageProperty);
                data.CashierCode= ReadProperty<string>(CashierCodeProperty);
                data.FiscalMode= ReadProperty<bool?>(fiscalModeProperty);

                ctx.ObjectContext.AddToMDSubjects_Subject(data);

                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty), data);

                ctx.ObjectContext.SaveChanges();


                //Get New id
                int newId = data.Id;
                //Load New Id into object
                LoadProperty(IdProperty, newId);
                //Load New EntityKey into Object
                LoadProperty(EntityKeyDataProperty, Serialize(data.EntityKey));

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Employee();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.SubjectType = ReadProperty<short>(subjectTypeProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.LongName = ReadProperty<string>(longNameProperty);
                data.Resident = ReadProperty<bool>(residentProperty);
                data.IsCustomer = ReadProperty<bool?>(isCustomerProperty);
                data.IsContractor = ReadProperty<bool?>(isContractorProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItId = ReadProperty<int>(employeeWhoLastChanedItIdProperty);
                data.HomeAddress_PlaceId = ReadProperty<int?>(homeAddress_PlaceIdProperty);
                data.HomeAddress_Street = ReadProperty<string>(homeAddress_StreetProperty);
                data.HomeAddress_Number = ReadProperty<string>(homeAddress_NumberProperty);
                data.HomeAddress_Description = ReadProperty<string>(homeAddress_DescriptionProperty);
                data.OIB = ReadProperty<string>(oIBProperty);

                data.IsAdmin = ReadProperty<bool?>(isAdminProperty);
                data.FirstName = ReadProperty<string>(firstNameProperty);
                data.LastName = ReadProperty<string>(lastNameProperty);
                data.HomePhone = ReadProperty<string>(homePhoneProperty);
                data.Mobile = ReadProperty<string>(mobileProperty);
                data.MDPlaces_Enums_OgranizationalUnitId = ReadProperty<int?>(mDPlaces_Enums_OgranizationalUnitIdProperty);
                data.MDSubjects_Enums_FunctionId = ReadProperty<int?>(mDSubjects_Enums_FunctionIdProperty);
                data.MDSubjects_Enums_EducationDegreeId = ReadProperty<int?>(mDSubjects_Enums_EducationDegreeIdProperty);
                data.BillingRatePerHour = ReadProperty<decimal?>(billingRatePerHourProperty);
                data.UserName = ReadProperty<string>(userNameProperty);
                data.Password = ReadProperty<string>(passwordProperty);
                data.IncomingInvoice = ReadProperty<bool?>(incomingInvoiceProperty);
                data.Invoice = ReadProperty<bool?>(invoiceProperty);
                data.Offer = ReadProperty<bool?>(offerProperty);
                data.Quote = ReadProperty<bool?>(quoteProperty);
                data.TravelOrder = ReadProperty<bool?>(travelOrderProperty);
                data.WorkOrder = ReadProperty<bool?>(workOrderProperty);
                data.PriceList = ReadProperty<bool?>(priceListProperty);
                data.Payment = ReadProperty<bool?>(paymentProperty);
                data.FirstPage = ReadProperty<bool?>(firstPageProperty);
                data.CashierCode = ReadProperty<string>(CashierCodeProperty);
                data.FiscalMode = ReadProperty<bool?>(fiscalModeProperty);

                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }

         [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<cMDSubjects_Employee, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Employee>().First(p => p.Id == criteria.Value);

                ctx.ObjectContext.MDSubjects_Subject.DeleteObject(data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
