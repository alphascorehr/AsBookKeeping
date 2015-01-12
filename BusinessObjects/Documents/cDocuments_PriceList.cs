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
	public partial class cDocuments_PriceList: cDocuments_Document<cDocuments_PriceList>
	{
		#region Business Methods
	
		private static readonly PropertyInfo< bool > isDefaultProperty = RegisterProperty<bool>(p => p.IsDefault, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public bool IsDefault
		{
			get { return GetProperty(isDefaultProperty); }
			set { SetProperty(isDefaultProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDSubjects_SubjectIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_SubjectId, string.Empty,(System.Int32?)null);
		public System.Int32? MDSubjects_SubjectId
		{
			get { return GetProperty(mDSubjects_SubjectIdProperty); }
			set { SetProperty(mDSubjects_SubjectIdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
			set { SetProperty(nameProperty, (value ?? "").Trim()); }
		}

        private static readonly PropertyInfo<System.DateTime> dateStartProperty = RegisterProperty<System.DateTime>(p => p.DateStart, string.Empty, System.DateTime.Now);
		//[System.ComponentModel.DataAnnotations.StringLength(10, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime DateStart
		{
			get { return GetProperty(dateStartProperty); }
			set { SetProperty(dateStartProperty, value); }
		}

        private static readonly PropertyInfo<System.DateTime?> dateEndProperty = RegisterProperty<System.DateTime?>(p => p.DateEnd, string.Empty, (System.DateTime?)null);
		public System.DateTime? DateEnd
		{
			get { return GetProperty(dateEndProperty); }
			set { SetProperty(dateEndProperty, value); }
		}


		#region Kolekcije
		public static PropertyInfo<cDocuments_PriceList_ItemsCol> Documents_PriceList_ItemsColProperty = RegisterProperty<cDocuments_PriceList_ItemsCol>(c => c.Documents_PriceList_ItemsCol);
		public cDocuments_PriceList_ItemsCol Documents_PriceList_ItemsCol
		{
			get
			{
				if (!FieldManager.FieldExists(Documents_PriceList_ItemsColProperty))
					LoadProperty<cDocuments_PriceList_ItemsCol>(Documents_PriceList_ItemsColProperty, cDocuments_PriceList_ItemsCol.NewDocuments_PriceList_ItemsCol());
				return GetProperty(Documents_PriceList_ItemsColProperty);
			}
			private set { SetProperty(Documents_PriceList_ItemsColProperty, value); }
		}

		#endregion
		#endregion

        #region Factory Methods

        public static cDocuments_PriceList NewDocuments_PriceList()
        {
            return DataPortal.Create<cDocuments_PriceList>();

        }

        public static cDocuments_PriceList GetDocuments_PriceList(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_PriceList>(new SingleCriteria<cDocuments_PriceList, int>(uniqueId));
        }

        public static cDocuments_PriceList GetDocuments_MainPriceList()
        {
            return DataPortal.Fetch<cDocuments_PriceList>();
        }

        internal static cDocuments_PriceList GetDocuments_PriceList(Documents_PriceList data)
        {
            return DataPortal.Fetch<cDocuments_PriceList>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_PriceList, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_PriceList>().First(p => p.Id == criteria.Value);

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

                LoadProperty<bool>(isDefaultProperty, data.IsDefault);
                LoadProperty<int?>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<DateTime>(dateStartProperty, data.DateStart);
                LoadProperty<DateTime?>(dateEndProperty, data.DateEnd);
              
                LastChanged = data.LastChanged;

                LoadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty, cDocuments_ItemsCol.GetDocuments_ItemsCol(data.Documents_ItemsCol));
                LoadProperty<cDocuments_PriceList_ItemsCol>(Documents_PriceList_ItemsColProperty, cDocuments_PriceList_ItemsCol.GetDocuments_PriceList_ItemsCol(data.Documents_PriceList_ItemsCol));

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_PriceList>().FirstOrDefault(p => p.CompanyUsingServiceId == ((BusinessObjects.Security.PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId && p.IsDefault == true);

                if (data != null)
                {
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

                    LoadProperty<bool>(isDefaultProperty, data.IsDefault);
                    LoadProperty<int?>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
                    LoadProperty<string>(nameProperty, data.Name);
                    LoadProperty<DateTime>(dateStartProperty, data.DateStart);
                    LoadProperty<DateTime?>(dateEndProperty, data.DateEnd);

                    LastChanged = data.LastChanged;

                    LoadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty, cDocuments_ItemsCol.GetDocuments_ItemsCol(data.Documents_ItemsCol));
                    LoadProperty<cDocuments_PriceList_ItemsCol>(Documents_PriceList_ItemsColProperty, cDocuments_PriceList_ItemsCol.GetDocuments_PriceList_ItemsCol(data.Documents_PriceList_ItemsCol));

                    BusinessRules.CheckRules(); 
                }

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_PriceList();

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

                data.IsDefault = ReadProperty<bool>(isDefaultProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.DateStart = ReadProperty<DateTime>(dateStartProperty);
                data.DateEnd = ReadProperty<DateTime?>(dateEndProperty);

                ctx.ObjectContext.AddToDocuments_Document(data);

                DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_PriceList_ItemsCol>(Documents_PriceList_ItemsColProperty), data);

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
                var data = new Documents_PriceList();

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

                data.IsDefault = ReadProperty<bool>(isDefaultProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.DateStart = ReadProperty<DateTime>(dateStartProperty);
                data.DateEnd = ReadProperty<DateTime?>(dateEndProperty);

                DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_PriceList_ItemsCol>(Documents_PriceList_ItemsColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
