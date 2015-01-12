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
	public partial class cMDGeneral_Enums_Language: CoreBusinessClass<cMDGeneral_Enums_Language>
	{
		#region Business Methods
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			internal set { SetProperty(IdProperty, value); }
		}

		private static readonly PropertyInfo< System.String > nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
			set { SetProperty(nameProperty, value.Trim()); }
		}

        private static readonly PropertyInfo<System.String> labelProperty = RegisterProperty<System.String>(p => p.Label, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(10, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Label
		{
            get { return GetProperty(labelProperty); }
            set { SetProperty(labelProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< bool > defaultLanguageProperty = RegisterProperty<bool>(p => p.DefaultLanguage, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public bool DefaultLanguage
		{
			get { return GetProperty(defaultLanguageProperty); }
			set { SetProperty(defaultLanguageProperty, value); }
		}

		private static readonly PropertyInfo< bool > inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public bool Inactive
		{
			get { return GetProperty(inactiveProperty); }
			set { SetProperty(inactiveProperty, value); }
		}

        protected static readonly PropertyInfo<System.Int32?> companyUsingServiceIdProperty = RegisterProperty<System.Int32?>(p => p.CompanyUsingServiceId, string.Empty);
        public System.Int32? CompanyUsingServiceId
        {
            get { return GetProperty(companyUsingServiceIdProperty); }
            set { SetProperty(companyUsingServiceIdProperty, value); }
        }

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#endregion

        #region Factory Methods

        public static cMDGeneral_Enums_Language NewMDGeneral_Enums_Language()
        {
            return DataPortal.Create<cMDGeneral_Enums_Language>();

        }

        public static cMDGeneral_Enums_Language GetMDGeneral_Enums_Language(int uniqueId)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Language>(new SingleCriteria<cMDGeneral_Enums_Language, int>(uniqueId));
        }

        internal static cMDGeneral_Enums_Language GetMDGeneral_Enums_Language(MDGeneral_Enums_Language data)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Language>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDGeneral_Enums_Language, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = ctx.ObjectContext.MDGeneral_Enums_Language.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<string>(labelProperty, data.Label);
                LoadProperty<bool>(defaultLanguageProperty, data.DefaultLanguage);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDGeneral_Enums_Language data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<string>(labelProperty, data.Label);
            LoadProperty<bool>(defaultLanguageProperty, data.DefaultLanguage);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
            MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var data = new MDGeneral_Enums_Language();

                data.Name = ReadProperty<string>(nameProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.DefaultLanguage = ReadProperty<bool>(defaultLanguageProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
               
                ctx.ObjectContext.AddToMDGeneral_Enums_Language(data);
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
                var data = new MDGeneral_Enums_Language();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Name = ReadProperty<string>(nameProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.DefaultLanguage = ReadProperty<bool>(defaultLanguageProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }

    public partial class cMDGeneral_Enums_Language_List : BusinessListBase<cMDGeneral_Enums_Language_List, cMDGeneral_Enums_Language>
    {
        public static cMDGeneral_Enums_Language_List GetcMDGeneral_Enums_Language_List()
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Language_List>();
        }

        public static cMDGeneral_Enums_Language_List GetcMDGeneral_Enums_Language_List(int companyId, int includeInactiveId)
        {
            return DataPortal.Fetch<cMDGeneral_Enums_Language_List>(new ActiveEnums_Criteria(companyId, includeInactiveId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var result = ctx.ObjectContext.MDGeneral_Enums_Language;

                foreach (var data in result)
                {
                    var obj = cMDGeneral_Enums_Language.GetMDGeneral_Enums_Language(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(ActiveEnums_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<MDGeneralEntities>.GetManager("MDGeneralEntities"))
            {
                var result = ctx.ObjectContext.MDGeneral_Enums_Language.Where(p => (p.CompanyUsingServiceId == criteria.CompanyId || (p.CompanyUsingServiceId ?? 0) == 0) && (p.Inactive == false || p.Id == criteria.IncludeInactiveId));

                foreach (var data in result)
                {
                    var obj = cMDGeneral_Enums_Language.GetMDGeneral_Enums_Language(data);

                    this.Add(obj);
                }
            }
        }
    }
}

