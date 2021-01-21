using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ER_CategoriesSatEmployeesServices : IRepositoryServices<ER_CategoriesSatEmployee>
    {
        private IRepository<ER_CategoriesSatEmployee> _repository;

        public ER_CategoriesSatEmployeesServices()
            : this(new Repository<ER_CategoriesSatEmployee>())
        {
        }

        public ER_CategoriesSatEmployeesServices(IRepository<ER_CategoriesSatEmployee> repository)
        {
            _repository = repository;
        }

        public bool Add(ER_CategoriesSatEmployee entity)
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

        public IQueryable<ER_CategoriesSatEmployee> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ER_CategoriesSatEmployee GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
