using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class PositionLevelsServices: IRepositoryServices<PositionLevel>
    {
        private IRepository<PositionLevel> _repository;

        public PositionLevelsServices()
            : this(new Repository<PositionLevel>())
        {
        }

        public PositionLevelsServices(IRepository<PositionLevel> repository)
        {
            _repository = repository;
        }

        public bool Add(PositionLevel entity)
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

        public IQueryable<PositionLevel> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<PositionLevel> GetByCompany(int? company_id)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id).OrderByDescending(l => l.Level);
        }

        public PositionLevel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int GetPositionLevelsCount(int company_id)
        {
            return this.GetAllRecords().Where(o => o.Company_Id == company_id).Count();
        }

        public Dictionary<int, string> GetPositionLevelsForDropDownList(int? company_id)
        {
            var levels = GetByCompany(company_id);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var level in levels)
            {
                Dictionary.Add(level.Id, level.Name);
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

            IQueryable<PositionLevel> levels = GetByCompany(company_id);
            levels = JqGrid<PositionLevel>.GetFilteredContent(sidx, sord, page, rows, filters, levels, ref totalPages, ref totalRecords);
            var rowsModel = (
                from level in levels.ToList()
                select new
                {
                    i = level.Id,
                    cell = new string[] { level.Id.ToString(), 
                            "<a href=\"/PositionLevels/Edit/"+level.Id+"\">" + 
                            level.Name + "</a>",
                            level.ShortName,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/PositionLevels/Edit/"+level.Id+"\"><span id=\""+level.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/PositionLevels/Details/"+level.Id+"\"><span id=\""+level.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+level.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<PositionLevel>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public string GetPositionLevelNameByLevel(int level, int company)
        {
            return GetAllRecords().Where(l => l.Level == level && l.Company_Id == company).Select(n => n.Name).FirstOrDefault();
        }

    }
}
