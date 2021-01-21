using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ExecutiveReportsServices : IRepositoryServices<ExecutiveReport>
    {
        private IRepository<ExecutiveReport> _repository;

        public ExecutiveReportsServices()
            : this(new Repository<ExecutiveReport>())
        {
        }

        public ExecutiveReportsServices(IRepository<ExecutiveReport> repository)
        {
            _repository = repository;
        }

        public bool Add(ExecutiveReport entity)
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

        public IQueryable<ExecutiveReport> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ExecutiveReport GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
