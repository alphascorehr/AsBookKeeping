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


namespace BusinessObjects.MDEntities
{
	[Serializable]
	public partial class cMDEntities_Enums_TaxRate: CoreBusinessClass<cMDEntities_Enums_TaxRate>
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

		private static readonly PropertyInfo< System.String > nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
            set { SetProperty(nameProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Decimal > percentageProperty = RegisterProperty<System.Decimal>(p => p.Percentage, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal Percentage
		{
			get { return GetProperty(percentageProperty); }
			set { SetProperty(percentageProperty, value); }
		}

		private static readonly PropertyInfo< System.String > descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
		public System.String Description
		{
			get { return GetProperty(descriptionProperty); }
            set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< bool > inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public bool Inactive
		{
			get { return GetProperty(inactiveProperty); }
			set { SetProperty(inactiveProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime > lastActivityDateProperty = RegisterProperty<System.DateTime>(p => p.LastActivityDate, string.Empty,System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime LastActivityDate
		{
			get { return GetProperty(lastActivityDateProperty); }
			set { SetProperty(lastActivityDateProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > employeeWhoLastChanedItUserIdProperty = RegisterProperty<System.Int32>(p => p.EmployeeWhoLastChanedItUserId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 EmployeeWhoLastChanedItUserId
		{
			get { return GetProperty(employeeWhoLastChanedItUserIdProperty); }
			set { SetProperty(employeeWhoLastChanedItUserIdProperty, value); }
		}

        protected static readonly PropertyInfo<System.Int32?> companyUsingServiceIdProperty = RegisterProperty<System.Int32?>(p => p.CompanyUsingServiceId, string.Empty);
        public System.Int32? CompanyUsingServiceId
        {
            get { return GetProperty(companyUsingServiceIdProperty); }
            set { SetProperty(companyUsingServiceIdProperty, value); }
        }

        private static readonly PropertyInfo<bool> isServiceProperty = RegisterProperty<bool>(p => p.IsService, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool IsService
        {
            get { return GetProperty(isServiceProperty); }
            set { SetProperty(isServiceProperty, value); }
        }

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#endregion

        #region Factory Methods

        public static cMDEntities_Enums_TaxRate NewMDEntities_Enums_TaxRate()
        {
            return DataPortal.Create<cMDEntities_Enums_TaxRate>();

        }

        public static cMDEntities_Enums_TaxRate GetMDEntities_Enums_TaxRate(int uniqueId)
        {
            return DataPortal.Fetch<cMDEntities_Enums_TaxRate>(new SingleCriteria<cMDEntities_Enums_TaxRate, int>(uniqueId));
        }

        internal static cMDEntities_Enums_TaxRate GetMDEntities_Enums_TaxRate(MDEntities_Enums_TaxRate data)
        {
            return DataPortal.Fetch<cMDEntities_Enums_TaxRate>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDEntities_Enums_TaxRate, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = ctx.ObjectContext.MDEntities_Enums_TaxRate.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<decimal>(percentageProperty, data.Percentage);
                LoadProperty<string>(descriptionProperty, data.Description);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(employeeWhoLastChanedItUserIdProperty, data.EmployeeWhoLastChanedItUserId);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<bool>(isServiceProperty, data.IsService);

                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDEntities_Enums_TaxRate data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<decimal>(percentageProperty, data.Percentage);
            LoadProperty<string>(descriptionProperty, data.Description);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
            LoadProperty<int>(employeeWhoLastChanedItUserIdProperty, data.EmployeeWhoLastChanedItUserId);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LoadProperty<bool>(isServiceProperty, data.IsService);

            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
            MarkAsChild();

        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Enums_TaxRate();

                data.Name = ReadProperty<string>(nameProperty);
                data.Percentage = ReadProperty<decimal>(percentageProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItUserId = ReadProperty<int>(employeeWhoLastChanedItUserIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
                data.IsService = ReadProperty<bool>(isServiceProperty);

                ctx.ObjectContext.AddToMDEntities_Enums_TaxRate(data);

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
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Enums_TaxRate();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Name = ReadProperty<string>(nameProperty);
                data.Percentage = ReadProperty<decimal>(percentageProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItUserId = ReadProperty<int>(employeeWhoLastChanedItUserIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
                data.IsService = ReadProperty<bool>(isServiceProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }

    public partial class cMDEntities_Enums_TaxRate_List : BusinessListBase<cMDEntities_Enums_TaxRate_List, cMDEntities_Enums_TaxRate>
    {
        public static cMDEntities_Enums_TaxRate_List GetcMDEntities_Enums_TaxRate_List()
        {
            return DataPortal.Fetch<cMDEntities_Enums_TaxRate_List>();
        }

        public static cMDEntities_Enums_TaxRate_List GetcMDEntities_Enums_TaxRate_List(int companyId, int includeInactiveId)
        {
            return DataPortal.Fetch<cMDEntities_Enums_TaxRate_List>(new ActiveEnums_Criteria(companyId, includeInactiveId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var result = ctx.ObjectContext.MDEntities_Enums_TaxRate;

                foreach (var data in result)
                {
                    var obj = cMDEntities_Enums_TaxRate.GetMDEntities_Enums_TaxRate(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(ActiveEnums_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var result = ctx.ObjectContext.MDEntities_Enums_TaxRate.Where(p => (p.CompanyUsingServiceId == criteria.CompanyId || (p.CompanyUsingServiceId ?? 0) == 0) && (p.Inactive == false || p.Id == criteria.IncludeInactiveId));

                foreach (var data in result)
                {
                    var obj = cMDEntities_Enums_TaxRate.GetMDEntities_Enums_TaxRate(data);

                    this.Add(obj);
                }
            }
        }
    }
}

