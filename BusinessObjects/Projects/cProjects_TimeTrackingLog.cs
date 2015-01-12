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
	public partial class cProjects_TimeTrackingLog: CoreBusinessClass<cProjects_TimeTrackingLog>
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

		private static readonly PropertyInfo< System.Int32? > work_OrderIdProperty = RegisterProperty<System.Int32?>(p => p.Work_OrderId, string.Empty,(System.Int32?)null);
		public System.Int32? Work_OrderId
		{
			get { return GetProperty(work_OrderIdProperty); }
			set { SetProperty(work_OrderIdProperty, value); }
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

		private static readonly PropertyInfo< System.Int32? > documents_TravelOrderIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_TravelOrderId, string.Empty,(System.Int32?)null);
		public System.Int32? Documents_TravelOrderId
		{
			get { return GetProperty(documents_TravelOrderIdProperty); }
			set { SetProperty(documents_TravelOrderIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDEntities_ServiceIdProperty = RegisterProperty<System.Int32?>(p => p.MDEntities_ServiceId, string.Empty,(System.Int32?)null);
		public System.Int32? MDEntities_ServiceId
		{
			get { return GetProperty(mDEntities_ServiceIdProperty); }
			set { SetProperty(mDEntities_ServiceIdProperty, value); }
		}

        protected static readonly PropertyInfo<System.String> service_DescriptionProperty = RegisterProperty<System.String>(p => p.Service_Description, string.Empty, (System.String)null);
        public System.String Service_Description
        {
            get { return GetProperty(service_DescriptionProperty); }
            set { SetProperty(service_DescriptionProperty, (value ?? "").Trim()); }
        }

		private static readonly PropertyInfo< System.Int32 > mDSubjects_SubjectTeamMemberIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_SubjectTeamMemberId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 MDSubjects_SubjectTeamMemberId
		{
			get { return GetProperty(mDSubjects_SubjectTeamMemberIdProperty); }
			set { SetProperty(mDSubjects_SubjectTeamMemberIdProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime > activityDateProperty = RegisterProperty<System.DateTime>(p => p.ActivityDate, string.Empty,System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime ActivityDate
		{
			get { return GetProperty(activityDateProperty); }
			set { SetProperty(activityDateProperty, value); }
		}

		private static readonly PropertyInfo< System.Byte > billingMethodProperty = RegisterProperty<System.Byte>(p => p.BillingMethod, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Byte BillingMethod
		{
			get { return GetProperty(billingMethodProperty); }
			set { SetProperty(billingMethodProperty, value); }
		}

        private static readonly PropertyInfo<System.Decimal> quantityProperty = RegisterProperty<System.Decimal>(p => p.Quantity, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Decimal Quantity
        {
            get { return GetProperty(quantityProperty); }
            set { SetProperty(quantityProperty, value); }
        }

		private static readonly PropertyInfo< System.Decimal > hoursProperty = RegisterProperty<System.Decimal>(p => p.Hours, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Decimal Hours
		{
			get { return GetProperty(hoursProperty); }
			set { SetProperty(hoursProperty, value); }
		}

		private static readonly PropertyInfo< bool > isBillableProperty = RegisterProperty<bool>(p => p.IsBillable, string.Empty, true);
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

		private static readonly PropertyInfo< System.String > descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty);
        //[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Description
		{
			get { return GetProperty(descriptionProperty); }
			set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
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

        public static cProjects_TimeTrackingLog NewProjects_TimeTrackingLog()
        {
            return DataPortal.Create<cProjects_TimeTrackingLog>();

        }

        public static cProjects_TimeTrackingLog GetProjects_TimeTrackingLog(int uniqueId)
        {
            return DataPortal.Fetch<cProjects_TimeTrackingLog>(new SingleCriteria<cProjects_TimeTrackingLog, int>(uniqueId));
        }

        public static cProjects_TimeTrackingLog NewChildProjects_TimeTrackingLog()
        {
            return DataPortal.CreateChild<cProjects_TimeTrackingLog>();
        }

        internal static cProjects_TimeTrackingLog GetProjects_TimeTrackingLog(Projects_TimeTrackingLog data)
        {
            return DataPortal.FetchChild<cProjects_TimeTrackingLog>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cProjects_TimeTrackingLog, int> criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = ctx.ObjectContext.Projects_TimeTrackingLog.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<int?>(projects_ProjectIdProperty, data.Projects_ProjectId);
                LoadProperty<int?>(work_OrderIdProperty, data.Work_OrderId);
                LoadProperty<int?>(ordinalInProjectProperty, data.OrdinalInProject);
                LoadProperty<int?>(ordinalInWorkOrderProperty, data.OrdinalInWorkOrder);
                LoadProperty<int?>(documents_TravelOrderIdProperty, data.Documents_TravelOrderId);
                LoadProperty<int?>(mDEntities_ServiceIdProperty, data.MDEntities_ServiceId);
                LoadProperty<string>(service_DescriptionProperty, data.Service_Description);
                LoadProperty<int>(mDSubjects_SubjectTeamMemberIdProperty, data.MDSubjects_SubjectTeamMemberId);
                LoadProperty<DateTime>(activityDateProperty, data.ActivityDate);
                LoadProperty<byte>(billingMethodProperty, data.BillingMethod);
                LoadProperty<decimal>(quantityProperty, data.Quantity);
                LoadProperty<decimal>(hoursProperty, data.Hours);
                LoadProperty<bool>(isBillableProperty, data.IsBillable);
                LoadProperty<int?>(documents_Invoice_ItemsColIdProperty, data.Documents_Invoice_ItemsColId);
                LoadProperty<string>(descriptionProperty, data.Description);
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
                var data = new Projects_TimeTrackingLog();

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.Work_OrderId = ReadProperty<int?>(work_OrderIdProperty);
                data.OrdinalInProject = ReadProperty<int?>(ordinalInProjectProperty);
                data.OrdinalInWorkOrder = ReadProperty<int?>(ordinalInWorkOrderProperty);
                data.Documents_TravelOrderId = ReadProperty<int?>(documents_TravelOrderIdProperty);
                data.MDEntities_ServiceId = ReadProperty<int?>(mDEntities_ServiceIdProperty);
                data.Service_Description = ReadProperty<string>(service_DescriptionProperty);
                data.MDSubjects_SubjectTeamMemberId = ReadProperty<int>(mDSubjects_SubjectTeamMemberIdProperty);
                data.ActivityDate = ReadProperty<DateTime>(activityDateProperty);
                data.BillingMethod = ReadProperty<byte>(billingMethodProperty);
                data.Quantity = ReadProperty<decimal>(quantityProperty);
                data.Hours = ReadProperty<decimal>(hoursProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_Invoice_ItemsColId = ReadProperty<int?>(documents_Invoice_ItemsColIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToProjects_TimeTrackingLog(data);
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
                var data = new Projects_TimeTrackingLog();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.Work_OrderId = ReadProperty<int?>(work_OrderIdProperty);
                data.OrdinalInProject = ReadProperty<int?>(ordinalInProjectProperty);
                data.OrdinalInWorkOrder = ReadProperty<int?>(ordinalInWorkOrderProperty);
                data.Documents_TravelOrderId = ReadProperty<int?>(documents_TravelOrderIdProperty);
                data.MDEntities_ServiceId = ReadProperty<int?>(mDEntities_ServiceIdProperty);
                data.Service_Description = ReadProperty<string>(service_DescriptionProperty);
                data.MDSubjects_SubjectTeamMemberId = ReadProperty<int>(mDSubjects_SubjectTeamMemberIdProperty);
                data.ActivityDate = ReadProperty<DateTime>(activityDateProperty);
                data.BillingMethod = ReadProperty<byte>(billingMethodProperty);
                data.Quantity = ReadProperty<decimal>(quantityProperty);
                data.Hours = ReadProperty<decimal>(hoursProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_Invoice_ItemsColId = ReadProperty<int?>(documents_Invoice_ItemsColIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
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

        private void Child_Fetch(Projects_TimeTrackingLog data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<int?>(projects_ProjectIdProperty, data.Projects_ProjectId);
            LoadProperty<int?>(work_OrderIdProperty, data.Work_OrderId);
            LoadProperty<int?>(ordinalInProjectProperty, data.OrdinalInProject);
            LoadProperty<int?>(ordinalInWorkOrderProperty, data.OrdinalInWorkOrder);
            LoadProperty<int?>(documents_TravelOrderIdProperty, data.Documents_TravelOrderId);
            LoadProperty<int?>(mDEntities_ServiceIdProperty, data.MDEntities_ServiceId);
            LoadProperty<string>(service_DescriptionProperty, data.Service_Description);
            LoadProperty<int>(mDSubjects_SubjectTeamMemberIdProperty, data.MDSubjects_SubjectTeamMemberId);
            LoadProperty<DateTime>(activityDateProperty, data.ActivityDate);
            LoadProperty<byte>(billingMethodProperty, data.BillingMethod);
            LoadProperty<decimal>(quantityProperty, data.Quantity);
            LoadProperty<decimal>(hoursProperty, data.Hours);
            LoadProperty<bool>(isBillableProperty, data.IsBillable);
            LoadProperty<int?>(documents_Invoice_ItemsColIdProperty, data.Documents_Invoice_ItemsColId);
            LoadProperty<string>(descriptionProperty, data.Description);
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
                var data = new Projects_TimeTrackingLog();

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.Work_OrderId = ReadProperty<int?>(work_OrderIdProperty);
                data.OrdinalInProject = ReadProperty<int?>(ordinalInProjectProperty);
                data.OrdinalInWorkOrder = ReadProperty<int?>(ordinalInWorkOrderProperty);
                data.Documents_TravelOrderId = ReadProperty<int?>(documents_TravelOrderIdProperty);
                data.MDEntities_ServiceId = ReadProperty<int?>(mDEntities_ServiceIdProperty);
                data.Service_Description = ReadProperty<string>(service_DescriptionProperty);
                data.MDSubjects_SubjectTeamMemberId = ReadProperty<int>(mDSubjects_SubjectTeamMemberIdProperty);
                data.ActivityDate = ReadProperty<DateTime>(activityDateProperty);
                data.BillingMethod = ReadProperty<byte>(billingMethodProperty);
                data.Quantity = ReadProperty<decimal>(quantityProperty);
                data.Hours = ReadProperty<decimal>(hoursProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_Invoice_ItemsColId = ReadProperty<int?>(documents_Invoice_ItemsColIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToProjects_TimeTrackingLog(data);

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
                var data = new Projects_TimeTrackingLog();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.Work_OrderId = ReadProperty<int?>(work_OrderIdProperty);
                data.OrdinalInProject = ReadProperty<int?>(ordinalInProjectProperty);
                data.OrdinalInWorkOrder = ReadProperty<int?>(ordinalInWorkOrderProperty);
                data.Documents_TravelOrderId = ReadProperty<int?>(documents_TravelOrderIdProperty);
                data.MDEntities_ServiceId = ReadProperty<int?>(mDEntities_ServiceIdProperty);
                data.Service_Description = ReadProperty<string>(service_DescriptionProperty);
                data.MDSubjects_SubjectTeamMemberId = ReadProperty<int>(mDSubjects_SubjectTeamMemberIdProperty);
                data.ActivityDate = ReadProperty<DateTime>(activityDateProperty);
                data.BillingMethod = ReadProperty<byte>(billingMethodProperty);
                data.Quantity = ReadProperty<decimal>(quantityProperty);
                data.Hours = ReadProperty<decimal>(hoursProperty);
                data.IsBillable = ReadProperty<bool>(isBillableProperty);
                data.Documents_Invoice_ItemsColId = ReadProperty<int?>(documents_Invoice_ItemsColIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
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
                var data = new Projects_TimeTrackingLog();

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
    public partial class cProjects_TimeTrackingLog_List : BusinessListBase<cProjects_TimeTrackingLog_List, cProjects_TimeTrackingLog>
    {
        public static cProjects_TimeTrackingLog_List NewProjects_TimeTrackingLog_List()
        {
            return DataPortal.Create<cProjects_TimeTrackingLog_List>();

        }

        public static cProjects_TimeTrackingLog_List GetcProjects_TimeTrackingLog_List()
        {
            return DataPortal.Fetch<cProjects_TimeTrackingLog_List>();
        }

        public static cProjects_TimeTrackingLog_List GetcProjects_TimeTrackingLog_List_ByLinkDocumentId(int? workorderId = null, int? projectId = null)
        {
            return DataPortal.Fetch<cProjects_TimeTrackingLog_List>(new TimeTracking_Criteria(workorderId, projectId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var result = ctx.ObjectContext.Projects_TimeTrackingLog.Where(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);

                foreach (var data in result)
                {
                    var obj = cProjects_TimeTrackingLog.GetProjects_TimeTrackingLog(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(TimeTracking_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                IQueryable<DalEf.Projects_TimeTrackingLog> result = null;

                if (criteria.WorkorderId != null)
                {
                    result = ctx.ObjectContext.Projects_TimeTrackingLog.Where(p => p.Work_OrderId == criteria.WorkorderId);
                }
                else if (criteria.ProjectId != null)
                {
                    result = ctx.ObjectContext.Projects_TimeTrackingLog.Where(p => p.Projects_ProjectId == criteria.ProjectId);
                }


                foreach (var data in result)
                {
                    var obj = cProjects_TimeTrackingLog.GetProjects_TimeTrackingLog(data);

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
