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
	public partial class cDocuments_Payoff: cDocuments_Document<cDocuments_Payoff>
	{
		#region Business Methods
		private static readonly PropertyInfo< System.Int32? > projects_ProjectIdProperty = RegisterProperty<System.Int32?>(p => p.Projects_ProjectId, string.Empty,(System.Int32?)null);
		public System.Int32? Projects_ProjectId
		{
			get { return GetProperty(projects_ProjectIdProperty); }
			set { SetProperty(projects_ProjectIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32?> subjectIdProperty = RegisterProperty<System.Int32?>(p => p.SubjectId, string.Empty, (System.Int32?)null);
        public System.Int32? SubjectId
        {
            get { return GetProperty(subjectIdProperty); }
            set { SetProperty(subjectIdProperty, value); }
        }

		private static readonly PropertyInfo< bool? > approvedProperty = RegisterProperty<bool?>(p => p.Approved, string.Empty,(bool?)null);
		public bool? Approved
		{
			get { return GetProperty(approvedProperty); }
			set { SetProperty(approvedProperty, value); }
		}

		private static readonly PropertyInfo< bool? > sentProperty = RegisterProperty<bool?>(p => p.Sent, string.Empty,(bool?)null);
		public bool? Sent
		{
			get { return GetProperty(sentProperty); }
			set { SetProperty(sentProperty, value); }
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

		private static readonly PropertyInfo< System.Int32? > mDGeneral_Enums_CurrencyIdProperty = RegisterProperty<System.Int32?>(p => p.MDGeneral_Enums_CurrencyId, string.Empty,(System.Int32?)null);
		public System.Int32? MDGeneral_Enums_CurrencyId
		{
			get { return GetProperty(mDGeneral_Enums_CurrencyIdProperty); }
			set { SetProperty(mDGeneral_Enums_CurrencyIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > courseValueProperty = RegisterProperty<System.Decimal?>(p => p.CourseValue, string.Empty, (System.Decimal?)null);
		public System.Decimal? CourseValue
		{
			get { return GetProperty(courseValueProperty); }
			set { SetProperty(courseValueProperty, value); }
		}
        private static readonly PropertyInfo<System.Int16?> statusProperty = RegisterProperty<System.Int16?>(p => p.Status, string.Empty, (System.Int16?)null);
        public System.Int16? Status
        {
            get { return GetProperty(statusProperty); }
            set { SetProperty(statusProperty, value); }
        }

		#endregion

        #region Factory Methods

        public static cDocuments_Payoff NewDocuments_Payoff()
        {
            return DataPortal.Create<cDocuments_Payoff>();

        }

        public static cDocuments_Payoff GetDocuments_Payoff(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_Payoff>(new SingleCriteria<cDocuments_Payoff, int>(uniqueId));
        }

        internal static cDocuments_Payoff GetDocuments_Payoff(Documents_Payoff data)
        {
            return DataPortal.Fetch<cDocuments_Payoff>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_Payoff, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_Payoff>().First(p => p.Id == criteria.Value);

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

                LoadProperty<int?>(projects_ProjectIdProperty, data.Projects_ProjectId);
                LoadProperty<int?>(subjectIdProperty, data.SubjectId);
                LoadProperty<bool?>(approvedProperty, data.Approved);
                LoadProperty<bool?>(sentProperty, data.Sent);
                LoadProperty<decimal>(wholesaleValueProperty, data.WholesaleValue);
                LoadProperty<decimal>(taxValueProperty, data.TaxValue);
                LoadProperty<decimal?>(rabateValueProperty, data.RabateValue);
                LoadProperty<decimal?>(otherDiscountsValueProperty, data.OtherDiscountsValue);
                LoadProperty<decimal>(retailValueProperty, data.RetailValue);
                LoadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty, data.MDGeneral_Enums_CurrencyId);
                LoadProperty<decimal?>(courseValueProperty, data.CourseValue);
 
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
                var data = new Documents_Payoff();

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

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.SubjectId = ReadProperty<int?>(subjectIdProperty);
                data.Approved = ReadProperty<bool?>(approvedProperty);
                data.Sent = ReadProperty<bool?>(sentProperty);
                data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
                data.TaxValue = ReadProperty<decimal>(taxValueProperty);
                data.RabateValue = ReadProperty<decimal?>(rabateValueProperty);
                data.OtherDiscountsValue = ReadProperty<decimal?>(otherDiscountsValueProperty);
                data.RetailValue = ReadProperty<decimal>(retailValueProperty);
                data.MDGeneral_Enums_CurrencyId = ReadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty);
                data.CourseValue = ReadProperty<decimal?>(courseValueProperty);

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
                var data = new Documents_Payoff();

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

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.SubjectId = ReadProperty<int?>(subjectIdProperty);
                data.Approved = ReadProperty<bool?>(approvedProperty);
                data.Sent = ReadProperty<bool?>(sentProperty);
                data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
                data.TaxValue = ReadProperty<decimal>(taxValueProperty);
                data.RabateValue = ReadProperty<decimal?>(rabateValueProperty);
                data.OtherDiscountsValue = ReadProperty<decimal?>(otherDiscountsValueProperty);
                data.RetailValue = ReadProperty<decimal>(retailValueProperty);
                data.MDGeneral_Enums_CurrencyId = ReadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty);
                data.CourseValue = ReadProperty<decimal?>(courseValueProperty);

                DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }
}

