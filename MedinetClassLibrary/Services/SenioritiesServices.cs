using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class SenioritiesServices: IRepositoryServices<Seniority>
    {
        private IRepository<Seniority> _repository;

        public SenioritiesServices()
            : this(new Repository<Seniority>())
        {
        }

        public SenioritiesServices(IRepository<Seniority> repository)
        {
            _repository = repository;
        }

        public bool Add(Seniority entity)
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

        public IQueryable<Seniority> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Seniority> GetByCompany(int? company_id)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id).OrderBy(l => l.Level);
        }

        public Seniority GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetSenioritiesForDropDownList(int? company_id)
        {
            var seniorities = GetByCompany(company_id);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var seniority in seniorities)
            {
                Dictionary.Add(seniority.Id, seniority.Name);
            }

            return Dictionary;
        }

        public int GetSeniorityCount(int company_id)
        {
            return this.GetAllRecords().Where(o => o.Company_Id == company_id).Count();
        }

        public bool IsNameDuplicated(int company_id, string name)
        {
            return GetByCompany(company_id).Where(d => d.Name == name).Count() > 0;
        }

        public bool IsLevelDuplicated(int company_id, int level)
        {
            return GetByCompany(company_id).Where(d => d.Level == level).Count() > 0;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int company_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Seniority> seniorities = GetByCompany(company_id);
            seniorities = JqGrid<Seniority>.GetFilteredContent(sidx, sord, page, rows, filters, seniorities, ref totalPages, ref totalRecords);
            var rowsModel = (
                from seniority in seniorities.ToList()
                select new
                {
                    i = seniority.Id,
                    cell = new string[] { seniority.Id.ToString(), 
                            "<a href=\"/Seniorities/Edit/"+seniority.Id+"\">" + 
                            seniority.Name + "</a>",
                            seniority.ShortName,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Seniorities/Edit/"+seniority.Id+"\"><span id=\""+seniority.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Seniorities/Details/"+seniority.Id+"\"><span id=\""+seniority.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+seniority.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Seniority>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
