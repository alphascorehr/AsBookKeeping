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
	public partial class cDocuments_TravelOrder: cDocuments_Document<cDocuments_TravelOrder>
	{
		#region Business Methods
	
		private static readonly PropertyInfo< System.Int32? > mDSubjects_EmployeeIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_EmployeeId, string.Empty,(System.Int32?)null);
		public System.Int32? MDSubjects_EmployeeId
		{
			get { return GetProperty(mDSubjects_EmployeeIdProperty); }
			set { SetProperty(mDSubjects_EmployeeIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDSubjects_Enums_FunctionIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Enums_FunctionId, string.Empty,(System.Int32?)null);
		public System.Int32? MDSubjects_Enums_FunctionId
		{
			get { return GetProperty(mDSubjects_Enums_FunctionIdProperty); }
			set { SetProperty(mDSubjects_Enums_FunctionIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDSubjects_Enums_EducationDegreeIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Enums_EducationDegreeId, string.Empty,(System.Int32?)null);
		public System.Int32? MDSubjects_Enums_EducationDegreeId
		{
			get { return GetProperty(mDSubjects_Enums_EducationDegreeIdProperty); }
			set { SetProperty(mDSubjects_Enums_EducationDegreeIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDPlaces_Enums_Geo_FromPlaceIdProperty = RegisterProperty<System.Int32?>(p => p.MDPlaces_Enums_Geo_FromPlaceId, string.Empty,(System.Int32?)null);
		public System.Int32? MDPlaces_Enums_Geo_FromPlaceId
		{
			get { return GetProperty(mDPlaces_Enums_Geo_FromPlaceIdProperty); }
			set { SetProperty(mDPlaces_Enums_Geo_FromPlaceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDPlaces_Enums_Geo_ToPlaceIdProperty = RegisterProperty<System.Int32?>(p => p.MDPlaces_Enums_Geo_ToPlaceId, string.Empty,(System.Int32?)null);
		public System.Int32? MDPlaces_Enums_Geo_ToPlaceId
		{
			get { return GetProperty(mDPlaces_Enums_Geo_ToPlaceIdProperty); }
			set { SetProperty(mDPlaces_Enums_Geo_ToPlaceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime? > travelOrderDateProperty = RegisterProperty<System.DateTime?>(p => p.TravelOrderDate, string.Empty, (System.DateTime?)null);
		public System.DateTime? TravelOrderDate
		{
			get { return GetProperty(travelOrderDateProperty); }
			set { SetProperty(travelOrderDateProperty, value); }
		}

		private static readonly PropertyInfo< bool? > travelToResidenceProperty = RegisterProperty<bool?>(p => p.TravelToResidence, string.Empty,(bool?)null);
		public bool? TravelToResidence
		{
			get { return GetProperty(travelToResidenceProperty); }
			set { SetProperty(travelToResidenceProperty, value); }
		}

		private static readonly PropertyInfo< System.String > taskDescriptionProperty = RegisterProperty<System.String>(p => p.TaskDescription, string.Empty, (System.String)null);
		public System.String TaskDescription
		{
			get { return GetProperty(taskDescriptionProperty); }
			set { SetProperty(taskDescriptionProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Decimal? > durationDaysProperty = RegisterProperty<System.Decimal?>(p => p.DurationDays, string.Empty, (System.Decimal?)null);
		public System.Decimal? DurationDays
		{
			get { return GetProperty(durationDaysProperty); }
			set { SetProperty(durationDaysProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDGeneral_Enums_KindOfTransportationIdProperty = RegisterProperty<System.Int32?>(p => p.MDGeneral_Enums_KindOfTransportationId, string.Empty,(System.Int32?)null);
		public System.Int32? MDGeneral_Enums_KindOfTransportationId
		{
			get { return GetProperty(mDGeneral_Enums_KindOfTransportationIdProperty); }
			set { SetProperty(mDGeneral_Enums_KindOfTransportationIdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > transportationDescriptionProperty = RegisterProperty<System.String>(p => p.TransportationDescription, string.Empty, (System.String)null);
		public System.String TransportationDescription
		{
			get { return GetProperty(transportationDescriptionProperty); }
			set { SetProperty(transportationDescriptionProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.String > costCenterProperty = RegisterProperty<System.String>(p => p.CostCenter, string.Empty, (System.String)null);
		public System.String CostCenter
		{
			get { return GetProperty(costCenterProperty); }
			set { SetProperty(costCenterProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.DateTime? > advanceDateProperty = RegisterProperty<System.DateTime?>(p => p.AdvanceDate, string.Empty, (System.DateTime?)null);
		public System.DateTime? AdvanceDate
		{
			get { return GetProperty(advanceDateProperty); }
			set { SetProperty(advanceDateProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > advanceAmmountProperty = RegisterProperty<System.Decimal?>(p => p.AdvanceAmmount, string.Empty, (System.Decimal?)null);
		public System.Decimal? AdvanceAmmount
		{
			get { return GetProperty(advanceAmmountProperty); }
			set { SetProperty(advanceAmmountProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDSubjects_ApprovedByEmployeeIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_ApprovedByEmployeeId, string.Empty,(System.Int32?)null);
		public System.Int32? MDSubjects_ApprovedByEmployeeId
		{
			get { return GetProperty(mDSubjects_ApprovedByEmployeeIdProperty); }
			set { SetProperty(mDSubjects_ApprovedByEmployeeIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > carDialStartProperty = RegisterProperty<System.Int32?>(p => p.CarDialStart, string.Empty,(System.Int32?)null);
		public System.Int32? CarDialStart
		{
			get { return GetProperty(carDialStartProperty); }
			set { SetProperty(carDialStartProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > carDialEndProperty = RegisterProperty<System.Int32?>(p => p.CarDialEnd, string.Empty,(System.Int32?)null);
		public System.Int32? CarDialEnd
		{
			get { return GetProperty(carDialEndProperty); }
			set { SetProperty(carDialEndProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > totalKilometersProperty = RegisterProperty<System.Int32?>(p => p.TotalKilometers, string.Empty,(System.Int32?)null);
		public System.Int32? TotalKilometers
		{
			get { return GetProperty(totalKilometersProperty); }
			set { SetProperty(totalKilometersProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > pricePerKilometerProperty = RegisterProperty<System.Decimal?>(p => p.PricePerKilometer, string.Empty, (System.Decimal?)null);
		public System.Decimal? PricePerKilometer
		{
			get { return GetProperty(pricePerKilometerProperty); }
			set { SetProperty(pricePerKilometerProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > amountForCarExpenseProperty = RegisterProperty<System.Decimal?>(p => p.AmountForCarExpense, string.Empty, (System.Decimal?)null);
		public System.Decimal? AmountForCarExpense
		{
			get { return GetProperty(amountForCarExpenseProperty); }
			set { SetProperty(amountForCarExpenseProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > totalAmmountProperty = RegisterProperty<System.Decimal?>(p => p.TotalAmmount, string.Empty, (System.Decimal?)null);
		public System.Decimal? TotalAmmount
		{
			get { return GetProperty(totalAmmountProperty); }
			set { SetProperty(totalAmmountProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > totalAmmountForPaymentProperty = RegisterProperty<System.Decimal?>(p => p.TotalAmmountForPayment, string.Empty, (System.Decimal?)null);
		public System.Decimal? TotalAmmountForPayment
		{
			get { return GetProperty(totalAmmountForPaymentProperty); }
			set { SetProperty(totalAmmountForPaymentProperty, value); }
		}

		private static readonly PropertyInfo< System.String > trevelDescriptionProperty = RegisterProperty<System.String>(p => p.TrevelDescription, string.Empty, (System.String)null);
		public System.String TrevelDescription
		{
			get { return GetProperty(trevelDescriptionProperty); }
			set { SetProperty(trevelDescriptionProperty, (value ?? "").Trim()); }
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

		#region Kolekcije
		public static PropertyInfo<cDocuments_TravelOrder_WageCol> Documents_TravelOrder_WageColProperty = RegisterProperty<cDocuments_TravelOrder_WageCol>(c => c.Documents_TravelOrder_WageCol);
		public cDocuments_TravelOrder_WageCol Documents_TravelOrder_WageCol
		{
			get
			{
				if (!FieldManager.FieldExists(Documents_TravelOrder_WageColProperty))
					LoadProperty<cDocuments_TravelOrder_WageCol>(Documents_TravelOrder_WageColProperty, cDocuments_TravelOrder_WageCol.NewDocuments_TravelOrder_WageCol());
				return GetProperty(Documents_TravelOrder_WageColProperty);
			}
			private set { SetProperty(Documents_TravelOrder_WageColProperty, value); }
		}

		public static PropertyInfo<cDocuments_TravelOrder_TravelCostsCol> Documents_TravelOrder_TravelCostsColProperty = RegisterProperty<cDocuments_TravelOrder_TravelCostsCol>(c => c.Documents_TravelOrder_TravelCostsCol);
		public cDocuments_TravelOrder_TravelCostsCol Documents_TravelOrder_TravelCostsCol
		{
			get
			{
				if (!FieldManager.FieldExists(Documents_TravelOrder_TravelCostsColProperty))
					LoadProperty<cDocuments_TravelOrder_TravelCostsCol>(Documents_TravelOrder_TravelCostsColProperty, cDocuments_TravelOrder_TravelCostsCol.NewDocuments_TravelOrder_TravelCostsCol());
				return GetProperty(Documents_TravelOrder_TravelCostsColProperty);
			}
			private set { SetProperty(Documents_TravelOrder_TravelCostsColProperty, value); }
		}

		public static PropertyInfo<cDocuments_TravelOrder_OtherExpensesCol> Documents_TravelOrder_OtherExpensesColProperty = RegisterProperty<cDocuments_TravelOrder_OtherExpensesCol>(c => c.Documents_TravelOrder_OtherExpensesCol);
		public cDocuments_TravelOrder_OtherExpensesCol Documents_TravelOrder_OtherExpensesCol
		{
			get
			{
				if (!FieldManager.FieldExists(Documents_TravelOrder_OtherExpensesColProperty))
					LoadProperty<cDocuments_TravelOrder_OtherExpensesCol>(Documents_TravelOrder_OtherExpensesColProperty, cDocuments_TravelOrder_OtherExpensesCol.NewDocuments_TravelOrder_OtherExpensesCol());
				return GetProperty(Documents_TravelOrder_OtherExpensesColProperty);
			}
			private set { SetProperty(Documents_TravelOrder_OtherExpensesColProperty, value); }
		}

		#endregion
		#endregion
        #region Factory Methods

        public static cDocuments_TravelOrder NewDocuments_TravelOrder()
        {
            return DataPortal.Create<cDocuments_TravelOrder>();

        }

        public static cDocuments_TravelOrder GetDocuments_TravelOrder(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_TravelOrder>(new SingleCriteria<cDocuments_TravelOrder, int>(uniqueId));
        }

        internal static cDocuments_TravelOrder GetDocuments_TravelOrder(Documents_TravelOrder data)
        {
            return DataPortal.Fetch<cDocuments_TravelOrder>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_TravelOrder, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Document.OfType<Documents_TravelOrder>().First(p => p.Id == criteria.Value);

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

                LoadProperty<int?>(mDSubjects_EmployeeIdProperty, data.MDSubjects_EmployeeId);
                LoadProperty<int?>(mDSubjects_Enums_FunctionIdProperty, data.MDSubjects_Enums_FunctionId);
                LoadProperty<int?>(mDSubjects_Enums_EducationDegreeIdProperty, data.MDSubjects_Enums_EducationDegreeId);
                LoadProperty<int?>(mDPlaces_Enums_Geo_FromPlaceIdProperty, data.MDPlaces_Enums_Geo_FromPlaceId);
                LoadProperty<int?>(mDPlaces_Enums_Geo_ToPlaceIdProperty, data.MDPlaces_Enums_Geo_ToPlaceId);
                LoadProperty<DateTime?>(travelOrderDateProperty, data.TravelOrderDate);
                LoadProperty<bool?>(travelToResidenceProperty, data.TravelToResidence);
                LoadProperty<string>(taskDescriptionProperty, data.TaskDescription);
                LoadProperty<decimal?>(durationDaysProperty, data.DurationDays);
                LoadProperty<int?>(mDGeneral_Enums_KindOfTransportationIdProperty, data.MDGeneral_Enums_KindOfTransportationId);
                LoadProperty<string>(transportationDescriptionProperty, data.TransportationDescription);
                LoadProperty<string>(costCenterProperty, data.CostCenter);
                LoadProperty<DateTime?>(advanceDateProperty, data.AdvanceDate);
                LoadProperty<decimal?>(advanceAmmountProperty, data.AdvanceAmmount);
                LoadProperty<int?>(mDSubjects_ApprovedByEmployeeIdProperty, data.MDSubjects_ApprovedByEmployeeId);
                LoadProperty<int?>(carDialStartProperty, data.CarDialStart);
                LoadProperty<int?>(carDialEndProperty, data.CarDialEnd);
                LoadProperty<int?>(totalKilometersProperty, data.TotalKilometers);
                LoadProperty<decimal?>(pricePerKilometerProperty, data.PricePerKilometer);
                LoadProperty<decimal?>(amountForCarExpenseProperty, data.AmountForCarExpense);
                LoadProperty<decimal?>(totalAmmountProperty, data.TotalAmmount);
                LoadProperty<decimal?>(totalAmmountForPaymentProperty, data.TotalAmmountForPayment);
                LoadProperty<string>(trevelDescriptionProperty, data.TrevelDescription);
                LoadProperty<bool?>(enteredInTheAccountsProperty, data.EnteredInTheAccounts);
                
                LastChanged = data.LastChanged;

                //LoadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty, cDocuments_ItemsCol.GetDocuments_ItemsCol(data.Documents_ItemsCol));
                LoadProperty<cDocuments_TravelOrder_WageCol>(Documents_TravelOrder_WageColProperty, cDocuments_TravelOrder_WageCol.GetDocuments_TravelOrder_WageCol(data.Documents_TravelOrder_WageCol));
                LoadProperty<cDocuments_TravelOrder_TravelCostsCol>(Documents_TravelOrder_TravelCostsColProperty, cDocuments_TravelOrder_TravelCostsCol.GetDocuments_TravelOrder_TravelCostsCol(data.Documents_TravelOrder_TravelCostsCol));
                LoadProperty<cDocuments_TravelOrder_OtherExpensesCol>(Documents_TravelOrder_OtherExpensesColProperty, cDocuments_TravelOrder_OtherExpensesCol.GetDocuments_TravelOrder_OtherExpensesCol(data.Documents_TravelOrder_OtherExpensesCol));

                BusinessRules.CheckRules();

            }
        }

     

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_TravelOrder();

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

                data.MDSubjects_EmployeeId = ReadProperty<int?>(mDSubjects_EmployeeIdProperty);
                data.MDSubjects_Enums_FunctionId = ReadProperty<int?>(mDSubjects_Enums_FunctionIdProperty);
                data.MDSubjects_Enums_EducationDegreeId = ReadProperty<int?>(mDSubjects_Enums_EducationDegreeIdProperty);
                data.MDPlaces_Enums_Geo_FromPlaceId = ReadProperty<int?>(mDPlaces_Enums_Geo_FromPlaceIdProperty);
                data.MDPlaces_Enums_Geo_ToPlaceId = ReadProperty<int?>(mDPlaces_Enums_Geo_ToPlaceIdProperty);
                data.TravelOrderDate = ReadProperty<DateTime?>(travelOrderDateProperty);
                data.TravelToResidence = ReadProperty<bool?>(travelToResidenceProperty);
                data.TaskDescription = ReadProperty<string>(taskDescriptionProperty);
                data.DurationDays = ReadProperty<decimal?>(durationDaysProperty);
                data.MDGeneral_Enums_KindOfTransportationId = ReadProperty<int?>(mDGeneral_Enums_KindOfTransportationIdProperty);
                data.TransportationDescription = ReadProperty<string>(transportationDescriptionProperty);
                data.CostCenter = ReadProperty<string>(costCenterProperty);
                data.AdvanceDate = ReadProperty<DateTime?>(advanceDateProperty);
                data.AdvanceAmmount = ReadProperty<decimal?>(advanceAmmountProperty);
                data.MDSubjects_ApprovedByEmployeeId = ReadProperty<int?>(mDSubjects_ApprovedByEmployeeIdProperty);
                data.CarDialStart = ReadProperty<int?>(carDialStartProperty);
                data.CarDialEnd = ReadProperty<int?>(carDialEndProperty);
                data.TotalKilometers = ReadProperty<int?>(totalKilometersProperty);
                data.PricePerKilometer = ReadProperty<decimal?>(pricePerKilometerProperty);
                data.AmountForCarExpense = ReadProperty<decimal?>(amountForCarExpenseProperty);
                data.TotalAmmount = ReadProperty<decimal?>(totalAmmountProperty);
                data.TotalAmmountForPayment = ReadProperty<decimal?>(totalAmmountForPaymentProperty);
                data.TrevelDescription = ReadProperty<string>(trevelDescriptionProperty);
                data.EnteredInTheAccounts = ReadProperty<bool?>(enteredInTheAccountsProperty);

                ctx.ObjectContext.AddToDocuments_Document(data);

                //DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_TravelOrder_WageCol>(Documents_TravelOrder_WageColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_TravelOrder_TravelCostsCol>(Documents_TravelOrder_TravelCostsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_TravelOrder_OtherExpensesCol>(Documents_TravelOrder_OtherExpensesColProperty), data);
                
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
                var data = new Documents_TravelOrder();

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

                data.MDSubjects_EmployeeId = ReadProperty<int?>(mDSubjects_EmployeeIdProperty);
                data.MDSubjects_Enums_FunctionId = ReadProperty<int?>(mDSubjects_Enums_FunctionIdProperty);
                data.MDSubjects_Enums_EducationDegreeId = ReadProperty<int?>(mDSubjects_Enums_EducationDegreeIdProperty);
                data.MDPlaces_Enums_Geo_FromPlaceId = ReadProperty<int?>(mDPlaces_Enums_Geo_FromPlaceIdProperty);
                data.MDPlaces_Enums_Geo_ToPlaceId = ReadProperty<int?>(mDPlaces_Enums_Geo_ToPlaceIdProperty);
                data.TravelOrderDate = ReadProperty<DateTime?>(travelOrderDateProperty);
                data.TravelToResidence = ReadProperty<bool?>(travelToResidenceProperty);
                data.TaskDescription = ReadProperty<string>(taskDescriptionProperty);
                data.DurationDays = ReadProperty<decimal?>(durationDaysProperty);
                data.MDGeneral_Enums_KindOfTransportationId = ReadProperty<int?>(mDGeneral_Enums_KindOfTransportationIdProperty);
                data.TransportationDescription = ReadProperty<string>(transportationDescriptionProperty);
                data.CostCenter = ReadProperty<string>(costCenterProperty);
                data.AdvanceDate = ReadProperty<DateTime?>(advanceDateProperty);
                data.AdvanceAmmount = ReadProperty<decimal?>(advanceAmmountProperty);
                data.MDSubjects_ApprovedByEmployeeId = ReadProperty<int?>(mDSubjects_ApprovedByEmployeeIdProperty);
                data.CarDialStart = ReadProperty<int?>(carDialStartProperty);
                data.CarDialEnd = ReadProperty<int?>(carDialEndProperty);
                data.TotalKilometers = ReadProperty<int?>(totalKilometersProperty);
                data.PricePerKilometer = ReadProperty<decimal?>(pricePerKilometerProperty);
                data.AmountForCarExpense = ReadProperty<decimal?>(amountForCarExpenseProperty);
                data.TotalAmmount = ReadProperty<decimal?>(totalAmmountProperty);
                data.TotalAmmountForPayment = ReadProperty<decimal?>(totalAmmountForPaymentProperty);
                data.TrevelDescription = ReadProperty<string>(trevelDescriptionProperty);
                data.EnteredInTheAccounts = ReadProperty<bool?>(enteredInTheAccountsProperty);

                //DataPortal.UpdateChild(ReadProperty<cDocuments_ItemsCol>(Documents_ItemsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_TravelOrder_WageCol>(Documents_TravelOrder_WageColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_TravelOrder_TravelCostsCol>(Documents_TravelOrder_TravelCostsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cDocuments_TravelOrder_OtherExpensesCol>(Documents_TravelOrder_OtherExpensesColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }
}

