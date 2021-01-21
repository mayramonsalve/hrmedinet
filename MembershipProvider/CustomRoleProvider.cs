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
using CustomMembershipProvider;

namespace CustomMembershipProvider.AspNet.RoleProvider
{
    class CustomRoleProvider : System.Web.Security.RoleProvider
    {
        private ICustomMembershipRepository _repository = new CustomMembershipRepository();

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            _repository.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return DeleteRole(roleName, throwOnPopulatedRole);
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return _repository.GetUsersByRole(roleName);
        }

        public override string[] GetAllRoles()
        {
            return _repository.GetAllRoles();
        }

        public override string[] GetRolesForUser(string username)
        {
            return new[] { _repository.GetUserRole(username) };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return _repository.GetUsersByRole(roleName);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _repository.IsUserInRole(username, roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
    
        public override bool RoleExists(string roleName)
        {
            return _repository.IsRoleDuplicated(roleName);
        }
    }
}
