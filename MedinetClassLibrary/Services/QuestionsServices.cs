using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class QuestionsServices : IRepositoryServices<Question>
    {
        private IRepository<Question> _repository;

        public QuestionsServices()
            : this(new Repository<Question>())
        {
        }

        public QuestionsServices(IRepository<Question> repository)
        {
            _repository = repository;
        }

        public bool Add(Question entity)
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

        public IQueryable<Question> GetAllRecords()
        {
            return _repository.GetAllRecords().OrderBy(q => q.SortOrder);
        }

        public Question GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetQuestionsForDropDownList(int? category_id)
        {
            var questions = GetByCategory(category_id).OrderBy(q => q.SortOrder);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var question in questions)
            {
                Dictionary.Add(question.Id, question.Text);
            }

            return Dictionary;
        }


        public Dictionary<int, string> GetQuestionsForDropDownList()
        {
            var questions = _repository.GetAllRecords().OrderBy(q => q.SortOrder);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var question in questions)
            {
                Dictionary.Add(question.Id, question.Text);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetQuestionsByCategory(int? category_id)
        {
            var questions = GetByCategory(category_id).OrderBy(q => q.SortOrder);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var question in questions)
            {
                Dictionary.Add(question.Id, question.Text);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetQuestionsByCategoryAndTyoe(int? category_id, int type_id)
        {
            var questions = GetByCategory(category_id).OrderBy(q => q.SortOrder);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var question in questions)
            {
                Dictionary.Add(question.Id, question.Text);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetQuestionsForOptionsByCategory(int? category_id)
        {
            var questions = GetForOptionsByCategory(category_id).OrderBy(q => q.SortOrder);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var question in questions)
            {
                Dictionary.Add(question.Id, question.Text);
            }

            return Dictionary;
        }
        
        public Dictionary<int, string> GetQuestionByCategory(int? category_id) 
        {
            var questions = GetByCategory(category_id);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach( var question in questions)
            {
                Dictionary.Add(question.Id, question.Text);
            }
            return Dictionary;
        }

        public Dictionary<int, string> GetQuestionsByCategoryAndType(int category_id, int type_id)
        {
            IQueryable<Question> questions = GetByCategoryAndType(category_id, type_id);
            if(questions.Count() == 0 && type_id == 1)
                questions = GetByCategoryAndType(category_id, 3);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var question in questions)
            {
                Dictionary.Add(question.Id, question.Text);
            }
            return Dictionary;
        }

        public int GetQuestionsCount(int category_id )
        {
            return GetAllRecords().Where(o => o.Category.Id == category_id).Count();
        }
        
        public Question[] GetQuestions(int category_id)
        {
            IEnumerable<Question> _questions = new QuestionsServices().GetByCategory(category_id).OrderBy(q => q.SortOrder);
            Question[] questions = new Question[GetQuestionsCount(category_id)];
            int pos = 0;
            foreach (var q in _questions)
            {
                questions[pos] = q;
                pos++;
            }
            return questions;
        }

        public List<object[]> GetQuestionsInfoByCategory(int category_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "QUESTIONSBYCATEGORYID");
            parameters.Add("id", category_id);
            List<object[]> data = (List<object[]>)new Commands("Questions", parameters).GetData();
            return data;
        }

        public List<object[]> GetQuestionsInfoByQuestionnaire(int questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "QUESTIONSBYQUESTIONNAIREID");
            parameters.Add("id", questionnaire_id);
            List<object[]> data = (List<object[]>)new Commands("Questions", parameters).GetData();
            return data;
        }

        public int GetQuestionsCountByQuestionnaire(int questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "QUESTIONSCOUNTBYQUESTIONNAIREID");
            parameters.Add("id", questionnaire_id);
            List<object[]> aux = new Commands("QuestionsCount", parameters).GetData();
            return (int)aux.FirstOrDefault()[0];
        }

        public int GetNextSortOrderByQuestionnaire(int questionnaire_id)
        {
            int nextSortOrder = 1;
            if (_repository.GetAllRecords().Where(q => q.Category.Questionnaire_Id == questionnaire_id).Count()>0)
                nextSortOrder = _repository.GetAllRecords().Where(q => q.Category.Questionnaire_Id == questionnaire_id).Max(q=> q.SortOrder) + 1;
            return nextSortOrder;
        }

        public IQueryable<Question> GetByCategory(int? category_id)
        {
            return _repository.GetAllRecords().Where(q => q.Category.Id == category_id).OrderBy(q => q.SortOrder);
        }

        public IQueryable<Question> GetByCategoryAndType(int category_id, int type_id)
        {
            return _repository.GetAllRecords().Where(q => q.Category.Id == category_id && q.QuestionType_Id == type_id).OrderBy(q => q.SortOrder);
        }

        public IQueryable<Question> GetForOptionsByCategory(int? category_id)
        {
            return _repository.GetAllRecords().Where(q => q.Category.Id == category_id
                && q.QuestionsType.Name == "Selection Answers").OrderBy(q => q.SortOrder);
        }

        public IQueryable<Question> GetByQuestionnaire(int questionnaire_id)
        {
            return _repository.GetAllRecords().Where(q => q.Category.Questionnaire_Id == questionnaire_id).OrderBy(q => q.SortOrder);
        }

        public int GetOptionsCount(int category_id) {
            return _repository.GetAllRecords().Where(o => o.Category_Id == category_id).Count();
        }

        public bool IsSortOrderValid(int questionnaire_id, int sortOrder)
        {
            return GetByQuestionnaire(questionnaire_id).Where(q => q.SortOrder == sortOrder).Count()>0;
        }

        public bool IsTextDuplicated(int questionnaire_id, string text)
        {
            return GetByQuestionnaire(questionnaire_id).Where(q => q.Text == text).Count() > 0;
        }

        public int GetQuestionsCountByQuestionnaire(int questionnaire_id, int? category_id)
        {
            if (category_id.HasValue)
            {
                return GetAllRecords().Where(q => q.Category.Questionnaire_Id == questionnaire_id).Count();
            }
            else
            {
                return GetAllRecords().Where(q => q.Category.Questionnaire_Id == questionnaire_id && q.Category_Id == category_id).Count();
            }
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public object RequestList(int questionnaire_id, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Question> questions = GetByQuestionnaire(questionnaire_id);
            questions = JqGrid<Question>.GetFilteredContent(sidx, sord, page, rows, filters, questions, ref totalPages, ref totalRecords);
            var rowsModel = (
                from question in questions.ToList()
                select new
                {
                    i = question.Id,
                    cell = new string[] { question.Id.ToString(), 
                            "<a href=\"/Questions/Edit/"+question.Id+"\">" + 
                            question.SortOrder + "</a>",
                            question.Category.Name,
                            question.Text,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Questions/Edit/"+question.Id+"\"><span id=\""+question.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Questions/Details/"+question.Id+"\"><span id=\""+question.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+question.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();
            return JqGrid<Question>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Question> questions = GetAllRecords();
            questions = JqGrid<Question>.GetFilteredContent(sidx, sord, page, rows, filters, questions, ref totalPages, ref totalRecords);
            var rowsModel = (
                from question in questions.ToList()
                select new
                {
                    i = question.Id,
                    cell = new string[] { question.Id.ToString(), 
                            "<a href=\"/Questions/Edit/"+question.Id+"\">" + 
                            question.SortOrder + "</a>",
                            question.Category.Name,
                            question.Text,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Questions/Edit/"+question.Id+"\"><span id=\""+question.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Questions/Details/"+question.Id+"\"><span id=\""+question.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+question.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();
            return JqGrid<Question>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public bool AddQuestionsTemplate(int category_id, int new_category_id, User user)
        {
            Question[] questions = new Question[GetQuestionsCount(category_id)];
            questions = GetQuestions(category_id);
            foreach (var question in questions)
            {
                Question new_question = new Question();
                new_question.CreationDate = DateTime.Now;
                new_question.Category_Id = new_category_id;
                new_question.QuestionType_Id = question.QuestionType_Id;
                new_question.SortOrder = question.SortOrder;
                new_question.Text = question.Text;
                new_question.Positive = question.Positive;
                if (!this.Add(new_question)){
                    return false;
                }
            }
            return true;
        }
    }
}
