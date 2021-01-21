using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class TestsServices : IRepositoryServices<Test>
    {
        private IRepository<Test> _repository;

        public TestsServices()
            : this(new Repository<Test>())
        {
        }

        public TestsServices(IRepository<Test> repository)
        {
            _repository = repository;
        }

        public bool Add(Test entity)
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

        public IQueryable<Test> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Dictionary<int, string> GetTestsForDropDownList()
        {
            var tests = GetAllRecords().OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var test in tests)
            {
                Dictionary.Add(test.Id, test.Name);
            }
            return Dictionary;
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public IQueryable<Test> GetTestsByAssociated(int company_id)
        {
            return _repository.GetAllRecords().Where(t => t.Company_Id == company_id ||
                t.Company.CompanyAssociated_Id == company_id);
        }

        public IQueryable<Test> GetTestsByCompany(int company_id)
        {
            return _repository.GetAllRecords().Where(t => t.Company_Id == company_id);
        }

        public Dictionary<int, string> GetTestsByCompanyForDropDownList(int company_id)
        {
            var tests = GetAllRecords().Where(o => o.Company_Id == company_id).OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var test in tests)
            {
                Dictionary.Add(test.Id, test.Name);
            }
            return Dictionary;
        }

        public Dictionary<int, string> GetTestsByUserForDropDownList(User user, bool questionnaire)
        {
            IQueryable<Test> tests = GetAllRecords().Where(t => (t.Company_Id == user.Company_Id ||
                t.Company.CompanyAssociated_Id == user.Company_Id)).OrderBy(d => d.Name);
            if (!questionnaire)
                tests = tests.Where(t => t.Questionnaire_Id.HasValue == false);
                //&& (t.Questionnaire_Id.HasValue == questionnaire));
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var test in tests)
            {
                Dictionary.Add(test.Id, test.Name);
            }
            return Dictionary;
        }
        public Test GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Test GetByCode(string code)
        {
            return _repository.GetAllRecords().Where(c => c.Code == code).SingleOrDefault();
        }

        public IQueryable<Test> GetByCompany(int company_id)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id);
        }

        public Dictionary<int, string> GetNotStartedByCompany(int company_id, int? test_id)
        {
            Dictionary<int, string> tests = new Dictionary<int, string>();
            IQueryable<Test> byCompany = GetByCompany(company_id).Where(d => d.CurrentEvaluations == 0);
            List<int> previousT = byCompany.Where(p => p.PreviousTest_Id.HasValue).Select(p => p.PreviousTest_Id.Value).ToList();
            foreach (Test test in byCompany.Where(t => !previousT.Contains(t.Id) && t.Id != test_id))
            {
                tests.Add(test.Id, test.Name);
            }
            return tests;
        }

        public IQueryable<Test> GetByQuestionnaire(int questionnaire_id)
        {
            return _repository.GetAllRecords().Where(c => c.Questionnaire_Id == questionnaire_id);
        }

        public Test GetLastTestByCompany(int company_id, int? questionnaire_id, int? country_id)
        { //t => t.CreationDate < Convert.ToDateTime(DateTime.Now.ToString(ViewRes.Views.Shared.Shared.Date))
            if (questionnaire_id.HasValue)
            {
                if(country_id.HasValue)
                    return GetByCompany(company_id).Where(t => t.Evaluations.Count > 0 && t.Evaluations.Select(l => l.Location.State.Country_Id == country_id.Value).Count() > 0 && t.Questionnaire_Id == questionnaire_id.Value).OrderByDescending(t => t.EndDate).FirstOrDefault();
                else
                    return GetByCompany(company_id).Where(t => t.Evaluations.Count > 0  && t.Questionnaire_Id == questionnaire_id.Value).OrderByDescending(t => t.EndDate).FirstOrDefault();
            }
            else
            {
                if (country_id.HasValue)
                    return GetByCompany(company_id).Where(t => t.Evaluations.Count > 0 && t.Evaluations.Select(l => l.Location.State.Country_Id == country_id.Value).Count() > 0 && t.Questionnaire_Id == questionnaire_id.Value).OrderByDescending(t => t.EndDate).FirstOrDefault();
                else
                    return GetByCompany(company_id).Where(t => t.Evaluations.Count > 0).OrderByDescending(t => t.EndDate).FirstOrDefault();
            }
        }

        public Test GetLastTestByCompanyAndCountry(int company_id, int country_id)
        { 
            IQueryable<Test> tests = GetByCompany(company_id).Where(t => t.Evaluations.Count > 0 && !t.PreviousTest_Id.HasValue).OrderByDescending(t => t.EndDate);
            tests = tests.Where(e => e.Evaluations.Any(l => l.Location.State.Country_Id == country_id));
            return tests.FirstOrDefault();
        }
        public Test GetLastTestByCompanyAndState(int company_id, int state_id)
        {
            IQueryable<Test> tests = GetByCompany(company_id).Where(t => t.Evaluations.Count > 0 && !t.PreviousTest_Id.HasValue).OrderByDescending(t => t.EndDate);
            tests = tests.Where(e => e.Evaluations.Any(l => l.Location.State_Id == state_id));
            return tests.FirstOrDefault();
        }
        public Test GetLastTestByCompanyAndBranch(int company_id, int branch_id)
        {
            IQueryable<Test> tests = GetByCompany(company_id).Where(t => t.Evaluations.Count > 0 && !t.PreviousTest_Id.HasValue).OrderByDescending(t => t.EndDate);
            tests = tests.Where(e => e.Evaluations.Any(l => l.Location_Id == branch_id));
            return tests.FirstOrDefault();
        }

        public bool IsNameDuplicated(int company_id, string name)
        {
            return GetByCompany(company_id).Where(r => r.Name == name).Count() > 0;
        }

        public bool IsIPDuplicated(Test test, string MAC)
        {
            return test.Evaluations.Where(ip => ip.RemoteHostName == MAC).Count() > 0;
        }

        public bool IsMinimumInvalid(int minimum, int total)
        {
            return minimum >= total ? true : false;
        }

        public object RequestList(int? company_id, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;
            IQueryable<Test> tests = GetByCompany((int)company_id);
            tests = JqGrid<Test>.GetFilteredContent(sidx, sord, page, rows, filters, tests, ref totalPages, ref totalRecords);
            var rowsModel = (
                from test in tests.ToList()
                select new
                {
                    i = test.Id, 
                    cell = new string[] { test.Id.ToString(), 
                            "<a href=\"/Tests/Edit/"+test.Id+"\">" + 
                            test.Name + "</a>",
                            test.Code,
                            test.Questionnaire_Id.HasValue ? test.Questionnaire.Name : "-",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Tests/Edit/"+test.Id+"\"><span id=\""+test.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            test.Finished ? "" :
                            "<a title=\""+ViewRes.Classes.Services.Finish+"\" href=\"/Tests/Finish/"+test.Id+"\"><span id=\""+test.Id+"\" class=\"ui-icon ui-icon-locked\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Tests/Details/"+test.Id+"\"><span id=\""+test.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Reports+"\" href=\"/ChartReports/ReportsList\"><span id=\""+test.Id+"\" class=\"ui-icon ui-icon-document\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Print+"\" href=\"/Evaluations/GetMyPdf/"+test.Id+"\"><span id=\""+test.Id+"\" class=\"ui-icon ui-icon-print\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+test.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();
            return JqGrid<Test>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

       
        public void IncreaseCurrentEvaluationsAndDecreaseEvaluationsLeft(int test_id)
        {
            Test test = GetById(test_id);
            test.CurrentEvaluations = test.CurrentEvaluations + 1;
            test.EvaluationsLefts = test.EvaluationsLefts - 1;
            SaveChanges();
        }

        private IQueryable<Test> GetTestsToCompare(Test test)
        {
            IQueryable<Test> tests = _repository.GetAllRecords().Where(t => t.Company_Id == test.Company_Id
                && t.Questionnaire_Id == test.Questionnaire_Id && t.Id != test.Id
                && t.Weighted == test.Weighted && t.Evaluations.Count > 0);
            List<Test> equalTests = new List<Test>();
            //if (test.Weighted)
            //{
            //    bool equal;
            //    foreach (Test t in tests)
            //    {
            //        equal = true;
            //        foreach (Category cat in test.Questionnaire.Categories)
            //        {
            //            equal = equal && t.Weighings.Select(w => w.Value) == test.Weighings.Select(w => w.Value);
            //        }
            //        if (equal)
            //            equalTests.Add(t);
            //    }
            //    return equalTests.AsQueryable();
            //}
            //else
                return tests;
        }

        public List<Test> GetTestsToFinish()
        {
            List<Test> tests = _repository.GetAllRecords().Where(t => t.ExecutiveReports.Count == 0 &&
                                                                        (t.Finished)).ToList();// || DateTime.Now > t.EndDate)).ToList();
            return tests;
        }

        public Dictionary<int, string> GetTestsToCompareForDropdownist(int test)
        {
            IQueryable<Test> tests = GetTestsToCompare(GetById(test));
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (Test t in tests)
            {
                Dictionary.Add(t.Id, t.Name);
            }
            return Dictionary;
        }

        public void AddWeeksAndEmployeesToTest(int test_id, int weeks, int employees)
        {
            Test test = GetById(test_id);
            test.EndDate = test.EndDate.AddDays(weeks * 7);
            test.EvaluationNumber = employees;
            test.EvaluationsLefts = employees - test.CurrentEvaluations;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
