using System;
using Csla;
using Csla.Data;
using DalEf;
using Csla.Serialization;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.Properties;
using System.Linq;

namespace BusinessObjects.MDSubjects
{
	[Serializable]
	public partial class cMDSubjects_Person: cMDSubjects_Subject<cMDSubjects_Person>
	{
		#region Business Methods
        private static readonly PropertyInfo<System.String> firstNameProperty = RegisterProperty<System.String>(p => p.FirstName, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String FirstName
        {
            get { return GetProperty(firstNameProperty); }
            set { SetProperty(firstNameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> middleNameProperty = RegisterProperty<System.String>(p => p.MiddleName, string.Empty, (System.String)null);
        public System.String MiddleName
        {
            get { return GetProperty(middleNameProperty); }
            set { SetProperty(middleNameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> surNameProperty = RegisterProperty<System.String>(p => p.SurName, string.Empty);
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessageResourceName = "ErrorMessageMaxLength", ErrorMessageResourceType = typeof(Resources))]
        [Required(ErrorMessageResourceName = "ErrorMessageRequired", ErrorMessageResourceType = typeof(Resources))]
        public System.String SurName
        {
            get { return GetProperty(surNameProperty); }
            set { SetProperty(surNameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> genderProperty = RegisterProperty<System.String>(p => p.Gender, string.Empty, (System.String)null);
        public System.String Gender
        {
            get { return GetProperty(genderProperty); }
            set { SetProperty(genderProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.Int32?> mDSubjects_Subject_EmployerIdProperty = RegisterProperty<System.Int32?>(p => p.MDSubjects_Subject_EmployerId, string.Empty, (System.Int32?)null);
        public System.Int32? MDSubjects_Subject_EmployerId
        {
            get { return GetProperty(mDSubjects_Subject_EmployerIdProperty); }
            set { SetProperty(mDSubjects_Subject_EmployerIdProperty, value); }
        }

        private static readonly PropertyInfo<System.String> employerNameProperty = RegisterProperty<System.String>(p => p.EmployerName, string.Empty, (System.String)null);
        public System.String EmployerName
        {
            get { return GetProperty(employerNameProperty); }
            set { SetProperty(employerNameProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> cRO_JMBGProperty = RegisterProperty<System.String>(p => p.CRO_JMBG, string.Empty, (System.String)null);
        public System.String CRO_JMBG
        {
            get { return GetProperty(cRO_JMBGProperty); }
            set { SetProperty(cRO_JMBGProperty, (value ?? "").Trim()); }
        }

        private static readonly PropertyInfo<System.String> homePhoneProperty = RegisterProperty<System.String>(p => p.HomePhone, string.Empty, (System.String)null);
        public System.String HomePhone
        {
            get { return GetProperty(homePhoneProperty); }
            set { SetProperty(homePhoneProperty, (value ?? "").Trim()); }
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

        private static readonly PropertyInfo<System.String> descriptionProperty = RegisterProperty<System.String>(p => p.Description, string.Empty, (System.String)null);
        public System.String Description
        {
            get { return GetProperty(descriptionProperty); }
            set { SetProperty(descriptionProperty, (value ?? "").Trim()); }
        }

		#endregion

        #region Factory Methods

        public static cMDSubjects_Person NewMDSubjects_Person()
        {
            return DataPortal.Create<cMDSubjects_Person>();

        }

        public static cMDSubjects_Person GetMDSubjects_Person(int uniqueId)
        {
            return DataPortal.Fetch<cMDSubjects_Person>(new SingleCriteria<cMDSubjects_Person, int>(uniqueId));
        }

        internal static cMDSubjects_Person GetMDSubjects_Person(MDSubjects_Person data)
        {
            return DataPortal.Fetch<cMDSubjects_Person>(data);
        }

        #endregion

        #region Data Access
        [RunLocal]
        protected override void DataPortal_Create()
        {
            BusinessRules.CheckRules();
        }

        private void DataPortal_Fetch(SingleCriteria<cMDSubjects_Person, int> criteria)
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Person>().First(p => p.Id == criteria.Value);

                LoadProperty<int>(IdProperty, data.Id);
                LoadProperty<byte[]>(EntityKeyDataProperty, Serialize(data.EntityKey));

                LoadProperty<int>(companyUsingServiceIdProperty, data.CompanyUsingServiceId);
                LoadProperty<Guid>(GUIDProperty, data.GUID);
                LoadProperty<short>(subjectTypeProperty, data.SubjectType);
                LoadProperty<string>(nameProperty, data.Name);
                LoadProperty<string>(longNameProperty, data.LongName);
                LoadProperty<bool>(residentProperty, data.Resident);
                LoadProperty<bool?>(isCustomerProperty, data.IsCustomer);
                LoadProperty<bool?>(isContractorProperty, data.IsContractor);
                LoadProperty<bool>(inactiveProperty, data.Inactive);
                LoadProperty<DateTime>(lastActivityDateProperty, data.LastActivityDate);
                LoadProperty<int>(employeeWhoLastChanedItIdProperty, data.EmployeeWhoLastChanedItId);
                LoadProperty<int?>(homeAddress_PlaceIdProperty, data.HomeAddress_PlaceId);
                LoadProperty<string>(homeAddress_StreetProperty, data.HomeAddress_Street);
                LoadProperty<string>(homeAddress_NumberProperty, data.HomeAddress_Number);
                LoadProperty<string>(homeAddress_DescriptionProperty, data.HomeAddress_Description);
                LoadProperty<string>(oIBProperty, data.OIB);
                                       
                LoadProperty<string>(firstNameProperty, data.FirstName);
                LoadProperty<string>(middleNameProperty, data.MiddleName);
                LoadProperty<string>(surNameProperty, data.SurName);
                LoadProperty<string>(genderProperty, data.Gender);
                LoadProperty<int?>(mDSubjects_Subject_EmployerIdProperty, data.MDSubjects_Subject_EmployerId);
                LoadProperty<string>(employerNameProperty, data.EmployerName);
                LoadProperty<string>(cRO_JMBGProperty, data.CRO_JMBG);
                LoadProperty<string>(homePhoneProperty, data.HomePhone);
                LoadProperty<string>(businessPhoneProperty, data.BusinessPhone);
                LoadProperty<string>(mobileProperty, data.Mobile);
                LoadProperty<string>(faxProperty, data.Fax);
                LoadProperty<string>(emailProperty, data.Email);
                LoadProperty<string>(descriptionProperty, data.Description);

                LastChanged = data.LastChanged;

                LoadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty, cMDSubjects_Subject_AccountsCol.GetcMDSubjects_Subject_AccountsCol(data.MDSubjects_Subject_AccountsCol));
                LoadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty, cMDSubjects_Subject_ContactsCol.GetcMDSubjects_Subject_ContactsCol(data.MDSubjects_Subject_ContactsCol));
                LoadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty, cMDSubjects_Subject_PermissionsForClientsCol.GetcMDSubjects_Subject_PermissionsForClientsCol(data.MDSubjects_Subject_PermissionsForClientsCol));

                BusinessRules.CheckRules();

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Person();

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.SubjectType = (short)Common.SubjectType.Person;
                data.Name = ReadProperty<string>(nameProperty);
                data.LongName = ReadProperty<string>(longNameProperty);
                data.Resident = ReadProperty<bool>(residentProperty);
                data.IsCustomer = ReadProperty<bool?>(isCustomerProperty);
                data.IsContractor = ReadProperty<bool?>(isContractorProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate =  ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItId = ReadProperty<int>(employeeWhoLastChanedItIdProperty);
                data.HomeAddress_PlaceId = ReadProperty<int?>(homeAddress_PlaceIdProperty);
                data.HomeAddress_Street = ReadProperty<string>(homeAddress_StreetProperty);
                data.HomeAddress_Number = ReadProperty<string>(homeAddress_NumberProperty);
                data.HomeAddress_Description = ReadProperty<string>(homeAddress_DescriptionProperty);
                data.OIB = ReadProperty<string>(oIBProperty);

                data.FirstName = ReadProperty<string>(firstNameProperty);
                data.MiddleName = ReadProperty<string>(middleNameProperty);
                data.SurName = ReadProperty<string>(surNameProperty);
                data.Gender = ReadProperty<string>(genderProperty);
                data.MDSubjects_Subject_EmployerId = ReadProperty<int?>(mDSubjects_Subject_EmployerIdProperty);
                data.EmployerName = ReadProperty<string>(employerNameProperty);
                data.CRO_JMBG = ReadProperty<string>(cRO_JMBGProperty);
                data.HomePhone = ReadProperty<string>(homePhoneProperty);
                data.BusinessPhone = ReadProperty<string>(businessPhoneProperty);
                data.Mobile = ReadProperty<string>(mobileProperty);
                data.Fax = ReadProperty<string>(faxProperty);
                data.Email = ReadProperty<string>(emailProperty);
                data.Description = ReadProperty<string>(descriptionProperty);

                ctx.ObjectContext.AddToMDSubjects_Subject(data);

                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty), data);

                ctx.ObjectContext.SaveChanges();


                //Get New id
                int newId = data.Id;
                //Load New Id into object
                LoadProperty(IdProperty, newId);
                //Load New EntityKey into Object
                LoadProperty(EntityKeyDataProperty, Serialize(data.EntityKey));

            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var data = new MDSubjects_Person();

                data.Id = ReadProperty<int>(IdProperty);
                data.EntityKey = Deserialize(ReadProperty(EntityKeyDataProperty)) as System.Data.EntityKey;

                ctx.ObjectContext.Attach(data);

                data.CompanyUsingServiceId = ReadProperty<int>(companyUsingServiceIdProperty);
                data.GUID = ReadProperty<Guid>(GUIDProperty);
                data.SubjectType = ReadProperty<short>(subjectTypeProperty);
                data.Name = ReadProperty<string>(nameProperty);
                data.LongName = ReadProperty<string>(longNameProperty);
                data.Resident = ReadProperty<bool>(residentProperty);
                data.IsCustomer = ReadProperty<bool?>(isCustomerProperty);
                data.IsContractor = ReadProperty<bool?>(isContractorProperty);
                data.Inactive = ReadProperty<bool>(inactiveProperty);
                data.LastActivityDate = ReadProperty<DateTime>(lastActivityDateProperty);
                data.EmployeeWhoLastChanedItId = ReadProperty<int>(employeeWhoLastChanedItIdProperty);
                data.HomeAddress_PlaceId = ReadProperty<int?>(homeAddress_PlaceIdProperty);
                data.HomeAddress_Street = ReadProperty<string>(homeAddress_StreetProperty);
                data.HomeAddress_Number = ReadProperty<string>(homeAddress_NumberProperty);
                data.HomeAddress_Description = ReadProperty<string>(homeAddress_DescriptionProperty);
                data.OIB = ReadProperty<string>(oIBProperty);

                data.FirstName = ReadProperty<string>(firstNameProperty);
                data.MiddleName = ReadProperty<string>(middleNameProperty);
                data.SurName = ReadProperty<string>(surNameProperty);
                data.Gender = ReadProperty<string>(genderProperty);
                data.MDSubjects_Subject_EmployerId = ReadProperty<int?>(mDSubjects_Subject_EmployerIdProperty);
                data.EmployerName = ReadProperty<string>(employerNameProperty);
                data.CRO_JMBG = ReadProperty<string>(cRO_JMBGProperty);
                data.HomePhone = ReadProperty<string>(homePhoneProperty);
                data.BusinessPhone = ReadProperty<string>(businessPhoneProperty);
                data.Mobile = ReadProperty<string>(mobileProperty);
                data.Fax = ReadProperty<string>(faxProperty);
                data.Email = ReadProperty<string>(emailProperty);
                data.Description = ReadProperty<string>(descriptionProperty);

                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_AccountsCol>(MDSubjects_Subject_AccountsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_ContactsCol>(MDSubjects_Subject_ContactsColProperty), data);
                DataPortal.UpdateChild(ReadProperty<cMDSubjects_Subject_PermissionsForClientsCol>(MDSubjects_Subject_PermissionsForClientsColProperty), data);

                ctx.ObjectContext.SaveChanges();
            }
        }
        #endregion
	}
}
