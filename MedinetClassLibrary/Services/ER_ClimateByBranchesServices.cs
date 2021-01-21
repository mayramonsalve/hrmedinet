using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ER_ClimateByBranchesServices : IRepositoryServices<ER_ClimateByBranch>
    {
        private IRepository<ER_ClimateByBranch> _repository;

        public ER_ClimateByBranchesServices()
            : this(new Repository<ER_ClimateByBranch>())
        {
        }

        public ER_ClimateByBranchesServices(IRepository<ER_ClimateByBranch> repository)
        {
            _repository = repository;
        }

        public bool Add(ER_ClimateByBranch entity)
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

        public IQueryable<ER_ClimateByBranch> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ER_ClimateByBranch GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
