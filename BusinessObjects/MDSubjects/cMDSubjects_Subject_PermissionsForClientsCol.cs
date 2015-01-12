using System;
using Csla;
using Csla.Data;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;
using DalEf;
using System.Collections.Generic;

namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public partial class cMDSubjects_Subject_PermissionsForClients: CoreBusinessChildClass<cMDSubjects_Subject_PermissionsForClients>
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

		private static readonly PropertyInfo< System.Int32 > mDSubjects_SubjectIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_SubjectId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 MDSubjects_SubjectId
		{
			get { return GetProperty(mDSubjects_SubjectIdProperty); }
			set { SetProperty(mDSubjects_SubjectIdProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32 > mDSubjects_EmployeeIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_EmployeeId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 MDSubjects_EmployeeId
		{
			get { return GetProperty(mDSubjects_EmployeeIdProperty); }
			set { SetProperty(mDSubjects_EmployeeIdProperty, value); }
		}

		private static readonly PropertyInfo< System.DateTime > lastActivityDateProperty = RegisterProperty<System.DateTime>(p => p.LastActivityDate, string.Empty,System.DateTime.Now);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.DateTime LastActivityDate
		{
			get { return GetProperty(lastActivityDateProperty); }
			set { SetProperty(lastActivityDateProperty, value); }
		}

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cMDSubjects_Subject_PermissionsForClients NewMDSubjects_Subject_PermissionsForClients()
		{
			return DataPortal.CreateChild<cMDSubjects_Subject_PermissionsForClients>();
		}

        public static cMDSubjects_Subject_PermissionsForClients GetcMDSubjects_Subject_PermissionsForClients(MDSubjects_Subject_PermissionsForClientsCol data)
        {
            return DataPortal.FetchChild<cMDSubjects_Subject_PermissionsForClients>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(MDSubjects_Subject_PermissionsForClientsCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
            LoadProperty<int>(mDSubjects_EmployeeIdProperty, data.MDSubjects_EmployeeId);
            LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(MDSubjects_Subject parent)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Subject_PermissionsForClientsCol();

                data.MDSubjects_Subject = parent;
                data.MDSubjects_EmployeeId = ReadProperty<int>(mDSubjects_EmployeeIdProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);

                ctx.ObjectContext.AddToMDSubjects_Subject_PermissionsForClientsCol(data);
                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "Id")
                    {
                        LoadProperty<int>(IdProperty, data.Id);
                        LoadProperty<int>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
                        LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                        LastChanged = data.LastChanged;
                    }
                };
            }
        }

        private void Child_Update()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Subject_PermissionsForClientsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);
                data.MDSubjects_EmployeeId = ReadProperty<int>(mDSubjects_EmployeeIdProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);

                data.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "LastChanged")
                        LastChanged = data.LastChanged;
                };

            }
        }

        private void Child_DeleteSelf()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Subject_PermissionsForClientsCol();

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
    public partial class cMDSubjects_Subject_PermissionsForClientsCol : BusinessListBase<cMDSubjects_Subject_PermissionsForClientsCol, cMDSubjects_Subject_PermissionsForClients>
    {
        internal static cMDSubjects_Subject_PermissionsForClientsCol NewMDSubjects_Subject_PermissionsForClientsCol()
        {
            return DataPortal.CreateChild<cMDSubjects_Subject_PermissionsForClientsCol>();
        }

        public static cMDSubjects_Subject_PermissionsForClientsCol GetcMDSubjects_Subject_PermissionsForClientsCol(IEnumerable<MDSubjects_Subject_PermissionsForClientsCol> dataSet)
        {
            var childList = new cMDSubjects_Subject_PermissionsForClientsCol();
            childList.Fetch(dataSet);
            return childList;
        }

        #region Data Access
        private void Fetch(IEnumerable<MDSubjects_Subject_PermissionsForClientsCol> dataSet)
        {

            RaiseListChangedEvents = false;

            foreach (var data in dataSet)
                this.Add(cMDSubjects_Subject_PermissionsForClients.GetcMDSubjects_Subject_PermissionsForClients(data));

            RaiseListChangedEvents = true;


        }

        #endregion //Data Access
    }
}

