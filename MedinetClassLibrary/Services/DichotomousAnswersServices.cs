using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class DichotomousAnswersServices : IRepositoryServices<DichotomousAnswer>
    {
        private IRepository<DichotomousAnswer> _repository;

        public DichotomousAnswersServices()
            : this(new Repository<DichotomousAnswer>())
        {
        }

        public DichotomousAnswersServices(IRepository<DichotomousAnswer> repository)
        {
            _repository = repository;
        }

        public bool Add(DichotomousAnswer entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("demographic", "DICHOTOMOUSANSWER");
                parameters.Add("evaluation", entity.Evaluation_Id);
                parameters.Add("question", entity.Question_Id);
                parameters.Add("affirmative", entity.Affirmative ? 1 : 0);
                List<object[]> aux = new Commands("Add", parameters).GetData();

                //_repository.Add(entity);
                //_repository.SaveChanges();

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

        public IQueryable<DichotomousAnswer> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<DichotomousAnswer> GetByEvaluation(int evaluation_id)
        {
            return _repository.GetAllRecords().Where(t => t.Evaluation_Id == evaluation_id);
        }

        public DichotomousAnswer GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<DichotomousAnswer> GetByQuestion(int question_id)
        {
            return _repository.GetAllRecords().Where(t => t.Question_Id == question_id);
        }

        public IQueryable<DichotomousAnswer> GetByQuestionAndEvaluation(int question_id, int evaluation_id)
        {
            return _repository.GetAllRecords().Where(t => t.Evaluation_Id == evaluation_id && t.Question_Id == question_id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
