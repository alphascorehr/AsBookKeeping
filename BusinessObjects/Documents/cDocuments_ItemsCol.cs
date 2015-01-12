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
	public partial class cDocuments_Items: CoreBusinessChildClass<cDocuments_Items>
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

		private static readonly PropertyInfo< System.Int32 > documentIdProperty = RegisterProperty<System.Int32>(p => p.DocumentId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 DocumentId
		{
			get { return GetProperty(documentIdProperty); }
			set { SetProperty(documentIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > ordinalProperty = RegisterProperty<System.Int32>(p => p.Ordinal, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Ordinal
		{
			get { return GetProperty(ordinalProperty); }
			set { SetProperty(ordinalProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > productIdProperty = RegisterProperty<System.Int32>(p => p.ProductId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 ProductId
		{
			get { return GetProperty(productIdProperty); }
			set { SetProperty(productIdProperty, value); }
		}

        protected static readonly PropertyInfo<System.String> productDescriptionProperty = RegisterProperty<System.String>(p => p.ProductDescription, string.Empty, (System.String)null);
            public System.String ProductDescription
		{
            get { return GetProperty(productDescriptionProperty); }
            set { SetProperty(productDescriptionProperty, (value ?? "").Trim()); }
		}
     

		private static readonly PropertyInfo< System.Int32 > unitIdProperty = RegisterProperty<System.Int32>(p => p.UnitId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 UnitId
		{
			get { return GetProperty(unitIdProperty); }
			set { SetProperty(unitIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Decimal> quantityProperty = RegisterProperty<System.Decimal>(p => p.Quantity, string.Empty, (decimal)1);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Decimal Quantity
        {
            get { return GetProperty(quantityProperty); }
            set { SetProperty(quantityProperty, value); }
        }

		private static readonly PropertyInfo< System.Decimal > ammountProperty = RegisterProperty<System.Decimal>(p => p.Ammount, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal Ammount
		{
			get { return GetProperty(ammountProperty); }
			set { SetProperty(ammountProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > priceListIdProperty = RegisterProperty<System.Int32?>(p => p.PriceListId, string.Empty,(System.Int32?)null);
		public System.Int32? PriceListId
		{
			get { return GetProperty(priceListIdProperty); }
			set { SetProperty(priceListIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > priceFromPriceListProperty = RegisterProperty<System.Decimal?>(p => p.PriceFromPriceList, string.Empty, (System.Decimal?)null);
		public System.Decimal? PriceFromPriceList
		{
			get { return GetProperty(priceFromPriceListProperty); }
			set { SetProperty(priceFromPriceListProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > priceProperty = RegisterProperty<System.Decimal>(p => p.Price, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal Price
		{
			get { return GetProperty(priceProperty); }
			set { SetProperty(priceProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > rabatePercentageProperty = RegisterProperty<System.Decimal?>(p => p.RabatePercentage, string.Empty, (System.Decimal?)null);
		public System.Decimal? RabatePercentage
		{
			get { return GetProperty(rabatePercentageProperty); }
			set { SetProperty(rabatePercentageProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > rabateAmmountProperty = RegisterProperty<System.Decimal?>(p => p.RabateAmmount, string.Empty, (System.Decimal?)null);
		public System.Decimal? RabateAmmount
		{
			get { return GetProperty(rabateAmmountProperty); }
			set { SetProperty(rabateAmmountProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > taxRateIdProperty = RegisterProperty<System.Int32>(p => p.TaxRateId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 TaxRateId
		{
			get { return GetProperty(taxRateIdProperty); }
			set { SetProperty(taxRateIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > taxAmmountProperty = RegisterProperty<System.Decimal>(p => p.TaxAmmount, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal TaxAmmount
		{
			get { return GetProperty(taxAmmountProperty); }
			set { SetProperty(taxAmmountProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > wholesaleValueProperty = RegisterProperty<System.Decimal>(p => p.WholesaleValue, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal WholesaleValue
		{
			get { return GetProperty(wholesaleValueProperty); }
			set { SetProperty(wholesaleValueProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		public static cDocuments_Items NewDocuments_Items()
		{
			return DataPortal.CreateChild<cDocuments_Items>();
		}

        public static cDocuments_Items GetDocuments_Items(Documents_ItemsCol data)
        {
            return DataPortal.FetchChild<cDocuments_Items>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_ItemsCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(documentIdProperty, data.DocumentId);
            LoadProperty<int>(ordinalProperty, data.Ordinal);
            LoadProperty<int>(productIdProperty, data.ProductId);
            LoadProperty<string>(productDescriptionProperty, data.ProductDescription);
            LoadProperty<int>(unitIdProperty, data.UnitId);
            LoadProperty<decimal>(quantityProperty, data.Quantity);
            LoadProperty<decimal>(ammountProperty, data.Ammount);
            LoadProperty<int?>(priceListIdProperty, data.PriceListId);
            LoadProperty<decimal?>(priceFromPriceListProperty, data.PriceFromPriceList);
            LoadProperty<decimal>(priceProperty, data.Price);
            LoadProperty<decimal?>(rabatePercentageProperty, data.RabatePercentage);
            LoadProperty<decimal?>(rabateAmmountProperty, data.RabateAmmount);
            LoadProperty<int>(taxRateIdProperty, data.TaxRateId);
            LoadProperty<decimal>(taxAmmountProperty, data.TaxAmmount);
            LoadProperty<decimal>(wholesaleValueProperty, data.WholesaleValue);


            ItemLoaded();
       
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        partial void ItemLoaded();

        private void Child_Insert(Documents_Document parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_ItemsCol();

                data.Documents_Document = parent;

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.ProductDescription = ReadProperty<string>(productDescriptionProperty);
                data.ProductId = ReadProperty<int>(productIdProperty);
                data.UnitId = ReadProperty<int>(unitIdProperty);
                data.Quantity = ReadProperty<decimal>(quantityProperty);
                data.Ammount = ReadProperty<decimal>(ammountProperty);
                data.PriceListId = ReadProperty<int?>(priceListIdProperty);
                data.PriceFromPriceList = ReadProperty<decimal?>(priceFromPriceListProperty);
                data.Price = ReadProperty<decimal>(priceProperty);
                data.RabatePercentage = ReadProperty<decimal?>(rabatePercentageProperty);
                data.RabateAmmount = ReadProperty<decimal?>(rabateAmmountProperty);
                data.TaxRateId = ReadProperty<int>(taxRateIdProperty);
                data.TaxAmmount = ReadProperty<decimal>(taxAmmountProperty);
                data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);

                ctx.ObjectContext.AddToDocuments_ItemsCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(documentIdProperty, data.DocumentId);
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
                var data = new Documents_ItemsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.ProductId = ReadProperty<int>(productIdProperty);
                data.ProductDescription = ReadProperty<string>(productDescriptionProperty);
                data.UnitId = ReadProperty<int>(unitIdProperty);
                data.Quantity = ReadProperty<decimal>(quantityProperty);
                data.Ammount = ReadProperty<decimal>(ammountProperty);
                data.PriceListId = ReadProperty<int?>(priceListIdProperty);
                data.PriceFromPriceList = ReadProperty<decimal?>(priceFromPriceListProperty);
                data.Price = ReadProperty<decimal>(priceProperty);
                data.RabatePercentage = ReadProperty<decimal?>(rabatePercentageProperty);
                data.RabateAmmount = ReadProperty<decimal?>(rabateAmmountProperty);
                data.TaxRateId = ReadProperty<int>(taxRateIdProperty);
                data.TaxAmmount = ReadProperty<decimal>(taxAmmountProperty);
                data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);

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
                var data = new Documents_ItemsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);

                ctx.ObjectContext.DeleteObject(data);
            }
        }
        #endregion

        public void MarkChild()
        {
            this.MarkAsChild();
        }

        public void SetNew()
        {
            Id = 0;
            MarkNew();
        }
    }
    [Serializable]
    public partial class cDocuments_ItemsCol : BusinessListBase<cDocuments_ItemsCol, cDocuments_Items>
    {
        internal static cDocuments_ItemsCol NewDocuments_ItemsCol()
        {
            return DataPortal.CreateChild<cDocuments_ItemsCol>();
        }

        public static cDocuments_ItemsCol GetDocuments_ItemsCol(IEnumerable<Documents_ItemsCol> dataSet)
        {
            var childList = new cDocuments_ItemsCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<Documents_ItemsCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cDocuments_Items.GetDocuments_Items(data));

            RaiseListChangedEvents = true;


        }

        #endregion //Data Access
    }
}

