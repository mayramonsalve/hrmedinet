using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class OptionsServices : IRepositoryServices<Option>
    {
        private IRepository<Option> _repository;

        public OptionsServices()
            : this(new Repository<Option>())
        {
        }

        public OptionsServices(IRepository<Option> repository)
        {
            _repository = repository;
        }

        public bool Add(Option entity)
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

        public IQueryable<Option> GetAllRecords()
        {
            return _repository.GetAllRecords().OrderByDescending(o => o.Value);
        }

        public Option GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Option GetByValue(int value, int questionnaire_id)
        {
            return _repository.GetAllRecords().Where(o => o.Questionnaire_Id == questionnaire_id && o.Value == value).FirstOrDefault();
        }

        public IQueryable<Option> GetByQuestionnaire(int questionnaire_id)
        {
            return _repository.GetAllRecords().Where(q => q.Questionnaire_Id == questionnaire_id).OrderByDescending(o => o.Value);
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public object RequestList(int questionnaire_id, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Option> options = GetByQuestionnaire(questionnaire_id);
            options = JqGrid<Option>.GetFilteredContent(sidx, sord, page, rows, filters, options, ref totalPages, ref totalRecords);
            var rowsModel = (
                from option in options.ToList()
                select new
                {
                    i = option.Id,
                    cell = new string[] { option.Id.ToString(), 
                            "<a href=\"/Options/Edit/"+option.Id+"\">" + 
                            option.Text + "</a>",
                            option.Value.ToString(),
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Options/Edit/"+option.Id+"\"><span id=\""+option.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Options/Details/"+option.Id+"\"><span id=\""+option.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+option.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();
            return JqGrid<Option>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public Option[] GetOptions(int questionnaire_id)
        {
            IEnumerable<Option> _options = GetByQuestionnaire(questionnaire_id);
            Option[] options = new Option[GetOptionsCount(questionnaire_id)];
            int pos = 0;
            foreach (var op in _options)
            {
                options[pos] = op;
                pos++;
            }
            return options;
        }

        public int GetOptionsCount(int questionnaire_id)
        {
            return GetAllRecords().Where(o => o.Questionnaire_Id == questionnaire_id).Count();
        }

        public bool AddOptionsTemplate(int questionnaire_id, int new_questionnaire_id, User user)
        {
            Option[] options = new Option[GetOptionsCount(questionnaire_id)];
            options = GetOptions(questionnaire_id);
            foreach (var option in options)
            {
                Option new_option = new Option();
                new_option.CreationDate = DateTime.Now;
                new_option.Questionnaire_Id = new_questionnaire_id;
                new_option.Text = option.Text;
                new_option.Value = option.Value;
                if (!this.Add(new_option)) {
                    return false;
                }
            }
            return true;
        }

        public bool IsValueDuplicated(int questionnaire_id, int value)
        {
            return GetByQuestionnaire(questionnaire_id).Where(o => o.Value == value).Count() > 0;
        }

        public bool IsTextDuplicated(int questionnaire_id, string text)
        {
            return GetByQuestionnaire(questionnaire_id).Where(o => o.Text == text).Count() > 0;
        }

        public bool IsThereTenOptions(int questionnaire_id)
        {
            return GetByQuestionnaire(questionnaire_id).Count() >= 10;
        }
    }
}