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
	public partial class cMDPlaces_Enums_Geo_Region: CoreBusinessClass<cMDPlaces_Enums_Geo_Region>
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
			set { SetProperty(nameProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< System.Int32 > countryIdProperty = RegisterProperty<System.Int32>(p => p.CountryId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 CountryId
		{
			get { return GetProperty(countryIdProperty); }
			set { SetProperty(countryIdProperty, value); }
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

        public static cMDPlaces_Enums_Geo_Region NewMDPlaces_Enums_Geo_Region()
        {
            return DataPortal.Create<cMDPlaces_Enums_Geo_Region>();

        }

        public static cMDPlaces_Enums_Geo_Region GetMDPlaces_Enums_Geo_Region(int uniqueId)
        {
            return DataPortal.Fetch<cMDPlaces_Enums_Geo_Region>(new SingleCriteria<cMDPlaces_Enums_Geo_Region, int>(uniqueId));
        }

        internal static cMDPlaces_Enums_Geo_Region GetMDPlaces_Enums_Geo_Region(MDPlaces_Enums_Geo_Region data)
        {
            return DataPortal.Fetch<cMDPlaces_Enums_Geo_Region>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDPlaces_Enums_Geo_Region, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var data = ctx.ObjectContext.MDPlaces_Enums_Geo_Region.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<int>(countryIdProperty, data.CountryId);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDPlaces_Enums_Geo_Region data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<int>(countryIdProperty, data.CountryId);
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
                var data = new MDPlaces_Enums_Geo_Region();

                data.Name = ReadProperty<string>(nameProperty);
                data.CountryId = ReadProperty<int>(countryIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToMDPlaces_Enums_Geo_Region(data);
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
                var data = new MDPlaces_Enums_Geo_Region();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Name = ReadProperty<string>(nameProperty);
                data.CountryId = ReadProperty<int>(countryIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }

    public partial class cMDPlaces_Enums_Geo_Region_List : BusinessListBase<cMDPlaces_Enums_Geo_Region_List, cMDPlaces_Enums_Geo_Region>
    {
        public static cMDPlaces_Enums_Geo_Region_List GetcMDPlaces_Enums_Geo_Region_List()
        {
            return DataPortal.Fetch<cMDPlaces_Enums_Geo_Region_List>();
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDPlacesEntities>.GetManager("MDPlacesEntities"))
            {
                var result = ctx.ObjectContext.MDPlaces_Enums_Geo_Region;

                foreach (var data in result)
                {
                    var obj = cMDPlaces_Enums_Geo_Region.GetMDPlaces_Enums_Geo_Region(data);

                    this.Add(obj);
                }
            }
        }
    }
}

