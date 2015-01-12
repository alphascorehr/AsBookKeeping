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
	public partial class cDocuments_TravelOrder_OtherExpenses: CoreBusinessChildClass<cDocuments_TravelOrder_OtherExpenses>
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

		private static readonly PropertyInfo< System.String > descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
		public System.String Description
		{
			get { return GetProperty(descriptionProperty); }
			set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Decimal? > ammountProperty = RegisterProperty<System.Decimal?>(p => p.Ammount, string.Empty, (System.Decimal?)null);
		public System.Decimal? Ammount
		{
			get { return GetProperty(ammountProperty); }
			set { SetProperty(ammountProperty, value); }
		}

		private static readonly PropertyInfo< bool? > admitedProperty = RegisterProperty<bool?>(p => p.Admited, string.Empty,(bool?)null);
		public bool? Admited
		{
			get { return GetProperty(admitedProperty); }
			set { SetProperty(admitedProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cDocuments_TravelOrder_OtherExpenses NewDocuments_TravelOrder_OtherExpenses()
		{
			return DataPortal.CreateChild<cDocuments_TravelOrder_OtherExpenses>();
		}

        public static cDocuments_TravelOrder_OtherExpenses GetDocuments_TravelOrder_OtherExpenses(Documents_TravelOrder_OtherExpensesCol data)
        {
            return DataPortal.FetchChild<cDocuments_TravelOrder_OtherExpenses>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_TravelOrder_OtherExpensesCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(documents_TravelOrderIdProperty, data.Documents_TravelOrderId);
            LoadProperty<int>(ordinalProperty, data.Ordinal);
            LoadProperty<string>(descriptionProperty, data.Description);
            LoadProperty<decimal?>(ammountProperty, data.Ammount);
            LoadProperty<bool?>(admitedProperty, data.Admited);
         
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(Documents_TravelOrder parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_TravelOrder_OtherExpensesCol();

                data.Documents_TravelOrder = parent;

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Ammount = ReadProperty<decimal?>(ammountProperty);
                data.Admited = ReadProperty<bool?>(admitedProperty);
             
                ctx.ObjectContext.AddToDocuments_TravelOrder_OtherExpensesCol(data);
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
                var data = new Documents_TravelOrder_OtherExpensesCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Ammount = ReadProperty<decimal?>(ammountProperty);
                data.Admited = ReadProperty<bool?>(admitedProperty);

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
                var data = new Documents_TravelOrder_OtherExpensesCol();

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
		public partial class cDocuments_TravelOrder_OtherExpensesCol : BusinessListBase<cDocuments_TravelOrder_OtherExpensesCol,cDocuments_TravelOrder_OtherExpenses>
		{
			internal static cDocuments_TravelOrder_OtherExpensesCol NewDocuments_TravelOrder_OtherExpensesCol()
			{
				return DataPortal.CreateChild<cDocuments_TravelOrder_OtherExpensesCol>();
			}

            public static cDocuments_TravelOrder_OtherExpensesCol GetDocuments_TravelOrder_OtherExpensesCol(IEnumerable<Documents_TravelOrder_OtherExpensesCol> dataSet)
            {
                var childList = new cDocuments_TravelOrder_OtherExpensesCol();
                childList.Fetch(dataSet);
                return childList;
            }

            #region Data Access
            private void Fetch(IEnumerable<Documents_TravelOrder_OtherExpensesCol> dataSet)
            {

                RaiseListChangedEvents = false;

                foreach (var data in dataSet)
                    this.Add(cDocuments_TravelOrder_OtherExpenses.GetDocuments_TravelOrder_OtherExpenses(data));

                RaiseListChangedEvents = true;


            }
            #endregion //Data Access
        }


}
