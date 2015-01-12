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
	public partial class cDocuments_PaymentItems: CoreBusinessChildClass<cDocuments_PaymentItems>
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

		private static readonly PropertyInfo< System.Int32 > documents_PaymentIdProperty = RegisterProperty<System.Int32>(p => p.Documents_PaymentId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Documents_PaymentId
		{
			get { return GetProperty(documents_PaymentIdProperty); }
			set { SetProperty(documents_PaymentIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32> ordinalProperty = RegisterProperty<System.Int32>(p => p.Ordinal, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 Ordinal
        {
            get { return GetProperty(ordinalProperty); }
            set { SetProperty(ordinalProperty, value); }
        }

		private static readonly PropertyInfo< System.Int32? > subjectBuyerIdProperty = RegisterProperty<System.Int32?>(p => p.SubjectBuyerId, string.Empty);
		public System.Int32? SubjectBuyerId
		{
			get { return GetProperty(subjectBuyerIdProperty); }
			set { SetProperty(subjectBuyerIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > subjectSupplierIdProperty = RegisterProperty<System.Int32?>(p => p.SubjectSupplierId, string.Empty);
		public System.Int32? SubjectSupplierId
		{
			get { return GetProperty(subjectSupplierIdProperty); }
			set { SetProperty(subjectSupplierIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Decimal?> advancePaymentAmmountProperty = RegisterProperty<System.Decimal?>(p => p.AdvancePaymentAmmount, string.Empty);
        public System.Decimal? AdvancePaymentAmmount
        {
            get { return GetProperty(advancePaymentAmmountProperty); }
            set { SetProperty(advancePaymentAmmountProperty, value); }
        }

		private static readonly PropertyInfo< System.Decimal > ammountProperty = RegisterProperty<System.Decimal>(p => p.Ammount, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal Ammount
		{
			get { return GetProperty(ammountProperty); }
			set { SetProperty(ammountProperty, value); }
		}

		private static readonly PropertyInfo< bool? > reversedProperty = RegisterProperty<bool?>(p => p.Reversed, string.Empty,(bool?)null);
		public bool? Reversed
		{
			get { return GetProperty(reversedProperty); }
			set { SetProperty(reversedProperty, value); }
		}

		private static readonly PropertyInfo< bool > inAdvanceProperty = RegisterProperty<bool>(p => p.InAdvance, string.Empty, false);
		public bool InAdvance
		{
			get { return GetProperty(inAdvanceProperty); }
			set { SetProperty(inAdvanceProperty, value); }
		}

        private static readonly PropertyInfo<bool?> generatedInAdvanceProperty = RegisterProperty<bool?>(p => p.GeneratedInAdvance, string.Empty);
        public bool? GeneratedInAdvance
        {
            get { return GetProperty(generatedInAdvanceProperty); }
            set { SetProperty(generatedInAdvanceProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> generatedFormDocuments_PaymentIdProperty = RegisterProperty<System.Int32?>(p => p.GeneratedFormDocuments_PaymentId, string.Empty);
        public System.Int32? GeneratedFormDocuments_PaymentId
		{
            get { return GetProperty(generatedFormDocuments_PaymentIdProperty); }
            set { SetProperty(generatedFormDocuments_PaymentIdProperty, value); }
		}

        protected static readonly PropertyInfo<System.String> descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, string.Empty);
        public System.String Description
        {
            get { return GetProperty(descriptionProperty); }
            set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<bool?> fullyDistributedProperty = RegisterProperty<bool?>(p => p.FullyDistributed, string.Empty, (bool?)null);
        public bool? FullyDistributed
        {
            get { return GetProperty(fullyDistributedProperty); }
            set { SetProperty(fullyDistributedProperty, value); }
        }

        private static readonly PropertyInfo<bool?> IsInvoiceProperty = RegisterProperty<bool?>(p => p.IsInvoice, string.Empty, (bool?)null);
        public bool? IsInvoice
        {
            get { return GetProperty(IsInvoiceProperty); }
            set { SetProperty(IsInvoiceProperty, value); }
        }
        
		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

        #region Kolekcije
        public static PropertyInfo<cDocuments_PaymentClosureGCol> Documents_PaymentClosureGColProperty = RegisterProperty<cDocuments_PaymentClosureGCol>(c => c.Documents_PaymentClosureGCol);
        public cDocuments_PaymentClosureGCol Documents_PaymentClosureGCol
        {
            get
            {
                if (!FieldManager.FieldExists(Documents_PaymentClosureGColProperty))
                    LoadProperty<cDocuments_PaymentClosureGCol>(Documents_PaymentClosureGColProperty, cDocuments_PaymentClosureGCol.NewDocuments_PaymentClosureGCol());
                return GetProperty(Documents_PaymentClosureGColProperty);
            }
            private set { SetProperty(Documents_PaymentClosureGColProperty, value); }
        }

        #endregion
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new MinAmmountRule { PrimaryProperty = ammountProperty });
           
        }

        #region Rules

        protected override void OnChildChanged(Csla.Core.ChildChangedEventArgs e)
        {
            base.OnChildChanged(e);
            if (e.ChildObject is cDocuments_PaymentClosureGCol)
            {
                BusinessRules.CheckRules(ammountProperty);
            }

            if (e.ChildObject is cDocuments_PaymentClosureG)
            {
                if (e.PropertyChangedArgs.PropertyName == "Ammount")
                BusinessRules.CheckRules(ammountProperty);
            }
        }
        private class MinAmmountRule : Csla.Rules.BusinessRule
        {

            protected override void Execute(Csla.Rules.RuleContext context)
            {

                var target = (cDocuments_PaymentItems)context.Target;
                var sum = target.Documents_PaymentClosureGCol.Sum(p => p.Ammount);

                if ((target.ReadProperty<decimal>(ammountProperty)) < sum)
                {
                    context.AddErrorResult("Iznos je manji od dozvoljenog!", true);
                }
            }
        }
        #endregion

		public static cDocuments_PaymentItems NewDocuments_PaymentItems()
		{
			return DataPortal.CreateChild<cDocuments_PaymentItems>();
		}

        public static cDocuments_PaymentItems GetDocuments_PaymentItems(Documents_PaymentItemsCol data)
        {
            return DataPortal.FetchChild<cDocuments_PaymentItems>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Documents_PaymentItemsCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(documents_PaymentIdProperty, data.Documents_PaymentId);
            LoadProperty<int>(ordinalProperty, data.Ordinal);
            LoadProperty<int?>(subjectBuyerIdProperty, data.SubjectBuyerId);
            LoadProperty<int?>(subjectSupplierIdProperty, data.SubjectSupplierId);
            LoadProperty<decimal?>(advancePaymentAmmountProperty, data.AdvancePaymentAmmount);
            LoadProperty<decimal>(ammountProperty, data.Ammount);
            LoadProperty<bool?>(reversedProperty, data.Reversed);
            LoadProperty<bool>(inAdvanceProperty, data.InAdvance);
            LoadProperty<bool?>(generatedInAdvanceProperty, data.GeneratedInAdvance);
            LoadProperty<int?>(generatedFormDocuments_PaymentIdProperty, data.GeneratedFormDocuments_PaymentId);
            LoadProperty<string>(descriptionProperty, data.Description);
            LoadProperty<bool?>(fullyDistributedProperty, data.FullyDistributed);
            LoadProperty<bool?>(IsInvoiceProperty, data.IsInvoice);
            
            
            LoadProperty<cDocuments_PaymentClosureGCol>(Documents_PaymentClosureGColProperty, cDocuments_PaymentClosureGCol.GetDocuments_PaymentClosureGCol(data.Documents_PaymentClosureGCol));

            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(Documents_Payment parent)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_PaymentItemsCol();

                data.Documents_Payment = parent;

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.SubjectBuyerId = ReadProperty<int?>(subjectBuyerIdProperty);
                data.SubjectSupplierId = ReadProperty<int?>(subjectSupplierIdProperty);
                data.AdvancePaymentAmmount = ReadProperty<decimal?>(advancePaymentAmmountProperty);
                data.Ammount = ReadProperty<decimal>(ammountProperty);
                data.Reversed = ReadProperty<bool?>(reversedProperty);
                data.InAdvance = ReadProperty<bool>(inAdvanceProperty);
                data.GeneratedInAdvance = ReadProperty<bool?>(generatedInAdvanceProperty);
                data.GeneratedFormDocuments_PaymentId = ReadProperty<int?>(generatedFormDocuments_PaymentIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.IsInvoice = ReadProperty<bool?>(IsInvoiceProperty);
                if ((data.IsInvoice ?? false) == false)
                    data.FullyDistributed = true;
                else
                    data.FullyDistributed = ReadProperty<bool?>(fullyDistributedProperty);
                
                ctx.ObjectContext.AddToDocuments_PaymentItemsCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(documents_PaymentIdProperty, data.Documents_PaymentId);
                        LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                        LastChanged = data.LastChanged;

                        //if (!this.InAdvance)
                        //{

                        //    using (var ctx1 = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
                        //    {

                        //        decimal closureAmmount = this.Documents_PaymentClosureGCol.Sum(p => p.Ammount); //Dodavanje novog itema kod preplate na novom itemu 
                        //        if (this.Ammount > closureAmmount)
                        //        {
                        //            var dataNew = new Documents_PaymentItemsCol();
                        //            dataNew.Ammount = this.Ammount - closureAmmount;
                        //            dataNew.AdvancePaymentAmmount = dataNew.Ammount;
                        //            dataNew.SubjectBuyerId = this.SubjectBuyerId;
                        //            dataNew.SubjectSupplierId = this.SubjectSupplierId;
                        //            dataNew.InAdvance = true;
                        //            dataNew.GeneratedInAdvance = true;

                        //            dataNew.GeneratedFormDocuments_PaymentId = data.Id;

                        //            ctx.Dispose();
                        //            ctx1.ObjectContext.AddToDocuments_PaymentItemsCol(dataNew);
                        //            ctx1.ObjectContext.SaveChanges();
                        //        }
                        //    }
                        //}
                    }
                };
                ctx.ObjectContext.SaveChanges();
                DataPortal.UpdateChild(ReadProperty<cDocuments_PaymentClosureGCol>(Documents_PaymentClosureGColProperty), data);
            
            }

           

           
        }

        private void Child_Update()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_PaymentItemsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Ordinal = ReadProperty<int>(ordinalProperty);
                data.SubjectBuyerId = ReadProperty<int?>(subjectBuyerIdProperty);
                data.SubjectSupplierId = ReadProperty<int?>(subjectSupplierIdProperty);
                data.AdvancePaymentAmmount = ReadProperty<decimal?>(advancePaymentAmmountProperty);
                data.Ammount = ReadProperty<decimal>(ammountProperty);
                data.Reversed = ReadProperty<bool?>(reversedProperty);
                data.InAdvance = ReadProperty<bool>(inAdvanceProperty);
                data.GeneratedInAdvance = ReadProperty<bool?>(generatedInAdvanceProperty);
                data.GeneratedFormDocuments_PaymentId = ReadProperty<int?>(generatedFormDocuments_PaymentIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.IsInvoice = ReadProperty<bool?>(IsInvoiceProperty);
                if ((data.IsInvoice ?? false) == false)
                    data.FullyDistributed = true;
                else
                    data.FullyDistributed = ReadProperty<bool?>(fullyDistributedProperty);

                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "LastChanged")
                        LastChanged = data.LastChanged;
                };
                ctx.ObjectContext.SaveChanges();

                DataPortal.UpdateChild(ReadProperty<cDocuments_PaymentClosureGCol>(Documents_PaymentClosureGColProperty), data);

             

            }
        }

        private void Child_DeleteSelf()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_PaymentItemsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);

                ctx.ObjectContext.DeleteObject(data);
                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion

        public void MarkChild()
        {
            this.MarkAsChild();
        }

    }
    [Serializable]
    public partial class cDocuments_PaymentItemsCol : BusinessListBase<cDocuments_PaymentItemsCol, cDocuments_PaymentItems>
    {
        internal static cDocuments_PaymentItemsCol NewDocuments_PaymentItemsCol()
        {
            return DataPortal.CreateChild<cDocuments_PaymentItemsCol>();
        }

        public static cDocuments_PaymentItemsCol GetDocuments_PaymentItemsCol(IEnumerable<Documents_PaymentItemsCol> dataSet)
        {
            var childList = new cDocuments_PaymentItemsCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<Documents_PaymentItemsCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cDocuments_PaymentItems.GetDocuments_PaymentItems(data));

            RaiseListChangedEvents = true;


        }

        #endregion //Data Access
    }
}


