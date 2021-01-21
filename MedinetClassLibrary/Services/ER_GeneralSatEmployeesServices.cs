using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ER_GeneralSatEmployeesServices : IRepositoryServices<ER_GeneralSatEmployee>
    {
        private IRepository<ER_GeneralSatEmployee> _repository;

        public ER_GeneralSatEmployeesServices()
            : this(new Repository<ER_GeneralSatEmployee>())
        {
        }

        public ER_GeneralSatEmployeesServices(IRepository<ER_GeneralSatEmployee> repository)
        {
            _repository = repository;
        }

        public bool Add(ER_GeneralSatEmployee entity)
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

        public IQueryable<ER_GeneralSatEmployee> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ER_GeneralSatEmployee GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
