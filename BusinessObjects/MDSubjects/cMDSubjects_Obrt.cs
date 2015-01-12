using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;

namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public partial class cMDSubjects_Obrt: cMDSubjects_Subject<cMDSubjects_Obrt>
	{
		#region Business Methods
     
        private static readonly PropertyInfo<System.String> jMBGProperty = RegisterProperty<System.String>(p => p.JMBG, string.Empty, (System.String)null);
        public System.String JMBG
        {
            get { return GetProperty(jMBGProperty); }
            set { SetProperty(jMBGProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> imeVlasnikaProperty = RegisterProperty<System.String>(p => p.ImeVlasnika, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String ImeVlasnika
        {
            get { return GetProperty(imeVlasnikaProperty); }
            set { SetProperty(imeVlasnikaProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> prezimeVlasnikaProperty = RegisterProperty<System.String>(p => p.PrezimeVlasnika, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String PrezimeVlasnika
        {
            get { return GetProperty(prezimeVlasnikaProperty); }
            set { SetProperty(prezimeVlasnikaProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<bool?> sezonskiObrtProperty = RegisterProperty<bool?>(p => p.SezonskiObrt, string.Empty, (bool?)null);
        public bool? SezonskiObrt
        {
            get { return GetProperty(sezonskiObrtProperty); }
            set { SetProperty(sezonskiObrtProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> mDSubjects_Enums_CoreBussinessIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Enums_CoreBussinessId, string.Empty, (System.Int32?)null);
        public System.Int32? MDSubjects_Enums_CoreBussinessId
        {
            get { return GetProperty(mDSubjects_Enums_CoreBussinessIdProperty); }
            set { SetProperty(mDSubjects_Enums_CoreBussinessIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> djelatnostOpisProperty = RegisterProperty<System.String>(p => p.DjelatnostOpis, string.Empty, (System.String)null);
        public System.String DjelatnostOpis
        {
            get { return GetProperty(djelatnostOpisProperty); }
            set { SetProperty(djelatnostOpisProperty, (value ?? "").Trim()); }
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

		#endregion

        #region Factory Methods

        public static cMDSubjects_Obrt NewMDSubjects_Obrt()
        {
            return DataPortal.Create<cMDSubjects_Obrt>();

        }

        public static cMDSubjects_Obrt GetMDSubjects_Obrt(int uniqueId)
        {
            return DataPortal.Fetch<cMDSubjects_Obrt>(new SingleCriteria<cMDSubjects_Obrt, int>(uniqueId));
        }

        internal static cMDSubjects_Obrt GetMDSubjects_Obrt(MDSubjects_Obrt data)
        {
            return DataPortal.Fetch<cMDSubjects_Obrt>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDSubjects_Obrt, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Obrt>().First(p => p.Id == criteria.Value);

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

                LoadProperty<string>(jMBGProperty, data.JMBG);
                LoadProperty<string>(imeVlasnikaProperty, data.ImeVlasnika);
                LoadProperty<string>(prezimeVlasnikaProperty, data.PrezimeVlasnika);
                LoadProperty<bool?>(sezonskiObrtProperty, data.SezonskiObrt);
                LoadProperty<int?>(mDSubjects_Enums_CoreBussinessIdProperty, data.MDSubjects_Enums_CoreBussinessId);
                LoadProperty<string>(djelatnostOpisProperty, data.DjelatnostOpis);
                LoadProperty<int?>(billToAddress_PlaceIdProperty, data.BillToAddress_PlaceId);
                LoadProperty<string>(billToAddress_StreetProperty, data.BillToAddress_Street);
                LoadProperty<string>(billToAddress_NumberProperty, data.BillToAddress_Number);
                LoadProperty<string>(billToAddress_DescriptionProperty, data.BillToAddress_Description);

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
                var data = new MDSubjects_Obrt();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.SubjectType = (short)Common.SubjectType.Obrt;
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
                
                data.JMBG = ReadProperty<string>(jMBGProperty);
                data.ImeVlasnika = ReadProperty<string>(imeVlasnikaProperty);
                data.PrezimeVlasnika = ReadProperty<string>(prezimeVlasnikaProperty);
                data.SezonskiObrt = ReadProperty<bool?>(sezonskiObrtProperty);
                data.MDSubjects_Enums_CoreBussinessId = ReadProperty<int?>(mDSubjects_Enums_CoreBussinessIdProperty);
                data.DjelatnostOpis = ReadProperty<string>(djelatnostOpisProperty);
                data.BillToAddress_PlaceId = ReadProperty<int?>(billToAddress_PlaceIdProperty);
                data.BillToAddress_Street= ReadProperty<string>(billToAddress_StreetProperty);
                data.BillToAddress_Number = ReadProperty<string>(billToAddress_NumberProperty);
                data.BillToAddress_Description = ReadProperty<string>(billToAddress_DescriptionProperty);

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
                var data = new MDSubjects_Obrt();

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

                data.JMBG = ReadProperty<string>(jMBGProperty);
                data.ImeVlasnika = ReadProperty<string>(imeVlasnikaProperty);
                data.PrezimeVlasnika = ReadProperty<string>(prezimeVlasnikaProperty);
                data.SezonskiObrt = ReadProperty<bool?>(sezonskiObrtProperty);
                data.MDSubjects_Enums_CoreBussinessId = ReadProperty<int?>(mDSubjects_Enums_CoreBussinessIdProperty);
                data.DjelatnostOpis = ReadProperty<string>(djelatnostOpisProperty);
                data.BillToAddress_PlaceId = ReadProperty<int?>(billToAddress_PlaceIdProperty);
                data.BillToAddress_Street = ReadProperty<string>(billToAddress_StreetProperty);
                data.BillToAddress_Number = ReadProperty<string>(billToAddress_NumberProperty);
                data.BillToAddress_Description = ReadProperty<string>(billToAddress_DescriptionProperty);

                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
