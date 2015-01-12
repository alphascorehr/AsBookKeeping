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
	public partial class cDocuments_PriceList_Items: CoreBusinessChildClass<cDocuments_PriceList_Items>
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

        private static readonly PropertyInfo<System.Int32> ordinalProperty = RegisterProperty<System.Int32>(p => p.Ordinal, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 Ordinal
        {
            get { return GetProperty(ordinalProperty); }
            set { SetProperty(ordinalProperty, value); }
        }

		private static readonly PropertyInfo< System.Int32 > mDEntities_EntityIdProperty = RegisterProperty<System.Int32>(p => p.MDEntities_EntityId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 MDEntities_EntityId
		{
			get { return GetProperty(mDEntities_EntityIdProperty); }
			set { SetProperty(mDEntities_EntityIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > calcualtedWholesalePriceProperty = RegisterProperty<System.Decimal?>(p => p.CalcualtedWholesalePrice, string.Empty, (System.Decimal?)null);
		public System.Decimal? CalcualtedWholesalePrice
		{
			get { return GetProperty(calcualtedWholesalePriceProperty); }
			set { SetProperty(calcualtedWholesalePriceProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > wholesalePriceProperty = RegisterProperty<System.Decimal?>(p => p.WholesalePrice, string.Empty, (System.Decimal?)null);
		public System.Decimal? WholesalePrice
		{
			get { return GetProperty(wholesalePriceProperty); }
			set { SetProperty(wholesalePriceProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > wholesalePriceOption1Property = RegisterProperty<System.Decimal?>(p => p.WholesalePriceOption1, string.Empty, (System.Decimal?)null);
		public System.Decimal? WholesalePriceOption1
		{
			get { return GetProperty(wholesalePriceOption1Property); }
			set { SetProperty(wholesalePriceOption1Property, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cDocuments_PriceList_Items NewDocuments_PriceList_Items()
		{
			return DataPortal.CreateChild<cDocuments_PriceList_Items>();
		}

        public static cDocuments_PriceList_Items GetDocuments_PriceList_Items(Documents_PriceList_ItemsCol data)
        {
            return DataPortal.FetchChild<cDocuments_PriceList_Items>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_PriceList_ItemsCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(documentIdProperty, data.DocumentId);
            LoadProperty<int>(ordinalProperty, data.Ordinal);
            LoadProperty<int>(mDEntities_EntityIdProperty, data.MDEntities_EntityId);
            LoadProperty<decimal?>(calcualtedWholesalePriceProperty, data.CalcualtedWholesalePrice);
            LoadProperty<decimal?>(wholesalePriceProperty, data.WholesalePrice);
            LoadProperty<decimal?>(wholesalePriceOption1Property, data.WholesalePriceOption1);
            
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(Documents_PriceList parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_PriceList_ItemsCol();

                data.Documents_PriceList = parent;

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.MDEntities_EntityId = ReadProperty<int>(mDEntities_EntityIdProperty);
                data.CalcualtedWholesalePrice = ReadProperty<decimal?>(calcualtedWholesalePriceProperty);
                data.WholesalePrice = ReadProperty<decimal?>(wholesalePriceProperty);
                data.WholesalePriceOption1 = ReadProperty<decimal?>(wholesalePriceOption1Property);
                
                ctx.ObjectContext.AddToDocuments_PriceList_ItemsCol(data);
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
                var data = new Documents_PriceList_ItemsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.MDEntities_EntityId = ReadProperty<int>(mDEntities_EntityIdProperty);
                data.CalcualtedWholesalePrice = ReadProperty<decimal?>(calcualtedWholesalePriceProperty);
                data.WholesalePrice = ReadProperty<decimal?>(wholesalePriceProperty);
                data.WholesalePriceOption1 = ReadProperty<decimal?>(wholesalePriceOption1Property);

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
                var data = new Documents_PriceList_ItemsCol();

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

    }
    [Serializable]
    public partial class cDocuments_PriceList_ItemsCol : BusinessListBase<cDocuments_PriceList_ItemsCol, cDocuments_PriceList_Items>
    {
        internal static cDocuments_PriceList_ItemsCol NewDocuments_PriceList_ItemsCol()
        {
            return DataPortal.CreateChild<cDocuments_PriceList_ItemsCol>();
        }

        public static cDocuments_PriceList_ItemsCol GetDocuments_PriceList_ItemsCol(IEnumerable<Documents_PriceList_ItemsCol> dataSet)
        {
            var childList = new cDocuments_PriceList_ItemsCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<Documents_PriceList_ItemsCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cDocuments_PriceList_Items.GetDocuments_PriceList_Items(data));

            RaiseListChangedEvents = true;


        }

        #endregion //Data Access
    }
}


