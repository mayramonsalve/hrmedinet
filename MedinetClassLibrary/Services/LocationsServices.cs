using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class LocationsServices : IRepositoryServices<Location>
    {
        private IRepository<Location> _repository;

        public LocationsServices()
            : this(new Repository<Location>())
        {
        }

        public LocationsServices(IRepository<Location> repository)
        {
            _repository = repository;
        }

        public bool Add(Location entity)
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

        public IQueryable<Location> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<Location> GetByCompany(int? company_id)
        {
            return _repository.GetAllRecords().Where(r => r.Company_Id == company_id);
        } 

        public Dictionary<int, string> GetCountriesByCompanyForDropDownList(int company_id)
        {
            var countries = GetByCompany(company_id).Select(l => l.State.Country).Distinct().OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var country in countries)
            {
                Dictionary.Add(country.Id, country.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetStatesByCompanyForDropDownList(int company_id)
        {
            var states = GetByCompany(company_id).Select(l => l.State).Distinct().OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var state in states)
            {
                Dictionary.Add(state.Id, state.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetRegionsByCompanyForDropDownList(int company_id)
        {
            var regions = GetByCompany(company_id).Select(l => l.Region).Distinct().OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var region in regions)
            {
                Dictionary.Add(region.Id, region.Name);
            }

            return Dictionary;
        }

        public Location GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Location> GetByState(int state_id)
        {
            return _repository.GetAllRecords().Where(r=>r.State_Id == state_id);
        }

        public Dictionary<int, string> GetLocationsForDropDownList(int? company_id)
        {
            var locations = GetByCompany(company_id).OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var location in locations)
            {
                Dictionary.Add(location.Id, location.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetLocationsForDropDownList(int company_id, int state_id)
        {
            var locations = GetByCompany(company_id).Where(r=>r.State_Id==state_id).OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var location in locations)
            {
                Dictionary.Add(location.Id, location.Name);
            }

            return Dictionary;
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public int GetLocationCount(int company_id)
        {
            return this.GetAllRecords().Where(o => o.Company_Id == company_id).Count();
        }

        public int GetCountryCountByLocation(int company_id)
        {
            return this.GetAllRecords().Where(o => o.Company_Id == company_id).GroupBy(o => o.State.Country_Id).Count();
        }


        public bool IsNameDuplicated(int company_id, string location)
        {
            return GetByCompany(company_id).Where(l => l.Name == location).Count() > 0;
        }

        public object RequestList(int company_id, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Location> locations = GetByCompany(company_id);
            locations = JqGrid<Location>.GetFilteredContent(sidx, sord, page, rows, filters, locations, ref totalPages, ref totalRecords);
            var rowsModel = (
                from location in locations.ToList()
                select new
                {
                    i = location.Id,
                    cell = new string[] { location.Id.ToString(), 
                           "<a href=\"/Locations/Edit/"+location.Id+"\">" +
                            location.Name + "</a>",
                            location.ShortName,
                            location.Region != null ? location.Region.Name : "-",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Locations/Edit/"+location.Id+"\"><span id=\""+location.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Locations/Details/"+location.Id+"\"><span id=\""+location.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+location.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Location>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public object RequestList(int company_id, int state_id, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Location> locations = GetByState(state_id).Where(c => c.Company_Id == company_id);
            locations = JqGrid<Location>.GetFilteredContent(sidx, sord, page, rows, filters, locations, ref totalPages, ref totalRecords);
            var rowsModel = (
                from location in locations.ToList()
                select new
                {
                    i = location.Id,
                    cell = new string[] { location.Id.ToString(), 
                            "<a href=\"/Locations/Edit/"+location.Id+"\">" + 
                            location.Name + "</a>",
                            location.ShortName,
                            location.Region != null ? location.Region.Name : "-",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Locations/Edit/"+location.Id+"\"><span id=\""+location.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Locations/Details/"+location.Id+"\"><span id=\""+location.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+location.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Location>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public List<Country> GetCountriesByCompany(int company_id)
        {
            return GetAllRecords().Where(c => c.Company_Id == company_id).Select(c => c.State.Country).Distinct().ToList();
        }

        public List<State> GetStatesByCompany(int company_id, int country_id)
        {
            return GetAllRecords().Where(c => c.Company_Id == company_id && c.State.Country_Id == country_id).Select(c => c.State).Distinct().ToList();
        }

        public List<Location> GetBranchesByCompany(int company_id)
        {
            return GetAllRecords().Where(c => c.Company_Id == company_id).ToList();
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}
