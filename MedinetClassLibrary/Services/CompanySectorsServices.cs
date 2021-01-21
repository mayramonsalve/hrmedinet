using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class CompanySectorsServices : IRepositoryServices<CompanySector>
    {
        private IRepository<CompanySector> _repository;

        public CompanySectorsServices()
            : this(new Repository<CompanySector>())
        {
        }

        public CompanySectorsServices(IRepository<CompanySector> repository)
        {
            _repository = repository;
        }

        public bool Add(CompanySector entity)
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

        public IQueryable<CompanySector> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public CompanySector GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetCompanySectorsForDropDownList()
        {
            var CompanySectors = _repository.GetAllRecords().OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var sector in CompanySectors)
            {
                Dictionary.Add(sector.Id, sector.Name);
            }
            return Dictionary;
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}
