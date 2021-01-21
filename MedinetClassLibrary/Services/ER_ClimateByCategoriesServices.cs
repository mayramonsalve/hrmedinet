using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ER_ClimateByCategoriesServices : IRepositoryServices<ER_ClimateByCategory>
    {
        private IRepository<ER_ClimateByCategory> _repository;

        public ER_ClimateByCategoriesServices()
            : this(new Repository<ER_ClimateByCategory>())
        {
        }

        public ER_ClimateByCategoriesServices(IRepository<ER_ClimateByCategory> repository)
        {
            _repository = repository;
        }

        public bool Add(ER_ClimateByCategory entity)
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

        public IQueryable<ER_ClimateByCategory> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ER_ClimateByCategory GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
