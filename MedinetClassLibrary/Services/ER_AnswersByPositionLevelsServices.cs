using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ER_AnswersByPositionLevelsServices : IRepositoryServices<ER_AnswersByPositionLevel>
    {
        private IRepository<ER_AnswersByPositionLevel> _repository;

        public ER_AnswersByPositionLevelsServices()
            : this(new Repository<ER_AnswersByPositionLevel>())
        {
        }

        public ER_AnswersByPositionLevelsServices(IRepository<ER_AnswersByPositionLevel> repository)
        {
            _repository = repository;
        }

        public bool Add(ER_AnswersByPositionLevel entity)
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

        public IQueryable<ER_AnswersByPositionLevel> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ER_AnswersByPositionLevel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
