using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class RegionsServices : IRepositoryServices<Region>
    {
        private IRepository<Region> _repository;

        public RegionsServices()
            : this(new Repository<Region>())
        {
        }

        public RegionsServices(IRepository<Region> repository)
        {
            _repository = repository;
        }

        public bool Add(Region entity)
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

        public IQueryable<Region> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Region> GetByCompany(int company_id)
        {
            return _repository.GetAllRecords().Where(c=>c.Company_Id == company_id);
        }

        public Region GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetRegionsForDropDownList(int company_id)
        {
            var regions = GetByCompany(company_id).OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var region in regions)
            {
                Dictionary.Add(region.Id, region.Name);
            }
            return Dictionary;
        }

        public Dictionary<int, string> GetRegionsForDropDownList()
        {
            var locations = GetAllRecords().OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var location in locations)
            {
                Dictionary.Add(location.Id, location.Name);
            }

            return Dictionary;
        }

        public int GetRegionCount(int company_id)
        {
            return this.GetAllRecords().Where(o => o.Company_Id == company_id).Count();
        }

        public bool IsNameDuplicated(int company_id, string name)
        {
            return GetByCompany(company_id).Where(r => r.Name == name).Count() > 0;
        }

        public object RequestList(int company_id, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Region> regions = GetByCompany(company_id);
            regions = JqGrid<Region>.GetFilteredContent(sidx, sord, page, rows, filters, regions, ref totalPages, ref totalRecords);
            var rowsModel = (
                from region in regions.ToList()
                select new
                {
                    i = region.Id,
                    cell = new string[] { region.Id.ToString(), 
                            "<a href=\"/Regions/Edit/"+region.Id+"\">" + 
                            region.Name + "</a>",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Regions/Edit/"+region.Id+"\"><span id=\""+region.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Regions/Details/"+region.Id+"\"><span id=\""+region.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+region.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Region>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}
