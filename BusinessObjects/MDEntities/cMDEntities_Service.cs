using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;


namespace BusinessObjects.MDEntities
{
	[Serializable]
	public partial class cMDEntities_Service: cMDEntities_Entity<cMDEntities_Service>
	{
		#region Business Methods
        private static readonly PropertyInfo<System.Decimal?> wholesaleTaskRatePerMonthProperty = RegisterProperty<System.Decimal?>(p => p.WholesaleTaskRatePerMonth, string.Empty, (System.Decimal?)null);
        public System.Decimal? WholesaleTaskRatePerMonth
        {
            get { return GetProperty(wholesaleTaskRatePerMonthProperty); }
            set { SetProperty(wholesaleTaskRatePerMonthProperty, value); }
        }

        private static readonly PropertyInfo<System.Decimal?> wholesaleTaskRatePerDayProperty = RegisterProperty<System.Decimal?>(p => p.WholesaleTaskRatePerDay, string.Empty, (System.Decimal?)null);
        public System.Decimal? WholesaleTaskRatePerDay
        {
            get { return GetProperty(wholesaleTaskRatePerDayProperty); }
            set { SetProperty(wholesaleTaskRatePerDayProperty, value); }
        }

        private static readonly PropertyInfo<System.Decimal?> wholesaleTaskRatePerHourProperty = RegisterProperty<System.Decimal?>(p => p.WholesaleTaskRatePerHour, string.Empty, (System.Decimal?)null);
        public System.Decimal? WholesaleTaskRatePerHour
        {
            get { return GetProperty(wholesaleTaskRatePerHourProperty); }
            set { SetProperty(wholesaleTaskRatePerHourProperty, value); }
        }

        private static readonly PropertyInfo<bool?> isBillableProperty = RegisterProperty<bool?>(p => p.IsBillable, string.Empty, (bool?)null);
        public bool? IsBillable
        {
            get { return GetProperty(isBillableProperty); }
            set { SetProperty(isBillableProperty, value); }
        }

        private static readonly PropertyInfo<bool?> allProjectsProperty = RegisterProperty<bool?>(p => p.AllProjects, string.Empty, (bool?)null);
        public bool? AllProjects
        {
            get { return GetProperty(allProjectsProperty); }
            set { SetProperty(allProjectsProperty, value); }
        }

        private static readonly PropertyInfo<System.Decimal> wholesalePriceProperty = RegisterProperty<System.Decimal>(p => p.WholesalePrice, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Decimal WholesalePrice
        {
            get { return GetProperty(wholesalePriceProperty); }
            set { SetProperty(wholesalePriceProperty, value); }
        }
		#endregion

        #region Factory Methods

        public static cMDEntities_Service NewMDEntities_Service()
        {
            return DataPortal.Create<cMDEntities_Service>();

        }

        public static cMDEntities_Service GetMDEntities_Service(int uniqueId)
        {
            return DataPortal.Fetch<cMDEntities_Service>(new SingleCriteria<cMDEntities_Service, int>(uniqueId));
        }

        internal static cMDEntities_Service GetMDEntities_Service(MDEntities_Service data)
        {
            return DataPortal.Fetch<cMDEntities_Service>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDEntities_Service, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = ctx.ObjectContext.MDEntities_Entity.OfType<MDEntities_Service>().First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<Guid>(GUIDProperty, data.GUID);
                LoadProperty<short>(entityTypeProperty, data.EntityType);
                LoadProperty<string>(labelProperty, data.Label);
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<int?>(mDEntities_Enums_EntityGroupIdProperty, data.MDEntities_Enums_EntityGroupId);
                LoadProperty<string>(tagProperty, data.Tag);
                LoadProperty<string>(descriptionProperty, data.Description);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(employeeWhoLastChanedItUserIdProperty, data.EmployeeWhoLastChanedItUserId);
                LoadProperty<int>(taxRateIdProperty, data.TaxRateId);
                LoadProperty<int>(unitIdProperty, data.UnitId);

                LoadProperty<decimal?>(wholesaleTaskRatePerMonthProperty, data.WholesaleTaskRatePerMonth);
                LoadProperty<decimal?>(wholesaleTaskRatePerDayProperty, data.WholesaleTaskRatePerDay);
                LoadProperty<decimal?>(wholesaleTaskRatePerHourProperty, data.WholesaleTaskRatePerHour);
                LoadProperty<bool?>(isBillableProperty, data.IsBillable);
                LoadProperty<bool?>(allProjectsProperty, data.AllProjects);
                LoadProperty<decimal>(wholesalePriceProperty, data.WholesalePrice);
              
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Service();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.EntityType = (short)Common.EntityType.Service;
                data.Name = ReadProperty<string>(nameProperty);
                data.MDEntities_Enums_EntityGroupId = ReadProperty<int?>(mDEntities_Enums_EntityGroupIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Tag = ReadProperty<string>(tagProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItUserId = ReadProperty<int>(employeeWhoLastChanedItUserIdProperty);
                data.TaxRateId = ReadProperty<int>(taxRateIdProperty);
                data.UnitId = ReadProperty<int>(unitIdProperty);

                data.WholesaleTaskRatePerMonth = ReadProperty<decimal?>(wholesaleTaskRatePerMonthProperty);
                data.WholesaleTaskRatePerDay = ReadProperty<decimal?>(wholesaleTaskRatePerDayProperty);
                data.WholesaleTaskRatePerHour = ReadProperty<decimal?>(wholesaleTaskRatePerHourProperty);
                data.IsBillable = ReadProperty<bool?>(isBillableProperty);
                data.AllProjects = ReadProperty<bool?>(allProjectsProperty);
                data.WholesalePrice = ReadProperty<decimal>(wholesalePriceProperty);
               
                ctx.ObjectContext.AddToMDEntities_Entity(data);

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
                var data = new MDEntities_Service();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.EntityType = ReadProperty<short>(entityTypeProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.MDEntities_Enums_EntityGroupId = ReadProperty<int?>(mDEntities_Enums_EntityGroupIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Tag = ReadProperty<string>(tagProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItUserId = ReadProperty<int>(employeeWhoLastChanedItUserIdProperty);
                data.TaxRateId = ReadProperty<int>(taxRateIdProperty);
                data.UnitId = ReadProperty<int>(unitIdProperty);

                data.WholesaleTaskRatePerMonth = ReadProperty<decimal?>(wholesaleTaskRatePerMonthProperty);
                data.WholesaleTaskRatePerDay = ReadProperty<decimal?>(wholesaleTaskRatePerDayProperty);
                data.WholesaleTaskRatePerHour = ReadProperty<decimal?>(wholesaleTaskRatePerHourProperty);
                data.IsBillable = ReadProperty<bool?>(isBillableProperty);
                data.AllProjects = ReadProperty<bool?>(allProjectsProperty);
                data.WholesalePrice = ReadProperty<decimal>(wholesalePriceProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
