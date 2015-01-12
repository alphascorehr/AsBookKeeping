using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Serialization;
using DalEf;
using Csla.Data;

namespace BusinessObjects.Security
{
    [Serializable]
    public class UserExistsCommand : CommandBase<UserExistsCommand>
    {
            public static PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);
        public string UserName
        {
          get { return ReadProperty(UserNameProperty); }
          set { LoadProperty(UserNameProperty, value); }
        }

        public static PropertyInfo<bool> UserExistsProperty = RegisterProperty<bool>(c => c.UserExists);
        public bool UserExists
        {
          get { return ReadProperty(UserExistsProperty); }
          set { LoadProperty(UserExistsProperty, value); }
        }

        public UserExistsCommand(string username)
        {
          UserName = username;
        }

        protected override void DataPortal_Execute()
        {
            using (var ctx = ObjectContextManager<MDSubjectsEntities>.GetManager("MDSubjectsEntities"))
            {
                var result = (from r in ctx.ObjectContext.MDSubjects_Subject.OfType<MDSubjects_Employee>()
                              where r.UserName.ToLower() == UserName.ToLower()
                              select r.Id).Count() > 0;

                UserExists = result;
            }
        }
    }
}
