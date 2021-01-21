using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ER_LocationsSatEmployeesServices : IRepositoryServices<ER_LocationsSatEmployee>
    {
        private IRepository<ER_LocationsSatEmployee> _repository;

        public ER_LocationsSatEmployeesServices()
            : this(new Repository<ER_LocationsSatEmployee>())
        {
        }

        public ER_LocationsSatEmployeesServices(IRepository<ER_LocationsSatEmployee> repository)
        {
            _repository = repository;
        }

        public bool Add(ER_LocationsSatEmployee entity)
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

        public IQueryable<ER_LocationsSatEmployee> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ER_LocationsSatEmployee GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
