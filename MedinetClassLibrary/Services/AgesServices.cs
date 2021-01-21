using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class AgesServices: IRepositoryServices<Age>
    {
        private IRepository<Age> _repository;

        public AgesServices()
            : this(new Repository<Age>())
        {
        }

        public AgesServices(IRepository<Age> repository)
        {
            _repository = repository;
        }

        public bool Add(Age entity)
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

        public IQueryable<Age> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Age> GetByCompany(int? company_id)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id).OrderBy(l => l.Level);
        }

        public Age GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int GetAgesCount(int company_id) {
            return this.GetAllRecords().Where(o => o.Company_Id == company_id).Count();
        }

        public Dictionary<int, string> GetAgesForDropDownList(int? company_id)
        {
            var  ages= GetByCompany(company_id);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var age in ages)
            {
                Dictionary.Add(age.Id, age.Name);
            }

            return Dictionary;
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

            IQueryable<Age> ages = GetByCompany(company_id);
            ages = JqGrid<Age>.GetFilteredContent(sidx, sord, page, rows, filters, ages, ref totalPages, ref totalRecords);
            var rowsModel = (
                from age in ages.ToList()
                select new
                {
                    i = age.Id,
                    cell = new string[] { age.Id.ToString(), 
                            "<a href=\"/Ages/Edit/"+age.Id+"\">" + 
                            age.Name + "</a>",
                            age.ShortName,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Ages/Edit/"+age.Id+"\"><span id=\""+age.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Ages/Details/"+age.Id+"\"><span id=\""+age.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+age.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Age>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
