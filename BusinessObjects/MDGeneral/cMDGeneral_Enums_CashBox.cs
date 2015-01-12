using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using BusinessObjects.Common;

namespace BusinessObjects.MDGeneral
{
    [Serializable]
    public partial class cMDGeneral_Enums_CashBox : CoreBusinessClass<cMDGeneral_Enums_CashBox>
    {
        #region Business Methods
        public static readonly PropertyInfo<System.Int32> IdProperty = RegisterProperty<System.Int32>(p => p.Id, string.Empty);
#if !SILVERLIGHT
        [System.ComponentModel.DataObjectField(true, true)]
#endif
        public System.Int32 Id
        {
            get { return GetProperty(IdProperty); }
            internal set { SetProperty(IdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> codeProperty= RegisterProperty<System.String>(p => p.Code, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(20, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String Code
        {
            get { return GetProperty(codeProperty); }
            set { SetProperty(codeProperty, value.Trim()); }
        }

        private static readonly PropertyInfo<System.String> nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String Name
        {
            get { return GetProperty(nameProperty); }
            set { SetProperty(nameProperty, value.Trim()); }
        }

        protected static readonly PropertyInfo<System.Int32?> locationIdProperty = RegisterProperty<System.Int32?>(p => p.LocationId, string.Empty);
        public System.Int32? LocationId
        {
            get { return GetProperty(locationIdProperty); }
            set { SetProperty(locationIdProperty, value); }
        }

        protected static readonly PropertyInfo<System.Int32?> companyUsingServiceIdProperty = RegisterProperty<System.Int32?>(p => p.CompanyUsingServiceId, string.Empty);
        public System.Int32? CompanyUsingServiceId
        {
            get { return GetProperty(companyUsingServiceIdProperty); }
            set { SetProperty(companyUsingServiceIdProperty, value); }
        }

        #endregion

        #region Factory Methods

        public static cMDGeneral_Enums_CashBox NewMDGeneral_Enums_CashBox()
        {
            return DataPortal.Create<cMDGeneral_Enums_CashBox>();

        }

        public static cMDGeneral_Enums_CashBox GetMDGeneral_Enums_CashBox(int uniqueId)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_CashBox>(new SingleCriteria<cMDGeneral_Enums_CashBox, int>(uniqueId));
        }

        internal static cMDGeneral_Enums_CashBox GetMDGeneral_Enums_CashBox(MDGeneral_Enums_CashBox data)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_CashBox>(data);
        }

        public static void DeleteMDGeneral_Enums_CashBox(int uniqueId)
        {
            DataPortal.Delete<cMDGeneral_Enums_CashBox>(new SingleCriteria<cMDGeneral_Enums_CashBox, int>(uniqueId));
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDGeneral_Enums_CashBox, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = ctx.ObjectContext.MDGeneral_Enums_CashBox.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(codeProperty, data.Code);
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<int?>(locationIdProperty, data.LocationId);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDGeneral_Enums_CashBox data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<string>(codeProperty, data.Code);
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<int?>(locationIdProperty, data.LocationId);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

            BusinessRules.CheckRules();
            MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = new MDGeneral_Enums_CashBox();

                data.Code = ReadProperty<string>(codeProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.LocationId = ReadProperty<int?>(locationIdProperty);
                data.CompanyUsingServiceId= ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToMDGeneral_Enums_CashBox(data);
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
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = new MDGeneral_Enums_CashBox();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Code = ReadProperty<string>(codeProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.LocationId = ReadProperty<int?>(locationIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<cMDGeneral_Enums_CashBox, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = ctx.ObjectContext.MDGeneral_Enums_CashBox.First(p => p.Id == criteria.Value);

                ctx.ObjectContext.MDGeneral_Enums_CashBox.DeleteObject(data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }
}

