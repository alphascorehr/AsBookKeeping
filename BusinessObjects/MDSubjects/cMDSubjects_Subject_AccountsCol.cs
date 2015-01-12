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
	public partial class cMDSubjects_Subject_Accounts: CoreBusinessChildClass<cMDSubjects_Subject_Accounts>
	{
		public static readonly PropertyInfo< System.Int32 > IdProperty = RegisterProperty< System.Int32 >(p => p.Id, string.Empty);
#if !SILVERLIGHT
		[System.ComponentModel.DataObjectField(true, true)]
#endif
		public System.Int32 Id
		{
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}

        private static readonly PropertyInfo<System.Int32> mDSubjects_SubjectIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_SubjectId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 MDSubjects_SubjectId
        {
            get { return GetProperty(mDSubjects_SubjectIdProperty); }
            set { SetProperty(mDSubjects_SubjectIdProperty, value); }
        }

        private static readonly PropertyInfo<System.Int32> mDSubjects_Enums_BankIdProperty = RegisterProperty<System.Int32>(p => p.MDSubjects_Enums_BankId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 MDSubjects_Enums_BankId
        {
            get { return GetProperty(mDSubjects_Enums_BankIdProperty); }
            set { SetProperty(mDSubjects_Enums_BankIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> accountNumberProperty = RegisterProperty<System.String>(p => p.AccountNumber, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String AccountNumber
        {
            get { return GetProperty(accountNumberProperty); }
            set { SetProperty(accountNumberProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32> mDGeneral_Enums_CurrencyIdProperty = RegisterProperty<System.Int32>(p => p.MDGeneral_Enums_CurrencyId, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.Int32 MDGeneral_Enums_CurrencyId
        {
            get { return GetProperty(mDGeneral_Enums_CurrencyIdProperty); }
            set { SetProperty(mDGeneral_Enums_CurrencyIdProperty, value); }
        }

        private static readonly PropertyInfo<bool> inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Inactive
        {
            get { return GetProperty(inactiveProperty); }
            set { SetProperty(inactiveProperty, value); }
        }

        private static readonly PropertyInfo<System.DateTime?> lastActivityDateProperty = RegisterProperty<System.DateTime?>(p => p.LastActivityDate, string.Empty, (System.DateTime?)null);
        public System.DateTime? LastActivityDate
        {
            get { return GetProperty(lastActivityDateProperty); }
            set { SetProperty(lastActivityDateProperty, value); }
        }

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		public static cMDSubjects_Subject_Accounts NewMDSubjects_Subject_Accounts()
		{
			return DataPortal.CreateChild<cMDSubjects_Subject_Accounts>();
		}

        public static cMDSubjects_Subject_Accounts GetcMDSubjects_Subject_Accounts(MDSubjects_Subject_AccountsCol data)
        {
            return DataPortal.FetchChild<cMDSubjects_Subject_Accounts>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(MDSubjects_Subject_AccountsCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
            LoadProperty<int>(mDSubjects_Enums_BankIdProperty, data.MDSubjects_Enums_BankId);
            LoadProperty<string>(accountNumberProperty, data.AccountNumber);
            LoadProperty<int>(mDGeneral_Enums_CurrencyIdProperty, data.MDGeneral_Enums_CurrencyId);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LoadProperty<DateTime?>(lastActivityDateProperty, data.LastActivityDate);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(MDSubjects_Subject parent)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Subject_AccountsCol();

                data.MDSubjects_Subject = parent;
                data.MDSubjects_Enums_BankId = ReadProperty<int>(mDSubjects_Enums_BankIdProperty);
                data.AccountNumber = ReadProperty<string>(accountNumberProperty);
                data.MDGeneral_Enums_CurrencyId = ReadProperty<int>(mDGeneral_Enums_CurrencyIdProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime?>(lastActivityDateProperty);

                ctx.ObjectContext.AddToMDSubjects_Subject_AccountsCol(data);
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
                var data = new MDSubjects_Subject_AccountsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;

                ctx.ObjectContext.Attach(data);
                data.MDSubjects_Enums_BankId = ReadProperty<int>(mDSubjects_Enums_BankIdProperty);
                data.AccountNumber = ReadProperty<string>(accountNumberProperty);
                data.MDGeneral_Enums_CurrencyId = ReadProperty<int>(mDGeneral_Enums_CurrencyIdProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime?>(lastActivityDateProperty);

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
                var data = new MDSubjects_Subject_AccountsCol();

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
		public partial class cMDSubjects_Subject_AccountsCol : BusinessListBase<cMDSubjects_Subject_AccountsCol,cMDSubjects_Subject_Accounts>
		{
			internal static cMDSubjects_Subject_AccountsCol NewMDSubjects_Subject_AccountsCol()
			{
				return DataPortal.CreateChild<cMDSubjects_Subject_AccountsCol>();
			}

            public static cMDSubjects_Subject_AccountsCol GetcMDSubjects_Subject_AccountsCol(IEnumerable<MDSubjects_Subject_AccountsCol> dataSet)
            {
                var childList = new cMDSubjects_Subject_AccountsCol();
                childList.Fetch(dataSet);
                return childList;
            }

            #region Data Access
            private void Fetch(IEnumerable<MDSubjects_Subject_AccountsCol> dataSet)
            {

                RaiseListChangedEvents = false;

                foreach (var data in dataSet)
                    this.Add(cMDSubjects_Subject_Accounts.GetcMDSubjects_Subject_Accounts(data));

                RaiseListChangedEvents = true;


            }

            #endregion //Data Access
		}
}
