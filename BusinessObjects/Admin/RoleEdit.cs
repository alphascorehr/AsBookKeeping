using Csla;
using Csla.Data;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Csla.Serialization;
using System.ComponentModel;

namespace BusinessObjects.Admin
{
    [Csla.Server.ObjectFactory("KreditniZahtjevDal.Admin.RoleEdit,KreditniZahtjevDal")]
    [Serializable]
    public class RoleEdit : BusinessObjects.CoreBusinessClasses.CoreBusinessClass<RoleEdit>
    {
        public RoleEdit()
        {
            MarkAsChild();
        }

        public static PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static PropertyInfo<string> NameProperty = RegisterProperty<string>(r => r.Name);
        [Required]
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static PropertyInfo<byte[]> TimeStampProperty = RegisterProperty<byte[]>(c => c.TimeStamp);
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] TimeStamp
        {
            get { return GetProperty(TimeStampProperty); }
            set { SetProperty(TimeStampProperty, value); }
        }
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            BusinessRules.AddRule(new NoDuplicates { PrimaryProperty = IdProperty });

            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, IdProperty, "Admin"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, NameProperty, "Admin"));
        }

        private class NoDuplicates : Csla.Rules.BusinessRule
        {
            protected override void Execute(Csla.Rules.RuleContext context)
            {
                var target = (RoleEdit)context.Target;
                RoleEditList parent = (RoleEditList)target.Parent;
                if (parent != null)
                    foreach (RoleEdit item in parent)
                        if (item.Id == target.ReadProperty(IdProperty) && !(ReferenceEquals(item, target)))
                        {
                            context.AddErrorResult("Role Id must be unique");
                            break;
                        }
            }
        }

        public static RoleEdit NewRoleEdit()
        {
            return DataPortal.CreateChild<RoleEdit>();
        }

#if !SILVERLIGHT
        public static RoleEdit GetRole(int id)
        {
            return DataPortal.Fetch<RoleEdit>();
        }

        internal static RoleEdit GetRole(object data)
        {
            return DataPortal.FetchChild<RoleEdit>(data);
        }

        internal new void MarkOld()
        {
            base.MarkOld();
        }
#endif
    }
}
