using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using BusinessObjects.Common;

namespace BusinessObjects.MDGeneral
{
	[Serializable]
	public partial class cMDGeneral_Enums_Currency: CoreBusinessClass<cMDGeneral_Enums_Currency>
	{
		#region Business Methods
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			internal set { SetProperty(IdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > labelProperty = RegisterProperty<System.String>(p => p.Label, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(3, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Label
		{
			get { return GetProperty(labelProperty); }
			set { SetProperty(labelProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< System.String > nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
			set { SetProperty(nameProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< System.Int32 > measureProperty = RegisterProperty<System.Int32>(p => p.Measure, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Measure
		{
			get { return GetProperty(measureProperty); }
			set { SetProperty(measureProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > decimalPlacesProperty = RegisterProperty<System.Int32>(p => p.DecimalPlaces, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 DecimalPlaces
		{
			get { return GetProperty(decimalPlacesProperty); }
			set { SetProperty(decimalPlacesProperty, value); }
		}

		private static readonly PropertyInfo< bool > inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public bool Inactive
		{
			get { return GetProperty(inactiveProperty); }
			set { SetProperty(inactiveProperty, value); }
		}

		private static readonly PropertyInfo< bool > defaultCurrencyProperty = RegisterProperty<bool>(p => p.DefaultCurrency, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public bool DefaultCurrency
		{
			get { return GetProperty(defaultCurrencyProperty); }
			set { SetProperty(defaultCurrencyProperty, value); }
		}

        protected static readonly PropertyInfo<System.Int32?> companyUsingServiceIdProperty = RegisterProperty<System.Int32?>(p => p.CompanyUsingServiceId, string.Empty);
        public System.Int32? CompanyUsingServiceId
        {
            get { return GetProperty(companyUsingServiceIdProperty); }
            set { SetProperty(companyUsingServiceIdProperty, value); }
        }

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#endregion

        #region Factory Methods

        public static cMDGeneral_Enums_Currency NewMDGeneral_Enums_Currency()
        {
            return DataPortal.Create<cMDGeneral_Enums_Currency>();

        }

        public static cMDGeneral_Enums_Currency GetMDGeneral_Enums_Currency(int uniqueId)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Currency>(new SingleCriteria<cMDGeneral_Enums_Currency, int>(uniqueId));
        }

        internal static cMDGeneral_Enums_Currency GetMDGeneral_Enums_Currency(MDGeneral_Enums_Currency data)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Currency>(data);
        }

        public static void DeleteMDGeneral_Enums_Currency(int uniqueId)
        {
            DataPortal.Delete<cMDGeneral_Enums_Currency>(new SingleCriteria<cMDGeneral_Enums_Currency, int>(uniqueId));
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDGeneral_Enums_Currency, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = ctx.ObjectContext.MDGeneral_Enums_Currency.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(labelProperty, data.Label);
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<int>(measureProperty, data.Measure);
                LoadProperty<int>(decimalPlacesProperty, data.DecimalPlaces);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<bool>(defaultCurrencyProperty, data.DefaultCurrency);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDGeneral_Enums_Currency data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<string>(labelProperty, data.Label);
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<int>(measureProperty, data.Measure);
            LoadProperty<int>(decimalPlacesProperty, data.DecimalPlaces);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LoadProperty<bool>(defaultCurrencyProperty, data.DefaultCurrency);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
            MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = new MDGeneral_Enums_Currency();

                data.Label = ReadProperty<string>(labelProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.Measure = ReadProperty<int>(measureProperty);
                data.DecimalPlaces = ReadProperty<int>(decimalPlacesProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.DefaultCurrency = ReadProperty<bool>(defaultCurrencyProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToMDGeneral_Enums_Currency(data);
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
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = new MDGeneral_Enums_Currency();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Label = ReadProperty<string>(labelProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.Measure = ReadProperty<int>(measureProperty);
                data.DecimalPlaces = ReadProperty<int>(decimalPlacesProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.DefaultCurrency = ReadProperty<bool>(defaultCurrencyProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<cMDGeneral_Enums_Currency, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = ctx.ObjectContext.MDGeneral_Enums_Currency.First(p => p.Id == criteria.Value);

                ctx.ObjectContext.MDGeneral_Enums_Currency.DeleteObject(data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }

    public partial class cMDGeneral_Enums_Currency_List : BusinessListBase<cMDGeneral_Enums_Currency_List, cMDGeneral_Enums_Currency>
    {
        public static cMDGeneral_Enums_Currency_List GetcMDGeneral_Enums_Currency_List()
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Currency_List>();
        }

        public static cMDGeneral_Enums_Currency_List GetcMDGeneral_Enums_Currency_List(int companyId, int includeInactiveId)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Currency_List>(new ActiveEnums_Criteria(companyId, includeInactiveId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var result = ctx.ObjectContext.MDGeneral_Enums_Currency;

                foreach (var data in result)
                {
                    var obj = cMDGeneral_Enums_Currency.GetMDGeneral_Enums_Currency(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(ActiveEnums_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var result = ctx.ObjectContext.MDGeneral_Enums_Currency.Where(p => (p.CompanyUsingServiceId == criteria.CompanyId || (p.CompanyUsingServiceId ?? 0) == 0) && (p.Inactive == false || p.Id == criteria.IncludeInactiveId));

                foreach (var data in result)
                {
                    var obj = cMDGeneral_Enums_Currency.GetMDGeneral_Enums_Currency(data);

                    this.Add(obj);
                }
            }
        }
    }
}

