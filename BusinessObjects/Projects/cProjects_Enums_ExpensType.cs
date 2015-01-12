using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;

namespace BusinessObjects.Projects
{
	[Serializable]
	public partial class cProjects_Enums_ExpensType: CoreBusinessClass<cProjects_Enums_ExpensType>
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

        protected static readonly PropertyInfo<System.Int32?> companyUsingServiceIdProperty = RegisterProperty<System.Int32?>(p => p.CompanyUsingServiceId, string.Empty);
        public System.Int32? CompanyUsingServiceId
        {
            get { return GetProperty(companyUsingServiceIdProperty); }
            set { SetProperty(companyUsingServiceIdProperty, value); }
        }

        private static readonly PropertyInfo<bool> inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Inactive
        {
            get { return GetProperty(inactiveProperty); }
            set { SetProperty(inactiveProperty, value); }
        }

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#endregion

        #region Factory Methods

        public static cProjects_Enums_ExpensType NewProjects_Enums_ExpensType()
        {
            return DataPortal.Create<cProjects_Enums_ExpensType>();

        }

        public static cProjects_Enums_ExpensType GetProjects_Enums_ExpensType(int uniqueId)
        {
            return DataPortal.Fetch<cProjects_Enums_ExpensType>(new SingleCriteria<cProjects_Enums_ExpensType, int>(uniqueId));
        }

        internal static cProjects_Enums_ExpensType GetProjects_Enums_ExpensType(Projects_Enums_ExpensType data)
        {
            return DataPortal.Fetch<cProjects_Enums_ExpensType>(data);
        }

        public static void DeleteProjects_Enums_ExpensType(int uniqueId)
        {
            DataPortal.Delete<cProjects_Enums_ExpensType>(new SingleCriteria<cProjects_Enums_ExpensType, int>(uniqueId));
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cProjects_Enums_ExpensType, int> criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = ctx.ObjectContext.Projects_Enums_ExpensType.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(Projects_Enums_ExpensType data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
            LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
            MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_Enums_ExpensType();

                data.Name = ReadProperty<string>(nameProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);

                ctx.ObjectContext.AddToProjects_Enums_ExpensType(data);
                ctx.ObjectContext.SaveChanges();
                //Get New id
                int newId = data.Id;
                //Load New Id into object
                LoadProperty(IdProperty, newId);
                //Load New EntityKey into Object
                LoadProperty(EntityKeyDataProperty, Serialize(data.EntityKey));

                ctx.ObjectContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_Enums_ExpensType();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Name = ReadProperty<string>(nameProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<cProjects_Enums_ExpensType, int> criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = ctx.ObjectContext.Projects_Enums_ExpensType.First(p => p.Id == criteria.Value);

                ctx.ObjectContext.Projects_Enums_ExpensType.DeleteObject(data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}

    public partial class cProjects_Enums_ExpensType_List : BusinessListBase<cProjects_Enums_ExpensType_List, cProjects_Enums_ExpensType>
    {
        public static cProjects_Enums_ExpensType_List GetcProjects_Enums_ExpensType_List()
        {
            return DataPortal.Fetch<cProjects_Enums_ExpensType_List>();
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var result = ctx.ObjectContext.Projects_Enums_ExpensType;

                foreach (var data in result)
                {
                    var obj = cProjects_Enums_ExpensType.GetProjects_Enums_ExpensType(data);

                    this.Add(obj);
                }
            }
        }
    }
}
