using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class EvaluationsServices : IRepositoryServices<Evaluation>
    {
        private IRepository<Evaluation> _repository;
        //private IRepository<InstructionLevel> _instructionLevelRepository;
        //private IRepository<PositionLevel> _positionLevelRepository;

        public EvaluationsServices()
            : this(new Repository<Evaluation>()/*,  new Repository<InstructionLevel>(),
                    new Repository<PositionLevel>()*/)
        {
        }

        public EvaluationsServices(IRepository<Evaluation> repository/*, 
                                    IRepository<InstructionLevel> instructionLevelRepository,
                                    IRepository<PositionLevel> positionLevelRepository*/)
        {
            _repository = repository;
            //_instructionLevelRepository = instructionLevelRepository;
            //_positionLevelRepository = positionLevelRepository;
        }

        public bool Add(Evaluation entity)
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

        public IQueryable<Evaluation> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Evaluation> GetEvaluationsByCompany(int company_id)
        {
            return _repository.GetAllRecords().Where(e => e.Test.Company_Id == company_id);
        }

        //public IQueryable<InstructionLevel> GetAllInstructionLevels()
        //{
        //    return _instructionLevelRepository.GetAllRecords();
        //}

        //public IQueryable<PositionLevel> GetAllPositionLevels()
        //{
        //    return _positionLevelRepository.GetAllRecords();
        //}

        public IQueryable<Evaluation> GetByTest(int? test_id)
        {
            return _repository.GetAllRecords().Where(e => e.Test_Id == test_id);
        }

        public Dictionary<int, string> GetEvaluationsByTest(int test_id)
        {
            var evaluations = GetByTest(test_id).OrderBy(e => e.CreationDate);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var test in evaluations)
            {
                Dictionary.Add(test.Id, test.CreationDate.ToString());
            }

            return Dictionary;
        }

        //public Dictionary<int, string> GetInstructionLevelsForDropDownList()
        //{
        //    _instructionLevelRepository = new Repository<InstructionLevel>();
        //    var instructionLevels = _instructionLevelRepository.GetAllRecords();
        //    Dictionary<int, string> Dictionary = new Dictionary<int, string>();
        //    foreach (var instructionLevel in instructionLevels)
        //    {
        //        Dictionary.Add(instructionLevel.Id, instructionLevel.Name);
        //    }
        //    return Dictionary;
        //}

        //public Dictionary<int, string> GetPositionLevelsForDropDownList()
        //{
        //    _positionLevelRepository = new Repository<PositionLevel>();
        //    var positionLevels = _positionLevelRepository.GetAllRecords();
        //    Dictionary<int, string> Dictionary = new Dictionary<int, string>();
        //    foreach (var positionLevel in positionLevels)
        //    {
        //        Dictionary.Add(positionLevel.Id, positionLevel.Name);
        //    }
        //    return Dictionary;
        //}

        public Evaluation GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public int GetEvaluationsCountByAge(Test test, int age_id)
        {
            return GetByTest(test.Id).Where(e => e.Age_Id == age_id).Count();
        }

        public int GetEvaluationsCountByGender(Test test, string gender)
        {
            return GetByTest(test.Id).Where(e => e.Sex == gender).Count();
        }

        public int GetEvaluationsCountByInstructionLevel(Test test, int instructionLevel_id)
        {
            return GetByTest(test.Id).Where(e => e.InstructionLevel_Id == instructionLevel_id).Count();
        }

        public int GetEvaluationsCountByPositionLevel(Test test, int positionLevel_id)
        {
            return GetByTest(test.Id).Where(e => e.PositionLevel_Id == positionLevel_id).Count();
        }

        public int GetEvaluationsCountBySeniority(Test test, int seniority_id)
        {
            return GetByTest(test.Id).Where(e => e.Seniority_Id == seniority_id).Count();
        }

        public int GetEvaluationsCountByLocation(Test test, int location_id)
        {
            return GetByTest(test.Id).Where(e => e.Location_Id == location_id).Count();
        }

        public int GetEvaluationsCountByPerformeEvaluation(Test test, int performance_id)
        {
            return GetByTest(test.Id).Where(e => e.Performance_Id == performance_id).Count();
        }

        public List<int> GetCountriesByTest(int test_id)
        {
            return GetByTest(test_id).Select(e => e.Location.State.Country_Id).Distinct().ToList();
        }

        public List<int> GetStatesByTest(int test_id, int country_id)
        {
            return GetByTest(test_id).Where(e => e.Location.State.Country_Id == country_id).Select(e => e.Location.State_Id).Distinct().ToList();
        }
    }
}
