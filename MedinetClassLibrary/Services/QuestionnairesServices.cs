using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;
using System.Web;

namespace MedinetClassLibrary.Services
{
    public class QuestionnairesServices : IRepositoryServices<Questionnaire>
    {
        private IRepository<Questionnaire> _repository;

        public QuestionnairesServices()
            : this(new Repository<Questionnaire>())
        {
        }

        public QuestionnairesServices(IRepository<Questionnaire> repository)
        {
            _repository = repository;
        }

        public bool Add(Questionnaire entity)
        {
            try
            {
                entity.CreationDate = DateTime.Now;
                _repository.Add(entity);
                _repository.SaveChanges();
                return true;
            }
            catch(Exception ex)
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

        public IQueryable<Questionnaire> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Questionnaire> GetQuestionnairesByCompany(int company_id)//////*****return _repository.GetAllRecords().Where(q => q.User.Company_Id == company_id);
        {

            //if (user.Role.Name == "HRCompany")//user.Role.Name == "HRCompany"
            //{
            //    return _repository.GetAllRecords().Where(q => q.User.Company_Id == company_id || q.Template == true);
            //}
            //else
            //{
            //    return _repository.GetAllRecords().Where(q => (q.User.Company_Id == company_id) || q.Template == true);
            //}
            return _repository.GetAllRecords().Where(q => q.User.Company_Id == company_id);
                                
        }
      
        public IQueryable<Questionnaire> GetQuestionnairesForCustomer(Company company)
        {
            return _repository.GetAllRecords().Where(q => q.User.Company_Id == company.Id ||
                (q.User.Company_Id == company.CompanyAssociated_Id && q.Template==true));
        }

        public IQueryable<Questionnaire> GetQuestionnairesForAssociated(int company_id)
        {
            return _repository.GetAllRecords().Where(q => q.User.Company_Id == company_id ||
                q.User.Company.CompanyAssociated_Id == company_id);
        }

        public IQueryable<Questionnaire> GetTemplatesByAssociated(int company_id)/////////////*****
        {
            return _repository.GetAllRecords().Where(q => (q.User.Company.CompanyAssociated_Id == company_id &&
                q.Template == true) || (q.User.Company.CompaniesType.Name.ToLower() == "owner" &&
                q.Template == true));
        }

        public Dictionary<int, string> GetTemplatesByAssociatedForDropDownList(int company_id)/////////////
        {
            var questionnaires = GetTemplatesByAssociated(company_id).OrderBy(p => p.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var questionnaire in questionnaires)
            {
                Dictionary.Add(questionnaire.Id, questionnaire.Name);
            }

            return Dictionary;
        }

        private IQueryable<Questionnaire> GetQuestionnairesByCustomerForAssociated(int companyAsso_id)
        {
            return GetAllRecords().Where(q => q.User.Company.CompanyAssociated_Id == companyAsso_id);
        }

        private IQueryable<Questionnaire> GetCustomersQuestionnaires(int company_id)
        {
            return GetAllRecords().Where(q => q.Tests.Where(c => c.Company_Id == company_id).Count() > 0);
        }

        public Dictionary<int, string> GetQuestionnairesByUCompanyForRanking(Company company, bool hradmin)
        {
            if (hradmin)
            {
                IQueryable<Questionnaire> questionnairesA = GetTemplatesByAssociated(company.Id).OrderBy(p => p.Name).Where(t => t.Tests.Select(e => e.Evaluations).Count() > 0);
                IQueryable<Questionnaire> questionnairesC = GetQuestionnairesByCustomerForAssociated(company.Id).OrderBy(p => p.Name).Where(t => t.Tests.Select(e => e.Evaluations).Count() > 0);
                return JoinDictionaries(questionnairesA, questionnairesC);
            }
            else
            {
                IQueryable<Questionnaire> questionnaires = GetQuestionnairesForCustomer(company).Where(t => t.Tests.Where(c => c.Company_Id == company.Id).Count() > 0).OrderBy(p => p.Name).Where(t => t.Tests.Select(e => e.Evaluations).Count() > 0);
                return ConvertIQueryableToDictionary(questionnaires);
            }
        }

        public Dictionary<int, string> GetQuestionnairesByAssociatedAndItsCustomersForDropDownList(int company_id)
        {
            IQueryable<Questionnaire> questionnairesA = GetTemplatesByAssociated(company_id).OrderBy(p => p.Name);
            IQueryable<Questionnaire> questionnairesC = GetQuestionnairesByCustomerForAssociated(company_id).OrderBy(p => p.Name);
            return JoinDictionaries(questionnairesA, questionnairesC);
        }

        private Dictionary<int, string> JoinDictionaries(IQueryable<Questionnaire> questionnairesA, IQueryable<Questionnaire> questionnairesC)
        {
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var questionnaire in questionnairesA)
            {
                Dictionary.Add(questionnaire.Id, questionnaire.Name);
            }
            foreach (var questionnaire in questionnairesC)
            {
                Dictionary.Add(questionnaire.Id, questionnaire.Name);
            }
            return Dictionary;
        }

        public Dictionary<int, string> GetQuestionnairesForCustomerForDropDownList(Company company)
        {
            var questionnaires = GetQuestionnairesForCustomer(company).OrderBy(p => p.Name);
            return ConvertIQueryableToDictionary(questionnaires);
        }

        private Dictionary<int, string> ConvertIQueryableToDictionary(IQueryable<Questionnaire> questionnaires)
        {
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (Questionnaire questionnaire in questionnaires)
            {
                    Dictionary.Add(questionnaire.Id, questionnaire.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetQuestionnairesForCustomerForDropDownList(int id_company, User user)//parametro id_company
        {
            /* 
             * //MAYRA
             var questionnaires = GetQuestionnairesByCompany(id_company).OrderBy(p => p.Name);
             //var questionnaires = GetAllRecords().OrderBy(p => p.Name);
             Dictionary<int, string> Dictionary = new Dictionary<int, string>();
             foreach (var questionnaire in questionnaires)
             {
                 Dictionary.Add(questionnaire.Id, questionnaire.Name);
             }
            
             return Dictionary;
             */
            //var questionnaires = _repository.GetAllRecords().Where(q => q.User.Company_Id == id_company);

            //List<Questionnaire> question = new List<Questionnaire>();

            //question = _repository.GetAllRecords().Where(q => q.User.Company_Id == id_company).ToList();


            var questionnaires = GetQuestionnairesByCompany(id_company).OrderBy(p => p.Id);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();

            int num = questionnaires.Count();

            //if (num == 0)
           // {
            //    questionnaires = GetQuestionnairesByCompany(user.Company_Id).OrderBy(p => p.Id);
           //     foreach (var questionnaire in questionnaires)
            //    {
            //        Dictionary.Add(questionnaire.Id, questionnaire.Name);
            //    }
           // }
           // else {                
                foreach (var questionnaire in questionnaires)
                {
                    Dictionary.Add(questionnaire.Id, questionnaire.Name);
                }

          //  }
           
            return Dictionary;
        }
        public Dictionary<int, string> GetQuestionnairesForCustomerForDropDownList2(int id_company, User user)//parametro id_company
        {


            var questionnaires = GetQuestionnairesByCompany(id_company).OrderBy(p => p.Id);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();

            int num = questionnaires.Count();

            if (num == 0)
             {
                questionnaires = GetQuestionnairesByCompany(user.Company_Id).OrderBy(p => p.Id);
                 foreach (var questionnaire in questionnaires)
                {
                    Dictionary.Add(questionnaire.Id, questionnaire.Name);
                }
             }
             else {                
                    foreach (var questionnaire in questionnaires)
                    {
                        Dictionary.Add(questionnaire.Id, questionnaire.Name);
                    }

              }

            return Dictionary;
        }
      
        public Questionnaire GetById(int id)
        {
            return _repository.GetById(id);
        }

        public string GetTemplateString(bool template)
        {
            if(template)
                return ViewRes.Classes.Services.True;
            else
                return ViewRes.Classes.Services.False;
        }

        public int GetQuestionsCount(int questionnaire_id)
        {
            int count = 0;
            Questionnaire questionnaire = GetById(questionnaire_id);
            foreach (Category category in questionnaire.Categories)
            {
                count += category.Questions.Count;
            }
            return count;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Questionnaire> questionnaires = GetAllRecords();
            questionnaires = JqGrid<Questionnaire>.GetFilteredContent(sidx, sord, page, rows, filters, questionnaires, ref totalPages, ref totalRecords);
            var rowsModel = (
                from questionnaire in questionnaires.ToList()
                select new
                {
                    i = questionnaire.Id,
                    cell = new string[] { questionnaire.Id.ToString(), 
                            "<a href=\"/Questionnaires/Edit/"+questionnaire.Id+"\">" + 
                            questionnaire.Name + "</a>",
                            questionnaire.Description,
                            GetTemplateString(questionnaire.Template),
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Questionnaires/Edit/"+questionnaire.Id+"\"><span id=\""+questionnaire.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Questionnaires/Details/"+questionnaire.Id+"\"><span id=\""+questionnaire.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+questionnaire.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Questionnaire>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public object RequestList(User userLogged, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;
            IQueryable<Questionnaire> questionnaires;

            if (userLogged.Role.Name == "HRAdministrator")
                questionnaires = GetQuestionnairesForAssociated(userLogged.Company_Id);
            else
                questionnaires = GetQuestionnairesForCustomer(userLogged.Company);
            
            questionnaires = JqGrid<Questionnaire>.GetFilteredContent(sidx, sord, page, rows, filters, questionnaires, ref totalPages, ref totalRecords);
            var rowsModel = (
                from questionnaire in questionnaires.ToList()
                select new
                {
                    i = questionnaire.Id,
                    cell = new string[] { questionnaire.Id.ToString(), 
                            "<a href=\"/Questionnaires/Edit/"+questionnaire.Id+"\">" + 
                            questionnaire.Name + "</a>",
                            questionnaire.Description,
                            GetTemplateString(questionnaire.Template),
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Questionnaires/Edit/"+questionnaire.Id+"\"><span id=\""+questionnaire.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Questionnaires/Details/"+questionnaire.Id+"\"><span id=\""+questionnaire.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+questionnaire.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Questionnaire>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public bool IsNameDuplicated(int company_id, string name)
        {
            Questionnaire questionnaire = GetQuestionnairesByCompany(company_id).Where(q => q.Name == name).FirstOrDefault();
            return questionnaire != null;
        }

    }
}
