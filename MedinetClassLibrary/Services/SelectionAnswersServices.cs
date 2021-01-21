using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class SelectionAnswersServices : IRepositoryServices<SelectionAnswer>
    {
        private IRepository<SelectionAnswer> _repository;

        public SelectionAnswersServices()
            : this(new Repository<SelectionAnswer>())
        {
        }

        public SelectionAnswersServices(IRepository<SelectionAnswer> repository)
        {
            _repository = repository;
        }

        public bool Add(SelectionAnswer entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("demographic", "SELECTIONANSWER");
                parameters.Add("evaluation", entity.Evaluation_Id);
                parameters.Add("question", entity.Question_Id);
                parameters.Add("option", entity.Option_Id);
                List<object[]> aux = new Commands("Add", parameters).GetData();
                //return aux.Count == 0;

                //_repository.Add(entity);
                //_repository.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddMultiple(int evaluation, int question, string options)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("demographic", "SELECTIONANSWERMULTIPLE");
                parameters.Add("options", options);
                parameters.Add("evaluation", evaluation);
                parameters.Add("question", question);
                List<object[]> aux = new Commands("Add", parameters).GetData();
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

        public IQueryable<SelectionAnswer> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<SelectionAnswer> GetByEvaluation(int evaluation_id)
        {
            return _repository.GetAllRecords().Where(t => t.Evaluation_Id == evaluation_id);
        }

        public int GetCountByTest(int test_id, int? questionnaire, int? category, int? question)
        {
            IQueryable<SelectionAnswer> sa = GetAllRecords().Where(e => e.Evaluation.Test_Id == test_id);
            if(questionnaire.HasValue)
                sa = sa.Where(e => e.Question.Category.Questionnaire_Id == questionnaire.Value);
            if (category.HasValue)
            {
                sa = sa.Where(e => e.Question.Category_Id == category.Value);
                if(question.HasValue)
                    sa = sa.Where(e => e.Question_Id == question.Value);
            }
            return sa.Count();
        }

        public SelectionAnswer GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
