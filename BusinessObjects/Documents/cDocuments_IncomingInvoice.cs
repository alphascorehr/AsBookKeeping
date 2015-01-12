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
	public partial class cDocuments_IncomingInvoice: cDocuments_Document<cDocuments_IncomingInvoice>
	{
		#region Business Methods
	
		private static readonly PropertyInfo< System.Int32 > subjectIdProperty = RegisterProperty<System.Int32>(p => p.SubjectId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 SubjectId
		{
			get { return GetProperty(subjectIdProperty); }
			set { SetProperty(subjectIdProperty, value); }
		}

        private static readonly PropertyInfo<System.DateTime> maturityDateProperty = RegisterProperty<System.DateTime>(p => p.MaturityDate, string.Empty);
		//[System.ComponentModel.DataAnnotations.StringLength(10, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.DateTime MaturityDate
		{
			get { return GetProperty(maturityDateProperty); }
            set { SetProperty(maturityDateProperty, value); }
		}

		private static readonly PropertyInfo< bool? > reversedProperty = RegisterProperty<bool?>(p => p.Reversed, string.Empty,(bool?)null);
		public bool? Reversed
		{
			get { return GetProperty(reversedProperty); }
			set { SetProperty(reversedProperty, value); }
		}

		private static readonly PropertyInfo< bool? > payedInFullProperty = RegisterProperty<bool?>(p => p.PayedInFull, string.Empty,(bool?)null);
		public bool? PayedInFull
		{
			get { return GetProperty(payedInFullProperty); }
			set { SetProperty(payedInFullProperty, value); }
		}

		private static readonly PropertyInfo< bool? > acceptedProperty = RegisterProperty<bool?>(p => p.Accepted, string.Empty,(bool?)null);
		public bool? Accepted
		{
			get { return GetProperty(acceptedProperty); }
			set { SetProperty(acceptedProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > wholesaleValueProperty = RegisterProperty<System.Decimal>(p => p.WholesaleValue, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal WholesaleValue
		{
			get { return GetProperty(wholesaleValueProperty); }
			set { SetProperty(wholesaleValueProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > taxValueProperty = RegisterProperty<System.Decimal>(p => p.TaxValue, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal TaxValue
		{
			get { return GetProperty(taxValueProperty); }
			set { SetProperty(taxValueProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > rabateValueProperty = RegisterProperty<System.Decimal?>(p => p.RabateValue, string.Empty, (System.Decimal?)null);
		public System.Decimal? RabateValue
		{
			get { return GetProperty(rabateValueProperty); }
			set { SetProperty(rabateValueProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > otherDiscountsValueProperty = RegisterProperty<System.Decimal?>(p => p.OtherDiscountsValue, string.Empty, (System.Decimal?)null);
		public System.Decimal? OtherDiscountsValue
		{
			get { return GetProperty(otherDiscountsValueProperty); }
			set { SetProperty(otherDiscountsValueProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > retailValueProperty = RegisterProperty<System.Decimal>(p => p.RetailValue, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal RetailValue
		{
			get { return GetProperty(retailValueProperty); }
			set { SetProperty(retailValueProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > scontoPercentageProperty = RegisterProperty<System.Decimal?>(p => p.ScontoPercentage, string.Empty, (System.Decimal?)null);
		public System.Decimal? ScontoPercentage
		{
			get { return GetProperty(scontoPercentageProperty); }
			set { SetProperty(scontoPercentageProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime? > scontoDateProperty = RegisterProperty<System.DateTime?>(p => p.ScontoDate, string.Empty, (System.DateTime?)null);
		public System.DateTime? ScontoDate
		{
			get { return GetProperty(scontoDateProperty); }
			set { SetProperty(scontoDateProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > scontoValueProperty = RegisterProperty<System.Decimal?>(p => p.ScontoValue, string.Empty, (System.Decimal?)null);
		public System.Decimal? ScontoValue
		{
			get { return GetProperty(scontoValueProperty); }
			set { SetProperty(scontoValueProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > retailValueScontoProperty = RegisterProperty<System.Decimal?>(p => p.RetailValueSconto, string.Empty, (System.Decimal?)null);
		public System.Decimal? RetailValueSconto
		{
			get { return GetProperty(retailValueScontoProperty); }
			set { SetProperty(retailValueScontoProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDGeneral_Enums_CurrencyIdProperty = RegisterProperty<System.Int32?>(p => p.MDGeneral_Enums_CurrencyId, string.Empty,(System.Int32?)null);
        //private static readonly PropertyInfo<System.Int32?> mDGeneral_Enums_CurrencyIdProperty = RegisterProperty<System.Int32?>(p => p.MDGeneral_Enums_CurrencyId, string.Empty, 2);
		public System.Int32? MDGeneral_Enums_CurrencyId
		{
			get { return GetProperty(mDGeneral_Enums_CurrencyIdProperty); }
			set { SetProperty(mDGeneral_Enums_CurrencyIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > courseValueProperty = RegisterProperty<System.Decimal?>(p => p.CourseValue, string.Empty, 1);
		public System.Decimal? CourseValue
		{
			get { return GetProperty(courseValueProperty); }
			set { SetProperty(courseValueProperty, value); }
		}

        private static readonly PropertyInfo<bool?> enteredInTheAccountsProperty = RegisterProperty<bool?>(p => p.EnteredInTheAccounts, string.Empty, (bool?)null);
            public bool? EnteredInTheAccounts
		{
            get { return GetProperty(enteredInTheAccountsProperty); }
            set { SetProperty(enteredInTheAccountsProperty, value); }
		}
        private static readonly PropertyInfo<System.Int16?> statusProperty = RegisterProperty<System.Int16?>(p => p.Status, string.Empty, (System.Int16?)null);
        public System.Int16? Status
        {
            get { return GetProperty(statusProperty); }
            set { SetProperty(statusProperty, value); }
        }

        protected static readonly PropertyInfo<System.String> ReferenceNumberProperty = RegisterProperty<System.String>(p => p.ReferenceNumber, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(30, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        public System.String ReferenceNumber
        {
            get { return GetProperty(ReferenceNumberProperty); }
            set { SetProperty(ReferenceNumberProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<System.String> PaymentModelProperty = RegisterProperty<System.String>(p => p.PaymentModel, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(5, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        public System.String PaymentModel
        {
            get { return GetProperty(PaymentModelProperty); }
            set { SetProperty(PaymentModelProperty, (value ?? "").Trim()); }
        }

		#endregion

        #region Factory Methods

        public static cDocuments_IncomingInvoice NewDocuments_IncomingInvoice()
        {
            return DataPortal.Create<cDocuments_IncomingInvoice>();

        }

        public static cDocuments_IncomingInvoice GetDocuments_IncomingInvoice(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_IncomingInvoice>(new SingleCriteria<cDocuments_IncomingInvoice, int>(uniqueId));
        }

        internal static cDocuments_IncomingInvoice GetDocuments_IncomingInvoice(Documents_IncomingInvoice data)
        {
            return DataPortal.Fetch<cDocuments_IncomingInvoice>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_IncomingInvoice, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_IncomingInvoice>().First(p => p.Id == criteria.Value);

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
                LoadProperty<short?>(statusProperty, data.Status);

                LoadProperty<int>(subjectIdProperty, data.SubjectId);
                LoadProperty<DateTime>(maturityDateProperty, data.MaturityDate);
                LoadProperty<bool?>(reversedProperty, data.Reversed);
                LoadProperty<bool?>(payedInFullProperty, data.PayedInFull);
                LoadProperty<bool?>(acceptedProperty, data.Accepted);
                LoadProperty<decimal>(wholesaleValueProperty, data.WholesaleValue);
                LoadProperty<decimal>(taxValueProperty, data.TaxValue);
                LoadProperty<decimal?>(rabateValueProperty, data.RabateValue);
                LoadProperty<decimal?>(otherDiscountsValueProperty, data.OtherDiscountsValue);
                LoadProperty<decimal>(retailValueProperty, data.RetailValue);
                LoadProperty<decimal?>(scontoPercentageProperty, data.ScontoPercentage);
                LoadProperty<DateTime?>(scontoDateProperty, data.ScontoDate);
                LoadProperty<decimal?>(scontoValueProperty, data.ScontoValue);
                LoadProperty<decimal?>(retailValueScontoProperty, data.RetailValueSconto);
                LoadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty, data.MDGeneral_Enums_CurrencyId);
                LoadProperty<decimal?>(courseValueProperty, data.CourseValue);
                LoadProperty<bool?>(enteredInTheAccountsProperty, data.EnteredInTheAccounts);

                LoadProperty<string>(ReferenceNumberProperty, data.ReferenceNumber);
                LoadProperty<string>(PaymentModelProperty, data.PaymentModel);
                
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
                var data = new Documents_IncomingInvoice();

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
                data.Status = ReadProperty<short?>(statusProperty);

                data.SubjectId = ReadProperty<int>(subjectIdProperty);
                data.MaturityDate = ReadProperty<DateTime>(maturityDateProperty);
                data.Reversed = ReadProperty<bool?>(reversedProperty);
                data.PayedInFull = ReadProperty<bool?>(payedInFullProperty);
                data.Accepted = ReadProperty<bool?>(acceptedProperty);
                data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
                data.TaxValue = ReadProperty<decimal>(taxValueProperty);
                data.RabateValue = ReadProperty<decimal?>(rabateValueProperty);
                data.OtherDiscountsValue = ReadProperty<decimal?>(otherDiscountsValueProperty);
                data.RetailValue = ReadProperty<decimal>(retailValueProperty);
                data.ScontoPercentage= ReadProperty<decimal?>(scontoPercentageProperty);
                data.ScontoDate = ReadProperty<DateTime?>(scontoDateProperty);
                data.ScontoValue = ReadProperty<decimal?>(scontoValueProperty);
                data.RetailValueSconto = ReadProperty<decimal?>(retailValueScontoProperty);
                data.MDGeneral_Enums_CurrencyId = ReadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty);
                data.CourseValue = ReadProperty<decimal?>(courseValueProperty);
                data.EnteredInTheAccounts = ReadProperty<bool?>(enteredInTheAccountsProperty);

                data.ReferenceNumber= ReadProperty<string>(ReferenceNumberProperty);
                data.PaymentModel= ReadProperty<string>(PaymentModelProperty);

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
                var data = new Documents_IncomingInvoice();

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
                data.Status = ReadProperty<short?>(statusProperty);

                data.SubjectId = ReadProperty<int>(subjectIdProperty);
                data.MaturityDate = ReadProperty<DateTime>(maturityDateProperty);
                data.Reversed = ReadProperty<bool?>(reversedProperty);
                data.PayedInFull = ReadProperty<bool?>(payedInFullProperty);
                data.Accepted = ReadProperty<bool?>(acceptedProperty);
                data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
                data.TaxValue = ReadProperty<decimal>(taxValueProperty);
                data.RabateValue = ReadProperty<decimal?>(rabateValueProperty);
                data.OtherDiscountsValue = ReadProperty<decimal?>(otherDiscountsValueProperty);
                data.RetailValue = ReadProperty<decimal>(retailValueProperty);
                data.ScontoPercentage = ReadProperty<decimal?>(scontoPercentageProperty);
                data.ScontoDate = ReadProperty<DateTime?>(scontoDateProperty);
                data.ScontoValue = ReadProperty<decimal?>(scontoValueProperty);
                data.RetailValueSconto = ReadProperty<decimal?>(retailValueScontoProperty);
                data.MDGeneral_Enums_CurrencyId = ReadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty);
                data.CourseValue = ReadProperty<decimal?>(courseValueProperty);
                data.EnteredInTheAccounts = ReadProperty<bool?>(enteredInTheAccountsProperty);

                data.ReferenceNumber = ReadProperty<string>(ReferenceNumberProperty);
                data.PaymentModel = ReadProperty<string>(PaymentModelProperty);

                DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }
}

