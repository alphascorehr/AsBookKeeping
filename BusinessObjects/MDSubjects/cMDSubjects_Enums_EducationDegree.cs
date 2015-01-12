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

namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public partial class cMDSubjects_Enums_EducationDegree: CoreBusinessClass<cMDSubjects_Enums_EducationDegree>
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

		private static readonly PropertyInfo< System.String > labelProperty = RegisterProperty<System.String>(p => p.Label, string.Empty, (System.String)null);
		public System.String Label
		{
			get { return GetProperty(labelProperty); }
			set { SetProperty(labelProperty, value.Trim()); }
		}

		private static readonly PropertyInfo< bool? > immutableProperty = RegisterProperty<bool?>(p => p.Immutable, string.Empty,(bool?)null);
		public bool? Immutable
		{
			get { return GetProperty(immutableProperty); }
			set { SetProperty(immutableProperty, value); }
		}

		private static readonly PropertyInfo< bool? > inactiveProperty = RegisterProperty<bool?>(p => p.Inactive, string.Empty,(bool?)null);
		public bool? Inactive
		{
			get { return GetProperty(inactiveProperty); }
			set { SetProperty(inactiveProperty, value); }
		}

		private static readonly PropertyInfo< System.Int32? > enumNumberProperty = RegisterProperty<System.Int32?>(p => p.EnumNumber, string.Empty,(System.Int32?)null);
		public System.Int32? EnumNumber
		{
			get { return GetProperty(enumNumberProperty); }
			set { SetProperty(enumNumberProperty, value); }
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

        public static cMDSubjects_Enums_EducationDegree NewMDSubjects_Enums_EducationDegree()
        {
            return DataPortal.Create<cMDSubjects_Enums_EducationDegree>();

        }

        public static cMDSubjects_Enums_EducationDegree GetMDSubjects_Enums_EducationDegree(int uniqueId)
        {
            return DataPortal.Fetch<cMDSubjects_Enums_EducationDegree>(new SingleCriteria<cMDSubjects_Enums_EducationDegree, int>(uniqueId));
        }

        internal static cMDSubjects_Enums_EducationDegree GetMDSubjects_Enums_EducationDegree(MDSubjects_Enums_EducationDegree data)
        {
            return DataPortal.Fetch<cMDSubjects_Enums_EducationDegree>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDSubjects_Enums_EducationDegree, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Enums_EducationDegree.First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<string>(labelProperty, data.Label);
                LoadProperty<bool?>(immutableProperty, data.Immutable);
                LoadProperty<int?>(enumNumberProperty, data.EnumNumber);
                LoadProperty<bool?>(inactiveProperty, data.Inactive);
                LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LastChanged = data.LastChanged;

                BusinessRules.CheckRules();

            }
        }

        private void DataPortal_Fetch(MDSubjects_Enums_EducationDegree data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));
            LoadProperty<string>(nameProperty, data.Name);
            LoadProperty<string>(labelProperty, data.Label);
            LoadProperty<bool?>(immutableProperty, data.Immutable);
            LoadProperty<int?>(enumNumberProperty, data.EnumNumber);
            LoadProperty<bool?>(inactiveProperty, data.Inactive);
            LoadProperty<int?>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
            MarkAsChild();
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Enums_EducationDegree();

                data.Name = ReadProperty<string>(nameProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.Immutable = ReadProperty<bool?>(immutableProperty);
                data.EnumNumber = ReadProperty<int?>(enumNumberProperty);
                data.Inactive = ReadProperty<bool?>(inactiveProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.AddToMDSubjects_Enums_EducationDegree(data);
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
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Enums_EducationDegree();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);
                data.Name = ReadProperty<string>(nameProperty);
                data.Label = ReadProperty<string>(labelProperty);
                data.Immutable = ReadProperty<bool?>(immutableProperty);
                data.EnumNumber = ReadProperty<int?>(enumNumberProperty);
                data.Inactive = ReadProperty<bool?>(inactiveProperty);
                data.CompanyUsingServiceId = ReadProperty<int?>(companyUsingServiceIdProperty);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}

    public partial class cMDSubjects_Enums_EducationDegree_List : BusinessListBase<cMDSubjects_Enums_EducationDegree_List, cMDSubjects_Enums_EducationDegree>
    {
        public static cMDSubjects_Enums_EducationDegree_List GetcMDSubjects_Enums_EducationDegree_List()
        {
            return DataPortal.Fetch<cMDSubjects_Enums_EducationDegree_List>();
        }

        public static cMDSubjects_Enums_EducationDegree_List GetcMDSubjects_Enums_EducationDegree_List(int companyId, int includeInactiveId)
        {
            return DataPortal.Fetch<cMDSubjects_Enums_EducationDegree_List>(new ActiveEnums_Criteria(companyId, includeInactiveId));
        }

        private void DataPortal_Fetch()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var result = ctx.ObjectContext.MDSubjects_Enums_EducationDegree;

                foreach (var data in result)
                {
                    var obj = cMDSubjects_Enums_EducationDegree.GetMDSubjects_Enums_EducationDegree(data);

                    this.Add(obj);
                }
            }
        }

        private void DataPortal_Fetch(ActiveEnums_Criteria criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var result = ctx.ObjectContext.MDSubjects_Enums_EducationDegree.Where(p => (p.CompanyUsingServiceId == criteria.CompanyId || (p.CompanyUsingServiceId ?? 0) == 0) && ((p.Inactive ?? false) == false || p.Id == criteria.IncludeInactiveId));

                foreach (var data in result)
                {
                    var obj = cMDSubjects_Enums_EducationDegree.GetMDSubjects_Enums_EducationDegree(data);

                    this.Add(obj);
                }
            }
        }
    }
}
