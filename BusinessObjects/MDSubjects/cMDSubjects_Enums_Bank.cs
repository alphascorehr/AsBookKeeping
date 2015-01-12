using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using BusinessObjects.Common;

namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public partial class cMDSubjects_Enums_Bank: CoreBusinessClass<cMDSubjects_Enums_Bank>
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

		private static readonly PropertyInfo< System.String > nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty, "");
		[System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
			set { SetProperty(nameProperty, (value ?? "").Trim()); }
		}

        private static readonly PropertyInfo<bool> inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Inactive
        {
            get { return GetProperty(inactiveProperty); }
            set { SetProperty(inactiveProperty, value); }
        }

		private static readonly PropertyInfo< System.Int32? > numberProperty = RegisterProperty<System.Int32?>(p => p.Number, string.Empty);
		public System.Int32? Number
		{
			get { return GetProperty(numberProperty); }
			set { SetProperty(numberProperty, value); }
		}

        protected static readonly PropertyInfo<System.Int32?> companyUsingServiceIdProperty = RegisterProperty<System.Int32?>(p => p.CompanyUsingServiceId, string.Empty);
        public System.Int32? CompanyUsingServiceId
        {
            get { return GetProperty(companyUsingServiceIdProperty); }
            set { SetProperty(companyUsingServiceIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> HomeAddress_PlaceIdProperty = RegisterProperty<System.Int32?>(p => p.HomeAddress_PlaceId, string.Empty);
        public System.Int32? HomeAddress_PlaceId
        {
            get { return GetProperty(HomeAddress_PlaceIdProperty); }
            set { SetProperty(HomeAddress_PlaceIdProperty, value); }
        }

        protected static readonly PropertyInfo<System.Int32?> HomeAddress_CountryNumberProperty = RegisterProperty<System.Int32?>(p => p.HomeAddress_CountryNumber, string.Empty);
        public System.Int32? HomeAddress_CountryNumber
        {
            get { return GetProperty(HomeAddress_CountryNumberProperty); }
            set { SetProperty(HomeAddress_CountryNumberProperty, value); }
        }
        private static readonly PropertyInfo<System.String> HomeAddressProperty = RegisterProperty<System.String>(p => p.HomeAddress, string.Empty, "");
        [System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        public System.String HomeAddress
        {
            get { return GetProperty(HomeAddressProperty); }
            set { SetProperty(HomeAddressProperty, (value ?? "").Trim()); }
        }
        
        /// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];


		#endregion

        #region Factory Methods

        public static cMDSubjects_Enums_Bank NewMDSubjects_Enums_Bank()
        {
            return DataPortal.Create<cMDSubjects_Enums_Bank>();

        }

        public static cMDSubjects_Enums_Bank GetMDSubjects_Enums_Bank(int uniqueId)
        {
            return DataPortal.Fetch<cMDSubjects_Enums_Bank>(new SingleCriteria<cMDSubjects_Enums_Bank, int>(uniqueId));
        }

        internal static cMDSubjects_Enums_Bank GetMDSubjects_Enums_Bank(MDSubjects_Enums_Bank data)
        {
            return DataPortal.Fetch<cMDSubjects_Enums_Bank>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDSubjects_Enums_Bank, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Enums_Bank.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<int?>(numberProperty, data.Number);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<int?>(HomeAddress_PlaceIdProperty, data.HomeAddress_PlaceId);
                LoadProperty<string>(HomeAddressProperty, data.HomeAddress);
                LoadProperty<int?>(HomeAddress_CountryNumberProperty, data.HomeAddress_CountryNumber);
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();
              
            }
        }

        private void DataPortal_Fetch(MDSubjects_Enums_Bank data)
        {

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<int?>(numberProperty, data.Number);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<int?>(HomeAddress_PlaceIdProperty, data.HomeAddress_PlaceId);
                LoadProperty<string>(HomeAddressProperty, data.HomeAddress);
                LoadProperty<int?>(HomeAddress_CountryNumberProperty, data.HomeAddress_CountryNumber);
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();
                MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Enums_Bank();

                data.Name = ReadProperty<string>(nameProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.Number = (ReadProperty<int?>(numberProperty) ?? 0);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
                data.HomeAddress_PlaceId = ReadProperty<int?>(HomeAddress_PlaceIdProperty);
                data.HomeAddress = ReadProperty<string>(HomeAddressProperty);
                data.HomeAddress_CountryNumber = ReadProperty<int?>(HomeAddress_CountryNumberProperty);

                ctx.ObjectContext.AddToMDSubjects_Enums_Bank(data);
                ctx.ObjectContext.SaveChanges();
                //Get New id
                int newId = data.Id;
                //Load New Id into object
                LoadProperty(IdProperty, newId);
                //Load New EntityKey into Object
                LoadProperty(EntityKeyDataProperty, Serialize(data.EntityKey));

                ctx.ObjectContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Enums_Bank();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Name = ReadProperty<string>(nameProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.Number = (ReadProperty<int?>(numberProperty) ?? 0);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
                data.HomeAddress_PlaceId = ReadProperty<int?>(HomeAddress_PlaceIdProperty);
                data.HomeAddress = ReadProperty<string>(HomeAddressProperty);
                data.HomeAddress_CountryNumber = ReadProperty<int?>(HomeAddress_CountryNumberProperty);
                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }

    public partial class cMDSubjects_Enums_Bank_List : BusinessListBase<cMDSubjects_Enums_Bank_List, cMDSubjects_Enums_Bank>
    {
        public static cMDSubjects_Enums_Bank_List GetcMDSubjects_Enums_Bank_List()
        {
            return DataPortal.Fetch<cMDSubjects_Enums_Bank_List>();
        }

        public static cMDSubjects_Enums_Bank_List GetcMDSubjects_Enums_Bank_List(int companyId, int includeInactiveId)
        {
            return DataPortal.Fetch<cMDSubjects_Enums_Bank_List>(new ActiveEnums_Criteria(companyId, includeInactiveId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var result = ctx.ObjectContext.MDSubjects_Enums_Bank;

                foreach (var data in result)
                {
                    var obj = cMDSubjects_Enums_Bank.GetMDSubjects_Enums_Bank(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(ActiveEnums_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var result = ctx.ObjectContext.MDSubjects_Enums_Bank.Where(p => (p.CompanyUsingServiceId == criteria.CompanyId || (p.CompanyUsingServiceId ?? 0) == 0) && (p.Inactive == false || p.Id == criteria.IncludeInactiveId));

                foreach (var data in result)
                {
                    var obj = cMDSubjects_Enums_Bank.GetMDSubjects_Enums_Bank(data);

                    this.Add(obj);
                }
            }
        }
    }
}
