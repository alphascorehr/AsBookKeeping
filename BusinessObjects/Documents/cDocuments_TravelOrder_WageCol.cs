using System;
using Csla;
using Csla.Data;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using DalEf;
using System.Collections.Generic;

namespace BusinessObjects.Documents
{
	[Serializable]
	public partial class cDocuments_TravelOrder_Wage: CoreBusinessChildClass<cDocuments_TravelOrder_Wage>
	{
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			internal set { SetProperty(IdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > documents_TravelOrderIdProperty = RegisterProperty<System.Int32>(p => p.Documents_TravelOrderId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Documents_TravelOrderId
		{
			get { return GetProperty(documents_TravelOrderIdProperty); }
			set { SetProperty(documents_TravelOrderIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32> ordinalProperty = RegisterProperty<System.Int32>(p => p.Ordinal, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 Ordinal
        {
            get { return GetProperty(ordinalProperty); }
            set { SetProperty(ordinalProperty, value); }
        }

		private static readonly PropertyInfo< System.DateTime? > departureProperty = RegisterProperty<System.DateTime?>(p => p.Departure, string.Empty, (System.DateTime?)null);
		public System.DateTime? Departure
		{
			get { return GetProperty(departureProperty); }
			set { SetProperty(departureProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime? > arrivalProperty = RegisterProperty<System.DateTime?>(p => p.Arrival, string.Empty, (System.DateTime?)null);
		public System.DateTime? Arrival
		{
			get { return GetProperty(arrivalProperty); }
			set { SetProperty(arrivalProperty, value); }
		}

        private static readonly PropertyInfo<System.Decimal?> hoursProperty = RegisterProperty<System.Decimal?>(p => p.Hours, string.Empty, (System.Decimal?)null);
		public System.Decimal? Hours
		{
			get { return GetProperty(hoursProperty); }
			set { SetProperty(hoursProperty, value); }
		}

        private static readonly PropertyInfo<System.Decimal?> numberOfWageProperty = RegisterProperty<System.Decimal?>(p => p.NumberOfWage, string.Empty, (System.Decimal?)null);
        public System.Decimal? NumberOfWage
		{
			get { return GetProperty(numberOfWageProperty); }
			set { SetProperty(numberOfWageProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > priceOfWageProperty = RegisterProperty<System.Decimal?>(p => p.PriceOfWage, string.Empty, (System.Decimal?)null);
		public System.Decimal? PriceOfWage
		{
			get { return GetProperty(priceOfWageProperty); }
			set { SetProperty(priceOfWageProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > ammountOfWageProperty = RegisterProperty<System.Decimal?>(p => p.AmmountOfWage, string.Empty, (System.Decimal?)null);
		public System.Decimal? AmmountOfWage
		{
			get { return GetProperty(ammountOfWageProperty); }
			set { SetProperty(ammountOfWageProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cDocuments_TravelOrder_Wage NewDocuments_TravelOrder_Wage()
		{
			return DataPortal.CreateChild<cDocuments_TravelOrder_Wage>();
		}

        public static cDocuments_TravelOrder_Wage GetDocuments_TravelOrder_Wage(Documents_TravelOrder_WageCol data)
        {
            return DataPortal.FetchChild<cDocuments_TravelOrder_Wage>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_TravelOrder_WageCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(documents_TravelOrderIdProperty, data.Documents_TravelOrderId);
            LoadProperty<int>(ordinalProperty, data.Ordinal);
            LoadProperty<DateTime?>(departureProperty, data.Departure);
            LoadProperty<DateTime?>(arrivalProperty, data.Arrival);
            LoadProperty<decimal?>(hoursProperty, data.Hours);
            LoadProperty<decimal?>(numberOfWageProperty, data.NumberOfWage);
            LoadProperty<decimal?>(priceOfWageProperty, data.PriceOfWage);
            LoadProperty<decimal?>(ammountOfWageProperty, data.AmmountOfWage);

            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(Documents_TravelOrder parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_TravelOrder_WageCol();

                data.Documents_TravelOrder = parent;

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.Departure = ReadProperty<DateTime?>(departureProperty);
                data.Arrival = ReadProperty<DateTime?>(arrivalProperty);
                data.Hours = ReadProperty<decimal?>(hoursProperty);
                data.NumberOfWage = ReadProperty<decimal?>(numberOfWageProperty);
                data.PriceOfWage = ReadProperty<decimal?>(priceOfWageProperty);
                data.AmmountOfWage = ReadProperty<decimal?>(ammountOfWageProperty);

                ctx.ObjectContext.AddToDocuments_TravelOrder_WageCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(documents_TravelOrderIdProperty, data.Documents_TravelOrderId);
                        LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                        LastChanged = data.LastChanged;
                    }
                };
            }
        }

        private void Child_Update()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_TravelOrder_WageCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.Departure = ReadProperty<DateTime?>(departureProperty);
                data.Arrival = ReadProperty<DateTime?>(arrivalProperty);
                data.Hours = ReadProperty<decimal?>(hoursProperty);
                data.NumberOfWage = ReadProperty<decimal?>(numberOfWageProperty);
                data.PriceOfWage = ReadProperty<decimal?>(priceOfWageProperty);
                data.AmmountOfWage = ReadProperty<decimal?>(ammountOfWageProperty);

                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "LastChanged")
                        LastChanged = data.LastChanged;
                };

            }
        }

        private void Child_DeleteSelf()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_TravelOrder_WageCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);

                ctx.ObjectContext.DeleteObject(data);
            }
        }
        #endregion
    }
    [Serializable]
    public partial class cDocuments_TravelOrder_WageCol : BusinessListBase<cDocuments_TravelOrder_WageCol, cDocuments_TravelOrder_Wage>
    {
        internal static cDocuments_TravelOrder_WageCol NewDocuments_TravelOrder_WageCol()
        {
            return DataPortal.CreateChild<cDocuments_TravelOrder_WageCol>();
        }

        public static cDocuments_TravelOrder_WageCol GetDocuments_TravelOrder_WageCol(IEnumerable<Documents_TravelOrder_WageCol> dataSet)
        {
            var childList = new cDocuments_TravelOrder_WageCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<Documents_TravelOrder_WageCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cDocuments_TravelOrder_Wage.GetDocuments_TravelOrder_Wage(data));

            RaiseListChangedEvents = true;


        }
        #endregion //Data Access
    }


}


