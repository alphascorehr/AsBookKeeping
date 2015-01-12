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
	public partial class cDocuments_PaymentClosureG: CoreBusinessChildClass<cDocuments_PaymentClosureG>
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

		private static readonly PropertyInfo< System.Int32 > documents_PaymentItemsColIdProperty = RegisterProperty<System.Int32>(p => p.Documents_PaymentItemsColId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Documents_PaymentItemsColId
		{
			get { return GetProperty(documents_PaymentItemsColIdProperty); }
			set { SetProperty(documents_PaymentItemsColIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > payedInvoiceDocumentIdProperty = RegisterProperty<System.Int32?>(p => p.PayedInvoiceDocumentId, string.Empty,(System.Int32?)null);
		public System.Int32? PayedInvoiceDocumentId
		{
			get { return GetProperty(payedInvoiceDocumentIdProperty); }
			set { SetProperty(payedInvoiceDocumentIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > payedQuoteDocumentIdProperty = RegisterProperty<System.Int32?>(p => p.PayedQuoteDocumentId, string.Empty,(System.Int32?)null);
		public System.Int32? PayedQuoteDocumentId
		{
			get { return GetProperty(payedQuoteDocumentIdProperty); }
			set { SetProperty(payedQuoteDocumentIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > ammountProperty = RegisterProperty<System.Decimal>(p => p.Ammount, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal Ammount
		{
			get { return GetProperty(ammountProperty); }
			set { SetProperty(ammountProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		public static cDocuments_PaymentClosureG NewDocuments_PaymentClosureG()
		{
			return DataPortal.CreateChild<cDocuments_PaymentClosureG>();
		}

        public static cDocuments_PaymentClosureG GetDocuments_PaymentClosureG(Documents_PaymentClosureGCol data)
        {
            return DataPortal.FetchChild<cDocuments_PaymentClosureG>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_PaymentClosureGCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(documents_PaymentItemsColIdProperty, data.Documents_PaymentItemsColId);
            LoadProperty<int?>(payedInvoiceDocumentIdProperty, data.PayedInvoiceDocumentId);
            LoadProperty<int?>(payedQuoteDocumentIdProperty, data.PayedQuoteDocumentId);
            LoadProperty<decimal>(ammountProperty, data.Ammount);
           
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(Documents_PaymentItemsCol parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_PaymentClosureGCol();

                data.Documents_PaymentItemsCol = parent;

                data.PayedInvoiceDocumentId = ReadProperty<int?>(payedInvoiceDocumentIdProperty);
                data.PayedQuoteDocumentId = ReadProperty<int?>(payedQuoteDocumentIdProperty);
                data.Ammount = ReadProperty<decimal>(ammountProperty);
               
                ctx.ObjectContext.AddToDocuments_PaymentClosureGCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(documents_PaymentItemsColIdProperty, data.Documents_PaymentItemsColId);
                        LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                        LastChanged = data.LastChanged;
                    }
                };
                ctx.ObjectContext.SaveChanges();
             
            }
        }

        private void Child_Update()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_PaymentClosureGCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.PayedInvoiceDocumentId = ReadProperty<int?>(payedInvoiceDocumentIdProperty);
                data.PayedQuoteDocumentId = ReadProperty<int?>(payedQuoteDocumentIdProperty);
                data.Ammount = ReadProperty<decimal>(ammountProperty);

                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "LastChanged")
                        LastChanged = data.LastChanged;
                };
                ctx.ObjectContext.SaveChanges();

              

            }
        }

        private void Child_DeleteSelf()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_PaymentClosureGCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);

                ctx.ObjectContext.DeleteObject(data);
                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion

    }
    [Serializable]
    public partial class cDocuments_PaymentClosureGCol : BusinessListBase<cDocuments_PaymentClosureGCol, cDocuments_PaymentClosureG>
    {
        internal static cDocuments_PaymentClosureGCol NewDocuments_PaymentClosureGCol()
        {
            return DataPortal.CreateChild<cDocuments_PaymentClosureGCol>();
        }

        public static cDocuments_PaymentClosureGCol GetDocuments_PaymentClosureGCol(IEnumerable<Documents_PaymentClosureGCol> dataSet)
        {
            var childList = new cDocuments_PaymentClosureGCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<Documents_PaymentClosureGCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cDocuments_PaymentClosureG.GetDocuments_PaymentClosureG(data));

            RaiseListChangedEvents = true;


        }

        #endregion //Data Access
    }
}


