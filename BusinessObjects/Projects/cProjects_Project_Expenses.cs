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
    public partial class cProjects_Project_Expenses : CoreBusinessClass<cProjects_Project_Expenses>
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

        private static readonly PropertyInfo<System.Int32?> projects_ProjectIdProperty = RegisterProperty<System.Int32?>(p => p.Projects_ProjectId, string.Empty, (System.Int32?)null);
        public System.Int32? Projects_ProjectId
        {
            get { return GetProperty(projects_ProjectIdProperty); }
            set { SetProperty(projects_ProjectIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> mDSubjects_SubjectIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_SubjectId, string.Empty, (System.Int32?)null);
        public System.Int32? MDSubjects_SubjectId
        {
            get { return GetProperty(mDSubjects_SubjectIdProperty); }
            set { SetProperty(mDSubjects_SubjectIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32> projects_Enums_ExpensTypeIdProperty = RegisterProperty<System.Int32>(p => p.Projects_Enums_ExpensTypeId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 Projects_Enums_ExpensTypeId
        {
            get { return GetProperty(projects_Enums_ExpensTypeIdProperty); }
            set { SetProperty(projects_Enums_ExpensTypeIdProperty, value); }
        }

        private static readonly PropertyInfo<System.DateTime> expenseDateProperty = RegisterProperty<System.DateTime>(p => p.ExpenseDate, string.Empty, System.DateTime.Now);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.DateTime ExpenseDate
        {
            get { return GetProperty(expenseDateProperty); }
            set { SetProperty(expenseDateProperty, value); }
        }

        private static readonly PropertyInfo<System.Decimal> amountProperty = RegisterProperty<System.Decimal>(p => p.Amount, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Decimal Amount
        {
            get { return GetProperty(amountProperty); }
            set { SetProperty(amountProperty, value); }
        }

        private static readonly PropertyInfo<bool?> invoicedProperty = RegisterProperty<bool?>(p => p.Invoiced, string.Empty, (bool?)null);
        public bool? Invoiced
        {
            get { return GetProperty(invoicedProperty); }
            set { SetProperty(invoicedProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32?> documents_InvoiceIdProperty = RegisterProperty<System.Int32?>(p => p.Documents_InvoiceId, string.Empty, (System.Int32?)null);
        public System.Int32? Documents_InvoiceId
        {
            get { return GetProperty(documents_InvoiceIdProperty); }
            set { SetProperty(documents_InvoiceIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
        public System.String Description
        {
            get { return GetProperty(descriptionProperty); }
            set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.DateTime> lastActivityDateProperty = RegisterProperty<System.DateTime>(p => p.LastActivityDate, string.Empty, System.DateTime.Now);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.DateTime LastActivityDate
        {
            get { return GetProperty(lastActivityDateProperty); }
            set { SetProperty(lastActivityDateProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32> mDSubjects_EmployeeWhoChengedIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_EmployeeWhoChengedId, string.Empty);
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

        private static readonly PropertyInfo<System.Int32?> ordinalProperty = RegisterProperty<System.Int32?>(p => p.Ordinal, string.Empty, (System.Int32?)null);
        public System.Int32? Ordinal
        {
            get { return GetProperty(ordinalProperty); }
            set { SetProperty(ordinalProperty, value); }
        }

        /// <summary>
        /// Used for optimistic concurrency.
        /// </summary>
        [NotUndoable]
        internal System.Byte[] LastChanged = new System.Byte[8];

        #endregion

        #region Factory Methods

        public static cProjects_Project_Expenses NewProjects_MaterialTrackingLog()
        {
            return DataPortal.Create<cProjects_Project_Expenses>();

        }

        public static cProjects_Project_Expenses GetProjects_MaterialTrackingLog(int uniqueId)
        {
            return DataPortal.Fetch<cProjects_Project_Expenses>(new SingleCriteria<cProjects_Project_Expenses, int>(uniqueId));
        }

        public static cProjects_Project_Expenses NewChildProjects_MaterialTrackingLog()
        {
            return DataPortal.CreateChild<cProjects_Project_Expenses>();

        }

        internal static cProjects_Project_Expenses GetProjects_MaterialTrackingLog(Projects_Project_Expenses data)
        {
            return DataPortal.FetchChild<cProjects_Project_Expenses>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cProjects_Project_Expenses, int> criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = ctx.ObjectContext.Projects_Project_Expenses.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<int?>(projects_ProjectIdProperty, data.Projects_ProjectId);
                LoadProperty<int?>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
                LoadProperty<int>(projects_Enums_ExpensTypeIdProperty, data.Projects_Enums_ExpensTypeId);
                LoadProperty<DateTime>(expenseDateProperty, data.ExpenseDate);
                LoadProperty<decimal>(amountProperty, data.Amount);
                LoadProperty<bool?>(invoicedProperty, data.Invoiced);
                LoadProperty<int?>(documents_InvoiceIdProperty, data.Documents_InvoiceId);
                LoadProperty<string>(descriptionProperty, data.Description);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<int?>(ordinalProperty, data.Ordinal);

                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }



        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_Project_Expenses();

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.Projects_Enums_ExpensTypeId = ReadProperty<int>(projects_Enums_ExpensTypeIdProperty);
                data.ExpenseDate = ReadProperty<DateTime>(expenseDateProperty);
                data.Amount = ReadProperty<decimal>(amountProperty);
                data.Invoiced = ReadProperty<bool?>(invoicedProperty);
                data.Documents_InvoiceId = ReadProperty<int?>(documents_InvoiceIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.Ordinal = ReadProperty<int?>(ordinalProperty);

                ctx.ObjectContext.AddToProjects_Project_Expenses(data);
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
                var data = new Projects_Project_Expenses();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.Projects_Enums_ExpensTypeId = ReadProperty<int>(projects_Enums_ExpensTypeIdProperty);
                data.ExpenseDate = ReadProperty<DateTime>(expenseDateProperty);
                data.Amount = ReadProperty<decimal>(amountProperty);
                data.Invoiced = ReadProperty<bool?>(invoicedProperty);
                data.Documents_InvoiceId = ReadProperty<int?>(documents_InvoiceIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.Ordinal = ReadProperty<int?>(ordinalProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }

        #region Child portals

        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        //private void Child_Fetch(Projects_MaterialTrackingLog data)
        private void Child_Fetch(Projects_Project_Expenses data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<int?>(projects_ProjectIdProperty, data.Projects_ProjectId);
            LoadProperty<int?>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
            LoadProperty<int>(projects_Enums_ExpensTypeIdProperty, data.Projects_Enums_ExpensTypeId);
            LoadProperty<DateTime>(expenseDateProperty, data.ExpenseDate);
            LoadProperty<decimal>(amountProperty, data.Amount);
            LoadProperty<bool?>(invoicedProperty, data.Invoiced);
            LoadProperty<int?>(documents_InvoiceIdProperty, data.Documents_InvoiceId);
            LoadProperty<string>(descriptionProperty, data.Description);
            LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
            LoadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty, data.MDSubjects_EmployeeWhoChengedId);
            LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LoadProperty<int?>(ordinalProperty, data.Ordinal);

            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();

        }

        private void Child_Insert()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_Project_Expenses();

                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.Projects_Enums_ExpensTypeId = ReadProperty<int>(projects_Enums_ExpensTypeIdProperty);
                data.ExpenseDate = ReadProperty<DateTime>(expenseDateProperty);
                data.Amount = ReadProperty<decimal>(amountProperty);
                data.Invoiced = ReadProperty<bool?>(invoicedProperty);
                data.Documents_InvoiceId = ReadProperty<int?>(documents_InvoiceIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.Ordinal = ReadProperty<int?>(ordinalProperty);

                ctx.ObjectContext.AddToProjects_Project_Expenses(data);

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
                var data = new Projects_Project_Expenses();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Projects_ProjectId = ReadProperty<int?>(projects_ProjectIdProperty);
                data.MDSubjects_SubjectId = ReadProperty<int?>(mDSubjects_SubjectIdProperty);
                data.Projects_Enums_ExpensTypeId = ReadProperty<int>(projects_Enums_ExpensTypeIdProperty);
                data.ExpenseDate = ReadProperty<DateTime>(expenseDateProperty);
                data.Amount = ReadProperty<decimal>(amountProperty);
                data.Invoiced = ReadProperty<bool?>(invoicedProperty);
                data.Documents_InvoiceId = ReadProperty<int?>(documents_InvoiceIdProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.MDSubjects_EmployeeWhoChengedId = ReadProperty<int>(mDSubjects_EmployeeWhoChengedIdProperty);
                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.Ordinal = ReadProperty<int?>(ordinalProperty);

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
                var data = new Projects_Project_Expenses();

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
    public partial class cProjects_Project_Expenses_List : BusinessListBase<cProjects_Project_Expenses_List, cProjects_Project_Expenses>
    {
        public static cProjects_Project_Expenses_List NewProjects_MaterialTrackingLog_List()
        {
            return DataPortal.Create<cProjects_Project_Expenses_List>();

        }

        public static cProjects_Project_Expenses_List GetcProjects_MaterialTrackingLog_List()
        {
            return DataPortal.Fetch<cProjects_Project_Expenses_List>();
        }

        public static cProjects_Project_Expenses_List GetcProjects_MaterialTrackingLog_List_ByLinkDocumentId(int? workorderId = null, int? projectId = null)
        {
            return DataPortal.Fetch<cProjects_Project_Expenses_List>(new ProjectExpenses_Criteria(workorderId, projectId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var result = ctx.ObjectContext.Projects_Project_Expenses.Where(p => p.CompanyUsingServiceId == ((PTIdentity)Csla.ApplicationContext.User.Identity).CompanyId);

                foreach (var data in result)
                {
                    var obj = cProjects_Project_Expenses.GetProjects_MaterialTrackingLog(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(ProjectExpenses_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                IQueryable<DalEf.Projects_Project_Expenses> result = null;

                //if (criteria.WorkorderId != null)
                //{
                //    result = ctx.ObjectContext.Projects_Project_Expenses.Where(p => p.Documents_WorkOrderId == criteria.WorkorderId);
                //}
                if (criteria.ProjectId != null)
                {
                    result = ctx.ObjectContext.Projects_Project_Expenses.Where(p => p.Projects_ProjectId == criteria.ProjectId);
                }


                foreach (var data in result)
                {
                    var obj = cProjects_Project_Expenses.GetProjects_MaterialTrackingLog(data);

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
