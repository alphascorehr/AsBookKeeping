using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;

namespace BusinessObjects.MDPlaces
{
	[Serializable]
	public partial class cMDPlaces_Enums_OgranizationalUnit: CoreBusinessClass<cMDPlaces_Enums_OgranizationalUnit>
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

		private static readonly PropertyInfo< System.Int32 > companyUsingServiceIdProperty = RegisterProperty<System.Int32>(p => p.CompanyUsingServiceId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 CompanyUsingServiceId
		{
			get { return GetProperty(companyUsingServiceIdProperty); }
			set { SetProperty(companyUsingServiceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > labelProperty = RegisterProperty<System.String>(p => p.Label, string.Empty, (System.String)null);
		public System.String Label
		{
			get { return GetProperty(labelProperty); }
			set { SetProperty(labelProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< System.String > nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
			set { SetProperty(nameProperty, value.Trim()); }
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

		private static readonly PropertyInfo< System.Int32 > userWhoLastChanedItUserIdProperty = RegisterProperty<System.Int32>(p => p.UserWhoLastChanedItUserId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 UserWhoLastChanedItUserId
		{
			get { return GetProperty(userWhoLastChanedItUserIdProperty); }
			set { SetProperty(userWhoLastChanedItUserIdProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#endregion

        #region Factory Methods

        public static cMDPlaces_Enums_OgranizationalUnit NewMDPlaces_Enums_OgranizationalUnit()
        {
            return DataPortal.Create<cMDPlaces_Enums_OgranizationalUnit>();

        }

        public static cMDPlaces_Enums_OgranizationalUnit GetMDPlaces_Enums_OgranizationalUnit(int uniqueId)
        {
            return DataPortal.Fetch<cMDPlaces_Enums_OgranizationalUnit>(new SingleCriteria<cMDPlaces_Enums_OgranizationalUnit, int>(uniqueId));
        }

        internal static cMDPlaces_Enums_OgranizationalUnit GetMDPlaces_Enums_OgranizationalUnit(MDPlaces_Enums_OgranizationalUnit data)
        {
            return DataPortal.Fetch<cMDPlaces_Enums_OgranizationalUnit>(data);
        }

        public static void DeleteMDPlaces_Enums_OgranizationalUnit(int uniqueId)
        {
            DataPortal.Delete<cMDPlaces_Enums_OgranizationalUnit>(new SingleCriteria<cMDPlaces_Enums_OgranizationalUnit, int>(uniqueId));
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDPlaces_Enums_OgranizationalUnit, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var data = ctx.ObjectContext.MDPlaces_Enums_OgranizationalUnit.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<string>(labelProperty, data.Label);
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(userWhoLastChanedItUserIdProperty, data.UserWhoLastChanedItUserId);
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDPlaces_Enums_OgranizationalUnit data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LoadProperty<string>(labelProperty, data.Label);
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
            LoadProperty<int>(userWhoLastChanedItUserIdProperty, data.UserWhoLastChanedItUserId);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
            MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var data = new MDPlaces_Enums_OgranizationalUnit();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.UserWhoLastChanedItUserId = ReadProperty<int>(userWhoLastChanedItUserIdProperty);

                ctx.ObjectContext.AddToMDPlaces_Enums_OgranizationalUnit(data);
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
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var data = new MDPlaces_Enums_OgranizationalUnit();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.UserWhoLastChanedItUserId = ReadProperty<int>(userWhoLastChanedItUserIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<cMDPlaces_Enums_OgranizationalUnit, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var data = ctx.ObjectContext.MDPlaces_Enums_OgranizationalUnit.First(p => p.Id == criteria.Value);

                ctx.ObjectContext.MDPlaces_Enums_OgranizationalUnit.DeleteObject(data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }

    public partial class cMDPlaces_Enums_OgranizationalUnit_List : BusinessListBase<cMDPlaces_Enums_OgranizationalUnit_List, cMDPlaces_Enums_OgranizationalUnit>
    {
        public static cMDPlaces_Enums_OgranizationalUnit_List GetcMDPlaces_Enums_OgranizationalUnit_List()
        {
            return DataPortal.Fetch<cMDPlaces_Enums_OgranizationalUnit_List>();
        }

        public static cMDPlaces_Enums_OgranizationalUnit_List GetcMDPlaces_Enums_OgranizationalUnit_List(int companyId)
        {
            return DataPortal.Fetch<cMDPlaces_Enums_OgranizationalUnit_List>(new SingleCriteria<cMDPlaces_Enums_OgranizationalUnit_List, int>(companyId));
        }

        public static cMDPlaces_Enums_OgranizationalUnit_List GetcMDPlaces_Enums_OgranizationalUnit_List(int companyId, int includeInactiveId)
        {
            return DataPortal.Fetch<cMDPlaces_Enums_OgranizationalUnit_List>(new OgranizationalUnit_Criteria(companyId, includeInactiveId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var result = ctx.ObjectContext.MDPlaces_Enums_OgranizationalUnit;

                foreach (var data in result)
                {
                    var obj = cMDPlaces_Enums_OgranizationalUnit.GetMDPlaces_Enums_OgranizationalUnit(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(SingleCriteria<cMDPlaces_Enums_OgranizationalUnit_List, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var result = ctx.ObjectContext.MDPlaces_Enums_OgranizationalUnit.Where(p=> p.CompanyUsingServiceId == criteria.Value);

                foreach (var data in result)
                {
                    var obj = cMDPlaces_Enums_OgranizationalUnit.GetMDPlaces_Enums_OgranizationalUnit(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(OgranizationalUnit_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var result = ctx.ObjectContext.MDPlaces_Enums_OgranizationalUnit.Where(p => p.CompanyUsingServiceId == criteria.CompanyId && (p.Inactive == false || p.Id == criteria.IncludeInactiveId));

                foreach (var data in result)
                {
                    var obj = cMDPlaces_Enums_OgranizationalUnit.GetMDPlaces_Enums_OgranizationalUnit(data);

                    this.Add(obj);
                }
            }
        }
    }
}
