using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ConfidenceLevelsServices : IRepositoryServices<ConfidenceLevel>
    {
        private IRepository<ConfidenceLevel> _repository;

        public ConfidenceLevelsServices()
            : this(new Repository<ConfidenceLevel>())
        {
        }

        public ConfidenceLevelsServices(IRepository<ConfidenceLevel> repository)
        {
            _repository = repository;
        }

        public bool Add(ConfidenceLevel entity)
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

        public IQueryable<ConfidenceLevel> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ConfidenceLevel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetConfidenceLevelsForDropDownList()
        {
            var ConfidenceLevels = _repository.GetAllRecords().OrderBy(c => c.Text);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var level in ConfidenceLevels)
            {
                Dictionary.Add(level.Id, level.Text);
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
