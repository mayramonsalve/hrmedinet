using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class StandardErrorsServices : IRepositoryServices<StandardError>
    {
        private IRepository<StandardError> _repository;

        public StandardErrorsServices()
            : this(new Repository<StandardError>())
        {
        }

        public StandardErrorsServices(IRepository<StandardError> repository)
        {
            _repository = repository;
        }

        public bool Add(StandardError entity)
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

        public IQueryable<StandardError> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public StandardError GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetStandardErrorsForDropDownList()
        {
            var StandardErrors = _repository.GetAllRecords().OrderBy(c => c.Value);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var error in StandardErrors)
            {
                Dictionary.Add(error.Id, error.Text);
            }
            return Dictionary;
        }

        public decimal GetValueById(int id)
        {
            return GetById(id).Value;
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
