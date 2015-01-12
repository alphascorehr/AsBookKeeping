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
	public partial class cDocuments_OrderForm: cDocuments_Document<cDocuments_OrderForm>
	{
		#region Business Methods

		private static readonly PropertyInfo< System.Int32 > mDSubjects_SubjectIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_SubjectId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 MDSubjects_SubjectId
		{
			get { return GetProperty(mDSubjects_SubjectIdProperty); }
			set { SetProperty(mDSubjects_SubjectIdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > orderedByPersonProperty = RegisterProperty<System.String>(p => p.OrderedByPerson, string.Empty, (System.String)null);
		public System.String OrderedByPerson
		{
			get { return GetProperty(orderedByPersonProperty); }
			set { SetProperty(orderedByPersonProperty, (value ?? "").Trim()); }
		}
        private static readonly PropertyInfo<System.Int16?> statusProperty = RegisterProperty<System.Int16?>(p => p.Status, string.Empty, (System.Int16?)null);
        public System.Int16? Status
        {
            get { return GetProperty(statusProperty); }
            set { SetProperty(statusProperty, value); }
        }
		#endregion

        #region Factory Methods

        public static cDocuments_OrderForm NewDocuments_OrderForm()
        {
            return DataPortal.Create<cDocuments_OrderForm>();

        }

        public static cDocuments_OrderForm GetDocuments_OrderForm(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_OrderForm>(new SingleCriteria<cDocuments_OrderForm, int>(uniqueId));
        }

        internal static cDocuments_OrderForm GetDocuments_OrderForm(Documents_OrderForm data)
        {
            return DataPortal.Fetch<cDocuments_OrderForm>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_OrderForm, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_OrderForm>().First(p => p.Id == criteria.Value);

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

                LoadProperty<int>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
                LoadProperty<string>(orderedByPersonProperty, data.OrderedByPerson);
            

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
                var data = new Documents_OrderForm();

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

                data.MDSubjects_SubjectId = ReadProperty<int>(mDSubjects_SubjectIdProperty);
                data.OrderedByPerson = ReadProperty<string>(orderedByPersonProperty);
              
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
                var data = new Documents_OrderForm();

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

                data.MDSubjects_SubjectId = ReadProperty<int>(mDSubjects_SubjectIdProperty);
                data.OrderedByPerson = ReadProperty<string>(orderedByPersonProperty);

                //DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
           

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
