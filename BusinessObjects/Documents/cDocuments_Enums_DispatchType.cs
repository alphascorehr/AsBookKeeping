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

namespace BusinessObjects.Documents
{
	[Serializable]
	public partial class cDocuments_Enums_DispatchType: CoreBusinessClass<cDocuments_Enums_DispatchType>
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
			set { SetProperty(nameProperty, (value ?? "").Trim()); }
		}

        private static readonly PropertyInfo<bool> inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Inactive
        {
            get { return GetProperty(inactiveProperty); }
            set { SetProperty(inactiveProperty, value); }
        }

		private static readonly PropertyInfo< System.String > descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
		public System.String Description
		{
			get { return GetProperty(descriptionProperty); }
			set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
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

        public static cDocuments_Enums_DispatchType NewDocuments_Enums_DispatchType()
        {
            return DataPortal.Create<cDocuments_Enums_DispatchType>();

        }

        public static cDocuments_Enums_DispatchType GetDocuments_Enums_DispatchType(int uniqueId)
        {
            return DataPortal.Fetch<cDocuments_Enums_DispatchType>(new SingleCriteria<cDocuments_Enums_DispatchType, int>(uniqueId));
        }

        internal static cDocuments_Enums_DispatchType GetDocuments_Enums_DispatchType(Documents_Enums_DispatchType data)
        {
            return DataPortal.Fetch<cDocuments_Enums_DispatchType>(data);
        }

        public static void DeleteDocuments_Enums_DispatchType(int uniqueId)
        {
            DataPortal.Delete<cDocuments_Enums_DispatchType>(new SingleCriteria<cDocuments_Enums_DispatchType, int>(uniqueId));
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cDocuments_Enums_DispatchType, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Enums_DispatchType.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<string>(descriptionProperty, data.Description);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
              
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(Documents_Enums_DispatchType data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LoadProperty<string>(descriptionProperty, data.Description);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
            MarkAsChild();

        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_Enums_DispatchType();

                data.Name = ReadProperty<string>(nameProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToDocuments_Enums_DispatchType(data);

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
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = new Documents_Enums_DispatchType();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.Name = ReadProperty<string>(nameProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<cDocuments_Enums_DispatchType, int> criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var data = ctx.ObjectContext.Documents_Enums_DispatchType.First(p => p.Id == criteria.Value);

                ctx.ObjectContext.Documents_Enums_DispatchType.DeleteObject(data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
    }

    public partial class cDocuments_Enums_DispatchType_List : BusinessListBase<cDocuments_Enums_DispatchType_List, cDocuments_Enums_DispatchType>
    {
        public static cDocuments_Enums_DispatchType_List GetcDocuments_Enums_DispatchType_List()
        {
            return DataPortal.Fetch<cDocuments_Enums_DispatchType_List>();
        }

        public static cDocuments_Enums_DispatchType_List GetcDocuments_Enums_DispatchType_List(int companyId, int includeInactiveId)
        {
            return DataPortal.Fetch<cDocuments_Enums_DispatchType_List>(new ActiveEnums_Criteria(companyId, includeInactiveId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var result = ctx.ObjectContext.Documents_Enums_DispatchType;

                foreach (var data in result)
                {
                    var obj = cDocuments_Enums_DispatchType.GetDocuments_Enums_DispatchType(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(ActiveEnums_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<DocumentsEntities>.GetManager("DocumentsEntities"))
            {
                var result = ctx.ObjectContext.Documents_Enums_DispatchType.Where(p => (p.CompanyUsingServiceId == criteria.CompanyId || (p.CompanyUsingServiceId ?? 0) == 0) && (p.Inactive == false || p.Id == criteria.IncludeInactiveId));

                foreach (var data in result)
                {
                    var obj = cDocuments_Enums_DispatchType.GetDocuments_Enums_DispatchType(data);

                    this.Add(obj);
                }
            }
        }
    }
    
}
