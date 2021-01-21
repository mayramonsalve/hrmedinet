using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ER_StepwiseValuesServices : IRepositoryServices<ER_StepwiseValue>
    {
        private IRepository<ER_StepwiseValue> _repository;

        public ER_StepwiseValuesServices()
            : this(new Repository<ER_StepwiseValue>())
        {
        }

        public ER_StepwiseValuesServices(IRepository<ER_StepwiseValue> repository)
        {
            _repository = repository;
        }

        public bool Add(ER_StepwiseValue entity)
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

        public IQueryable<ER_StepwiseValue> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ER_StepwiseValue GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
