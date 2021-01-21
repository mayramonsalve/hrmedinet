using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class ChiSquareDistributionServices : IRepositoryServices<ChiSquareDistribution>
    {
        private IRepository<ChiSquareDistribution> _repository;

        public ChiSquareDistributionServices()
            : this(new Repository<ChiSquareDistribution>())
        {
        }

        public ChiSquareDistributionServices(IRepository<ChiSquareDistribution> repository)
        {
            _repository = repository;
        }

        public bool Add(ChiSquareDistribution entity)
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

        public IQueryable<ChiSquareDistribution> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public ChiSquareDistribution GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ChiSquareDistribution GetChiSquareValue(double degreesOfFreedom, double significanceLevel)
        {
            return GetAllRecords().Where(d => d.DegreesOfFreedom.Value == degreesOfFreedom && d.SignificanceLevel.Value == (decimal)significanceLevel).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
