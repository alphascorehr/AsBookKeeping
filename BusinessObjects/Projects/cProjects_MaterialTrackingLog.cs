using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using BusinessObjects.Security;

namespace BusinessObjects.Projects
{
	[Serializable]
	public partial class cProjects_MaterialTrackingLog: CoreBusinessClass<cProjects_MaterialTrackingLog>
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

		private static readonly PropertyInfo< System.Int32? > projects_ProjectIdProperty = RegisterProperty<System.Int32?>(p => p.Projects_ProjectId, string.Empty,(System.Int32?)null);
		public System.Int32? Projects_ProjectId
		{
			get { return GetProperty(projects_ProjectIdProperty); }
			set { SetProperty(projects_ProjectIdProperty, value); }
		}


		private static readonly PropertyInfo< System.Int32? > documents_WorkOrderIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_WorkOrderId, string.Empty,(System.Int32?)null);
		public System.Int32? Documents_WorkOrderId
		{
			get { return GetProperty(documents_WorkOrderIdProperty); }
			set { SetProperty(documents_WorkOrderIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32?> ordinalInProjectProperty = RegisterProperty<System.Int32?>(p => p.OrdinalInProject, string.Empty);
        public System.Int32? OrdinalInProject
        {
            get { return GetProperty(ordinalInProjectProperty); }
            set { SetProperty(ordinalInProjectProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> ordinalInWorkOrderProperty = RegisterProperty<System.Int32?>(p => p.OrdinalInWorkOrder, string.Empty);
		public System.Int32? OrdinalInWorkOrder
		{
            get { return GetProperty(ordinalInWorkOrderProperty); }
            set { SetProperty(ordinalInWorkOrderProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > productIdProperty = RegisterProperty<System.Int32?>(p => p.ProductId, string.Empty,(System.Int32?)null);
		public System.Int32? ProductId
		{
			get { return GetProperty(productIdProperty); }
			set { SetProperty(productIdProperty, value); }
		}

        protected static readonly PropertyInfo<System.String> product_DescriptionProperty = RegisterProperty<System.String>(p => p.Product_Description, string.Empty, (System.String)null);
        public System.String Product_Description
        {
            get { return GetProperty(product_DescriptionProperty); }
            set { SetProperty(product_DescriptionProperty, (value ?? "").Trim()); }
        }

		private static readonly PropertyInfo< System.Int32? > productUnitIdProperty = RegisterProperty<System.Int32?>(p => p.ProductUnitId, string.Empty,(System.Int32?)null);
		public System.Int32? ProductUnitId
		{
			get { return GetProperty(productUnitIdProperty); }
			set { SetProperty(productUnitIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > productAmmountProperty = RegisterProperty<System.Decimal?>(p => p.ProductAmmount, string.Empty, (System.Decimal?)null);
		public System.Decimal? ProductAmmount
		{
			get { return GetProperty(productAmmountProperty); }
			set { SetProperty(productAmmountProperty, value); }
		}

        private static readonly PropertyInfo<bool> isBillableProperty = RegisterProperty<bool>(p => p.IsBillable, string.Empty, true);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public bool IsBillable
		{
			get { return GetProperty(isBillableProperty); }
			set { SetProperty(isBillableProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > documents_Invoice_ItemsColIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_Invoice_ItemsColId, string.Empty,(System.Int32?)null);
		public System.Int32? Documents_Invoice_ItemsColId
		{
			get { return GetProperty(documents_Invoice_ItemsColIdProperty); }
			set { SetProperty(documents_Invoice_ItemsColIdProperty, value); }
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

        protected static readonly PropertyInfo<System.Int32> companyUsingServiceIdProperty = RegisterProperty<System.Int32>(p => p.CompanyUsingServiceId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 CompanyUsingServiceId
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

        public static cProjects_MaterialTrackingLog NewProjects_MaterialTrackingLog()
        {
            return DataPortal.Create<cProjects_MaterialTrackingLog>();

        }

        public static cProjects_MaterialTrackingLog GetProjects_MaterialTrackingLog(int uniqueId)
        {
            return DataPortal.Fetch<cProjects_MaterialTrackingLog>(new SingleCriteria<cProjects_MaterialTrackingLog, int>(uniqueId));
        }

        public static cProjects_MaterialTrackingLog NewChildProjects_MaterialTrackingLog()
        {
            return DataPortal.CreateChild<cProjects_MaterialTrackingLog>();

        }

        internal static cProjects_MaterialTrackingLog GetProjects_MaterialTrackingLog(Projects_MaterialTrackingLog data)
        {
            return DataPortal.FetchChild<cProjects_MaterialTrackingLog>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cProjects_MaterialTrackingLog, int> criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = ctx.ObjectContext.Projects_MaterialTrackingLog.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<int?>(projects_ProjectIdProperty, data.Projects_ProjectId);
                LoadProperty<int?>(documents_WorkOrderIdProperty, data.Documents_WorkOrderId);
                LoadProperty<int?>(ordinalInProjectProperty, data.OrdinalInProject);
                LoadProperty<int?>(ordinalInWorkOrderProperty, data.OrdinalInWorkOrder);
                LoadProperty<int?>(productIdProperty, data.ProductId);
                LoadProperty<string>(product_DescriptionProperty, data.Product_Description);
                LoadProperty<int?>(productUnitIdProperty, data.ProductUnitId);
                LoadProperty<decimal?>(productAmmountProperty, data.ProductAmmount);
                LoadProperty<bool>(isBillableProperty, data.IsBillable);
                LoadProperty<int?>(documents_Invoice_ItemsColIdProperty, data.Documents_Invoice_ItemsColId);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

     

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_MaterialTrackingLog();

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.Documents_WorkOrderId = ReadProperty<int?>(documents_WorkOrderIdProperty);
                data.OrdinalInProject = ReadProperty<int?>(ordinalInProjectProperty);
                data.OrdinalInWorkOrder = ReadProperty<int?>(ordinalInWorkOrderProperty);
                data.ProductId = ReadProperty<int?>(productIdProperty);
                data.Product_Description = ReadProperty<string>(product_DescriptionProperty);
                data.ProductUnitId = ReadProperty<int?>(productUnitIdProperty);
                data.ProductAmmount = ReadProperty<decimal?>(productAmmountProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_Invoice_ItemsColId = ReadProperty<int?>(documents_Invoice_ItemsColIdProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToProjects_MaterialTrackingLog(data);
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
                var data = new Projects_MaterialTrackingLog();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.Documents_WorkOrderId = ReadProperty<int?>(documents_WorkOrderIdProperty);
                data.OrdinalInProject = ReadProperty<int?>(ordinalInProjectProperty);
                data.OrdinalInWorkOrder = ReadProperty<int?>(ordinalInWorkOrderProperty);
                data.ProductId = ReadProperty<int?>(productIdProperty);
                data.Product_Description = ReadProperty<string>(product_DescriptionProperty);
                data.ProductUnitId = ReadProperty<int?>(productUnitIdProperty);
                data.ProductAmmount = ReadProperty<decimal?>(productAmmountProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_Invoice_ItemsColId = ReadProperty<int?>(documents_Invoice_ItemsColIdProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }

        #region Child portals

        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Projects_MaterialTrackingLog data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<int?>(projects_ProjectIdProperty, data.Projects_ProjectId);
            LoadProperty<int?>(documents_WorkOrderIdProperty, data.Documents_WorkOrderId);
            LoadProperty<int?>(ordinalInProjectProperty, data.OrdinalInProject);
            LoadProperty<int?>(ordinalInWorkOrderProperty, data.OrdinalInWorkOrder);
            LoadProperty<int?>(productIdProperty, data.ProductId);
            LoadProperty<string>(product_DescriptionProperty, data.Product_Description);
            LoadProperty<int?>(productUnitIdProperty, data.ProductUnitId);
            LoadProperty<decimal?>(productAmmountProperty, data.ProductAmmount);
            LoadProperty<bool>(isBillableProperty, data.IsBillable);
            LoadProperty<int?>(documents_Invoice_ItemsColIdProperty, data.Documents_Invoice_ItemsColId);
            LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
            LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
            LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();

        }

        private void Child_Insert()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_MaterialTrackingLog();

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.Documents_WorkOrderId = ReadProperty<int?>(documents_WorkOrderIdProperty);
                data.OrdinalInProject = ReadProperty<int?>(ordinalInProjectProperty);
                data.OrdinalInWorkOrder = ReadProperty<int?>(ordinalInWorkOrderProperty);
                data.ProductId = ReadProperty<int?>(productIdProperty);
                data.Product_Description = ReadProperty<string>(product_DescriptionProperty);
                data.ProductUnitId = ReadProperty<int?>(productUnitIdProperty);
                data.ProductAmmount = ReadProperty<decimal?>(productAmmountProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_Invoice_ItemsColId = ReadProperty<int?>(documents_Invoice_ItemsColIdProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToProjects_MaterialTrackingLog(data);

                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                        LastChanged = data.LastChanged;
                    }
                };
            }
        }

        private void Child_Update()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_MaterialTrackingLog();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.Documents_WorkOrderId = ReadProperty<int?>(documents_WorkOrderIdProperty);
                data.OrdinalInProject = ReadProperty<int?>(ordinalInProjectProperty);
                data.OrdinalInWorkOrder = ReadProperty<int?>(ordinalInWorkOrderProperty);
                data.ProductId = ReadProperty<int?>(productIdProperty);
                data.Product_Description = ReadProperty<string>(product_DescriptionProperty);
                data.ProductUnitId = ReadProperty<int?>(productUnitIdProperty);
                data.ProductAmmount = ReadProperty<decimal?>(productAmmountProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_Invoice_ItemsColId = ReadProperty<int?>(documents_Invoice_ItemsColIdProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);

                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "LastChanged")
                        LastChanged = data.LastChanged;
                };
            }


        }

        private void Child_DeleteSelf()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_MaterialTrackingLog();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                ctx.ObjectContext.DeleteObject(data);
            }
        } 
        #endregion
        #endregion
	}

    [Serializable]
    public partial class cProjects_MaterialTrackingLog_List : BusinessListBase<cProjects_MaterialTrackingLog_List, cProjects_MaterialTrackingLog>
    {
        public static cProjects_MaterialTrackingLog_List NewProjects_MaterialTrackingLog_List()
        {
            return DataPortal.Create<cProjects_MaterialTrackingLog_List>();

        }

        public static cProjects_MaterialTrackingLog_List GetcProjects_MaterialTrackingLog_List()
        {
            return DataPortal.Fetch<cProjects_MaterialTrackingLog_List>();
        }

        public static cProjects_MaterialTrackingLog_List GetcProjects_MaterialTrackingLog_List_ByLinkDocumentId(int? workorderId= null, int? projectId = null)
        {
            return DataPortal.Fetch<cProjects_MaterialTrackingLog_List>(new MaterialTracking_Criteria(workorderId, projectId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var result = ctx.ObjectContext.Projects_MaterialTrackingLog.Where(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);

                foreach (var data in result)
                {
                    var obj = cProjects_MaterialTrackingLog.GetProjects_MaterialTrackingLog(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(MaterialTracking_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                IQueryable<DalEf.Projects_MaterialTrackingLog> result = null;

                if (criteria.WorkorderId != null)
                {
                    result = ctx.ObjectContext.Projects_MaterialTrackingLog.Where(p => p.Documents_WorkOrderId == criteria.WorkorderId);
                }
                else if (criteria.ProjectId != null)
                {
                    result = ctx.ObjectContext.Projects_MaterialTrackingLog.Where(p => p.Projects_ProjectId == criteria.ProjectId);
                }
      

                foreach (var data in result)
                {
                    var obj = cProjects_MaterialTrackingLog.GetProjects_MaterialTrackingLog(data);

                    this.Add(obj);
                }
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                Child_Update();
                ctx.ObjectContext.SaveChanges();
            }
        }
    }
}
