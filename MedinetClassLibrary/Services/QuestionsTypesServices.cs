using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class QuestionsTypeServices : IRepositoryServices<QuestionsType>
    {
        private IRepository<QuestionsType> _repository;

        public QuestionsTypeServices()
            : this(new Repository<QuestionsType>())
        {
        }

        public QuestionsTypeServices(IRepository<QuestionsType> repository)
        {
            _repository = repository;
        }

        public bool Add(QuestionsType entity)
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

        public IQueryable<QuestionsType> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public QuestionsType GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetQuestionsTypesForDropDownList()
        {
            var questionsTypes = _repository.GetAllRecords().OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            string types = "hola";
            foreach (var type in questionsTypes)
            {
                if (type.Name.ToString() == "Selection Answers")
                    types = ViewRes.Views.Question.Create.SelectionAnswers;
                if (type.Name.ToString() == "Text Answers")
                    types = ViewRes.Views.Question.Create.TextAnswers;
                if (type.Name.ToString() == "Dichotomous Answers")
                    types = ViewRes.Views.Question.Create.DichotomousAnswers;
                if (type.Name.ToString() == "Multiple Selection Answers")
                    types = ViewRes.Views.Question.Create.MultipleSelectionAnswers;
                if (type.Name.ToString() == "Mixed Answers")
                    types = ViewRes.Views.Question.Create.MixedAnswers;

                Dictionary.Add(type.Id, types);
            }

            return Dictionary;
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}
