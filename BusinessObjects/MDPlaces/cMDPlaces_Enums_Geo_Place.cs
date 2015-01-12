using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;

namespace BusinessObjects.MdPlaces
{
	[Serializable]
	public partial class cMDPlaces_Enums_Geo_Place: CoreBusinessClass<cMDPlaces_Enums_Geo_Place>
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
		[System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
			set { SetProperty(nameProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< System.String > zIPCodeProperty = RegisterProperty<System.String>(p => p.ZIPCode, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(5, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String ZIPCode
		{
			get { return GetProperty(zIPCodeProperty); }
			set { SetProperty(zIPCodeProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< System.Int32? > regionIdProperty = RegisterProperty<System.Int32?>(p => p.RegionId, string.Empty,(System.Int32?)null);
		public System.Int32? RegionId
		{
			get { return GetProperty(regionIdProperty); }
			set { SetProperty(regionIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > distanceProperty = RegisterProperty<System.Int32?>(p => p.Distance, string.Empty,(System.Int32?)null);
		public System.Int32? Distance
		{
			get { return GetProperty(distanceProperty); }
			set { SetProperty(distanceProperty, value); }
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

        public static cMDPlaces_Enums_Geo_Place NewMDPlaces_Enums_Geo_Place()
        {
            return DataPortal.Create<cMDPlaces_Enums_Geo_Place>();

        }

        public static cMDPlaces_Enums_Geo_Place GetMDPlaces_Enums_Geo_Place(int uniqueId)
        {
            return DataPortal.Fetch<cMDPlaces_Enums_Geo_Place>(new SingleCriteria<cMDPlaces_Enums_Geo_Place, int>(uniqueId));
        }

        internal static cMDPlaces_Enums_Geo_Place GetMDPlaces_Enums_Geo_Place(MDPlaces_Enums_Geo_Place data)
        {
            return DataPortal.Fetch<cMDPlaces_Enums_Geo_Place>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDPlaces_Enums_Geo_Place, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var data = ctx.ObjectContext.MDPlaces_Enums_Geo_Place.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<string>(zIPCodeProperty, data.ZIPCode);
                LoadProperty<int?>(regionIdProperty, data.RegionId);
                LoadProperty<int?>(distanceProperty, data.Distance);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDPlaces_Enums_Geo_Place data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<string>(zIPCodeProperty, data.ZIPCode);
            LoadProperty<int?>(regionIdProperty, data.RegionId);
            LoadProperty<int?>(distanceProperty, data.Distance);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
            MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var data = new MDPlaces_Enums_Geo_Place();

                data.Name = ReadProperty<string>(nameProperty);
                data.ZIPCode = ReadProperty<string>(zIPCodeProperty);
                data.RegionId = ReadProperty<int?>(regionIdProperty);
                data.Distance = ReadProperty<int?>(distanceProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToMDPlaces_Enums_Geo_Place(data);
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
                var data = new MDPlaces_Enums_Geo_Place();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Name = ReadProperty<string>(nameProperty);
                data.ZIPCode = ReadProperty<string>(zIPCodeProperty);
                data.RegionId = ReadProperty<int?>(regionIdProperty);
                data.Distance = ReadProperty<int?>(distanceProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }

    public partial class cMDPlaces_Enums_Geo_Place_List : BusinessListBase<cMDPlaces_Enums_Geo_Place_List, cMDPlaces_Enums_Geo_Place>
    {
        public static cMDPlaces_Enums_Geo_Place_List GetcMDPlaces_Enums_Geo_Place_List()
        {
            return DataPortal.Fetch<cMDPlaces_Enums_Geo_Place_List>();
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var result = ctx.ObjectContext.MDPlaces_Enums_Geo_Place;

                foreach (var data in result)
                {
                    var obj = cMDPlaces_Enums_Geo_Place.GetMDPlaces_Enums_Geo_Place(data);

                    this.Add(obj);
                }
            }
        }
    }
}
