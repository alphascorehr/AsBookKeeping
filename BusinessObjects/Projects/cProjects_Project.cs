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
	public partial class cProjects_Project: CoreBusinessClass<cProjects_Project>
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

		private static readonly PropertyInfo< bool? > activeProperty = RegisterProperty<bool?>(p => p.Active, string.Empty,(bool?)null);
		public bool? Active
		{
			get { return GetProperty(activeProperty); }
			set { SetProperty(activeProperty, value); }
		}

		private static readonly PropertyInfo< bool? > payedInFullProperty = RegisterProperty<bool?>(p => p.PayedInFull, string.Empty,(bool?)null);
		public bool? PayedInFull
		{
			get { return GetProperty(payedInFullProperty); }
			set { SetProperty(payedInFullProperty, value); }
		}

		private static readonly PropertyInfo< System.String > descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
		public System.String Description
		{
			get { return GetProperty(descriptionProperty); }
			set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
		}

		private static readonly PropertyInfo< System.Int32? > mDSubjects_SubjectIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_SubjectId, string.Empty,(System.Int32?)null);
		public System.Int32? MDSubjects_SubjectId
		{
			get { return GetProperty(mDSubjects_SubjectIdProperty); }
			set { SetProperty(mDSubjects_SubjectIdProperty, value); }
		}

		private static readonly PropertyInfo< bool? > isInternalProperty = RegisterProperty<bool?>(p => p.IsInternal, string.Empty,(bool?)null);
		public bool? IsInternal
		{
			get { return GetProperty(isInternalProperty); }
			set { SetProperty(isInternalProperty, value); }
		}

		private static readonly PropertyInfo< System.Byte? > billingMethodProperty = RegisterProperty<System.Byte?>(p => p.BillingMethod, string.Empty, (System.Byte?)null);
		public System.Byte? BillingMethod
		{
			get { return GetProperty(billingMethodProperty); }
			set { SetProperty(billingMethodProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > timeBudget_HoursProperty = RegisterProperty<System.Decimal?>(p => p.TimeBudget_Hours, string.Empty, (System.Decimal?)null);
		public System.Decimal? TimeBudget_Hours
		{
			get { return GetProperty(timeBudget_HoursProperty); }
			set { SetProperty(timeBudget_HoursProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > projectRatePerHourProperty = RegisterProperty<System.Decimal?>(p => p.ProjectRatePerHour, string.Empty, (System.Decimal?)null);
		public System.Decimal? ProjectRatePerHour
		{
			get { return GetProperty(projectRatePerHourProperty); }
			set { SetProperty(projectRatePerHourProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > flatAmountProperty = RegisterProperty<System.Decimal?>(p => p.FlatAmount, string.Empty, (System.Decimal?)null);
		public System.Decimal? FlatAmount
		{
			get { return GetProperty(flatAmountProperty); }
			set { SetProperty(flatAmountProperty, value); }
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

		#region Kolekcije
		public static PropertyInfo<cProjects_Project_TeamMemebersCol> Projects_Project_TeamMemebersColProperty = RegisterProperty<cProjects_Project_TeamMemebersCol>(c => c.Projects_Project_TeamMemebersCol);
		public cProjects_Project_TeamMemebersCol Projects_Project_TeamMemebersCol
		{
			get
			{
				if (!FieldManager.FieldExists(Projects_Project_TeamMemebersColProperty))
					LoadProperty<cProjects_Project_TeamMemebersCol>(Projects_Project_TeamMemebersColProperty, cProjects_Project_TeamMemebersCol.NewProjects_Project_TeamMemebersCol());
				return GetProperty(Projects_Project_TeamMemebersColProperty);
			}
			private set { SetProperty(Projects_Project_TeamMemebersColProperty, value); }
		}

		#endregion
		#endregion

        #region Factory Methods

        public static cProjects_Project NewProjects_Project()
        {
            return DataPortal.Create<cProjects_Project>();

        }

        public static cProjects_Project GetProjects_Project(int uniqueId)
        {
            return DataPortal.Fetch<cProjects_Project>(new SingleCriteria<cProjects_Project, int>(uniqueId));
        }

        internal static cProjects_Project GetProjects_Project(Projects_Project data)
        {
            return DataPortal.Fetch<cProjects_Project>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cProjects_Project, int> criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = ctx.ObjectContext.Projects_Project.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<bool?>(activeProperty, data.Active);
                LoadProperty<bool?>(payedInFullProperty, data.PayedInFull);
                LoadProperty<string>(descriptionProperty, data.Description);
                LoadProperty<int?>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
                LoadProperty<bool?>(isInternalProperty, data.IsInternal);
                LoadProperty<byte?>(billingMethodProperty, data.BillingMethod);
                LoadProperty<decimal?>(timeBudget_HoursProperty, data.TimeBudget_Hours);
                LoadProperty<decimal?>(projectRatePerHourProperty, data.ProjectRatePerHour);
                LoadProperty<decimal?>(flatAmountProperty, data.FlatAmount);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);

                LastChanged = data.LastChanged;

                LoadProperty<cProjects_Project_TeamMemebersCol>(Projects_Project_TeamMemebersColProperty, cProjects_Project_TeamMemebersCol.GetcProjects_Project_TeamMemebersCol(data.Projects_Project_TeamMemebersCol));

                BusinessRules.CheckRules();
                
            }
        }

        

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_Project();

                data.Name = ReadProperty<string>(nameProperty);
                data.Active = ReadProperty<bool?>(activeProperty);
                data.PayedInFull = ReadProperty<bool?>(payedInFullProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.IsInternal = ReadProperty<bool?>(isInternalProperty);
                data.BillingMethod = ReadProperty<byte?>(billingMethodProperty);
                data.TimeBudget_Hours = ReadProperty<decimal?>(timeBudget_HoursProperty);
                data.ProjectRatePerHour = ReadProperty<decimal?>(projectRatePerHourProperty);
                data.FlatAmount = ReadProperty<decimal?>(flatAmountProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);

                
                ctx.ObjectContext.AddToProjects_Project(data);

                DataPortal.UpdateChild(ReadProperty<cProjects_Project_TeamMemebersCol>(Projects_Project_TeamMemebersColProperty), data);

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
                var data = new Projects_Project();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Name = ReadProperty<string>(nameProperty);
                data.Active = ReadProperty<bool?>(activeProperty);
                data.PayedInFull = ReadProperty<bool?>(payedInFullProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.IsInternal = ReadProperty<bool?>(isInternalProperty);
                data.BillingMethod = ReadProperty<byte?>(billingMethodProperty);
                data.TimeBudget_Hours = ReadProperty<decimal?>(timeBudget_HoursProperty);
                data.ProjectRatePerHour = ReadProperty<decimal?>(projectRatePerHourProperty);
                data.FlatAmount = ReadProperty<decimal?>(flatAmountProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);

                DataPortal.UpdateChild(ReadProperty<cProjects_Project_TeamMemebersCol>(Projects_Project_TeamMemebersColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
