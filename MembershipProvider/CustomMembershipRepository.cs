using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;

namespace CustomMembershipProvider
{
    class CustomMembershipRepository : ICustomMembershipRepository
    {
        #region Attributes and Constructor

        private RolesServices service;

        public CustomMembershipRepository()
        {
            service =  new RolesServices();
        }

        #endregion

        #region Role Provider Methods
        
        public void CreateRole(string name)
        {
            try
            {
                Role role = new Role();
                role.Name = name;

                if (!IsRoleDuplicated(name))
                {
                    service.Add(role);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public bool DeleteRole(string name)
        {
            
            Role rol = service.GetAllRecords().Where(r => r.Name == name).SingleOrDefault();

            try
            {
                service.Delete(rol.Id);               
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }  
        }

        public bool IsRoleDuplicated(string name)
        {
            try
            {
                return service.GetAllRecords().Where(c => c.Name == name).Count() > 0;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public string[] GetAllRoles()
        {
            
            try
            {
                var rs = service.GetAllRecords().Select(r => r.Name).ToArray();                
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[] GetUsersByRole(string name)
        {

            try
            {
                var rs = new UsersServices().GetAllRecords().Where(r => r.Role.Name == name).Select(u => u.UserName).ToArray();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetUserRole(string login)
        {

            try
            {
                var role = new UsersServices().GetByUserName(login).Role.Name;
                return role;
            }
            catch (Exception ex)
            {
                
                
                throw ex;
            }
        }

        public bool IsUserInRole(string login, string role)
        {
            try
            {
                var user = new UsersServices().GetByUserName(login);
                if (user != null)
                    return (user.Role.Name == role);
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion

        #region Membership Provider Methods

        public bool ChangePassword(string login, string oldPassword, string newPassword)
        {
            UsersServices userService = new UsersServices();
            User user = userService.GetByUserName(login);

            if (user != null)
            {
                if (ValidatePassword(oldPassword, user.Password))
                {
                    user.Password = EncryptPassword(newPassword);
                    userService.SaveChanges();                    
                    return true;
                }
            }
            
            return false;
        }

        public int CountOnLineUsers()
        {
            return new UsersServices().GetAllRecords().Where(u => u.LastLoginDate.Date == DateTime.Now.Date && u.LastLoginDate.Hour == DateTime.Now.Hour).Count();
        }
       
        public MembershipUser CreateUser(string login, string password, string email, out MembershipCreateStatus status)
        {
            UsersServices userService = new UsersServices();

            if (userService.GetByEmail(email).Email != String.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            else if (userService.GetByUserName(login).UserName != String.Empty)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }
            else
            {
                User user = new User();
                user.UserName = login;
                user.Password = EncryptPassword(password);
                user.Email = email;
                user.CreationDate = DateTime.Now;
                user.LastLockOutDate = DateTime.Now;
                user.LastPasswordChangedDate = DateTime.Now;
                user.LastLoginDate = DateTime.Now;
                user.LastLogoutDate = DateTime.Now;
                user.FailedLoginAttemptsCounter = 0;
                user.IsApproved = true;
                user.IsLockedOut = false;
                user.IsOnline = false;
                userService.Add(user);
                status = MembershipCreateStatus.Success;

                return GetMembershipUser(login);
            }
        }

        public bool DeleteUser(string login)
        {
            try
            {
                UsersServices userService = new UsersServices();
                User user = userService.GetByUserName(login);
                userService.Delete(user.Id);
                userService.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        
        public string EncryptPassword(string originalPassword)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes);
        }

        public string GenerateNewPassword(string username)
        {
            UsersServices userService = new UsersServices();
            string randomString = "";
            string pwd = "";
            Random randObj = new Random();

            randomString = randomString + randObj.Next().ToString();

            User user = userService.GetByUserName(username);
            pwd = EncryptPassword(randomString);

            user.Password = pwd;
            user.LastPasswordChangedDate = DateTime.Now;
            user.FailedLoginAttemptsCounter = 0;
            user.IsApproved = true;
            user.IsLockedOut = false;
            user.IsOnline = true;
            userService.SaveChanges();

            return randomString;
        }

        public MembershipUser GetMembershipUser(string login)
        {
             try
            {
                User user = new UsersServices().GetByUserName(login);
                MembershipUser membershipUser = new MembershipUser(
                    "CustomMembershipProvider", user.UserName,
                    user.Id,
                    user.Email,
                    string.Empty,
                    string.Empty,
                    user.IsApproved,
                    user.IsLockedOut,
                    (DateTime)user.CreationDate,
                    (DateTime)user.LastLoginDate,
                    (DateTime)user.LastLogoutDate,
                    (DateTime)user.LastPasswordChangedDate,
                    (DateTime)user.LastLockOutDate
                );
                
                return membershipUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int GetMinimumPasswordLength()
        {
            return 6;
        }
        
        public string GetUserByEmail(string email) 
        {
            string username = String.Empty;
            try
            {
                User user = new UsersServices().GetByEmail(email);

                if (user != null)
                {
                    username = user.UserName.ToString();
                }
                
                return username;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetUserByLogin(string login)
        {
            string username = String.Empty;
            try
            {
                User user = new UsersServices().GetByUserName(login);

                if (user != null)
                {
                    username = user.UserName.ToString();
                }
                
                return username;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUserLogout(string login)
        {
            try
            {
                UsersServices userService = new UsersServices();
                User user = userService.GetByUserName(login);
                user.IsOnline = false;
                user.LastLogoutDate = DateTime.Now;
                userService.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidatePassword(string inputPassword, string dbPassword)
        {
            string encodedPassword = EncryptPassword(inputPassword);

            if (dbPassword == encodedPassword)
            {
                return true;
            }
            else
                return false;
        }

        public bool ValidateUser(string login, string password, int attemptsCounter)
        {
            try
            {
                UsersServices userService = new UsersServices();
                User user = userService.GetByUserName(login);
                if (user.IsLockedOut)
                {                    
                    return false;
                }
                if (ValidatePassword(password, user.Password))
                {
                    user.FailedLoginAttemptsCounter = 0;
                    user.LastLoginDate = DateTime.Now;
                    user.IsOnline = true;
                    userService.SaveChanges();                 
                    return true;
                }
                else
                {
                    int previousLoginAttempts = user.FailedLoginAttemptsCounter;
                    previousLoginAttempts = previousLoginAttempts + 1;

                    if (previousLoginAttempts >= attemptsCounter)
                    {
                        user.IsLockedOut = true;
                        user.LastLockOutDate = DateTime.Now;
                    }

                    userService.SaveChanges();
                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

    }
}
