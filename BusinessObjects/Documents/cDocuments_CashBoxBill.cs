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
	public partial class cDocuments_CashBoxBill : cDocuments_Document<cDocuments_CashBoxBill>
	{
		#region Business Method

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
		private static readonly PropertyInfo< System.Decimal > retailValueProperty = RegisterProperty<System.Decimal>(p => p.RetailValue, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal RetailValue
		{
			get { return GetProperty(retailValueProperty); }
			set { SetProperty(retailValueProperty, value); }
		}
		private static readonly PropertyInfo<System.Int16?> statusProperty = RegisterProperty<System.Int16?>(p => p.Status, string.Empty, (System.Int16?)null);
		public System.Int16? Status
		{
			get { return GetProperty(statusProperty); }
			set { SetProperty(statusProperty, value); }
		}
		//FiscalizationZKIR	nchar(32)	Checked
		private static readonly PropertyInfo<System.String> fiscalizationZKIRProperty = RegisterProperty<System.String>(p => p.FiscalizationZKIR, string.Empty, (System.String)null);
		public System.String FiscalizationZKIR
		{
			get { return GetProperty(fiscalizationZKIRProperty); }
			set { SetProperty(fiscalizationZKIRProperty, (value ?? "").Trim()); }
		}
		//FiscalizationJIR	nchar(36)	Checked
		private static readonly PropertyInfo<System.String> fiscalizationJIRProperty = RegisterProperty<System.String>(p => p.FiscalizationJIR, string.Empty, (System.String)null);
		public System.String FiscalizationJIR
		{
			get { return GetProperty(fiscalizationJIRProperty); }
			set { SetProperty(fiscalizationJIRProperty, (value ?? "").Trim()); }
		}
		//FiscalizationMessageItentifier	nchar(36)	Checked
		private static readonly PropertyInfo<System.String> fiscalizationMessageItentifierProperty = RegisterProperty<System.String>(p => p.FiscalizationMessageItentifier, string.Empty, (System.String)null);
		public System.String FiscalizationMessageItentifier
		{
			get { return GetProperty(fiscalizationMessageItentifierProperty); }
			set { SetProperty(fiscalizationMessageItentifierProperty, (value ?? "").Trim()); }
		}
		//FiscalizationTime	datetime	Checked
		private static readonly PropertyInfo<System.DateTime?> fiscalizationTimeProperty = RegisterProperty<System.DateTime?>(p => p.FiscalizationTime, string.Empty, System.DateTime.Now);
		public System.DateTime? FiscalizationTime
		{
			get { return GetProperty(fiscalizationTimeProperty); }
			set { SetProperty(fiscalizationTimeProperty, value); }
		}
		//FiscalizationTypeOfPayment	int	Checked
		private static readonly PropertyInfo<System.Int32?> fiscalizationTypeOfPaymentProperty = RegisterProperty<System.Int32?>(p => p.FiscalizationTypeOfPayment, string.Empty, (System.Int32?)null);
		public System.Int32? FiscalizationTypeOfPayment
		{
			get { return GetProperty(fiscalizationTypeOfPaymentProperty); }
			set { SetProperty(fiscalizationTypeOfPaymentProperty, value); }
		}
		//FiscalizationDelayedDeparture	bit	Checked
		private static readonly PropertyInfo<bool?> fiscalizationDelayedDepartureProperty = RegisterProperty<bool?>(p => p.FiscalizationDelayedDeparture, string.Empty, (bool?)null);
		public bool? FiscalizationDelayedDeparture
		{
			get { return GetProperty(fiscalizationDelayedDepartureProperty); }
			set { SetProperty(fiscalizationDelayedDepartureProperty, value); }
		}

		//FiscalizationCashierId	int	Checked
		private static readonly PropertyInfo<System.Int32?> fiscalizationCashierIdProperty = RegisterProperty<System.Int32?>(p => p.FiscalizationCashierId, string.Empty, (System.Int32?)null);
		public System.Int32? FiscalizationCashierId
		{
			get { return GetProperty(fiscalizationCashierIdProperty); }
			set { SetProperty(fiscalizationCashierIdProperty, value); }
		}

		#region Kolekcije
		public static PropertyInfo<cDocuments_CashBoxBillItemsCol> Documents_CashBoxBillItemsColProperty = RegisterProperty<cDocuments_CashBoxBillItemsCol>(c => c.Documents_CashBoxBillItemsCol);
		public cDocuments_CashBoxBillItemsCol Documents_CashBoxBillItemsCol
		{
			get
			{
				if (!FieldManager.FieldExists(Documents_CashBoxBillItemsColProperty))
					LoadProperty<cDocuments_CashBoxBillItemsCol>(Documents_CashBoxBillItemsColProperty, cDocuments_CashBoxBillItemsCol.NewDocuments_CashBoxBillItemsCol());
				return GetProperty(Documents_CashBoxBillItemsColProperty);
			}
			private set { SetProperty(Documents_CashBoxBillItemsColProperty, value); }
		}
		#endregion

		#endregion

		#region Factory Methods

		public static cDocuments_CashBoxBill NewDocuments_CashBoxBill()
		{
			return DataPortal.Create<cDocuments_CashBoxBill>();

		}

		public static cDocuments_CashBoxBill GetDocuments_CashBoxBill(int uniqueId)
		{
			return DataPortal.Fetch<cDocuments_CashBoxBill>(new SingleCriteria<cDocuments_CashBoxBill, int>(uniqueId));
		}

		internal static cDocuments_CashBoxBill GetDocuments_CashBoxBill(Documents_CashBoxBill data)
		{
			return DataPortal.Fetch<cDocuments_CashBoxBill>(data);
		}

		#endregion

		#region Data Access
		[RunLocal]
		protected override void DataPortal_Create()
		{
			BusinessRules.CheckRules();
		}

		private void DataPortal_Fetch(SingleCriteria<cDocuments_CashBoxBill, int> criteria)
		{
			using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
			{
				var data = ctx.ObjectContext.Documents_Document.OfType<Documents_CashBoxBill>().First(p => p.Id == criteria.Value);

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
				LoadProperty<decimal>(wholesaleValueProperty, data.WholesaleValue);
				LoadProperty<decimal>(taxValueProperty, data.TaxValue);
				LoadProperty<decimal>(retailValueProperty, data.RetailValue);
				LoadProperty<short?>(statusProperty, data.Status);
				
				LoadProperty<string>(cashBoxCodeProperty, data.CashBoxCode);
				LoadProperty<string>(locationCodeProperty, data.LocationCode);

				LoadProperty<string>(fiscalizationZKIRProperty, data.FiscalizationZKIR);
				LoadProperty<string>(fiscalizationJIRProperty, data.FiscalizationJIR);
				LoadProperty<string>(fiscalizationMessageItentifierProperty, data.FiscalizationMessageItentifier);
				LoadProperty<DateTime?>(fiscalizationTimeProperty, data.FiscalizationTime);
				LoadProperty<int?>(fiscalizationTypeOfPaymentProperty, data.FiscalizationTypeOfPayment);
				LoadProperty<bool?>(fiscalizationDelayedDepartureProperty, data.FiscalizationDelayedDeparture);
				LoadProperty<int?>(fiscalizationCashierIdProperty, data.FiscalizationCashierId);

				LastChanged = data.LastChanged;

				LoadProperty<cDocuments_CashBoxBillItemsCol>(Documents_CashBoxBillItemsColProperty, cDocuments_CashBoxBillItemsCol.GetDocuments_CashBoxBillItemsCol(data.Documents_CashBoxBillItemsCol));

				BusinessRules.CheckRules();

			}
		}

		[Transactional(TransactionalTypes.TransactionScope)]
		protected override void DataPortal_Insert()
		{
			using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
			{
				var data = new Documents_CashBoxBill();

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

				data.CashBoxCode = ReadProperty<string>(cashBoxCodeProperty);
				data.LocationCode = ReadProperty<string>(locationCodeProperty);

				data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
				data.TaxValue = ReadProperty<decimal>(taxValueProperty);
				data.RetailValue = ReadProperty<decimal>(retailValueProperty);

				data.FiscalizationZKIR = ReadProperty<string>(fiscalizationZKIRProperty);
				data.FiscalizationJIR = ReadProperty<string>(fiscalizationJIRProperty);
				data.FiscalizationMessageItentifier = ReadProperty<string>(fiscalizationMessageItentifierProperty);
				data.FiscalizationTime = ReadProperty<DateTime?>(fiscalizationTimeProperty);
				data.FiscalizationTypeOfPayment = ReadProperty<int?>(fiscalizationTypeOfPaymentProperty);
				data.FiscalizationDelayedDeparture = ReadProperty<bool?>(fiscalizationDelayedDepartureProperty);
				data.FiscalizationCashierId = ReadProperty<int?>(fiscalizationCashierIdProperty);

				ctx.ObjectContext.AddToDocuments_Document(data);

				DataPortal.UpdateChild(ReadProperty<cDocuments_CashBoxBillItemsCol>(Documents_CashBoxBillItemsColProperty), data);

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
				var data = new Documents_CashBoxBill();

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

				data.CashBoxCode = ReadProperty<string>(cashBoxCodeProperty);
				data.LocationCode = ReadProperty<string>(locationCodeProperty);
				
				data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
				data.TaxValue = ReadProperty<decimal>(taxValueProperty);
				data.RetailValue = ReadProperty<decimal>(retailValueProperty);

				data.FiscalizationZKIR = ReadProperty<string>(fiscalizationZKIRProperty);
				data.FiscalizationJIR = ReadProperty<string>(fiscalizationJIRProperty);
				data.FiscalizationMessageItentifier = ReadProperty<string>(fiscalizationMessageItentifierProperty);
				data.FiscalizationTime = ReadProperty<DateTime?>(fiscalizationTimeProperty);
				data.FiscalizationTypeOfPayment = ReadProperty<int?>(fiscalizationTypeOfPaymentProperty);
				data.FiscalizationDelayedDeparture = ReadProperty<bool?>(fiscalizationDelayedDepartureProperty);
				data.FiscalizationCashierId = ReadProperty<int?>(fiscalizationCashierIdProperty);

				DataPortal.UpdateChild(ReadProperty<cDocuments_CashBoxBillItemsCol>(Documents_CashBoxBillItemsColProperty), data);

				ctx.ObjectContext.SaveChanges();
			}
		}
		#endregion
	}
}

