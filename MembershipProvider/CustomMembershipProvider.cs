using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;

namespace CustomMembershipProvider.AspNet.MembershipProvider
{
    class CustomMembershipProvider : System.Web.Security.MembershipProvider
    {
        private ICustomMembershipRepository _repository = new CustomMembershipRepository();
        
        public override string ApplicationName
        {
            get
            {
                return "MembershipProvider";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _repository.ChangePassword(username, oldPassword, newPassword);
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public MembershipUser CreateUser(string username, string password, string email, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = 0;
            string login = username;
            return _repository.CreateUser(login, password, email, out status);
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return CreateUser(username, password, email, isApproved, providerUserKey, out status);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            string login = username;
            return _repository.DeleteUser(login);
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection myCollection = new MembershipUserCollection();

            if (String.IsNullOrEmpty(emailToMatch))
            {
                var users = new UsersServices().GetAllRecords();

                foreach (var user in users)
                {
                    User thisUser = new UsersServices().GetByUserName(user.UserName);
                    MembershipUser membershipUser = new MembershipUser(
                        "CustomMembershipProvider", thisUser.UserName,
                        thisUser.Id,
                        thisUser.Email,
                        string.Empty,
                        string.Empty,
                        thisUser.IsApproved,
                        thisUser.IsLockedOut,
                        (DateTime)thisUser.CreationDate,
                        (DateTime)thisUser.LastLoginDate,
                        (DateTime)thisUser.LastLogoutDate,
                        (DateTime)thisUser.LastPasswordChangedDate,
                        (DateTime)thisUser.LastLockOutDate
                    );

                    myCollection.Add(membershipUser);
                }
            }
            else
            {
                var thisUser = new UsersServices().GetByEmail(emailToMatch);
                if (thisUser != null)
                {
                    MembershipUser membershipUser = new MembershipUser(
                        "CustomMembershipProvider", thisUser.UserName,
                        thisUser.Id,
                        thisUser.Email,
                        string.Empty,
                        string.Empty,
                        thisUser.IsApproved,
                        thisUser.IsLockedOut,
                        (DateTime)thisUser.CreationDate,
                        (DateTime)thisUser.LastLoginDate,
                        (DateTime)thisUser.LastLogoutDate,
                        (DateTime)thisUser.LastPasswordChangedDate,
                        (DateTime)thisUser.LastLockOutDate
                    );

                    myCollection.Add(membershipUser);
                }
            }

            totalRecords = myCollection.Count;

            return myCollection;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection myCollection = new MembershipUserCollection();

            if (String.IsNullOrEmpty(usernameToMatch))
            {
                var users = new UsersServices().GetAllRecords();

                foreach (var user in users)
                {
                    User thisUser = new UsersServices().GetByUserName(user.UserName);
                    MembershipUser membershipUser = new MembershipUser(
                        "CustomMembershipProvider", thisUser.UserName,
                        thisUser.Id,
                        thisUser.Email,
                        string.Empty,
                        string.Empty,
                        thisUser.IsApproved,
                        thisUser.IsLockedOut,
                        (DateTime)thisUser.CreationDate,
                        (DateTime)thisUser.LastLoginDate,
                        (DateTime)thisUser.LastLogoutDate,
                        (DateTime)thisUser.LastPasswordChangedDate,
                        (DateTime)thisUser.LastLockOutDate
                    );

                    myCollection.Add(membershipUser);
                }
            }
            else
            {
                var thisUser = new UsersServices().GetByUserName(usernameToMatch);
                if (thisUser != null)
                {
                    MembershipUser membershipUser = new MembershipUser(
                        "CustomMembershipProvider", thisUser.UserName,
                        thisUser.Id,
                        thisUser.Email,
                        string.Empty,
                        string.Empty,
                        thisUser.IsApproved,
                        thisUser.IsLockedOut,
                        (DateTime)thisUser.CreationDate,
                        (DateTime)thisUser.LastLoginDate,
                        (DateTime)thisUser.LastLogoutDate,
                        (DateTime)thisUser.LastPasswordChangedDate,
                        (DateTime)thisUser.LastLockOutDate
                    );

                    myCollection.Add(membershipUser);
                }
            }

            totalRecords = myCollection.Count;

            return myCollection;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MembershipUserCollection myCollection = new MembershipUserCollection();
            
            var users = new UsersServices().GetAllRecords();

            foreach (var user in users)
            {
                User thisUser = new UsersServices().GetByUserName(user.UserName);
                MembershipUser membershipUser = new MembershipUser(
                    "CustomMembershipProvider", thisUser.UserName,
                    thisUser.Id,
                    thisUser.Email,
                    string.Empty,
                    string.Empty,
                    thisUser.IsApproved,
                    thisUser.IsLockedOut,
                    (DateTime)thisUser.CreationDate,
                    (DateTime)thisUser.LastLoginDate,
                    (DateTime)thisUser.LastLogoutDate,
                    (DateTime)thisUser.LastPasswordChangedDate,
                    (DateTime)thisUser.LastLockOutDate
                );
                myCollection.Add(membershipUser);
            }


            totalRecords = myCollection.Count;

            return myCollection;
        }

        public override int GetNumberOfUsersOnline()
        {
            return _repository.CountOnLineUsers();
        }

        public override string GetPassword(string username, string answer)
        {
            return new UsersServices().GetByUserName(username).Password;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return _repository.GetMembershipUser(username); 
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            return _repository.GetUserByEmail(email);
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _repository.GetMinimumPasswordLength(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            return _repository.GenerateNewPassword(username);
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            _repository.UpdateUserLogout(user.UserName);
        }

        public override bool ValidateUser(string username, string password)
        {
            int intentos = Membership.Provider.MaxInvalidPasswordAttempts;
            return _repository.ValidateUser(username, password, intentos);
        }
    }
}
