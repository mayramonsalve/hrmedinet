using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;


namespace MedinetClassLibrary.Services
{
    public class RolesServices : IRepositoryServices<Role>
    {
        private IRepository<Role> _repository;

        public RolesServices()
            : this(new Repository<Role>())
        {
        }

        public RolesServices(IRepository<Role> repository)
        {
            _repository = repository;
        }

        public bool Add(Role entity)
        {
            try
            {
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

        public IQueryable<Role> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Role GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetRolesForAdministrator()
        {
            var roles = _repository.GetAllRecords().OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var role in roles)
            {
                Dictionary.Add(role.Id, role.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetAdministratorsRoles()
        {
            var roles = _repository.GetAllRecords().Where(n => n.Name != "CompanyAppManager"
                && n.Name != "HRCompany" && n.Name != "CompanyManager").OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var role in roles)
            {
                Dictionary.Add(role.Id, role.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetRolesForCompanyAppManager()
        {
            var roles = _repository.GetAllRecords().Where(n => n.Name != "Administrator" &&
                n.Name != "HRAdministrator").OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var role in roles)
            {
                Dictionary.Add(role.Id, role.Name);
            }

            return Dictionary;
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}
