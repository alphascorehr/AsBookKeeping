using System;
using Csla;
using Csla.Data;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;
using BusinessObjects.CoreBusinessClasses;

namespace BusinessObjects.MDEntities
{
	[Serializable]
	public abstract class cMDEntities_Entity<T> : CoreBusinessClass<T> where T : cMDEntities_Entity<T>
	{
		#region Business Methods
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}

        protected static readonly PropertyInfo<System.Int32> companyUsingServiceIdProperty = RegisterProperty<System.Int32>(p => p.CompanyUsingServiceId, string.Empty);
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.Int32 CompanyUsingServiceId
		{
			get { return GetProperty(companyUsingServiceIdProperty); }
			set { SetProperty(companyUsingServiceIdProperty, value); }
		}

        protected static readonly PropertyInfo<Guid> GUIDProperty = RegisterProperty<Guid>(p => p.GUID, string.Empty, Guid.NewGuid());
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public Guid GUID
        {
            get { return GetProperty(GUIDProperty); }
            set { SetProperty(GUIDProperty, value); }
        }

        protected static readonly PropertyInfo<short> entityTypeProperty = RegisterProperty<short>(p => p.EntityType, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public short EntityType
        {
            get { return GetProperty(entityTypeProperty); }
            set { SetProperty(entityTypeProperty, value); }
        }

        protected static readonly PropertyInfo<System.String> labelProperty = RegisterProperty<System.String>(p => p.Label, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(20, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Label
		{
			get { return GetProperty(labelProperty); }
            set { SetProperty(labelProperty, (value ?? "").Trim()); }
		}

        protected static readonly PropertyInfo<System.String> nameProperty = RegisterProperty<System.String>(p => p.Name, string.Empty);
		[System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
		public System.String Name
		{
			get { return GetProperty(nameProperty); }
            set { SetProperty(nameProperty, (value ?? "").Trim()); }
		}

        protected static readonly PropertyInfo<System.Int32?> mDEntities_Enums_EntityGroupIdProperty = RegisterProperty<System.Int32?>(p => p.MDEntities_Enums_EntityGroupId, string.Empty, (System.Int32?)null);
        public System.Int32? MDEntities_Enums_EntityGroupId
        {
            get { return GetProperty(mDEntities_Enums_EntityGroupIdProperty); }
            set { SetProperty(mDEntities_Enums_EntityGroupIdProperty, value); }
        }

        protected static readonly PropertyInfo<System.String> tagProperty = RegisterProperty<System.String>(p => p.Tag, string.Empty, (System.String)null);
        public System.String Tag
        {
            get { return GetProperty(tagProperty); }
            set { SetProperty(tagProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<System.String> descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
        public System.String Description
        {
            get { return GetProperty(descriptionProperty); }
            set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
        }

        protected static readonly PropertyInfo<bool> inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Inactive
        {
            get { return GetProperty(inactiveProperty); }
            set { SetProperty(inactiveProperty, value); }
        }

        protected static readonly PropertyInfo<System.DateTime> lastActivityDateProperty = RegisterProperty<System.DateTime>(p => p.LastActivityDate, string.Empty, System.DateTime.Now);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.DateTime LastActivityDate
        {
            get { return GetProperty(lastActivityDateProperty); }
            set { SetProperty(lastActivityDateProperty, value); }
        }

        protected static readonly PropertyInfo<System.Int32> employeeWhoLastChanedItUserIdProperty = RegisterProperty<System.Int32>(p => p.EmployeeWhoLastChanedItUserId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 EmployeeWhoLastChanedItUserId
        {
            get { return GetProperty(employeeWhoLastChanedItUserIdProperty); }
            set { SetProperty(employeeWhoLastChanedItUserIdProperty, value); }
        }

        protected static readonly PropertyInfo<System.Int32> taxRateIdProperty = RegisterProperty<System.Int32>(p => p.TaxRateId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 TaxRateId
        {
            get { return GetProperty(taxRateIdProperty); }
            set { SetProperty(taxRateIdProperty, value); }
        }

        protected static readonly PropertyInfo<System.Int32> unitIdProperty = RegisterProperty<System.Int32>(p => p.UnitId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 UnitId
        {
            get { return GetProperty(unitIdProperty); }
            set { SetProperty(unitIdProperty, value); }
        }

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		#endregion
	}
}
