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
	public partial class cMDEntities_Enums_Unit: BusinessObjects.CoreBusinessClasses.CoreBusinessClass<cMDEntities_Enums_Unit>
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

        private static readonly PropertyInfo<System.String> nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String Name
        {
            get { return GetProperty(nameProperty); }
            set { SetProperty(nameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> labelProperty = RegisterProperty<System.String>(p => p.Label, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(10, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String Label
        {
            get { return GetProperty(labelProperty); }
            set { SetProperty(labelProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<bool> inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Inactive
        {
            get { return GetProperty(inactiveProperty); }
            set { SetProperty(inactiveProperty, value); }
        }

        private static readonly PropertyInfo<System.DateTime> lastActivityDateProperty = RegisterProperty<System.DateTime>(p => p.LastActivityDate, string.Empty, System.DateTime.Now);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.DateTime LastActivityDate
        {
            get { return GetProperty(lastActivityDateProperty); }
            set { SetProperty(lastActivityDateProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32> employeeWhoLastChanedItUserIdProperty = RegisterProperty<System.Int32>(p => p.EmployeeWhoLastChanedItUserId, string.Empty);
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

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#endregion


        #region Factory Methods

        public static cMDEntities_Enums_Unit NewMDEntities_Enums_Unit()
        {
            return DataPortal.Create<cMDEntities_Enums_Unit>();

        }

        public static cMDEntities_Enums_Unit GetMDEntities_Enums_Unit(int uniqueId)
        {
            return DataPortal.Fetch<cMDEntities_Enums_Unit>(new SingleCriteria<cMDEntities_Enums_Unit, int>(uniqueId));
        }

        internal static cMDEntities_Enums_Unit GetMDEntities_Enums_Unit(MDEntities_Enums_Unit data)
        {
            return DataPortal.Fetch<cMDEntities_Enums_Unit>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDEntities_Enums_Unit, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = ctx.ObjectContext.MDEntities_Enums_Unit.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<string>(labelProperty, data.Label);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(employeeWhoLastChanedItUserIdProperty, data.EmployeeWhoLastChanedItUserId);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDEntities_Enums_Unit data)
        {
         
                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<string>(labelProperty, data.Label);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(employeeWhoLastChanedItUserIdProperty, data.EmployeeWhoLastChanedItUserId);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();
                MarkAsChild();

        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Enums_Unit();

                data.Name = ReadProperty<string>(nameProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItUserId = ReadProperty<int>(employeeWhoLastChanedItUserIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
                
                ctx.ObjectContext.AddToMDEntities_Enums_Unit(data);

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
                var data = new MDEntities_Enums_Unit();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Name = ReadProperty<string>(nameProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItUserId = ReadProperty<int>(employeeWhoLastChanedItUserIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}

    public partial class cMDEntities_Enums_Unit_List : BusinessListBase<cMDEntities_Enums_Unit_List, cMDEntities_Enums_Unit>
    {
        public static cMDEntities_Enums_Unit_List GetcMDEntities_Enums_Unit_List()
        {
            return DataPortal.Fetch<cMDEntities_Enums_Unit_List>();
        }

        public static cMDEntities_Enums_Unit_List GetcMDEntities_Enums_Unit_List(int companyId, int includeInactiveId)
        {
            return DataPortal.Fetch<cMDEntities_Enums_Unit_List>(new ActiveEnums_Criteria(companyId, includeInactiveId));
        }


        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var result = ctx.ObjectContext.MDEntities_Enums_Unit;

                foreach (var data in result)
                {
                    var obj = cMDEntities_Enums_Unit.GetMDEntities_Enums_Unit(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(ActiveEnums_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var result = ctx.ObjectContext.MDEntities_Enums_Unit.Where(p => (p.CompanyUsingServiceId == criteria.CompanyId || (p.CompanyUsingServiceId ?? 0) == 0) && (p.Inactive == false || p.Id == criteria.IncludeInactiveId));

                foreach (var data in result)
                {
                    var obj = cMDEntities_Enums_Unit.GetMDEntities_Enums_Unit(data);

                    this.Add(obj);
                }
            }
        }
    }
}
