using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;
using System.Web.Mvc;

namespace MedinetClassLibrary.Services
{
    public class CategoriesServices : IRepositoryServices<Category>
    {
        private IRepository<Category> _repository;

        public CategoriesServices()
            : this(new Repository<Category>())
        {
        }

        public CategoriesServices(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public bool Add(Category entity)
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

        public IQueryable<Category> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Category> GetByQuestionnaire(int? questionnaire_id)
        {
            return _repository.GetAllRecords().Where(c => c.Questionnaire_Id == questionnaire_id); // && !c.CategoryGroup_Id.HasValue);
        }

        public IQueryable<Category> GetByCompany(int company_id)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id && !c.CategoryGroup_Id.HasValue && c.Questions.Count == 0);
        }

        public Category GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetCategoriesForDropDownList(int? questionnaire_id)
        {
            var categories = GetByQuestionnaire(questionnaire_id).OrderBy(p => p.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var category in categories)
            {
                Dictionary.Add(category.Id, category.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetEmptyCategoriesForDropDownList(int company_id)
        {
            var categories = GetByQuestionnaire(company_id).OrderBy(p => p.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var category in categories)
            {
                Dictionary.Add(category.Id, category.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetGroupingCategoriesByCompanyForDropDownList(int company_id)
        {
            var categories = GetByCompany(company_id).OrderBy(p => p.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var category in categories)
            {
                Dictionary.Add(category.Id, category.Name);
            }

            return Dictionary;
        }

        private IQueryable<Category> GetCategoriesByQuestionTypeAndQuestionnaire(int questionnaire_id, int questionType)
        {
            return GetByQuestionnaire(questionnaire_id).Where(c => c.Questions.Where(q => q.QuestionType_Id == questionType).Count() > 0).OrderBy(n => n.Name);
        }

        public List<SelectListItem> GetCategoriesForList(int questionnaire_id, int type)
        {
            IQueryable<Category> categories = GetCategoriesByQuestionTypeAndQuestionnaire(questionnaire_id, type);
            List<SelectListItem> List = new List<SelectListItem>();
            foreach (var category in categories)
            {
                List.Add(new SelectListItem(){ Value = category.Id.ToString(), Text = category.Name, Selected = false});
            }
            //List.Add(new SelectListItem() { Value = "00", Text = "Item", Selected = false});
            return List;
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public Category[] GetCategories(int questionnaire_id)
        {
            IEnumerable<Category> _categories = GetByQuestionnaire(questionnaire_id);
            Category[] categories = new Category[this.GetCategoriesCount(questionnaire_id)];
            int pos = 0;
            foreach (var q in _categories)
            {
                categories[pos] = q;
                pos++;
            }
            return categories;
        }
        
        public List<object[]> GetCategoriesInfo(int questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "CATEGORIESBYQUESTIONNAIREID");
            parameters.Add("id", questionnaire_id);
            List<object[]> data = (List<object[]>)new Commands("Categories", parameters).GetData();
            return data;
        }

        public int GetCategoriesCount(int questionnaire_id)
        {
            return GetAllRecords().Where(o => o.Questionnaire_Id == questionnaire_id).Count();
        }

        public bool IsNameDuplicated(int? questionnaire_id, int? company_id, string name)
        {
            if(questionnaire_id.HasValue)
                return GetByQuestionnaire(questionnaire_id.Value).Where(d => d.Name == name).Count() > 0;
            else
                return GetByCompany(company_id.Value).Where(d => d.Name == name).Count() > 0;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Category> categories = GetAllRecords();
            categories = JqGrid<Category>.GetFilteredContent(sidx, sord, page, rows, filters, categories, ref totalPages, ref totalRecords);
            var rowsModel = (
                from category in categories.ToList()
                select new
                {
                    i = category.Id,
                    cell = new string[] { category.Id.ToString(), 
                            "<a href=\"/Categories/Edit/"+category.Id+"\">" + 
                            category.Name + "</a>",
                            category.Description,
                            category.Questionnaire.Name,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\"href=\"/Categories/Edit/"+category.Id+"\"><span id=\""+category.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Categories/Details/"+category.Id+"\"><span id=\""+category.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+category.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Category>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int questionnaire_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Category> categories = GetByQuestionnaire(questionnaire_id);
            categories = JqGrid<Category>.GetFilteredContent(sidx, sord, page, rows, filters, categories, ref totalPages, ref totalRecords);
            var rowsModel = (
                from category in categories.ToList()
                select new
                {
                    i = category.Id,
                    cell = new string[] { category.Id.ToString(), 
                            "<a href=\"/Categories/Edit/"+category.Id+"\">" + 
                            category.Name + "</a>",
                            category.Description,
                            category.Questionnaire.Name,
                            category.CategoryGroup_Id.HasValue ? category.GroupingCategory.Name : "-",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Categories/Edit/"+category.Id+"\"><span id=\""+category.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Categories/Details/"+category.Id+"\"><span id=\""+category.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+category.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Category>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public bool AddCategoryTemplate(int template_id, int new_questionnaire_id, User user)
        {
            Category[] categories = new Category[GetCategoriesCount(template_id)];
            categories = GetCategories(template_id);
            foreach (var category in categories)
            {
                Category new_category = new Category();
                new_category.CreationDate = DateTime.Now;
                new_category.Description = category.Description;
                new_category.Name = category.Name;
                new_category.Questionnaire_Id = new_questionnaire_id;
                new_category.User_Id = user.Id;
                if (this.Add(new_category)) {
                    if (!(new QuestionsServices().AddQuestionsTemplate(category.Id, new_category.Id, user)))
                    {
                        return false;
                    }
                }   
            }
            return true;
        }
    }
}
