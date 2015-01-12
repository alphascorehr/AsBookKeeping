using System;
using Csla;
using Csla.Data;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using DalEf;
using System.Collections.Generic;

namespace BusinessObjects.Documents
{
	[Serializable]
	public partial class cDocuments_TravelOrder_TravelCosts: CoreBusinessChildClass<cDocuments_TravelOrder_TravelCosts>
	{
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			internal set { SetProperty(IdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > documents_TravelOrderIdProperty = RegisterProperty<System.Int32>(p => p.Documents_TravelOrderId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Documents_TravelOrderId
		{
			get { return GetProperty(documents_TravelOrderIdProperty); }
			set { SetProperty(documents_TravelOrderIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32> ordinalProperty = RegisterProperty<System.Int32>(p => p.Ordinal, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 Ordinal
        {
            get { return GetProperty(ordinalProperty); }
            set { SetProperty(ordinalProperty, value); }
        }

		private static readonly PropertyInfo< System.Int32? > mDPlaces_Enums_Geo_FromPlaceIdProperty = RegisterProperty<System.Int32?>(p => p.MDPlaces_Enums_Geo_FromPlaceId, string.Empty,(System.Int32?)null);
		public System.Int32? MDPlaces_Enums_Geo_FromPlaceId
		{
			get { return GetProperty(mDPlaces_Enums_Geo_FromPlaceIdProperty); }
			set { SetProperty(mDPlaces_Enums_Geo_FromPlaceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDPlaces_Enums_Geo_ToPlaceIdProperty = RegisterProperty<System.Int32?>(p => p.MDPlaces_Enums_Geo_ToPlaceId, string.Empty,(System.Int32?)null);
		public System.Int32? MDPlaces_Enums_Geo_ToPlaceId
		{
			get { return GetProperty(mDPlaces_Enums_Geo_ToPlaceIdProperty); }
			set { SetProperty(mDPlaces_Enums_Geo_ToPlaceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDGeneral_Enums_KindOfTransportationIdProperty = RegisterProperty<System.Int32?>(p => p.MDGeneral_Enums_KindOfTransportationId, string.Empty,(System.Int32?)null);
		public System.Int32? MDGeneral_Enums_KindOfTransportationId
		{
			get { return GetProperty(mDGeneral_Enums_KindOfTransportationIdProperty); }
			set { SetProperty(mDGeneral_Enums_KindOfTransportationIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > kilometersProperty = RegisterProperty<System.Int32?>(p => p.Kilometers, string.Empty,(System.Int32?)null);
		public System.Int32? Kilometers
		{
			get { return GetProperty(kilometersProperty); }
			set { SetProperty(kilometersProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cDocuments_TravelOrder_TravelCosts NewDocuments_TravelOrder_TravelCosts()
		{
			return DataPortal.CreateChild<cDocuments_TravelOrder_TravelCosts>();
		}

        public static cDocuments_TravelOrder_TravelCosts GetDocuments_TravelOrder_TravelCosts(Documents_TravelOrder_TravelCostsCol data)
        {
            return DataPortal.FetchChild<cDocuments_TravelOrder_TravelCosts>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_TravelOrder_TravelCostsCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(documents_TravelOrderIdProperty, data.Documents_TravelOrderId);
            LoadProperty<int>(ordinalProperty, data.Ordinal);
            LoadProperty<int?>(mDPlaces_Enums_Geo_FromPlaceIdProperty, data.MDPlaces_Enums_Geo_FromPlaceId);
            LoadProperty<int?>(mDPlaces_Enums_Geo_ToPlaceIdProperty, data.MDPlaces_Enums_Geo_ToPlaceId);
            LoadProperty<int?>(mDGeneral_Enums_KindOfTransportationIdProperty, data.MDGeneral_Enums_KindOfTransportationId);
            LoadProperty<int?>(kilometersProperty, data.Kilometers);
            
            LastChanged = data.LastChanged;

            ItemLoaded();

            BusinessRules.CheckRules();
        }

        partial void ItemLoaded();

        private void Child_Insert(Documents_TravelOrder parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_TravelOrder_TravelCostsCol();

                data.Documents_TravelOrder = parent;

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.MDPlaces_Enums_Geo_FromPlaceId = ReadProperty<int?>(mDPlaces_Enums_Geo_FromPlaceIdProperty);
                data.MDPlaces_Enums_Geo_ToPlaceId = ReadProperty<int?>(mDPlaces_Enums_Geo_ToPlaceIdProperty);
                data.MDGeneral_Enums_KindOfTransportationId = ReadProperty<int?>(mDGeneral_Enums_KindOfTransportationIdProperty);
                data.Kilometers = ReadProperty<int?>(kilometersProperty);

                ctx.ObjectContext.AddToDocuments_TravelOrder_TravelCostsCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(documents_TravelOrderIdProperty, data.Documents_TravelOrderId);
                        LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                        LastChanged = data.LastChanged;
                    }
                };
            }
        }

        private void Child_Update()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_TravelOrder_TravelCostsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.MDPlaces_Enums_Geo_FromPlaceId = ReadProperty<int?>(mDPlaces_Enums_Geo_FromPlaceIdProperty);
                data.MDPlaces_Enums_Geo_ToPlaceId = ReadProperty<int?>(mDPlaces_Enums_Geo_ToPlaceIdProperty);
                data.MDGeneral_Enums_KindOfTransportationId = ReadProperty<int?>(mDGeneral_Enums_KindOfTransportationIdProperty);
                data.Kilometers = ReadProperty<int?>(kilometersProperty);

                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "LastChanged")
                        LastChanged = data.LastChanged;
                };

            }
        }

        private void Child_DeleteSelf()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_TravelOrder_TravelCostsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);

                ctx.ObjectContext.DeleteObject(data);
            }
        }
        #endregion
    }
    [Serializable]
    public partial class cDocuments_TravelOrder_TravelCostsCol : BusinessListBase<cDocuments_TravelOrder_TravelCostsCol, cDocuments_TravelOrder_TravelCosts>
    {
        internal static cDocuments_TravelOrder_TravelCostsCol NewDocuments_TravelOrder_TravelCostsCol()
        {
            return DataPortal.CreateChild<cDocuments_TravelOrder_TravelCostsCol>();
        }

        public static cDocuments_TravelOrder_TravelCostsCol GetDocuments_TravelOrder_TravelCostsCol(IEnumerable<Documents_TravelOrder_TravelCostsCol> dataSet)
        {
            var childList = new cDocuments_TravelOrder_TravelCostsCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<Documents_TravelOrder_TravelCostsCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cDocuments_TravelOrder_TravelCosts.GetDocuments_TravelOrder_TravelCosts(data));

            RaiseListChangedEvents = true;


        }
        #endregion //Data Access
    }


}

