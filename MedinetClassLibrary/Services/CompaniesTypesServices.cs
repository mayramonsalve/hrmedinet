using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class CompaniesTypesServices : IRepositoryServices<CompaniesType>
    {
        private IRepository<CompaniesType> _repository;

        public CompaniesTypesServices()
            : this(new Repository<CompaniesType>())
        {
        }

        public CompaniesTypesServices(IRepository<CompaniesType> repository)
        {
            _repository = repository;
        }

        public bool Add(CompaniesType entity)
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

        public IQueryable<CompaniesType> GetAllRecords()
        {
            return _repository.GetAllRecords().Where(c => c.Name!="Owner");
        }

        public CompaniesType GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetCompaniesTypesForDropDownList()
        {
            var CompaniesTypes = _repository.GetAllRecords().OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var type in CompaniesTypes)
            {
                Dictionary.Add(type.Id, type.Name);
            }

            return Dictionary;
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public Dictionary<int, string> GetCompaniesTypesForOwner()
        {
            var CompaniesTypes = _repository.GetAllRecords().Where(c => c.Name!="Owner").OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var type in CompaniesTypes)
            {
                Dictionary.Add(type.Id, type.Name);
            }

            return Dictionary;
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}
