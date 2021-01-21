using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class PerformanceEvaluationsServices : IRepositoryServices<PerformanceEvaluation>
    {

        private IRepository<PerformanceEvaluation> _repository;

        public PerformanceEvaluationsServices()
            : this(new Repository<PerformanceEvaluation>())
        {
        }

        public PerformanceEvaluationsServices(IRepository<PerformanceEvaluation> repository)
        {
            _repository = repository;
        }

        public bool Add(PerformanceEvaluation entity)
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

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public IQueryable<PerformanceEvaluation> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public PerformanceEvaluation GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<PerformanceEvaluation> GetByCompany(int? company_id)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id).OrderBy(l => l.Level);
        }

        public int GetPerformanceCount(int company_id)
        {
            return this.GetAllRecords().Where(o => o.Company_Id == company_id).Count();
        }

        public Dictionary<int, string> GetPerformanceEvaluationsForDropDownList(int? company_id)
        {
            var performanceEvaluations = GetByCompany(company_id);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var performanceEvaluation in performanceEvaluations)
            {
                Dictionary.Add(performanceEvaluation.Id, performanceEvaluation.Name);
            }

            return Dictionary;
        }

        public bool IsNameDuplicated(string name, int company_id)
        {
            PerformanceEvaluation performanceEvaluation = GetByCompany(company_id).Where(r => r.Name == name).FirstOrDefault();
            return performanceEvaluation != null;
        }

        public bool IsLevelDuplicated(int company_id, int level)
        {
            return GetByCompany(company_id).Where(d => d.Level == level).Count() > 0;
        }


        public object RequestList(string sidx, string sord, int page, int rows, string filters, int company_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<PerformanceEvaluation> performanceEvaluations = GetByCompany(company_id);
            performanceEvaluations = JqGrid<PerformanceEvaluation>.GetFilteredContent(sidx, sord, page, rows, filters, performanceEvaluations, ref totalPages, ref totalRecords);
            var rowsModel = (
                from performanceEvaluation in performanceEvaluations.ToList()
                select new
                {
                    i = performanceEvaluation.Id,
                    cell = new string[] { performanceEvaluation.Id.ToString(), 
                            "<a href=\"/PerformanceEvaluations/Edit/"+performanceEvaluation.Id+"\">" + 
                            performanceEvaluation.Name + "</a>",
                            performanceEvaluation.ShortName,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/PerformanceEvaluations/Edit/"+performanceEvaluation.Id+"\"><span id=\""+performanceEvaluation.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/PerformanceEvaluations/Details/"+performanceEvaluation.Id+"\"><span id=\""+performanceEvaluation.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\"href=\"#\"><span id=\""+performanceEvaluation.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();
            return JqGrid<PerformanceEvaluation>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }
    }
}
