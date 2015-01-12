﻿using System;
using BusinessObjects.Security;
using System.Web;
 using System.Linq;
using System.Web.Security;
using Csla;
using BusinessObjects.MDSubjects;

namespace AlphaWebCommodityBookkeeping
{
  public class CustomMembershipProvider : MembershipProvider
  {
    public override bool ValidateUser(
      string username, string password)
    {
      bool result = PTPrincipal.Login(username, password);
      HttpContext.Current.Session["CslaPrincipal"] =
        Csla.ApplicationContext.User;
      return result;
    }

    #region Non-Implemented Members

    // the following members must be implemented due to the abstract class MembershipProvider,
    // but not required to be functional for using the Login control.
    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        try
        {
            var user = cMDSubjects_Employee.GetMDSubjects_Employee(new Csla.Security.UsernameCriteria(username, oldPassword));

            user.Password = newPassword;

            cMDSubjects_Employee temp = user.Clone();
            user = temp.Save();

            return true;
        }
        catch
        {

            return false;
        }
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        throw new NotSupportedException("The method or operation is not implemented.");
        //if (username.Trim() == "")
        //{
        //    status = MembershipCreateStatus.InvalidUserName;
        //    return null;
        //}

        //try
        //{
        //    UserExistsCommand com = new UserExistsCommand(username);
        //    com = DataPortal.Execute(com);

        //    if (com.UserExists)
        //    {
        //        status = MembershipCreateStatus.DuplicateUserName;
        //        return null;
        //    }

        //    //KreditniZahtjevBusinessObjects.cKorisnici newKorisnik = KreditniZahtjevBusinessObjects.cKorisnici.NewKorisnici();
        //    //newKorisnik.Email = username;
        //    //newKorisnik.Password = password;
        //    //newKorisnik.IsReferent = false;


        //    //    newKorisnik.ApplyEdit();
        //    //    KreditniZahtjevBusinessObjects.cKorisnici temp = newKorisnik.Clone();
        //    //    newKorisnik = temp.Save();

        //    status = MembershipCreateStatus.Success;
        //    return new MembershipUser(this.Name, username, providerUserKey, username, passwordQuestion, "", isApproved, true, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);

        //}
        //catch
        //{
        //    status = MembershipCreateStatus.ProviderError;
        //    return null;
        //}
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override bool EnablePasswordReset
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override bool EnablePasswordRetrieval
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override int GetNumberOfUsersOnline()
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override string GetPassword(string username, string answer)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        var user = cMDSubjects_Employee.GetMDSubjects_Employee(username);

        return new MembershipUser(this.Name, username, null, username, "", "", true, true, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override string GetUserNameByEmail(string email)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override int MaxInvalidPasswordAttempts
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override int MinRequiredPasswordLength
    {
        get { return 6; }
    }

    public override int PasswordAttemptWindow
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override string PasswordStrengthRegularExpression
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override bool RequiresQuestionAndAnswer
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override bool RequiresUniqueEmail
    {
      get { throw new NotSupportedException("The method or operation is not implemented."); }
    }

    public override string ResetPassword(string username, string answer)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override bool UnlockUser(string userName)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override void UpdateUser(MembershipUser user)
    {
      throw new NotSupportedException("The method or operation is not implemented.");
    }

    public override string ApplicationName
    {
      get
      {
        throw new NotSupportedException("The method or operation is not implemented.");
      }
      set
      {
        throw new NotSupportedException("The method or operation is not implemented.");
      }
    }

    #endregion
  }
}
