using System;
using System.Web.Security;
namespace CustomMembershipProvider
{
    interface ICustomMembershipRepository
    {
        #region Role Provider Methods

        void CreateRole(string name);
        bool DeleteRole(string name);
        bool IsRoleDuplicated(string name);
        string[] GetAllRoles();
        string[] GetUsersByRole(string name);
        string GetUserRole(string login);
        bool IsUserInRole(string login, string role);

        #endregion

        #region Membership Provider Methods

        bool ChangePassword(string login, string oldPassword, string newPassword);
        int CountOnLineUsers();
        MembershipUser CreateUser(string login, string password, string email, out System.Web.Security.MembershipCreateStatus status);
        bool DeleteUser(string login);
        string EncryptPassword(string originalPassword);
        string GenerateNewPassword(string username);
        MembershipUser GetMembershipUser(string login);
        int GetMinimumPasswordLength();
        string GetUserByEmail(string email);
        string GetUserByLogin(string login);
        void UpdateUserLogout(string login);
        bool ValidatePassword(string inputPassword, string dbPassword);
        bool ValidateUser(string login, string password, int attemptsCounter);

        #endregion
    }
}
