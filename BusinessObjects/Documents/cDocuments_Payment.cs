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
	public partial class cDocuments_Payment: cDocuments_Document<cDocuments_Payment>
	{
		#region Business Methods

		private static readonly PropertyInfo< System.Int32 > bankIdProperty = RegisterProperty<System.Int32>(p => p.BankId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 BankId
		{
			get { return GetProperty(bankIdProperty); }
			set { SetProperty(bankIdProperty, value); }
		}

        private static readonly PropertyInfo<bool?> enteredInTheAccountsProperty = RegisterProperty<bool?>(p => p.EnteredInTheAccounts, string.Empty, (bool?)null);
        public bool? EnteredInTheAccounts
        {
            get { return GetProperty(enteredInTheAccountsProperty); }
            set { SetProperty(enteredInTheAccountsProperty, value); }
        }

        private static readonly PropertyInfo<decimal?> accountBalanceProperty = RegisterProperty<decimal?>(p => p.AccountBalance, string.Empty);
        public decimal? AccountBalance
        {
            get { return GetProperty(accountBalanceProperty); }
            set { SetProperty(accountBalanceProperty, value); }
        }

		#region Kolekcije
		public static PropertyInfo<cDocuments_PaymentItemsCol> Documents_PaymentItemsColProperty = RegisterProperty<cDocuments_PaymentItemsCol>(c => c.Documents_PaymentItemsCol);
		public cDocuments_PaymentItemsCol Documents_PaymentItemsCol
		{
			get
			{
				if (!FieldManager.FieldExists(Documents_PaymentItemsColProperty))
					LoadProperty<cDocuments_PaymentItemsCol>(Documents_PaymentItemsColProperty, cDocuments_PaymentItemsCol.NewDocuments_PaymentItemsCol());
				return GetProperty(Documents_PaymentItemsColProperty);
			}
			private set { SetProperty(Documents_PaymentItemsColProperty, value); }
		}

		#endregion
		#endregion

        #region Factory Methods

        public static cDocuments_Payment NewDocuments_Payment()
        {
            return DataPortal.Create<cDocuments_Payment>();

        }

        public static cDocuments_Payment GetDocuments_Payment(int uniqueId, int? paymentItemId = null)
        {
            return DataPortal.Fetch<cDocuments_Payment>(new Documents_Payment_Criteria(uniqueId, paymentItemId));
        }

        internal static cDocuments_Payment GetDocuments_Payment(Documents_Payment data)
        {
            return DataPortal.Fetch<cDocuments_Payment>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(Documents_Payment_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_Payment>().First(p => p.Id == criteria.DocumentId);

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

                LoadProperty<int>(bankIdProperty, data.BankId);
                LoadProperty<bool?>(enteredInTheAccountsProperty, data.EnteredInTheAccounts);
                LoadProperty<decimal?>(accountBalanceProperty, data.AccountBalance);
                
             
                LastChanged = data.LastChanged;

                LoadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty, cDocuments_ItemsCol.GetDocuments_ItemsCol(data.Documents_ItemsCol));
                if (criteria.PaymentItemId == null)
                LoadProperty<cDocuments_PaymentItemsCol>(Documents_PaymentItemsColProperty, cDocuments_PaymentItemsCol.GetDocuments_PaymentItemsCol(data.Documents_PaymentItemsCol));
                else
                LoadProperty<cDocuments_PaymentItemsCol>(Documents_PaymentItemsColProperty, cDocuments_PaymentItemsCol.GetDocuments_PaymentItemsCol(data.Documents_PaymentItemsCol.Where(p=> p.Id == criteria.PaymentItemId)));

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_Payment();

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
                data.EnteredInTheAccounts = ReadProperty<bool?>(enteredInTheAccountsProperty);

                data.BankId = ReadProperty<int>(bankIdProperty);
                data.AccountBalance = ReadProperty<decimal?>(accountBalanceProperty);
                

                ctx.ObjectContext.AddToDocuments_Document(data);

                ctx.ObjectContext.SaveChanges();

                DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_PaymentItemsCol>(Documents_PaymentItemsColProperty), data);

              


                //Get New id
                int newId = data.Id;
                //Load New Id into object
                LoadProperty(IdProperty, newId);
                //Load New EntityKey into Object
                LoadProperty(EntityKeyDataProperty, Serialize(data.EntityKey));

                bool UpdateOverpaids = false;
                foreach (var item in this.Documents_PaymentItemsCol)//Dodavanje ispravanog GeneratedFormDocuments_PaymentId na nove overpaid iteme
                {
                    if ((item.LinkOverpaidWithOrdinalNumber ?? 0) != 0)
                    {

                        var itemForUpdate = ctx.ObjectContext.Documents_PaymentItemsCol.FirstOrDefault(p => p.Id == item.Id);
                        if (itemForUpdate != null)
                        {
                            item.GeneratedFormDocuments_PaymentId = this.Documents_PaymentItemsCol.First(p => p.Ordinal == item.LinkOverpaidWithOrdinalNumber).Id;
                            itemForUpdate.GeneratedFormDocuments_PaymentId = this.Documents_PaymentItemsCol.First(p => p.Ordinal == item.LinkOverpaidWithOrdinalNumber).Id;
                            UpdateOverpaids = true;
                        }

                    }
                }

                if (UpdateOverpaids)
                    ctx.ObjectContext.SaveChanges();


            }
        }


        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_Payment();

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
                data.EnteredInTheAccounts = ReadProperty<bool?>(enteredInTheAccountsProperty);

                data.BankId = ReadProperty<int>(bankIdProperty);
                data.AccountBalance = ReadProperty<decimal?>(accountBalanceProperty);
                

                ctx.ObjectContext.SaveChanges();
               
                DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_PaymentItemsCol>(Documents_PaymentItemsColProperty), data);


                bool UpdateOverpaids = false;
                foreach (var item in this.Documents_PaymentItemsCol)//Dodavanje ispravanog GeneratedFormDocuments_PaymentId na nove overpaid iteme
                {
                    if ((item.LinkOverpaidWithOrdinalNumber ?? 0) != 0)
                    {

                        var itemForUpdate = ctx.ObjectContext.Documents_PaymentItemsCol.FirstOrDefault(p => p.Id == item.Id);
                        if (itemForUpdate != null)
                        {
                            item.GeneratedFormDocuments_PaymentId = this.Documents_PaymentItemsCol.First(p => p.Ordinal == item.LinkOverpaidWithOrdinalNumber).Id;
                            itemForUpdate.GeneratedFormDocuments_PaymentId = this.Documents_PaymentItemsCol.First(p => p.Ordinal == item.LinkOverpaidWithOrdinalNumber).Id;
                            UpdateOverpaids = true;
                        }

                    }
                }

                if (UpdateOverpaids)
                    ctx.ObjectContext.SaveChanges();
                
            }

            
        }
        #endregion
	}
}
