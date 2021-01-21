using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class EvaluationsFOServices : IRepositoryServices<EvaluationFO>
    {
        private IRepository<EvaluationFO> _repository;

        public EvaluationsFOServices()
            : this(new Repository<EvaluationFO>())
        {
        }

        public EvaluationsFOServices(IRepository<EvaluationFO> repository)
        {
            _repository = repository;
        }

        public bool Add(EvaluationFO entity)
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

        public IQueryable<EvaluationFO> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public EvaluationFO GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<EvaluationFO> GetByFunctionalOrganization(int fo_id)
        {
            return _repository.GetAllRecords().Where(fo => fo.FunctionalOrganization_Id == fo_id).OrderBy(fo => fo.Evaluation_Id);
        }

        public IQueryable<EvaluationFO> GetByFunctionalOrganizationType(int? foType_id)
        {
            return _repository.GetAllRecords().Where(fo => fo.FunctionalOrganization.Type_Id == foType_id).OrderBy(fo => fo.Evaluation_Id);
        }

        public IQueryable<EvaluationFO> GetByFunctionalOrganizationTypeAndUbication(List<int> evaluationsId, int foType_id)
        {
            return _repository.GetAllRecords().Where(fo => fo.FunctionalOrganization.Type_Id == foType_id && evaluationsId.Contains(fo.Evaluation_Id)).OrderBy(fo => fo.Evaluation_Id);
        }

        public IQueryable<EvaluationFO> GetByFunctionalOrganizationTypeAndTest(int test_id, int foType_id)
        {
            return _repository.GetAllRecords().Where(fo => fo.FunctionalOrganization.Type_Id == foType_id && fo.Evaluation.Test_Id == test_id).OrderBy(fo => fo.Evaluation_Id);
        }

        public IQueryable<EvaluationFO> GetByEvaluation(int evaluation_id)
        {
            return _repository.GetAllRecords().Where(fo => fo.Evaluation_Id == evaluation_id).OrderBy(fo => fo.Evaluation_Id);
        }

        public IQueryable<EvaluationFO> GetByTest(int test_id)
        {
            return _repository.GetAllRecords().Where(fo => fo.Evaluation.Test_Id == test_id).OrderBy(fo => fo.Evaluation_Id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
