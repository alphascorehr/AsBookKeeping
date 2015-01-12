using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;

namespace BusinessObjects.Documents
{
	[Serializable]
	public partial class cDocuments_Dispatch: cDocuments_Document<cDocuments_Dispatch>
	{
		#region Business Methods

		private static readonly PropertyInfo< System.Int32 > subjectIdProperty = RegisterProperty<System.Int32>(p => p.SubjectId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 SubjectId
		{
			get { return GetProperty(subjectIdProperty); }
			set { SetProperty(subjectIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > shipToAddress_PlaceIdProperty = RegisterProperty<System.Int32?>(p => p.ShipToAddress_PlaceId, string.Empty,(System.Int32?)null);
		public System.Int32? ShipToAddress_PlaceId
		{
			get { return GetProperty(shipToAddress_PlaceIdProperty); }
			set { SetProperty(shipToAddress_PlaceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > shipToAddress_StreetProperty = RegisterProperty<System.String>(p => p.ShipToAddress_Street, string.Empty, (System.String)null);
		public System.String ShipToAddress_Street
		{
			get { return GetProperty(shipToAddress_StreetProperty); }
			set { SetProperty(shipToAddress_StreetProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > shipToAddress_DescriptionProperty = RegisterProperty<System.String>(p => p.ShipToAddress_Description, string.Empty, (System.String)null);
		public System.String ShipToAddress_Description
		{
			get { return GetProperty(shipToAddress_DescriptionProperty); }
			set { SetProperty(shipToAddress_DescriptionProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Int32? > mDSubjects_Employee_DispatcherIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Employee_DispatcherId, string.Empty,(System.Int32?)null);
		public System.Int32? MDSubjects_Employee_DispatcherId
		{
			get { return GetProperty(mDSubjects_Employee_DispatcherIdProperty); }
			set { SetProperty(mDSubjects_Employee_DispatcherIdProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime? > shippingDateProperty = RegisterProperty<System.DateTime?>(p => p.ShippingDate, string.Empty, (System.DateTime?)null);
		public System.DateTime? ShippingDate
		{
			get { return GetProperty(shippingDateProperty); }
			set { SetProperty(shippingDateProperty, value); }
		}

		private static readonly PropertyInfo< System.String > personReceivedProperty = RegisterProperty<System.String>(p => p.PersonReceived, string.Empty, (System.String)null);
		public System.String PersonReceived
		{
			get { return GetProperty(personReceivedProperty); }
			set { SetProperty(personReceivedProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Int32? > dispatchTypeIdProperty = RegisterProperty<System.Int32?>(p => p.DispatchTypeId, string.Empty,(System.Int32?)null);
		public System.Int32? DispatchTypeId
		{
			get { return GetProperty(dispatchTypeIdProperty); }
			set { SetProperty(dispatchTypeIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > dispatchCompanyIdProperty = RegisterProperty<System.Int32?>(p => p.DispatchCompanyId, string.Empty,(System.Int32?)null);
		public System.Int32? DispatchCompanyId
		{
			get { return GetProperty(dispatchCompanyIdProperty); }
			set { SetProperty(dispatchCompanyIdProperty, value); }
		}

		private static readonly PropertyInfo< bool? > allShippedProperty = RegisterProperty<bool?>(p => p.AllShipped, string.Empty,(bool?)null);
		public bool? AllShipped
		{
			get { return GetProperty(allShippedProperty); }
			set { SetProperty(allShippedProperty, value); }
		}
        private static readonly PropertyInfo<System.Int16?> statusProperty = RegisterProperty<System.Int16?>(p => p.Status, string.Empty, (System.Int16?)null);
        public System.Int16? Status
        {
            get { return GetProperty(statusProperty); }
            set { SetProperty(statusProperty, value); }
        }
		#endregion

        #region Factory Methods

        public static cDocuments_Dispatch NewDocuments_Dispatch()
        {
            return DataPortal.Create<cDocuments_Dispatch>();

        }

        public static cDocuments_Dispatch GetDocuments_Dispatch(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_Dispatch>(new SingleCriteria<cDocuments_Dispatch, int>(uniqueId));
        }

        internal static cDocuments_Dispatch GetDocuments_Dispatch(Documents_Dispatch data)
        {
            return DataPortal.Fetch<cDocuments_Dispatch>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_Dispatch, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_Dispatch>().First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<Guid>(GUIDProperty, data.GUID);
                LoadProperty<short>(documentTypeProperty, data.DocumentType);
                LoadProperty<string>(uniqueIdentifierProperty, data.UniqueIdentifier);
                LoadProperty<string>(barcodeProperty, data.Barcode);
                LoadProperty<int>(ordinalNumberProperty, data.OrdinalNumber);
                LoadProperty<int>(ordinalNumberInYearProperty, data.OrdinalNumberInYear);
                LoadProperty<DateTime>(documentDateTimeProperty, data.DocumentDateTime);
                LoadProperty<DateTime>(creationDateTimeProperty, data.CreationDateTime);
                LoadProperty<int>(mDSubjects_Employee_AuthorIdProperty, data.MDSubjects_Employee_AuthorId);
                LoadProperty<string>(descriptionProperty, data.Description);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);

                LoadProperty<int>(subjectIdProperty, data.SubjectId);
                LoadProperty<int?>(shipToAddress_PlaceIdProperty, data.ShipToAddress_PlaceId);
                LoadProperty<string>(shipToAddress_StreetProperty, data.ShipToAddress_Street);
                LoadProperty<string>(shipToAddress_DescriptionProperty, data.ShipToAddress_Description);
                LoadProperty<int?>(mDSubjects_Employee_DispatcherIdProperty, data.MDSubjects_Employee_DispatcherId);
                LoadProperty<DateTime?>(shippingDateProperty, data.ShippingDate);
                LoadProperty<string>(personReceivedProperty, data.PersonReceived);
                LoadProperty<int?>(dispatchTypeIdProperty, data.DispatchTypeId);
                LoadProperty<int?>(dispatchCompanyIdProperty, data.DispatchCompanyId);
                LoadProperty<bool?>(allShippedProperty, data.AllShipped);
                LoadProperty<short?>(statusProperty, data.Status);

                LastChanged = data.LastChanged;

                LoadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty, cDocuments_ItemsCol.GetDocuments_ItemsCol(data.Documents_ItemsCol));

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_Dispatch();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.DocumentType = ReadProperty<short>(documentTypeProperty);
                data.UniqueIdentifier = ReadProperty<string>(uniqueIdentifierProperty);
                data.Barcode = ReadProperty<string>(barcodeProperty);
                data.OrdinalNumber = ReadProperty<int>(ordinalNumberProperty);
                data.OrdinalNumberInYear = ReadProperty<int>(ordinalNumberInYearProperty);
                data.DocumentDateTime = ReadProperty<DateTime>(documentDateTimeProperty);
                data.CreationDateTime = ReadProperty<DateTime>(creationDateTimeProperty);
                data.MDSubjects_Employee_AuthorId = ReadProperty<int>(mDSubjects_Employee_AuthorIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);

                data.SubjectId = ReadProperty<int>(subjectIdProperty);
                data.ShipToAddress_PlaceId = ReadProperty<int?>(shipToAddress_PlaceIdProperty);
                data.ShipToAddress_Street = ReadProperty<string>(shipToAddress_StreetProperty);
                data.ShipToAddress_Description = ReadProperty<string>(shipToAddress_DescriptionProperty);
                data.MDSubjects_Employee_DispatcherId = ReadProperty<int?>(mDSubjects_Employee_DispatcherIdProperty);
                data.ShippingDate = ReadProperty<DateTime?>(shippingDateProperty);
                data.PersonReceived = ReadProperty<string>(personReceivedProperty);
                data.DispatchTypeId = ReadProperty<int?>(dispatchTypeIdProperty);
                data.DispatchCompanyId = ReadProperty<int?>(dispatchCompanyIdProperty);
                data.AllShipped = ReadProperty<bool?>(allShippedProperty);
                data.Status = ReadProperty<short?>(statusProperty);

                ctx.ObjectContext.AddToDocuments_Document(data);

                DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);

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
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_Dispatch();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.DocumentType = ReadProperty<short>(documentTypeProperty);
                data.UniqueIdentifier = ReadProperty<string>(uniqueIdentifierProperty);
                data.Barcode = ReadProperty<string>(barcodeProperty);
                data.OrdinalNumber = ReadProperty<int>(ordinalNumberProperty);
                data.OrdinalNumberInYear = ReadProperty<int>(ordinalNumberInYearProperty);
                data.DocumentDateTime = ReadProperty<DateTime>(documentDateTimeProperty);
                data.CreationDateTime = ReadProperty<DateTime>(creationDateTimeProperty);
                data.MDSubjects_Employee_AuthorId = ReadProperty<int>(mDSubjects_Employee_AuthorIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);

                data.SubjectId = ReadProperty<int>(subjectIdProperty);
                data.ShipToAddress_PlaceId = ReadProperty<int?>(shipToAddress_PlaceIdProperty);
                data.ShipToAddress_Street = ReadProperty<string>(shipToAddress_StreetProperty);
                data.ShipToAddress_Description = ReadProperty<string>(shipToAddress_DescriptionProperty);
                data.MDSubjects_Employee_DispatcherId = ReadProperty<int?>(mDSubjects_Employee_DispatcherIdProperty);
                data.ShippingDate = ReadProperty<DateTime?>(shippingDateProperty);
                data.PersonReceived = ReadProperty<string>(personReceivedProperty);
                data.DispatchTypeId = ReadProperty<int?>(dispatchTypeIdProperty);
                data.DispatchCompanyId = ReadProperty<int?>(dispatchCompanyIdProperty);
                data.AllShipped = ReadProperty<bool?>(allShippedProperty);
                data.Status = ReadProperty<short?>(statusProperty);

                DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }
}
