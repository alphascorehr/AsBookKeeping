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
    public partial class cDocuments_CompensationChild : CoreBusinessChildClass<cDocuments_CompensationChild>
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

        private static readonly PropertyInfo<System.Int32> compenzationIdProperty = RegisterProperty<System.Int32>(p => p.CompenzationId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 CompenzationId
		{
            get { return GetProperty(compenzationIdProperty); }
            set { SetProperty(compenzationIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > ordinalProperty = RegisterProperty<System.Int32>(p => p.Ordinal, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Ordinal
		{
			get { return GetProperty(ordinalProperty); }
			set { SetProperty(ordinalProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32?> invoiceIdProperty = RegisterProperty<System.Int32?>(p => p.InvoiceId, string.Empty);
        public System.Int32? InvoiceId
		{
            get { return GetProperty(invoiceIdProperty); }
            set { SetProperty(invoiceIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Decimal?> payedAmmountInvoiceProperty = RegisterProperty<System.Decimal?>(p => p.PayedAmmountInvoice, string.Empty, (decimal)1);
        public System.Decimal? PayedAmmountInvoice
        {
            get { return GetProperty(payedAmmountInvoiceProperty); }
            set { SetProperty(payedAmmountInvoiceProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> incomingInvoiceIdProperty = RegisterProperty<System.Int32?>(p => p.IncomingInvoiceId, string.Empty);
        public System.Int32? IncomingInvoiceId
		{
            get { return GetProperty(incomingInvoiceIdProperty); }
            set { SetProperty(incomingInvoiceIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Decimal?> payedAmmountIncomingInvoiceProperty = RegisterProperty<System.Decimal?>(p => p.PayedAmmountIncomingInvoice, string.Empty, (decimal)1);
        public System.Decimal? PayedAmmountIncomingInvoice
        {
            get { return GetProperty(payedAmmountIncomingInvoiceProperty); }
            set { SetProperty(payedAmmountIncomingInvoiceProperty, value); }
        }


		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

        public static cDocuments_CompensationChild NewDocuments_CompensationChild()
		{
            return DataPortal.CreateChild<cDocuments_CompensationChild>();
		}

        public static cDocuments_CompensationChild GetDocuments_CompensationChild(Documents_CompensationChildCol data)
        {
            return DataPortal.FetchChild<cDocuments_CompensationChild>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_CompensationChildCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(compenzationIdProperty, data.CompensationId);
            LoadProperty<int>(ordinalProperty, data.Ordinal);
            LoadProperty<int?>(invoiceIdProperty, data.InvoiceId);

            LoadProperty<decimal?>(payedAmmountInvoiceProperty, data.PayedAmmountInvoice);
            LoadProperty<int?>(incomingInvoiceIdProperty, data.IncomingInvoiceId);
            LoadProperty<decimal?>(payedAmmountIncomingInvoiceProperty, data.PayedAmmountIncomingInvoice);

            ItemLoaded();
       
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        partial void ItemLoaded();

        private void Child_Insert(Documents_Compensation parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_CompensationChildCol();

                data.Documents_Compensation = parent;

                data.CompensationId = ReadProperty<int>(compenzationIdProperty);
                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.InvoiceId = ReadProperty<int?>(invoiceIdProperty);
                data.PayedAmmountInvoice = ReadProperty<decimal?>(payedAmmountInvoiceProperty);
                data.IncomingInvoiceId = ReadProperty<int?>(incomingInvoiceIdProperty);
                data.PayedAmmountIncomingInvoice = ReadProperty<decimal?>(payedAmmountIncomingInvoiceProperty);

                ctx.ObjectContext.AddToDocuments_CompensationChildCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(compenzationIdProperty, data.CompensationId);
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
                var data = new Documents_CompensationChildCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.CompensationId = ReadProperty<int>(compenzationIdProperty);
                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.InvoiceId = ReadProperty<int?>(invoiceIdProperty);
                data.PayedAmmountInvoice = ReadProperty<decimal?>(payedAmmountInvoiceProperty);
                data.IncomingInvoiceId = ReadProperty<int?>(incomingInvoiceIdProperty);
                data.PayedAmmountIncomingInvoice = ReadProperty<decimal?>(payedAmmountIncomingInvoiceProperty);

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
                var data = new Documents_CompensationChildCol();

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
    public partial class cDocuments_CompensationChildCol : BusinessListBase<cDocuments_CompensationChildCol, cDocuments_CompensationChild>
    {
        internal static cDocuments_CompensationChildCol NewDocuments_CompensationChildCol()
        {
            return DataPortal.CreateChild<cDocuments_CompensationChildCol>();
        }

        public static cDocuments_CompensationChildCol GetDocuments_CompensationChildCol(IEnumerable<Documents_CompensationChildCol> dataSet)
        {
            var childList = new cDocuments_CompensationChildCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<Documents_CompensationChildCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cDocuments_CompensationChild.GetDocuments_CompensationChild(data));

            RaiseListChangedEvents = true;


        }
        #endregion //Data Access
    }
}

