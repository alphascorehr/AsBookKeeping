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
	public partial class cDocuments_Invoice : cDocuments_Document<cDocuments_Invoice>
	{
		#region Business Method

		private static readonly PropertyInfo< System.Int32? > subjectIdProperty = RegisterProperty<System.Int32?>(p => p.SubjectId, string.Empty);
		public System.Int32? SubjectId
		{
			get { return GetProperty(subjectIdProperty); }
			set { SetProperty(subjectIdProperty, value); }
		}

		private static readonly PropertyInfo<System.DateTime> maturityDateProperty = RegisterProperty<System.DateTime>(p => p.MaturityDate, string.Empty, System.DateTime.Now);
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

		private static readonly PropertyInfo< bool? > returnedProperty = RegisterProperty<bool?>(p => p.Returned, string.Empty,(bool?)null);
		public bool? Returned
		{
			get { return GetProperty(returnedProperty); }
			set { SetProperty(returnedProperty, value); }
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

		private static readonly PropertyInfo< bool? > payedInAdvanceProperty = RegisterProperty<bool?>(p => p.PayedInAdvance, string.Empty,(bool?)null);
		public bool? PayedInAdvance
		{
			get { return GetProperty(payedInAdvanceProperty); }
			set { SetProperty(payedInAdvanceProperty, value); }
		}

		private static readonly PropertyInfo< bool? > recursionGenerationProperty = RegisterProperty<bool?>(p => p.RecursionGeneration, string.Empty,(bool?)null);
		public bool? RecursionGeneration
		{
			get { return GetProperty(recursionGenerationProperty); }
			set { SetProperty(recursionGenerationProperty, value); }
		}

		private static readonly PropertyInfo< System.Byte? > recursionRateProperty = RegisterProperty<System.Byte?>(p => p.RecursionRate, string.Empty, (System.Byte?)null);
		public System.Byte? RecursionRate
		{
			get { return GetProperty(recursionRateProperty); }
			set { SetProperty(recursionRateProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime? > recursionStartProperty = RegisterProperty<System.DateTime?>(p => p.RecursionStart, string.Empty, (System.DateTime?)null);
		public System.DateTime? RecursionStart
		{
			get { return GetProperty(recursionStartProperty); }
			set { SetProperty(recursionStartProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > recursionNumberOfRatesProperty = RegisterProperty<System.Int32?>(p => p.RecursionNumberOfRates, string.Empty,(System.Int32?)null);
		public System.Int32? RecursionNumberOfRates
		{
			get { return GetProperty(recursionNumberOfRatesProperty); }
			set { SetProperty(recursionNumberOfRatesProperty, value); }
		}

		private static readonly PropertyInfo<System.DateTime?> dispatchDateProperty = RegisterProperty<System.DateTime?>(p => p.DispatchDate, string.Empty, System.DateTime.Now);
		public System.DateTime? DispatchDate
		{
			get { return GetProperty(dispatchDateProperty); }
			set { SetProperty(dispatchDateProperty, value); }
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

		private static readonly PropertyInfo< System.String > dispatchDescriptionProperty = RegisterProperty<System.String>(p => p.DispatchDescription, string.Empty, (System.String)null);
		public System.String DispatchDescription
		{
			get { return GetProperty(dispatchDescriptionProperty); }
			set { SetProperty(dispatchDescriptionProperty, (value ?? "").Trim()); }
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

		private static readonly PropertyInfo< System.String > conditionsProperty = RegisterProperty<System.String>(p => p.Conditions, string.Empty, (System.String)null);
		public System.String Conditions
		{
			get { return GetProperty(conditionsProperty); }
			set { SetProperty(conditionsProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Int32? > mDGeneral_Enums_CurrencyIdProperty = RegisterProperty<System.Int32?>(p => p.MDGeneral_Enums_CurrencyId, string.Empty,(System.Int32?)null);
		//private static readonly PropertyInfo<System.Int32?> mDGeneral_Enums_CurrencyIdProperty = RegisterProperty<System.Int32?>(p => p.MDGeneral_Enums_CurrencyId, string.Empty, 23);
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

		private static readonly PropertyInfo<System.String> billToAddress_NameProperty = RegisterProperty<System.String>(p => p.BillToAddress_Name, string.Empty, (System.String)null);
		public System.String BillToAddress_Name
		{
			get { return GetProperty(billToAddress_NameProperty); }
			set { SetProperty(billToAddress_NameProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Int32? > billToAddress_PlaceIdProperty = RegisterProperty<System.Int32?>(p => p.BillToAddress_PlaceId, string.Empty);
		public System.Int32? BillToAddress_PlaceId
		{
			get { return GetProperty(billToAddress_PlaceIdProperty); }
			set { SetProperty(billToAddress_PlaceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > billToAddress_StreetProperty = RegisterProperty<System.String>(p => p.BillToAddress_Street, string.Empty, (System.String)null);
		public System.String BillToAddress_Street
		{
			get { return GetProperty(billToAddress_StreetProperty); }
			set { SetProperty(billToAddress_StreetProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > billToAddress_NumberProperty = RegisterProperty<System.String>(p => p.BillToAddress_Number, string.Empty, (System.String)null);
		public System.String BillToAddress_Number
		{
			get { return GetProperty(billToAddress_NumberProperty); }
			set { SetProperty(billToAddress_NumberProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > billToAddress_DescriptionProperty = RegisterProperty<System.String>(p => p.BillToAddress_Description, string.Empty, (System.String)null);
		public System.String BillToAddress_Description
		{
			get { return GetProperty(billToAddress_DescriptionProperty); }
			set { SetProperty(billToAddress_DescriptionProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Int32? > shipToAddress_PlaceIdProperty = RegisterProperty<System.Int32?>(p => p.ShipToAddress_PlaceId, string.Empty);
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

		private static readonly PropertyInfo< System.String > shipToAddress_NumberProperty = RegisterProperty<System.String>(p => p.ShipToAddress_Number, string.Empty, (System.String)null);
		public System.String ShipToAddress_Number
		{
			get { return GetProperty(shipToAddress_NumberProperty); }
			set { SetProperty(shipToAddress_NumberProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > shipToAddress_DescriptionProperty = RegisterProperty<System.String>(p => p.ShipToAddress_Description, string.Empty, (System.String)null);
		public System.String ShipToAddress_Description
		{
			get { return GetProperty(shipToAddress_DescriptionProperty); }
			set { SetProperty(shipToAddress_DescriptionProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo<bool?> dispatchedProperty = RegisterProperty<bool?>(p => p.Dispatched, string.Empty, (bool?)null);
		public bool? Dispatched
		{
			get { return GetProperty(dispatchedProperty); }
			set { SetProperty(dispatchedProperty, value); }
		}


		private static readonly PropertyInfo<System.Int32?> documents_DispatchedIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_DispatchedId, string.Empty, (System.Int32?)null);
		public System.Int32? Documents_DispatchedId
		{
			get { return GetProperty(documents_DispatchedIdProperty); }
			set { SetProperty(documents_DispatchedIdProperty, value); }
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
        private static readonly PropertyInfo<System.String> fiscalizationConsistenceCodeProperty = RegisterProperty<System.String>(p => p.FiscalizationConsistenceCode, string.Empty, (System.String)null);
        public System.String FiscalizationConsistenceCode
        {
            get { return GetProperty(fiscalizationConsistenceCodeProperty); }
            set { SetProperty(fiscalizationConsistenceCodeProperty, (value ?? "").Trim()); }
        }
		#endregion

		#region Factory Methods

		public static cDocuments_Invoice NewDocuments_Invoice()
		{
			return DataPortal.Create<cDocuments_Invoice>();

		}

		public static cDocuments_Invoice GetDocuments_Invoice(int uniqueId)
		{
			return DataPortal.Fetch<cDocuments_Invoice>(new SingleCriteria<cDocuments_Invoice, int>(uniqueId));
		}

		internal static cDocuments_Invoice GetDocuments_Invoice(Documents_Invoice data)
		{
			return DataPortal.Fetch<cDocuments_Invoice>(data);
		}

		#endregion

		#region Data Access
		[RunLocal]
		protected override void DataPortal_Create()
		{
			BusinessRules.CheckRules();
		}

		private void DataPortal_Fetch(SingleCriteria<cDocuments_Invoice, int> criteria)
		{
			using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
			{
				var data = ctx.ObjectContext.Documents_Document.OfType<Documents_Invoice>().First(p => p.Id == criteria.Value);

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

                LoadProperty<string>(cashBoxCodeProperty, data.CashBoxCode);
                LoadProperty<string>(locationCodeProperty, data.LocationCode);

				LoadProperty<int?>(subjectIdProperty, data.SubjectId);
				LoadProperty<DateTime>(maturityDateProperty, data.MaturityDate);
				LoadProperty<bool?>(reversedProperty, data.Reversed);
				LoadProperty<bool?>(payedInFullProperty, data.PayedInFull);
				LoadProperty<bool?>(returnedProperty, data.Returned);
				LoadProperty<bool?>(approvedProperty, data.Approved);
				LoadProperty<bool?>(sentProperty, data.Sent);
				LoadProperty<bool?>(payedInAdvanceProperty, data.PayedInAdvance);
				LoadProperty<bool?>(recursionGenerationProperty, data.RecursionGeneration);
				LoadProperty<byte?>(recursionRateProperty, data.RecursionRate);
				LoadProperty<DateTime?>(recursionStartProperty, data.RecursionStart);
				LoadProperty<int?>(recursionNumberOfRatesProperty, data.RecursionNumberOfRates);
				LoadProperty<DateTime?>(dispatchDateProperty, data.DispatchDate);
				LoadProperty<int?>(dispatchTypeIdProperty, data.DispatchTypeId);
				LoadProperty<int?>(dispatchCompanyIdProperty, data.DispatchCompanyId);
				LoadProperty<string>(dispatchDescriptionProperty, data.DispatchDescription);
				LoadProperty<decimal>(wholesaleValueProperty, data.WholesaleValue);
				LoadProperty<decimal>(taxValueProperty, data.TaxValue);
				LoadProperty<decimal?>(rabateValueProperty, data.RabateValue);
				LoadProperty<decimal?>(otherDiscountsValueProperty, data.OtherDiscountsValue);
				LoadProperty<decimal>(retailValueProperty, data.RetailValue);
				LoadProperty<decimal?>(scontoPercentageProperty, data.ScontoPercentage);
				LoadProperty<DateTime?>(scontoDateProperty, data.ScontoDate);
				LoadProperty<decimal?>(scontoValueProperty, data.ScontoValue);
				LoadProperty<decimal?>(retailValueScontoProperty, data.RetailValueSconto);
				LoadProperty<string>(conditionsProperty, data.Conditions);
				LoadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty, data.MDGeneral_Enums_CurrencyId);
				LoadProperty<decimal?>(courseValueProperty, data.CourseValue);
				LoadProperty<string>(billToAddress_NameProperty, data.BillToAddress_Name);
				LoadProperty<int?>(billToAddress_PlaceIdProperty, data.BillToAddress_PlaceId);
				LoadProperty<string>(billToAddress_StreetProperty, data.BillToAddress_Street);
				LoadProperty<string>(billToAddress_NumberProperty, data.BillToAddress_Number);
				LoadProperty<string>(billToAddress_DescriptionProperty, data.BillToAddress_Description);
				LoadProperty<int?>(shipToAddress_PlaceIdProperty, data.ShipToAddress_PlaceId);
				LoadProperty<string>(shipToAddress_StreetProperty, data.ShipToAddress_Street);
				LoadProperty<string>(shipToAddress_NumberProperty, data.ShipToAddress_Number);
				LoadProperty<string>(shipToAddress_DescriptionProperty, data.ShipToAddress_Description);
				LoadProperty<bool?>(dispatchedProperty, data.Dispatched);
				LoadProperty<int?>(documents_DispatchedIdProperty, data.Documents_DispatchedId);
				LoadProperty<bool?>(enteredInTheAccountsProperty, data.EnteredInTheAccounts);

				LoadProperty<string>(fiscalizationZKIRProperty, data.FiscalizationZKIR);
				LoadProperty<string>(fiscalizationJIRProperty, data.FiscalizationJIR);
				LoadProperty<string>(fiscalizationMessageItentifierProperty, data.FiscalizationMessageItentifier);
				LoadProperty<DateTime?>(fiscalizationTimeProperty, data.FiscalizationTime);
				LoadProperty<int?>(fiscalizationTypeOfPaymentProperty, data.FiscalizationTypeOfPayment);
				LoadProperty<bool?>(fiscalizationDelayedDepartureProperty, data.FiscalizationDelayedDeparture);
				LoadProperty<int?>(fiscalizationCashierIdProperty, data.FiscalizationCashierId);
                LoadProperty<string>(fiscalizationConsistenceCodeProperty, data.FiscalizationConsistenceCode);

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
				var data = new Documents_Invoice();

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

				data.SubjectId = ReadProperty<int?>(subjectIdProperty);
				data.MaturityDate = ReadProperty<DateTime>(maturityDateProperty);
				data.Reversed = ReadProperty<bool?>(reversedProperty);
				data.PayedInFull = ReadProperty<bool?>(payedInFullProperty);
				data.Returned = ReadProperty<bool?>(returnedProperty);
				data.Approved = ReadProperty<bool?>(approvedProperty);
				data.Sent = ReadProperty<bool?>(sentProperty);
				data.PayedInAdvance = ReadProperty<bool?>(payedInAdvanceProperty);
				data.RecursionGeneration = ReadProperty<bool?>(recursionGenerationProperty);
				data.RecursionRate = ReadProperty<byte?>(recursionRateProperty);
				data.RecursionStart = ReadProperty<DateTime?>(recursionStartProperty);
				data.RecursionNumberOfRates = ReadProperty<int?>(recursionNumberOfRatesProperty);
				data.DispatchDate = ReadProperty<DateTime?>(dispatchDateProperty);
				data.DispatchTypeId = ReadProperty<int?>(dispatchTypeIdProperty);
				data.DispatchCompanyId = ReadProperty<int?>(dispatchCompanyIdProperty);
				data.DispatchDescription = ReadProperty<string>(dispatchDescriptionProperty);
				data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
				data.TaxValue = ReadProperty<decimal>(taxValueProperty);
				data.RabateValue = ReadProperty<decimal?>(rabateValueProperty);
				data.OtherDiscountsValue = ReadProperty<decimal?>(otherDiscountsValueProperty);
				data.RetailValue = ReadProperty<decimal>(retailValueProperty);
				data.ScontoPercentage = ReadProperty<decimal?>(scontoPercentageProperty);
				data.ScontoDate = ReadProperty<DateTime?>(scontoDateProperty);
				data.ScontoValue = ReadProperty<decimal?>(scontoValueProperty);
				data.RetailValueSconto = ReadProperty<decimal?>(retailValueScontoProperty);
				data.Conditions = ReadProperty<string>(conditionsProperty);
				data.MDGeneral_Enums_CurrencyId = ReadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty);
				data.CourseValue = ReadProperty<decimal?>(courseValueProperty);
				data.BillToAddress_Name = ReadProperty<string>(billToAddress_NameProperty);
				data.BillToAddress_PlaceId = ReadProperty<int?>(billToAddress_PlaceIdProperty);
				data.BillToAddress_Street = ReadProperty<string>(billToAddress_StreetProperty);
				data.BillToAddress_Number = ReadProperty<string>(billToAddress_NumberProperty);
				data.BillToAddress_Description = ReadProperty<string>(billToAddress_DescriptionProperty);
				data.ShipToAddress_PlaceId = ReadProperty<int?>(shipToAddress_PlaceIdProperty);
				data.ShipToAddress_Street = ReadProperty<string>(shipToAddress_StreetProperty);
				data.ShipToAddress_Number = ReadProperty<string>(shipToAddress_NumberProperty);
				data.ShipToAddress_Description = ReadProperty<string>(shipToAddress_DescriptionProperty);
				data.Dispatched = ReadProperty<bool?>(dispatchedProperty);
				data.Documents_DispatchedId = ReadProperty<int?>(documents_DispatchedIdProperty);
				data.EnteredInTheAccounts = ReadProperty<bool?>(enteredInTheAccountsProperty);

				//FiscalizationTime	datetime	Checked
				//FiscalizationTypeOfPayment	int	Checked
				//FiscalizationDelayedDeparture	bit	Checked

				data.FiscalizationZKIR = ReadProperty<string>(fiscalizationZKIRProperty);
				data.FiscalizationJIR = ReadProperty<string>(fiscalizationJIRProperty);
				data.FiscalizationMessageItentifier = ReadProperty<string>(fiscalizationMessageItentifierProperty);
				data.FiscalizationTime = ReadProperty<DateTime?>(fiscalizationTimeProperty);
				data.FiscalizationTypeOfPayment = ReadProperty<int?>(fiscalizationTypeOfPaymentProperty);
				data.FiscalizationDelayedDeparture = ReadProperty<bool?>(fiscalizationDelayedDepartureProperty);
				data.FiscalizationCashierId = ReadProperty<int?>(fiscalizationCashierIdProperty);
                data.FiscalizationConsistenceCode = ReadProperty<string>(fiscalizationConsistenceCodeProperty);

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
				var data = new Documents_Invoice();

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

				data.SubjectId = ReadProperty<int?>(subjectIdProperty);
				data.MaturityDate = ReadProperty<DateTime>(maturityDateProperty);
				data.Reversed = ReadProperty<bool?>(reversedProperty);
				data.PayedInFull = ReadProperty<bool?>(payedInFullProperty);
				data.Returned = ReadProperty<bool?>(returnedProperty);
				data.Approved = ReadProperty<bool?>(approvedProperty);
				data.Sent = ReadProperty<bool?>(sentProperty);
				data.PayedInAdvance = ReadProperty<bool?>(payedInAdvanceProperty);
				data.RecursionGeneration = ReadProperty<bool?>(recursionGenerationProperty);
				data.RecursionRate = ReadProperty<byte?>(recursionRateProperty);
				data.RecursionStart = ReadProperty<DateTime?>(recursionStartProperty);
				data.RecursionNumberOfRates = ReadProperty<int?>(recursionNumberOfRatesProperty);
				data.DispatchDate = ReadProperty<DateTime?>(dispatchDateProperty);
				data.DispatchTypeId = ReadProperty<int?>(dispatchTypeIdProperty);
				data.DispatchCompanyId = ReadProperty<int?>(dispatchCompanyIdProperty);
				data.DispatchDescription = ReadProperty<string>(dispatchDescriptionProperty);
				data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
				data.TaxValue = ReadProperty<decimal>(taxValueProperty);
				data.RabateValue = ReadProperty<decimal?>(rabateValueProperty);
				data.OtherDiscountsValue = ReadProperty<decimal?>(otherDiscountsValueProperty);
				data.RetailValue = ReadProperty<decimal>(retailValueProperty);
				data.ScontoPercentage = ReadProperty<decimal?>(scontoPercentageProperty);
				data.ScontoDate = ReadProperty<DateTime?>(scontoDateProperty);
				data.ScontoValue = ReadProperty<decimal?>(scontoValueProperty);
				data.RetailValueSconto = ReadProperty<decimal?>(retailValueScontoProperty);
				data.Conditions = ReadProperty<string>(conditionsProperty);
				data.MDGeneral_Enums_CurrencyId = ReadProperty<int?>(mDGeneral_Enums_CurrencyIdProperty);
				data.CourseValue = ReadProperty<decimal?>(courseValueProperty);
				data.BillToAddress_Name = ReadProperty<string>(billToAddress_NameProperty);
				data.BillToAddress_PlaceId = ReadProperty<int?>(billToAddress_PlaceIdProperty);
				data.BillToAddress_Street = ReadProperty<string>(billToAddress_StreetProperty);
				data.BillToAddress_Number = ReadProperty<string>(billToAddress_NumberProperty);
				data.BillToAddress_Description = ReadProperty<string>(billToAddress_DescriptionProperty);
				data.ShipToAddress_PlaceId = ReadProperty<int?>(shipToAddress_PlaceIdProperty);
				data.ShipToAddress_Street = ReadProperty<string>(shipToAddress_StreetProperty);
				data.ShipToAddress_Number = ReadProperty<string>(shipToAddress_NumberProperty);
				data.ShipToAddress_Description = ReadProperty<string>(shipToAddress_DescriptionProperty);
				data.Dispatched = ReadProperty<bool?>(dispatchedProperty);
				data.Documents_DispatchedId = ReadProperty<int?>(documents_DispatchedIdProperty);
				data.EnteredInTheAccounts = ReadProperty<bool?>(enteredInTheAccountsProperty);

				data.FiscalizationZKIR = ReadProperty<string>(fiscalizationZKIRProperty);
				data.FiscalizationJIR = ReadProperty<string>(fiscalizationJIRProperty);
				data.FiscalizationMessageItentifier = ReadProperty<string>(fiscalizationMessageItentifierProperty);
				data.FiscalizationTime = ReadProperty<DateTime?>(fiscalizationTimeProperty);
				data.FiscalizationTypeOfPayment = ReadProperty<int?>(fiscalizationTypeOfPaymentProperty);
				data.FiscalizationDelayedDeparture = ReadProperty<bool?>(fiscalizationDelayedDepartureProperty);
				data.FiscalizationCashierId = ReadProperty<int?>(fiscalizationCashierIdProperty);
                data.FiscalizationConsistenceCode = ReadProperty<string>(fiscalizationConsistenceCodeProperty);

				DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);

				ctx.ObjectContext.SaveChanges();
			}
		}
		#endregion
	}
}
