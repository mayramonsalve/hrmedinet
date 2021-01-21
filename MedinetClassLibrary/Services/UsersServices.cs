using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;
using System.Security.Cryptography;

namespace MedinetClassLibrary.Services
{
    public class UsersServices : IRepositoryServices<User>
    {

        private IRepository<User> _repository;

        public UsersServices()
            : this(new Repository<User>())
        {
        }

        public UsersServices(IRepository<User> repository)
        {
            _repository = repository;
        }

        public bool Add(User entity)
        {
            try
            {
                entity.CreationDate = DateTime.Now;
                entity.LastLoginDate = DateTime.Now;
                entity.LastLogoutDate = DateTime.Now;
                entity.LastPasswordChangedDate = DateTime.Now;
                entity.LastLockOutDate = DateTime.Now;
                entity.IsApproved = true;
                entity.IsLockedOut = false;
                entity.IsOnline = false;
                entity.FailedLoginAttemptsCounter = 0;
                entity.Password = EncryptPassword(entity.Password);
                _repository.Add(entity);
                _repository.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string EncryptPassword(string originalPassword)
        {
            //Declarations
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

        public IQueryable<User> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public User GetByUserName(string userName)
        {
            return _repository.GetAllRecords().Where(u => u.UserName == userName).Single();
        }

        public User GetByEmail(string email)
        {
            return _repository.GetAllRecords().Where(u => u.Email == email).Single();
        }

        public bool IsUserNameDuplicated(string userName)
        {
            User user = _repository.GetAllRecords().Where(u => u.UserName == userName).FirstOrDefault();

            return user != null;
        }

        public bool IsEmailDuplicated(string email)
        {
            User user = _repository.GetAllRecords().Where(u => u.Email == email).FirstOrDefault();

            return user != null;
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public object RequestList(User userLogged, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;
            IQueryable<User> users;
            if (userLogged.Company.CompaniesType.Name == "Owner")
                users = GetAllRecords();
            else
                users = GetAllRecords().Where(u => u.Company_Id == userLogged.Company_Id ||
                                        u.Company.CompanyAssociated_Id == userLogged.Company_Id);

            users = JqGrid<User>.GetFilteredContent(sidx, sord, page, rows, filters, users, ref totalPages, ref totalRecords);
            var rowsModel = (
                from user in users.ToList()
                select new
                {
                    i = user.Id,
                    cell = new string[] { user.Id.ToString(), 
                            "<a href=\"/Users/Edit/"+user.Id+"\">" + 
                            user.UserName + "</a>",
                            user.FirstName + " " + user.LastName,
                            user.Email,
                            user.ContactPhone,
                            user.Role.Name,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Users/Edit/"+user.Id+"\"><span id=\""+user.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Users/Details/"+user.Id+"\"><span id=\""+user.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+user.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<User>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }
    }
}
