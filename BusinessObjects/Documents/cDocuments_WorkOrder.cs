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
	public partial class cDocuments_WorkOrder: cDocuments_Document<cDocuments_WorkOrder>
	{
		#region Business Methods
	
		private static readonly PropertyInfo< System.Int32? > projects_ProjectIdProperty = RegisterProperty<System.Int32?>(p => p.Projects_ProjectId, string.Empty,(System.Int32?)null);
		public System.Int32? Projects_ProjectId
		{
			get { return GetProperty(projects_ProjectIdProperty); }
			set { SetProperty(projects_ProjectIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDSubjects_SubjectIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_SubjectId, string.Empty);
		public System.Int32? MDSubjects_SubjectId
		{
			get { return GetProperty(mDSubjects_SubjectIdProperty); }
			set { SetProperty(mDSubjects_SubjectIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > documents_OrderFormIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_OrderFormId, string.Empty,(System.Int32?)null);
		public System.Int32? Documents_OrderFormId
		{
			get { return GetProperty(documents_OrderFormIdProperty); }
			set { SetProperty(documents_OrderFormIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > placeIdProperty = RegisterProperty<System.Int32?>(p => p.PlaceId, string.Empty);
		public System.Int32? PlaceId
		{
			get { return GetProperty(placeIdProperty); }
			set { SetProperty(placeIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > documents_TravelOrderIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_TravelOrderId, string.Empty,(System.Int32?)null);
		public System.Int32? Documents_TravelOrderId
		{
			get { return GetProperty(documents_TravelOrderIdProperty); }
			set { SetProperty(documents_TravelOrderIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > distanceProperty = RegisterProperty<System.Decimal?>(p => p.Distance, string.Empty, (System.Decimal?)null);
		public System.Decimal? Distance
		{
			get { return GetProperty(distanceProperty); }
			set { SetProperty(distanceProperty, value); }
		}

		private static readonly PropertyInfo< bool? > isVerifiedProperty = RegisterProperty<bool?>(p => p.IsVerified, string.Empty,(bool?)null);
		public bool? IsVerified
		{
			get { return GetProperty(isVerifiedProperty); }
			set { SetProperty(isVerifiedProperty, value); }
		}

		private static readonly PropertyInfo< System.String > verifierNameProperty = RegisterProperty<System.String>(p => p.VerifierName, string.Empty, (System.String)null);
		public System.String VerifierName
		{
			get { return GetProperty(verifierNameProperty); }
			set { SetProperty(verifierNameProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > verifierPhoneProperty = RegisterProperty<System.String>(p => p.VerifierPhone, string.Empty, (System.String)null);
		public System.String VerifierPhone
		{
			get { return GetProperty(verifierPhoneProperty); }
			set { SetProperty(verifierPhoneProperty, (value ?? "").Trim()); }
		}

        private static readonly PropertyInfo<bool> isBillableProperty = RegisterProperty<bool>(p => p.IsBillable, string.Empty, true);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool IsBillable
        {
            get { return GetProperty(isBillableProperty); }
            set { SetProperty(isBillableProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> documents_InvoiceIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_InvoiceId, string.Empty, (System.Int32?)null);
        public System.Int32? Documents_InvoiceId
		{
            get { return GetProperty(documents_InvoiceIdProperty); }
            set { SetProperty(documents_InvoiceIdProperty, value); }
		}

        private static readonly PropertyInfo<bool?> invoicedProperty = RegisterProperty<bool?>(p => p.Invoiced, string.Empty);
              public bool? Invoiced
        {
            get { return GetProperty(invoicedProperty); }
            set { SetProperty(invoicedProperty, value); }
        }

        private static readonly PropertyInfo<System.Int16?> statusProperty = RegisterProperty<System.Int16?>(p => p.Status, string.Empty, (System.Int16?)null);
        public System.Int16? Status
        {
            get { return GetProperty(statusProperty); }
            set { SetProperty(statusProperty, value); }
        }

        

		#endregion

        #region Factory Methods

        public static cDocuments_WorkOrder NewDocuments_WorkOrder()
        {
            return DataPortal.Create<cDocuments_WorkOrder>();

        }

        public static cDocuments_WorkOrder GetDocuments_WorkOrder(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_WorkOrder>(new SingleCriteria<cDocuments_WorkOrder, int>(uniqueId));
        }

        internal static cDocuments_WorkOrder GetDocuments_WorkOrder(Documents_WorkOrder data)
        {
            return DataPortal.Fetch<cDocuments_WorkOrder>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_WorkOrder, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_WorkOrder>().First(p => p.Id == criteria.Value);

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

                LoadProperty<int?>(projects_ProjectIdProperty, data.Projects_ProjectId);
                LoadProperty<int?>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
                LoadProperty<int?>(documents_OrderFormIdProperty, data.Documents_OrderFormId);
                LoadProperty<int?>(placeIdProperty, data.PlaceId);
                LoadProperty<int?>(documents_TravelOrderIdProperty, data.Documents_TravelOrderId);
                LoadProperty<decimal?>(distanceProperty, data.Distance);
                LoadProperty<bool?>(isVerifiedProperty, data.IsVerified);
                LoadProperty<string>(verifierNameProperty, data.VerifierName);
                LoadProperty<string>(verifierPhoneProperty, data.VerifierPhone);
                LoadProperty<bool>(isBillableProperty, data.IsBillable);
                LoadProperty<int?>(documents_InvoiceIdProperty, data.Documents_InvoiceId);
                LoadProperty<bool?>(invoicedProperty, data.Invoiced);
                LoadProperty<short?>(statusProperty, data.Status);
            
                LastChanged = data.LastChanged;

                //LoadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty, cDocuments_ItemsCol.GetDocuments_ItemsCol(data.Documents_ItemsCol));
             

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_WorkOrder();

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

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.Documents_OrderFormId = ReadProperty<int?>(documents_OrderFormIdProperty);
                data.PlaceId = ReadProperty<int?>(placeIdProperty);
                data.Documents_TravelOrderId = ReadProperty<int?>(documents_TravelOrderIdProperty);
                data.Distance = ReadProperty<decimal?>(distanceProperty);
                data.IsVerified = ReadProperty<bool?>(isVerifiedProperty);
                data.VerifierName = ReadProperty<string>(verifierNameProperty);
                data.VerifierPhone = ReadProperty<string>(verifierPhoneProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_InvoiceId = ReadProperty<int?>(documents_InvoiceIdProperty);
                data.Invoiced = ReadProperty<bool?>(invoicedProperty);
                data.Status = ReadProperty<short?>(statusProperty);

                ctx.ObjectContext.AddToDocuments_Document(data);

                //DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
               

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
                var data = new Documents_WorkOrder();

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

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.Documents_OrderFormId = ReadProperty<int?>(documents_OrderFormIdProperty);
                data.PlaceId = ReadProperty<int?>(placeIdProperty);
                data.Documents_TravelOrderId = ReadProperty<int?>(documents_TravelOrderIdProperty);
                data.Distance = ReadProperty<decimal?>(distanceProperty);
                data.IsVerified = ReadProperty<bool?>(isVerifiedProperty);
                data.VerifierName = ReadProperty<string>(verifierNameProperty);
                data.VerifierPhone = ReadProperty<string>(verifierPhoneProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_InvoiceId = ReadProperty<int?>(documents_InvoiceIdProperty);
                data.Invoiced = ReadProperty<bool?>(invoicedProperty);
                data.Status = ReadProperty<short?>(statusProperty);

                //DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
             

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
