using System;
using Csla;
using Csla.Data;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;

namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public abstract class cMDSubjects_Subject<T> : CoreBusinessClass<T> where T : cMDSubjects_Subject<T>
	{
		#region Business Methods
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			internal set { SetProperty(IdProperty, value); }
		}

		protected static readonly PropertyInfo<System.Int32> companyUsingServiceIdProperty = RegisterProperty<System.Int32>(p => p.CompanyUsingServiceId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 CompanyUsingServiceId
		{
			get { return GetProperty(companyUsingServiceIdProperty); }
			set { SetProperty(companyUsingServiceIdProperty, value); }
		}

        protected static readonly PropertyInfo<Guid> GUIDProperty = RegisterProperty<Guid>(p => p.GUID, string.Empty, Guid.NewGuid());
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public Guid GUID
		{
			get { return GetProperty(GUIDProperty); }
			set { SetProperty(GUIDProperty, value); }
		}

        protected static readonly PropertyInfo<short> subjectTypeProperty = RegisterProperty<short>(p => p.SubjectType, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public short SubjectType
		{
			get { return GetProperty(subjectTypeProperty); }
			set { SetProperty(subjectTypeProperty, value); }
		}

        protected static readonly PropertyInfo<System.String> nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String Name
        {
            get { return GetProperty(nameProperty); }
            set { SetProperty(nameProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<System.String> longNameProperty = RegisterProperty<System.String>(p => p.LongName, string.Empty, (System.String)null);
        public System.String LongName
        {
            get { return GetProperty(longNameProperty); }
            set { SetProperty(longNameProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<bool> residentProperty = RegisterProperty<bool>(p => p.Resident, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Resident
        {
            get { return GetProperty(residentProperty); }
            set { SetProperty(residentProperty, value); }
        }

        protected static readonly PropertyInfo<bool?> isCustomerProperty = RegisterProperty<bool?>(p => p.IsCustomer, string.Empty, (bool?)null);
        public bool? IsCustomer
        {
            get { return GetProperty(isCustomerProperty); }
            set { SetProperty(isCustomerProperty, value); }
        }

        protected static readonly PropertyInfo<bool?> isContractorProperty = RegisterProperty<bool?>(p => p.IsContractor, string.Empty, (bool?)null);
        public bool? IsContractor
        {
            get { return GetProperty(isContractorProperty); }
            set { SetProperty(isContractorProperty, value); }
        }

        protected static readonly PropertyInfo<bool> inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Inactive
        {
            get { return GetProperty(inactiveProperty); }
            set { SetProperty(inactiveProperty, value); }
        }

        protected static readonly PropertyInfo<System.DateTime> lastActivityDateProperty = RegisterProperty<System.DateTime>(p => p.LastActivityDate, string.Empty, System.DateTime.Now);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.DateTime LastActivityDate
        {
            get { return GetProperty(lastActivityDateProperty); }
            set { SetProperty(lastActivityDateProperty, value); }
        }

        protected static readonly PropertyInfo<System.Int32> employeeWhoLastChanedItIdProperty = RegisterProperty<System.Int32>(p => p.EmployeeWhoLastChanedItId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 EmployeeWhoLastChanedItId
        {
            get { return GetProperty(employeeWhoLastChanedItIdProperty); }
            set { SetProperty(employeeWhoLastChanedItIdProperty, value); }
        }

        protected static readonly PropertyInfo<System.Int32?> homeAddress_PlaceIdProperty = RegisterProperty<System.Int32?>(p => p.HomeAddress_PlaceId, string.Empty, (System.Int32?)null);
        public System.Int32? HomeAddress_PlaceId
        {
            get { return GetProperty(homeAddress_PlaceIdProperty); }
            set { SetProperty(homeAddress_PlaceIdProperty, value); }
        }

        protected static readonly PropertyInfo<System.String> homeAddress_StreetProperty = RegisterProperty<System.String>(p => p.HomeAddress_Street, string.Empty, (System.String)null);
        public System.String HomeAddress_Street
        {
            get { return GetProperty(homeAddress_StreetProperty); }
            set { SetProperty(homeAddress_StreetProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<System.String> homeAddress_NumberProperty = RegisterProperty<System.String>(p => p.HomeAddress_Number, string.Empty, (System.String)null);
        public System.String HomeAddress_Number
        {
            get { return GetProperty(homeAddress_NumberProperty); }
            set { SetProperty(homeAddress_NumberProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<System.String> homeAddress_DescriptionProperty = RegisterProperty<System.String>(p => p.HomeAddress_Description, string.Empty, (System.String)null);
        public System.String HomeAddress_Description
        {
            get { return GetProperty(homeAddress_DescriptionProperty); }
            set { SetProperty(homeAddress_DescriptionProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<System.String> oIBProperty = RegisterProperty<System.String>(p => p.OIB, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(11, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        public System.String OIB
        {
            get { return GetProperty(oIBProperty); }
            set { SetProperty(oIBProperty, (value ?? "").Trim()); }
        }


        protected static readonly PropertyInfo<bool> IndividualCustomerProperty = RegisterProperty<bool>(p => p.IndividualCustomer, string.Empty);
        public bool IndividualCustomer
        {
            get { return GetProperty(IndividualCustomerProperty); }
            set { SetProperty(IndividualCustomerProperty, value); }
        }
		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#region Kolekcije
        protected static PropertyInfo<cMDSubjects_Subject_AccountsCol> MDSubjects_Subject_AccountsColProperty = RegisterProperty<cMDSubjects_Subject_AccountsCol>(c => c.MDSubjects_Subject_AccountsCol);
		public cMDSubjects_Subject_AccountsCol MDSubjects_Subject_AccountsCol
		{
			get {
                if (!FieldManager.FieldExists(MDSubjects_Subject_AccountsColProperty))
                    LoadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty, cMDSubjects_Subject_AccountsCol.NewMDSubjects_Subject_AccountsCol());
                    return GetProperty(MDSubjects_Subject_AccountsColProperty); 
                }
			private set { SetProperty(MDSubjects_Subject_AccountsColProperty, value); }
		}

        protected static PropertyInfo<cMDSubjects_Subject_ContactsCol> MDSubjects_Subject_ContactsColProperty = RegisterProperty<cMDSubjects_Subject_ContactsCol>(c => c.MDSubjects_Subject_ContactsCol);
        public cMDSubjects_Subject_ContactsCol MDSubjects_Subject_ContactsCol
        {
            get
            {
                if (!FieldManager.FieldExists(MDSubjects_Subject_ContactsColProperty))
                    LoadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty, cMDSubjects_Subject_ContactsCol.NewMDSubjects_Subject_ContactsCol());
                return GetProperty(MDSubjects_Subject_ContactsColProperty);
            }
            private set { SetProperty(MDSubjects_Subject_ContactsColProperty, value); }
        }

        protected static PropertyInfo<cMDSubjects_Subject_PermissionsForClientsCol> MDSubjects_Subject_PermissionsForClientsColProperty = RegisterProperty<cMDSubjects_Subject_PermissionsForClientsCol>(c => c.MDSubjects_Subject_PermissionsForClientsCol);
        public cMDSubjects_Subject_PermissionsForClientsCol MDSubjects_Subject_PermissionsForClientsCol
        {
            get
            {
                if (!FieldManager.FieldExists(MDSubjects_Subject_PermissionsForClientsColProperty))
                    LoadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty, cMDSubjects_Subject_PermissionsForClientsCol.NewMDSubjects_Subject_PermissionsForClientsCol());
                return GetProperty(MDSubjects_Subject_PermissionsForClientsColProperty);
            }
            private set { SetProperty(MDSubjects_Subject_PermissionsForClientsColProperty, value); }
        }

		#endregion
		#endregion
	}
}
