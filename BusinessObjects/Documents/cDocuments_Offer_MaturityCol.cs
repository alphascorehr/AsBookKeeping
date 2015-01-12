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
	public partial class cDocuments_Offer_Maturity: CoreBusinessChildClass<cDocuments_Offer_Maturity>
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

		private static readonly PropertyInfo< System.Int32 > documents_OfferIdProperty = RegisterProperty<System.Int32>(p => p.Documents_OfferId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Documents_OfferId
		{
			get { return GetProperty(documents_OfferIdProperty); }
			set { SetProperty(documents_OfferIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32> ordinalProperty = RegisterProperty<System.Int32>(p => p.Ordinal, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 Ordinal
        {
            get { return GetProperty(ordinalProperty); }
            set { SetProperty(ordinalProperty, value); }
        }

		private static readonly PropertyInfo< System.DateTime > paymentDateProperty = RegisterProperty<System.DateTime>(p => p.PaymentDate, string.Empty,System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime PaymentDate
		{
			get { return GetProperty(paymentDateProperty); }
			set { SetProperty(paymentDateProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal > paymentAmountProperty = RegisterProperty<System.Decimal>(p => p.PaymentAmount, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal PaymentAmount
		{
			get { return GetProperty(paymentAmountProperty); }
			set { SetProperty(paymentAmountProperty, value); }
		}

        private static readonly PropertyInfo<System.Boolean?> invoicedProperty = RegisterProperty<System.Boolean?>(p => p.Invoiced, string.Empty, (System.Boolean?)null);
        public System.Boolean? Invoiced
		{
			get { return GetProperty(invoicedProperty); }
			set { SetProperty(invoicedProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > documents_InvoiceIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_InvoiceId, string.Empty,(System.Int32?)null);
		public System.Int32? Documents_InvoiceId
		{
			get { return GetProperty(documents_InvoiceIdProperty); }
			set { SetProperty(documents_InvoiceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime > lastActivityDateProperty = RegisterProperty<System.DateTime>(p => p.LastActivityDate, string.Empty,System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime LastActivityDate
		{
			get { return GetProperty(lastActivityDateProperty); }
			set { SetProperty(lastActivityDateProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > mDSubjects_EmployeeWhoChengedIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_EmployeeWhoChengedId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 MDSubjects_EmployeeWhoChengedId
		{
			get { return GetProperty(mDSubjects_EmployeeWhoChengedIdProperty); }
			set { SetProperty(mDSubjects_EmployeeWhoChengedIdProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cDocuments_Offer_Maturity NewDocuments_Offer_Maturity()
		{
			return DataPortal.CreateChild<cDocuments_Offer_Maturity>();
		}

        public static cDocuments_Offer_Maturity GetDocuments_Offer_Maturity(Documents_Offer_MaturityCol data)
        {
            return DataPortal.FetchChild<cDocuments_Offer_Maturity>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_Offer_MaturityCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(documents_OfferIdProperty, data.Documents_OfferId);
            LoadProperty<int>(ordinalProperty, data.Ordinal);
            LoadProperty<DateTime>(paymentDateProperty, data.PaymentDate);
            LoadProperty<decimal>(paymentAmountProperty, data.PaymentAmount);
            LoadProperty<bool?>(invoicedProperty, data.Invoiced);
            LoadProperty<int?>(documents_InvoiceIdProperty, data.Documents_InvoiceId);
            LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
            LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
    
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(Documents_Offer parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_Offer_MaturityCol();

                data.Documents_Offer = parent;

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.PaymentDate = ReadProperty<DateTime>(paymentDateProperty);
                data.PaymentAmount = ReadProperty<decimal>(paymentAmountProperty);
                data.Invoiced = ReadProperty<bool?>(invoicedProperty);
                data.Documents_InvoiceId = ReadProperty<int?>(documents_InvoiceIdProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
               
                ctx.ObjectContext.AddToDocuments_Offer_MaturityCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(documents_OfferIdProperty, data.Documents_OfferId);
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
                var data = new Documents_Offer_MaturityCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.PaymentDate = ReadProperty<DateTime>(paymentDateProperty);
                data.PaymentAmount = ReadProperty<decimal>(paymentAmountProperty);
                data.Invoiced = ReadProperty<bool?>(invoicedProperty);
                data.Documents_InvoiceId = ReadProperty<int?>(documents_InvoiceIdProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
               
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
                var data = new Documents_Offer_MaturityCol();

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
    public partial class cDocuments_Offer_MaturityCol : BusinessListBase<cDocuments_Offer_MaturityCol, cDocuments_Offer_Maturity>
    {
        internal static cDocuments_Offer_MaturityCol NewDocuments_Offer_MaturityCol()
        {
            return DataPortal.CreateChild<cDocuments_Offer_MaturityCol>();
        }

        public static cDocuments_Offer_MaturityCol GetDocuments_Offer_MaturityCol(IEnumerable<Documents_Offer_MaturityCol> dataSet)
        {
            var childList = new cDocuments_Offer_MaturityCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<Documents_Offer_MaturityCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cDocuments_Offer_Maturity.GetDocuments_Offer_Maturity(data));

            RaiseListChangedEvents = true;


        }

        #endregion //Data Access
    }
}

