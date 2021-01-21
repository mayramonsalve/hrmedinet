using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class FeedbackTypesServices : IRepositoryServices<FeedbackType>
    {
        private IRepository<FeedbackType> _repository;

        public FeedbackTypesServices()
            : this(new Repository<FeedbackType>())
        {
        }

        public FeedbackTypesServices(IRepository<FeedbackType> repository)
        {
            _repository = repository;
        }

        public bool Add(FeedbackType entity)
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

        public IQueryable<FeedbackType> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public FeedbackType GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetFeedbackTypesForDropDownList()
        {
            var FeedbackTypes = _repository.GetAllRecords().OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var type in FeedbackTypes)
            {
                Dictionary.Add(type.Id, type.Name);
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
