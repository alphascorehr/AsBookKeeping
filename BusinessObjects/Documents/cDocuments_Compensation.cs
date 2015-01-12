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
    public partial class cDocuments_Compensation : cDocuments_Document<cDocuments_Compensation>
	{
		#region Business Methods
		private static readonly PropertyInfo< System.Int32 > buyerIdProperty = RegisterProperty<System.Int32>(p => p.BuyerId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 BuyerId
		{
			get { return GetProperty(buyerIdProperty); }
			set { SetProperty(buyerIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32> supplierIdProperty = RegisterProperty<System.Int32>(p => p.SupplierId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 SupplierId
        {
            get { return GetProperty(buyerIdProperty); }
            set { SetProperty(buyerIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Boolean?> approvedProperty = RegisterProperty<System.Boolean?>(p => p.Approved, string.Empty, (System.Boolean?)null);
		public System.Boolean? Approved
		{
            get { return GetProperty(approvedProperty); }
            set { SetProperty(approvedProperty, value) ; }
		}

        private static readonly PropertyInfo<System.DateTime?> approvedDateProperty = RegisterProperty<System.DateTime?>(p => p.ApprovedDate, string.Empty, System.DateTime.Now);
        public System.DateTime? ApprovedDate
        {
            get { return GetProperty(approvedDateProperty); }
            set { SetProperty(approvedDateProperty, value); }
        }

        private static readonly PropertyInfo<System.Int16?> statusProperty = RegisterProperty<System.Int16?>(p => p.Status, string.Empty, (System.Int16?)null);
        public System.Int16? Status
        {
            get { return GetProperty(statusProperty); }
            set { SetProperty(statusProperty, value); }
        }

        #region Kolekcije
        public static PropertyInfo<cDocuments_CompensationChildCol> Documents_CompensationColProperty = RegisterProperty<cDocuments_CompensationChildCol>(c => c.Documents_CompensationCol);
        public cDocuments_CompensationChildCol Documents_CompensationCol
        {
            get
            {
                if (!FieldManager.FieldExists(Documents_CompensationColProperty))
                    LoadProperty<cDocuments_CompensationChildCol>(Documents_CompensationColProperty, cDocuments_CompensationChildCol.NewDocuments_CompensationChildCol());
                return GetProperty(Documents_CompensationColProperty);
            }
            private set { SetProperty(Documents_CompensationColProperty, value); }
        }
        #endregion
        #endregion

        #region Factory Methods

        public static cDocuments_Compensation NewDocuments_Compensation()
        {
            return DataPortal.Create<cDocuments_Compensation>();

        }

        public static cDocuments_Compensation GetDocuments_Compensation(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_Compensation>(new SingleCriteria<cDocuments_Compensation, int>(uniqueId));
        }

        internal static cDocuments_Compensation GetDocuments_TakeoverOfGoods(Documents_Compensation data)
        {
            return DataPortal.Fetch<cDocuments_Compensation>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_Compensation, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_Compensation>().First(p => p.Id == criteria.Value);

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

                LoadProperty<int>(buyerIdProperty, data.BuyerId);
                LoadProperty<int>(supplierIdProperty, data.SupplierId);
                LoadProperty<bool?>(approvedProperty, data.Approved);
                LoadProperty<DateTime?>(approvedDateProperty, data.ApprovedDate);

                LastChanged = data.LastChanged;

                LoadProperty<cDocuments_CompensationChildCol>(Documents_CompensationColProperty, cDocuments_CompensationChildCol.GetDocuments_CompensationChildCol(data.Documents_CompensationChildCol));
//                LoadProperty<cDocuments_TravelOrder_WageCol>(Documents_TravelOrder_WageColProperty, cDocuments_TravelOrder_WageCol.GetDocuments_TravelOrder_WageCol(data.Documents_TravelOrder_WageCol));

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_Compensation();

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

                data.BuyerId = ReadProperty<int>(buyerIdProperty);
                data.SupplierId = ReadProperty<int>(supplierIdProperty);
                data.Approved = ReadProperty<bool?>(approvedProperty);
                data.ApprovedDate = ReadProperty<DateTime?>(approvedDateProperty);

                ctx.ObjectContext.AddToDocuments_Document(data);

                DataPortal.UpdateChild(ReadProperty<cDocuments_CompensationChildCol>(Documents_CompensationColProperty), data);

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
                var data = new Documents_Compensation();

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

                data.BuyerId = ReadProperty<int>(buyerIdProperty);
                data.SupplierId = ReadProperty<int>(supplierIdProperty);
                data.Approved = ReadProperty<bool?>(approvedProperty);
                data.ApprovedDate = ReadProperty<DateTime?>(approvedDateProperty);

                DataPortal.UpdateChild(ReadProperty<cDocuments_CompensationChildCol>(Documents_CompensationColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }
}
