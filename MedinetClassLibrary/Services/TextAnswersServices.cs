using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class TextAnswersServices : IRepositoryServices<TextAnswer>
    {
        private IRepository<TextAnswer> _repository;

        public TextAnswersServices()
            : this(new Repository<TextAnswer>())
        {
        }

        public TextAnswersServices(IRepository<TextAnswer> repository)
        {
            _repository = repository;
        }

        public bool Add(TextAnswer entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("demographic", "TEXTANSWER");
                parameters.Add("evaluation", entity.Evaluation_Id);
                parameters.Add("question", entity.Question_Id);
                parameters.Add("text", entity.Text);
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

        public IQueryable<TextAnswer> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<TextAnswer> GetByEvaluation(int evaluation_id)
        {
            return _repository.GetAllRecords().Where(t => t.Evaluation_Id == evaluation_id);
        }

        public TextAnswer GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<TextAnswer> GetByQuestion(int question_id)
        {
            return _repository.GetAllRecords().Where(t => t.Question_Id == question_id);
        }

        public IQueryable<TextAnswer> GetByQuestionAndEvaluation(int question_id, int evaluation_id)
        {
            return _repository.GetAllRecords().Where(t => t.Evaluation_Id == evaluation_id && t.Question_Id == question_id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
