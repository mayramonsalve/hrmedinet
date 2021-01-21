using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ER_AnswersByFOTypesServices : IRepositoryServices<ER_AnswersByFOType>
    {
        private IRepository<ER_AnswersByFOType> _repository;

        public ER_AnswersByFOTypesServices()
            : this(new Repository<ER_AnswersByFOType>())
        {
        }

        public ER_AnswersByFOTypesServices(IRepository<ER_AnswersByFOType> repository)
        {
            _repository = repository;
        }

        public bool Add(ER_AnswersByFOType entity)
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

        public IQueryable<ER_AnswersByFOType> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ER_AnswersByFOType GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
