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
	public partial class cMDSubjects_Subject_Contacts: CoreBusinessChildClass<cMDSubjects_Subject_Contacts>
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

        private static readonly PropertyInfo<System.Int32?> contactSubjectIdProperty = RegisterProperty<System.Int32?>(p => p.ContactSubjectId, string.Empty, (System.Int32?)null);
        public System.Int32? ContactSubjectId
        {
            get { return GetProperty(contactSubjectIdProperty); }
            set { SetProperty(contactSubjectIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> contactSubjecNameProperty = RegisterProperty<System.String>(p => p.ContactSubjecName, string.Empty, (System.String)null);
        public System.String ContactSubjecName
        {
            get { return GetProperty(contactSubjecNameProperty); }
            set { SetProperty(contactSubjecNameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> contactSubjectFunctionIdProperty = RegisterProperty<System.Int32?>(p => p.ContactSubjectFunctionId, string.Empty, (System.Int32?)null);
        public System.Int32? ContactSubjectFunctionId
        {
            get { return GetProperty(contactSubjectFunctionIdProperty); }
            set { SetProperty(contactSubjectFunctionIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> businessPhoneProperty = RegisterProperty<System.String>(p => p.BusinessPhone, string.Empty, (System.String)null);
        public System.String BusinessPhone
        {
            get { return GetProperty(businessPhoneProperty); }
            set { SetProperty(businessPhoneProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> mobileProperty = RegisterProperty<System.String>(p => p.Mobile, string.Empty, (System.String)null);
        public System.String Mobile
        {
            get { return GetProperty(mobileProperty); }
            set { SetProperty(mobileProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> faxProperty = RegisterProperty<System.String>(p => p.Fax, string.Empty, (System.String)null);
        public System.String Fax
        {
            get { return GetProperty(faxProperty); }
            set { SetProperty(faxProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> emailProperty = RegisterProperty<System.String>(p => p.Email, string.Empty, (System.String)null);
        public System.String Email
        {
            get { return GetProperty(emailProperty); }
            set { SetProperty(emailProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<bool> inactiveProperty = RegisterProperty<bool>(p => p.Inactive, string.Empty);
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public bool Inactive
        {
            get { return GetProperty(inactiveProperty); }
            set { SetProperty(inactiveProperty, value); }
        }

        private static readonly PropertyInfo<System.String> descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
        public System.String Description
        {
            get { return GetProperty(descriptionProperty); }
            set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
        }

		/// <summary>
		/// Used for optimistic concurrency.
		/// </summary>
		[NotUndoable]
		internal System.Byte[] LastChanged = new System.Byte[8];

		internal static cMDSubjects_Subject_Contacts NewMDSubjects_Subject_Contacts()
		{
			return DataPortal.CreateChild<cMDSubjects_Subject_Contacts>();
		}

        public static cMDSubjects_Subject_Contacts GetcMDSubjects_Subject_Contacts(MDSubjects_Subject_ContactsCol data)
        {
            return DataPortal.FetchChild<cMDSubjects_Subject_Contacts>(data);
        }

        #region Data Access
        [RunLocal]
        protected override void Child_Create()
        {
            BusinessRules.CheckRules();
        }

        private void Child_Fetch(MDSubjects_Subject_ContactsCol data)
        {

            LoadProperty<int>(IdProperty, data.Id);
            LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

            LoadProperty<int>(mDSubjects_SubjectIdProperty, data.MDSubjects_SubjectId);
            LoadProperty<int?>(contactSubjectIdProperty, data.ContactSubjectId);
            LoadProperty<string>(contactSubjecNameProperty, data.ContactSubjecName);
            LoadProperty<int?>(contactSubjectFunctionIdProperty, data.ContactSubjectFunctionId);
            LoadProperty<string>(businessPhoneProperty, data.BusinessPhone);
            LoadProperty<string>(mobileProperty, data.Mobile);
            LoadProperty<string>(faxProperty, data.Fax);
            LoadProperty<string>(emailProperty, data.Email);
            LoadProperty<bool>(inactiveProperty, data.Inactive);
            LoadProperty<string>(descriptionProperty, data.Description);
            LastChanged = data.LastChanged;

            BusinessRules.CheckRules();
        }

        private void Child_Insert(MDSubjects_Subject parent)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Subject_ContactsCol();

                data.MDSubjects_Subject = parent;
                data.ContactSubjectId = ReadProperty<int?>(contactSubjectIdProperty);
                data.ContactSubjecName = ReadProperty<string>(contactSubjecNameProperty);
                data.ContactSubjectFunctionId = ReadProperty<int?>(contactSubjectFunctionIdProperty);
                data.BusinessPhone = ReadProperty<string>(businessPhoneProperty);
                data.Mobile = ReadProperty<string>(mobileProperty);
                data.Fax = ReadProperty<string>(faxProperty);
                data.Email = ReadProperty<string>(emailProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.Description = ReadProperty<string>(descriptionProperty);
                    
                ctx.ObjectContext.AddToMDSubjects_Subject_ContactsCol(data);
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
                var data = new MDSubjects_Subject_ContactsCol();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;
                //data.SubjectId = parent.Id;
      
                ctx.ObjectContext.Attach(data);
                data.ContactSubjectId = ReadProperty<int?>(contactSubjectIdProperty);
                data.ContactSubjecName = ReadProperty<string>(contactSubjecNameProperty);
                data.ContactSubjectFunctionId = ReadProperty<int?>(contactSubjectFunctionIdProperty);
                data.BusinessPhone = ReadProperty<string>(businessPhoneProperty);
                data.Mobile = ReadProperty<string>(mobileProperty);
                data.Fax = ReadProperty<string>(faxProperty);
                data.Email = ReadProperty<string>(emailProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.Description = ReadProperty<string>(descriptionProperty);

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
                var data = new MDSubjects_Subject_ContactsCol();

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
		public partial class cMDSubjects_Subject_ContactsCol : BusinessListBase<cMDSubjects_Subject_ContactsCol,cMDSubjects_Subject_Contacts>
		{
			internal static cMDSubjects_Subject_ContactsCol NewMDSubjects_Subject_ContactsCol()
			{
				return DataPortal.CreateChild<cMDSubjects_Subject_ContactsCol>();
			}

            public static cMDSubjects_Subject_ContactsCol GetcMDSubjects_Subject_ContactsCol(IEnumerable<MDSubjects_Subject_ContactsCol> dataSet)
            {
                var childList = new cMDSubjects_Subject_ContactsCol();
                childList.Fetch(dataSet);
                return childList;
            }

            #region Data Access
            private void Fetch(IEnumerable<MDSubjects_Subject_ContactsCol> dataSet)
            {

                RaiseListChangedEvents = false;

                foreach (var data in dataSet)
                    this.Add(cMDSubjects_Subject_Contacts.GetcMDSubjects_Subject_Contacts(data));

                RaiseListChangedEvents = true;

              
            }

            #endregion //Data Access

        
		}
}
