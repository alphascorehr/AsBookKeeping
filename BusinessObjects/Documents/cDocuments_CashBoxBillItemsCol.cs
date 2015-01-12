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
	public partial class cDocuments_CashBoxBillItems : CoreBusinessChildClass<cDocuments_CashBoxBillItems>
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

		//private static readonly PropertyInfo< System.Int32 > unitIdProperty = RegisterProperty<System.Int32>(p => p.UnitId, string.Empty);
		//[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		//public System.Int32 UnitId
		//{
		//    get { return GetProperty(unitIdProperty); }
		//    set { SetProperty(unitIdProperty, value); }
		//}

		private static readonly PropertyInfo<System.Decimal> quantityProperty = RegisterProperty<System.Decimal>(p => p.Quantity, string.Empty, (decimal)1);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal Quantity
		{
			get { return GetProperty(quantityProperty); }
			set { SetProperty(quantityProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > priceProperty = RegisterProperty<System.Decimal>(p => p.Price, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal Price
		{
			get { return GetProperty(priceProperty); }
			set { SetProperty(priceProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > taxRateIdProperty = RegisterProperty<System.Int32>(p => p.TaxRateId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 TaxRateId
		{
			get { return GetProperty(taxRateIdProperty); }
			set { SetProperty(taxRateIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > taxValueProperty = RegisterProperty<System.Decimal>(p => p.TaxValue, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal TaxValue
		{
			get { return GetProperty(taxValueProperty); }
			set { SetProperty(taxValueProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > wholesaleValueProperty = RegisterProperty<System.Decimal>(p => p.WholesaleValue, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal WholesaleValue
		{
			get { return GetProperty(wholesaleValueProperty); }
			set { SetProperty(wholesaleValueProperty, value); }
		}

		private static readonly PropertyInfo<System.Decimal> retailValueProperty = RegisterProperty<System.Decimal>(p => p.RetailValue, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal RetailValue
		{
			get { return GetProperty(retailValueProperty); }
			set { SetProperty(retailValueProperty, value); }
		}


		public static cDocuments_CashBoxBillItems NewDocuments_CashBoxItems()
		{
			return DataPortal.CreateChild<cDocuments_CashBoxBillItems>();
		}

		public static cDocuments_CashBoxBillItems GetDocuments_CashBoxItem(Documents_CashBoxBillItemsCol data)
		{
			return DataPortal.FetchChild<cDocuments_CashBoxBillItems>(data);
		}

		#region Data Access
		[RunLocal]
		protected override void Child_Create()
		{
			BusinessRules.CheckRules();
		}

		private void Child_Fetch(Documents_CashBoxBillItemsCol data)
		{

			LoadProperty<int>(IdProperty, data.Id);
			LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

			LoadProperty<int>(documentIdProperty, data.DocumentId);
			LoadProperty<int>(ordinalProperty, data.Ordinal);
			LoadProperty<int>(productIdProperty, data.ProductId);
			LoadProperty<decimal>(quantityProperty, data.Quantity);
			LoadProperty<decimal>(retailValueProperty, data.RetailValue);
			LoadProperty<decimal>(priceProperty, data.Price);
			LoadProperty<int>(taxRateIdProperty, data.TaxRateId);
			LoadProperty<decimal>(taxValueProperty, data.TaxValue);
			LoadProperty<decimal>(wholesaleValueProperty, data.WholesaleValue);


			ItemLoaded();
	   
			BusinessRules.CheckRules();
		}

		partial void ItemLoaded();

		private void Child_Insert(Documents_CashBoxBill parent)
		{
			using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
			{
				var data = new Documents_CashBoxBillItemsCol();

				data.Documents_CashBoxBill = parent;

				data.Ordinal = ReadProperty<int>(ordinalProperty);
				data.ProductId = ReadProperty<int>(productIdProperty);
				data.Quantity = ReadProperty<decimal>(quantityProperty);
				data.RetailValue= ReadProperty<decimal>(retailValueProperty);
				data.Price = ReadProperty<decimal>(priceProperty);
				data.TaxRateId = ReadProperty<int>(taxRateIdProperty);
				data.TaxValue= ReadProperty<decimal>(taxValueProperty);
				data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);

				ctx.ObjectContext.AddToDocuments_CashBoxBillItemsCol(data);

				data.PropertyChanged += (o, e) =>
				{
					if (e.PropertyName == "Id")
					{
						LoadProperty<int>(IdProperty, data.Id);
						LoadProperty<int>(documentIdProperty, data.DocumentId);
						LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
					}
				};
			}
		}

		private void Child_Update()
		{
			using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
			{
				var data = new Documents_CashBoxBillItemsCol();

				data.Id = ReadProperty<int>(IdProperty);
				data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

				ctx.ObjectContext.Attach(data);

				data.Ordinal = ReadProperty<int>(ordinalProperty);
				data.ProductId = ReadProperty<int>(productIdProperty);
				data.Quantity = ReadProperty<decimal>(quantityProperty);
				data.RetailValue= ReadProperty<decimal>(retailValueProperty);
				data.Price = ReadProperty<decimal>(priceProperty);
				data.TaxRateId = ReadProperty<int>(taxRateIdProperty);
				data.TaxValue= ReadProperty<decimal>(taxValueProperty);
				data.WholesaleValue = ReadProperty<decimal>(wholesaleValueProperty);
			}
		}

		private void Child_DeleteSelf()
		{
			using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
			{
				var data = new Documents_CashBoxBillItemsCol();

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
	public partial class cDocuments_CashBoxBillItemsCol : BusinessListBase<cDocuments_CashBoxBillItemsCol, cDocuments_CashBoxBillItems>
	{
		internal static cDocuments_CashBoxBillItemsCol NewDocuments_CashBoxBillItemsCol()
		{
			return DataPortal.CreateChild<cDocuments_CashBoxBillItemsCol>();
		}

		public static cDocuments_CashBoxBillItemsCol GetDocuments_CashBoxBillItemsCol(IEnumerable<Documents_CashBoxBillItemsCol> dataSet)
		{
			var childList = new cDocuments_CashBoxBillItemsCol();
			childList.Fetch(dataSet);
			return childList;
		}

		#region Data Access
		private void Fetch(IEnumerable<Documents_CashBoxBillItemsCol> dataSet)
		{

			RaiseListChangedEvents = false;

			foreach (var data in dataSet)
				this.Add(cDocuments_CashBoxBillItems.GetDocuments_CashBoxItem(data));

			RaiseListChangedEvents = true;


		}

		#endregion //Data Access
	}
}

