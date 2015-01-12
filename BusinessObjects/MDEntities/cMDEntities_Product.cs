using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;


namespace BusinessObjects.MDEntities
{
	[Serializable]
	public partial class cMDEntities_Product: cMDEntities_Entity<cMDEntities_Product>
	{
		#region Business Methods
    
        private static readonly PropertyInfo<System.String> barcodeProperty = RegisterProperty<System.String>(p => p.Barcode, string.Empty, (System.String)null);
        public System.String Barcode
        {
            get { return GetProperty(barcodeProperty); }
            set { SetProperty(barcodeProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Decimal> wholesalePriceProperty = RegisterProperty<System.Decimal>(p => p.WholesalePrice, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Decimal WholesalePrice
        {
            get { return GetProperty(wholesalePriceProperty); }
            set { SetProperty(wholesalePriceProperty, value); }
        }

		#region Kolekcije

        public static PropertyInfo<cMDEntities_Product_PicturesCol> MDEntities_Product_PicturesColProperty = RegisterProperty<cMDEntities_Product_PicturesCol>(c => c.MDEntities_Product_PicturesCol);
        public cMDEntities_Product_PicturesCol MDEntities_Product_PicturesCol
        {
            get {
                    if (!FieldManager.FieldExists(MDEntities_Product_PicturesColProperty))
                        LoadProperty<cMDEntities_Product_PicturesCol>(MDEntities_Product_PicturesColProperty, cMDEntities_Product_PicturesCol.NewMDEntities_Product_PicturesCol());
                    return GetProperty(MDEntities_Product_PicturesColProperty); 
                }
            private set { SetProperty(MDEntities_Product_PicturesColProperty, value); }
        }

		#endregion
		#endregion

        #region Factory Methods

        public static cMDEntities_Product NewMDEntities_Product()
        {
            return DataPortal.Create<cMDEntities_Product>();

        }

        public static cMDEntities_Product GetMDEntities_Product(int uniqueId)
        {
            return DataPortal.Fetch<cMDEntities_Product>(new SingleCriteria<cMDEntities_Product, int>(uniqueId));
        }

        internal static cMDEntities_Product GetMDEntities_Product(MDEntities_Product data)
        {
            return DataPortal.Fetch<cMDEntities_Product>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDEntities_Product, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = ctx.ObjectContext.MDEntities_Entity.OfType<MDEntities_Product>().First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<Guid>(GUIDProperty, data.GUID);
                LoadProperty<string>(labelProperty, data.Label);
                LoadProperty<short>(entityTypeProperty, data.EntityType);
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<int?>(mDEntities_Enums_EntityGroupIdProperty, data.MDEntities_Enums_EntityGroupId);
                LoadProperty<string>(tagProperty, data.Tag);
                LoadProperty<string>(descriptionProperty, data.Description);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(employeeWhoLastChanedItUserIdProperty, data.EmployeeWhoLastChanedItUserId);
                LoadProperty<int>(taxRateIdProperty, data.TaxRateId);
                LoadProperty<int>(unitIdProperty, data.UnitId);

                LoadProperty<string>(barcodeProperty, data.Barcode);
                LoadProperty<decimal>(wholesalePriceProperty, data.WholesalePrice);

                LastChanged = data.LastChanged;

                LoadProperty<cMDEntities_Product_PicturesCol>(MDEntities_Product_PicturesColProperty, cMDEntities_Product_PicturesCol.GetcMDEntities_Product_PicturesCol(data.MDEntities_Product_PicturesCol));

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Product();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.EntityType = (short)Common.EntityType.Product;
                data.Name = ReadProperty<string>(nameProperty);
                data.MDEntities_Enums_EntityGroupId = ReadProperty<int?>(mDEntities_Enums_EntityGroupIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Tag = ReadProperty<string>(tagProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItUserId = ReadProperty<int>(employeeWhoLastChanedItUserIdProperty);
                data.TaxRateId = ReadProperty<int>(taxRateIdProperty);
                data.UnitId = ReadProperty<int>(unitIdProperty);

                data.Barcode = ReadProperty<string>(barcodeProperty);
                data.WholesalePrice = ReadProperty<decimal>(wholesalePriceProperty);

                ctx.ObjectContext.AddToMDEntities_Entity(data);

                DataPortal.UpdateChild(ReadProperty<cMDEntities_Product_PicturesCol>(MDEntities_Product_PicturesColProperty), data);

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
            using (var ctx = ObjectContextManager<MDEntitiesEntities>.GetManager("MDEntitiesEntities"))
            {
                var data = new MDEntities_Product();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.EntityType = ReadProperty<short>(entityTypeProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.MDEntities_Enums_EntityGroupId = ReadProperty<int?>(mDEntities_Enums_EntityGroupIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.Tag = ReadProperty<string>(tagProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItUserId = ReadProperty<int>(employeeWhoLastChanedItUserIdProperty);
                data.TaxRateId = ReadProperty<int>(taxRateIdProperty);
                data.UnitId = ReadProperty<int>(unitIdProperty);

                data.Barcode = ReadProperty<string>(barcodeProperty);
                data.WholesalePrice = ReadProperty<decimal>(wholesalePriceProperty);

                DataPortal.UpdateChild(ReadProperty<cMDEntities_Product_PicturesCol>(MDEntities_Product_PicturesColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
