using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;

namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public partial class cMDSubjects_Company: cMDSubjects_Subject<cMDSubjects_Company>
	{
		#region Business Methods
        private static readonly PropertyInfo<System.DateTime?> establishedDateProperty = RegisterProperty<System.DateTime?>(p => p.EstablishedDate, string.Empty, System.DateTime.Now);
        public System.DateTime? EstablishedDate
        {
            get { return GetProperty(establishedDateProperty); }
            set { SetProperty(establishedDateProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> mDSubjects_Enums_CompanyTypeIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Enums_CompanyTypeId, string.Empty, (System.Int32?)null);
        public System.Int32? MDSubjects_Enums_CompanyTypeId
        {
            get { return GetProperty(mDSubjects_Enums_CompanyTypeIdProperty); }
            set { SetProperty(mDSubjects_Enums_CompanyTypeIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> numberOfEmployeesProperty = RegisterProperty<System.Int32?>(p => p.NumberOfEmployees, string.Empty, (System.Int32?)null);
        public System.Int32? NumberOfEmployees
        {
            get { return GetProperty(numberOfEmployeesProperty); }
            set { SetProperty(numberOfEmployeesProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> mDSubjects_Enums_CoreBussinessIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Enums_CoreBussinessId, string.Empty, (System.Int32?)null);
        public System.Int32? MDSubjects_Enums_CoreBussinessId
        {
            get { return GetProperty(mDSubjects_Enums_CoreBussinessIdProperty); }
            set { SetProperty(mDSubjects_Enums_CoreBussinessIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> coreBusinessDescriptionProperty = RegisterProperty<System.String>(p => p.CoreBusinessDescription, string.Empty, (System.String)null);
        public System.String CoreBusinessDescription
        {
            get { return GetProperty(coreBusinessDescriptionProperty); }
            set { SetProperty(coreBusinessDescriptionProperty, (value ?? "").Trim()); }
        }

        //private static readonly PropertyInfo<System.String> cRO_OIBProperty = RegisterProperty<System.String>(p => p.CRO_OIB, string.Empty);
        //[System.ComponentModel.DataAnnotations.StringLength(11, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        //[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        //public System.String CRO_OIB
        //{
        //    get { return GetProperty(cRO_OIBProperty); }
        //    set { SetProperty(cRO_OIBProperty, (value ?? "").Trim()); }
        //}

        private static readonly PropertyInfo<System.String> cRO_MBRProperty = RegisterProperty<System.String>(p => p.CRO_MBR, string.Empty, (System.String)null);
        public System.String CRO_MBR
        {
            get { return GetProperty(cRO_MBRProperty); }
            set { SetProperty(cRO_MBRProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> billToAddress_PlaceIdProperty = RegisterProperty<System.Int32?>(p => p.BillToAddress_PlaceId, string.Empty);
        public System.Int32? BillToAddress_PlaceId
        {
            get { return GetProperty(billToAddress_PlaceIdProperty); }
            set { SetProperty(billToAddress_PlaceIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> billToAddress_StreetProperty = RegisterProperty<System.String>(p => p.BillToAddress_Street, string.Empty, (System.String)null);
        public System.String BillToAddress_Street
        {
            get { return GetProperty(billToAddress_StreetProperty); }
            set { SetProperty(billToAddress_StreetProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> billToAddress_NumberProperty = RegisterProperty<System.String>(p => p.BillToAddress_Number, string.Empty, (System.String)null);
        public System.String BillToAddress_Number
        {
            get { return GetProperty(billToAddress_NumberProperty); }
            set { SetProperty(billToAddress_NumberProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> billToAddress_DescriptionProperty = RegisterProperty<System.String>(p => p.BillToAddress_Description, string.Empty, (System.String)null);
        public System.String BillToAddress_Description
        {
            get { return GetProperty(billToAddress_DescriptionProperty); }
            set { SetProperty(billToAddress_DescriptionProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> pDVTypeProperty = RegisterProperty<System.Int32?>(p => p.PDVType, string.Empty);
           public System.Int32? PDVType
        {
            get { return GetProperty(pDVTypeProperty); }
            set { SetProperty(pDVTypeProperty, value); }
        }

        private static readonly PropertyInfo<System.DateTime?> iS_DateProperty = RegisterProperty<System.DateTime?>(p => p.IS_Date, string.Empty, System.DateTime.Now);
        public System.DateTime? IS_Date
        {
            get { return GetProperty(iS_DateProperty); }
            set { SetProperty(iS_DateProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> iS_MDGeneral_Enums_CurrencyIdProperty = RegisterProperty<System.Int32?>(p => p.IS_MDGeneral_Enums_CurrencyId, string.Empty);
        public System.Int32? IS_MDGeneral_Enums_CurrencyId
        {
            get { return GetProperty(iS_MDGeneral_Enums_CurrencyIdProperty); }
            set { SetProperty(iS_MDGeneral_Enums_CurrencyIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Decimal?> iS_CourseProperty = RegisterProperty<System.Decimal?>(p => p.IS_Course, string.Empty, Convert.ToDecimal(1));
        public System.Decimal? IS_Course
        {
            get { return GetProperty(iS_CourseProperty); }
            set { SetProperty(iS_CourseProperty, value); }
        }

        private static readonly PropertyInfo<System.Decimal?> iS_DemandFromPartnerProperty = RegisterProperty<System.Decimal?>(p => p.IS_DemandFromPartner, string.Empty, Convert.ToDecimal(0));
        public System.Decimal? IS_DemandFromPartner
        {
            get { return GetProperty(iS_DemandFromPartnerProperty); }
            set { SetProperty(iS_DemandFromPartnerProperty, value); }
        }

        private static readonly PropertyInfo<System.Decimal?> iS_DebitToPartnerProperty = RegisterProperty<System.Decimal?>(p => p.IS_DebitToPartner, string.Empty, Convert.ToDecimal(0));
        public System.Decimal? IS_DebitToPartner
        {
            get { return GetProperty(iS_DebitToPartnerProperty); }
            set { SetProperty(iS_DebitToPartnerProperty, value); }
        }

        
        
		#endregion

        #region Factory Methods

        public static cMDSubjects_Company NewMDSubjects_Company()
        {
            return DataPortal.Create<cMDSubjects_Company>();

        }

        public static cMDSubjects_Company GetMDSubjects_Company(int uniqueId)
        {
            return DataPortal.Fetch<cMDSubjects_Company>(new SingleCriteria<cMDSubjects_Company, int>(uniqueId));
        }

        internal static cMDSubjects_Company GetMDSubjects_Company(MDSubjects_Company data)
        {
            return DataPortal.Fetch<cMDSubjects_Company>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDSubjects_Company, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Company>().First(p => p.Id == criteria.Value);

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

                LoadProperty<DateTime?>(establishedDateProperty, data.EstablishedDate);
                LoadProperty<int?>(mDSubjects_Enums_CompanyTypeIdProperty, data.MDSubjects_Enums_CompanyTypeId);
                LoadProperty<int?>(numberOfEmployeesProperty, data.NumberOfEmployees);
                LoadProperty<int?>(mDSubjects_Enums_CoreBussinessIdProperty, data.MDSubjects_Enums_CoreBussinessId);
                LoadProperty<string>(coreBusinessDescriptionProperty, data.CoreBusinessDescription);
                LoadProperty<string>(cRO_MBRProperty, data.CRO_MBR);
                LoadProperty<int?>(billToAddress_PlaceIdProperty, data.BillToAddress_PlaceId);
                LoadProperty<string>(billToAddress_StreetProperty, data.BillToAddress_Street);
                LoadProperty<string>(billToAddress_NumberProperty, data.BillToAddress_Number);
                LoadProperty<string>(billToAddress_DescriptionProperty, data.BillToAddress_Description);
                LoadProperty<int?>(pDVTypeProperty, data.PDVType);
                LoadProperty<DateTime?>(iS_DateProperty, data.IS_Date);
                LoadProperty<int?>(iS_MDGeneral_Enums_CurrencyIdProperty, data.IS_MDGeneral_Enums_CurrencyId);
                LoadProperty<decimal?>(iS_CourseProperty, data.IS_Course);
                LoadProperty<decimal?>(iS_DemandFromPartnerProperty, data.IS_DemandFromPartner);
                LoadProperty<decimal?>(iS_DebitToPartnerProperty, data.IS_DebitToPartner);
                

                LastChanged = data.LastChanged;

                LoadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty, cMDSubjects_Subject_AccountsCol.GetcMDSubjects_Subject_AccountsCol(data.MDSubjects_Subject_AccountsCol));
                LoadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty, cMDSubjects_Subject_ContactsCol.GetcMDSubjects_Subject_ContactsCol(data.MDSubjects_Subject_ContactsCol));
                LoadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty, cMDSubjects_Subject_PermissionsForClientsCol.GetcMDSubjects_Subject_PermissionsForClientsCol(data.MDSubjects_Subject_PermissionsForClientsCol));

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Company();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.SubjectType = (short)Common.SubjectType.Company;
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

                data.EstablishedDate = ReadProperty<DateTime?>(establishedDateProperty);
                data.MDSubjects_Enums_CompanyTypeId = ReadProperty<int?>(mDSubjects_Enums_CompanyTypeIdProperty);
                data.NumberOfEmployees = ReadProperty<int?>(numberOfEmployeesProperty);
                data.MDSubjects_Enums_CoreBussinessId = ReadProperty<int?>(mDSubjects_Enums_CoreBussinessIdProperty);
                data.CoreBusinessDescription = ReadProperty<string>(coreBusinessDescriptionProperty);
                data.CRO_MBR = ReadProperty<string>(cRO_MBRProperty);
                data.BillToAddress_PlaceId = ReadProperty<int?>(billToAddress_PlaceIdProperty);
                data.BillToAddress_Street = ReadProperty<string>(billToAddress_StreetProperty);
                data.BillToAddress_Number = ReadProperty<string>(billToAddress_NumberProperty);
                data.BillToAddress_Description = ReadProperty<string>(billToAddress_DescriptionProperty);
                data.PDVType = ReadProperty<int?>(pDVTypeProperty);
                data.IS_Date= ReadProperty<DateTime?>(iS_DateProperty);
                data.IS_MDGeneral_Enums_CurrencyId = ReadProperty<int?>(iS_MDGeneral_Enums_CurrencyIdProperty);
                data.IS_Course = ReadProperty<decimal?>(iS_CourseProperty);
                data.IS_DemandFromPartner = ReadProperty<decimal?>(iS_DemandFromPartnerProperty);
                data.IS_DebitToPartner = ReadProperty<decimal?>(iS_DebitToPartnerProperty);

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
                var data = new MDSubjects_Company();

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

                data.EstablishedDate = ReadProperty<DateTime?>(establishedDateProperty);
                data.MDSubjects_Enums_CompanyTypeId = ReadProperty<int?>(mDSubjects_Enums_CompanyTypeIdProperty);
                data.NumberOfEmployees = ReadProperty<int?>(numberOfEmployeesProperty);
                data.MDSubjects_Enums_CoreBussinessId = ReadProperty<int?>(mDSubjects_Enums_CoreBussinessIdProperty);
                data.CoreBusinessDescription = ReadProperty<string>(coreBusinessDescriptionProperty);
                data.CRO_MBR = ReadProperty<string>(cRO_MBRProperty);
                data.BillToAddress_PlaceId = ReadProperty<int?>(billToAddress_PlaceIdProperty);
                data.BillToAddress_Street = ReadProperty<string>(billToAddress_StreetProperty);
                data.BillToAddress_Number = ReadProperty<string>(billToAddress_NumberProperty);
                data.BillToAddress_Description = ReadProperty<string>(billToAddress_DescriptionProperty);
                data.PDVType = ReadProperty<int?>(pDVTypeProperty);
                data.IS_Date = ReadProperty<DateTime?>(iS_DateProperty);
                data.IS_MDGeneral_Enums_CurrencyId = ReadProperty<int?>(iS_MDGeneral_Enums_CurrencyIdProperty);
                data.IS_Course = ReadProperty<decimal?>(iS_CourseProperty);
                data.IS_DemandFromPartner = ReadProperty<decimal?>(iS_DemandFromPartnerProperty);
                data.IS_DebitToPartner = ReadProperty<decimal?>(iS_DebitToPartnerProperty);

                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
