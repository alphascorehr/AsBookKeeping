using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using System.Collections.Generic;

namespace BusinessObjects.Projects
{
	[Serializable]
	public partial class cProjects_Project_TeamMemebers: CoreBusinessChildClass<cProjects_Project_TeamMemebers>
	{
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			internal set { SetProperty(IdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > projects_ProjectIdProperty = RegisterProperty<System.Int32>(p => p.Projects_ProjectId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 Projects_ProjectId
		{
			get { return GetProperty(projects_ProjectIdProperty); }
			set { SetProperty(projects_ProjectIdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32> mDSubjects_SubjectIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_SubjectId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 MDSubjects_SubjectId
		{
            get { return GetProperty(mDSubjects_SubjectIdProperty); }
            set { SetProperty(mDSubjects_SubjectIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > mDEntities_ServiceIdProperty = RegisterProperty<System.Int32?>(p => p.MDEntities_ServiceId, string.Empty,(System.Int32?)null);
		public System.Int32? MDEntities_ServiceId
		{
			get { return GetProperty(mDEntities_ServiceIdProperty); }
			set { SetProperty(mDEntities_ServiceIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > contractorRebillRatePerHourProperty = RegisterProperty<System.Decimal?>(p => p.ContractorRebillRatePerHour, string.Empty, (System.Decimal?)null);
		public System.Decimal? ContractorRebillRatePerHour
		{
			get { return GetProperty(contractorRebillRatePerHourProperty); }
			set { SetProperty(contractorRebillRatePerHourProperty, value); }
		}

		private static readonly PropertyInfo< System.Decimal? > contractoRebillFlatAmountProperty = RegisterProperty<System.Decimal?>(p => p.ContractoRebillFlatAmount, string.Empty, (System.Decimal?)null);
		public System.Decimal? ContractoRebillFlatAmount
		{
			get { return GetProperty(contractoRebillFlatAmountProperty); }
			set { SetProperty(contractoRebillFlatAmountProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cProjects_Project_TeamMemebers NewProjects_Project_TeamMemebers()
		{
			return DataPortal.CreateChild<cProjects_Project_TeamMemebers>();
		}

        public static cProjects_Project_TeamMemebers GetcProjects_Project_TeamMemebers(Projects_Project_TeamMemebersCol data)
        {
            return DataPortal.FetchChild<cProjects_Project_TeamMemebers>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(Projects_Project_TeamMemebersCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(projects_ProjectIdProperty, data.Projects_ProjectId);
            LoadProperty<int>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
            LoadProperty<int?>(mDEntities_ServiceIdProperty, data.MDEntities_ServiceId);
            LoadProperty<decimal?>(contractorRebillRatePerHourProperty, data.ContractorRebillRatePerHour);
            LoadProperty<decimal?>(contractoRebillFlatAmountProperty, data.ContractoRebillFlatAmount);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(Projects_Project parent)
        {
            using (var ctx = ObjectContextManager<ProjectsEntities>.GetManager("ProjectsEntities"))
            {
                var data = new Projects_Project_TeamMemebersCol();

                data.Projects_Project = parent;
                data.Projects_ProjectId = ReadProperty<int>(projects_ProjectIdProperty);
                data.MDSubjects_SubjectId = ReadProperty<int>(mDSubjects_SubjectIdProperty);
                data.MDEntities_ServiceId = ReadProperty<int?>(mDEntities_ServiceIdProperty);
                data.ContractorRebillRatePerHour = ReadProperty<decimal?>(contractorRebillRatePerHourProperty);
                data.ContractoRebillFlatAmount = ReadProperty<decimal?>(contractoRebillFlatAmountProperty);

                ctx.ObjectContext.AddToProjects_Project_TeamMemebersCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(projects_ProjectIdProperty, data.Projects_ProjectId);
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
                var data = new Projects_Project_TeamMemebersCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);
                data.Projects_ProjectId = ReadProperty<int>(projects_ProjectIdProperty);
                data.MDSubjects_SubjectId = ReadProperty<int>(mDSubjects_SubjectIdProperty);
                data.MDEntities_ServiceId = ReadProperty<int?>(mDEntities_ServiceIdProperty);
                data.ContractorRebillRatePerHour = ReadProperty<decimal?>(contractorRebillRatePerHourProperty);
                data.ContractoRebillFlatAmount = ReadProperty<decimal?>(contractoRebillFlatAmountProperty);

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
                var data = new Projects_Project_TeamMemebersCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);

                ctx.ObjectContext.DeleteObject(data);
            }
        }
        #endregion

        public void MarkChild()
        {
            this.MarkAsChild();
        }

	}
		[Serializable]
		public partial class cProjects_Project_TeamMemebersCol : BusinessListBase<cProjects_Project_TeamMemebersCol,cProjects_Project_TeamMemebers>
		{
			internal static cProjects_Project_TeamMemebersCol NewProjects_Project_TeamMemebersCol()
			{
				return DataPortal.CreateChild<cProjects_Project_TeamMemebersCol>();
			}

            public static cProjects_Project_TeamMemebersCol GetcProjects_Project_TeamMemebersCol(IEnumerable<Projects_Project_TeamMemebersCol> dataSet)
            {
                var childList = new cProjects_Project_TeamMemebersCol();
                childList.Fetch(dataSet);
                return childList;
            }

            #region Data Access
            private void Fetch(IEnumerable<Projects_Project_TeamMemebersCol> dataSet)
            {

                RaiseListChangedEvents = false;

                foreach (var data in dataSet)
                    this.Add(cProjects_Project_TeamMemebers.GetcProjects_Project_TeamMemebers(data));

                RaiseListChangedEvents = true;


            }

            #endregion //Data Access
		}
}
