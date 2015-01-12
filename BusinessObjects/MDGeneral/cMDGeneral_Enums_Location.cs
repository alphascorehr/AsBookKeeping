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
    public partial class cMDGeneral_Enums_Location : CoreBusinessClass<cMDGeneral_Enums_Location>
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

        private static readonly PropertyInfo<System.String> cityProperty = RegisterProperty<System.String>(p => p.City, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String City
        {
            get { return GetProperty(cityProperty); }
            set { SetProperty(cityProperty, value.Trim()); }
        }

        private static readonly PropertyInfo<System.String> addressProperty = RegisterProperty<System.String>(p => p.Address, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(200, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String Address
        {
            get { return GetProperty(addressProperty); }
            set { SetProperty(addressProperty, value.Trim()); }
        }

        protected static readonly PropertyInfo<System.Int32?> companyUsingServiceIdProperty = RegisterProperty<System.Int32?>(p => p.CompanyUsingServiceId, string.Empty);
        public System.Int32? CompanyUsingServiceId
        {
            get { return GetProperty(companyUsingServiceIdProperty); }
            set { SetProperty(companyUsingServiceIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> workingHourProperty = RegisterProperty<System.String>(p => p.WorkingHour, string.Empty);
        public System.String WorkingHour
        {
            get { return GetProperty(workingHourProperty); }
            set { SetProperty(workingHourProperty, value.Trim()); }
        }

        #endregion

        #region Factory Methods

        public static cMDGeneral_Enums_Location NewMDGeneral_Enums_Location()
        {
            return DataPortal.Create<cMDGeneral_Enums_Location>();

        }

        public static cMDGeneral_Enums_Location GetMDGeneral_Enums_Location(int uniqueId)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Location>(new SingleCriteria<cMDGeneral_Enums_Location, int>(uniqueId));
        }

        internal static cMDGeneral_Enums_Location GetMDGeneral_Enums_Location(MDGeneral_Enums_Location data)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Location>(data);
        }

        public static void DeleteMDGeneral_Enums_Location(int uniqueId)
        {
            DataPortal.Delete<cMDGeneral_Enums_Location>(new SingleCriteria<cMDGeneral_Enums_Location, int>(uniqueId));
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDGeneral_Enums_Location, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = ctx.ObjectContext.MDGeneral_Enums_Location.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(codeProperty, data.Code);
                LoadProperty<string>(cityProperty, data.City);
                LoadProperty<string>(addressProperty, data.Address);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<string>(workingHourProperty, data.WorkingHour);

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDGeneral_Enums_Location data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<string>(codeProperty, data.Code);
            LoadProperty<string>(cityProperty, data.City);
            LoadProperty<string>(addressProperty, data.Address);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LoadProperty<string>(workingHourProperty, data.WorkingHour);

            BusinessRules.CheckRules();
            MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = new MDGeneral_Enums_Location();

                data.Code = ReadProperty<string>(codeProperty);
                data.City = ReadProperty<string>(cityProperty);
                data.Address= ReadProperty<string>(addressProperty);
                data.CompanyUsingServiceId= ReadProperty<int?>(companyUsingServiceIdProperty);
                data.WorkingHour = ReadProperty<string>(workingHourProperty);

                ctx.ObjectContext.AddToMDGeneral_Enums_Location(data);
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
                var data = new MDGeneral_Enums_Location();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Code = ReadProperty<string>(codeProperty);
                data.City = ReadProperty<string>(cityProperty);
                data.Address = ReadProperty<string>(addressProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
                data.WorkingHour = ReadProperty<string>(workingHourProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<cMDGeneral_Enums_Location, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = ctx.ObjectContext.MDGeneral_Enums_Location.First(p => p.Id == criteria.Value);

                ctx.ObjectContext.MDGeneral_Enums_Location.DeleteObject(data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }
}

