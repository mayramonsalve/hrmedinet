using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class CountriesServices : IRepositoryServices<Country>
    {
        private IRepository<Country> _repository;

        public CountriesServices()
            : this(new Repository<Country>())
        {
        }

        public CountriesServices(IRepository<Country> repository)
        {
            _repository = repository;
        }

        public bool Add(Country entity)
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

        public IQueryable<Country> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Country GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Country GetByName(string countryName) {
            if (this.IsNameDuplicated(countryName))
                return _repository.GetAllRecords().Where(o => o.Name == countryName).Single();
            else
                return null;
        }

        public Dictionary<int, string> GetCountriesForDropDownList()
        {
            var countries = _repository.GetAllRecords().OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var country in countries)
            {
                Dictionary.Add(country.Id, country.Name);
            }

            return Dictionary;
        }

        public bool IsNameDuplicated(string name)
        {
            return GetAllRecords().Where(c => c.Name == name).Count()>0;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Country> countries = GetAllRecords();
            countries = JqGrid<Country>.GetFilteredContent(sidx, sord, page, rows, filters, countries, ref totalPages, ref totalRecords);
            var rowsModel = (
                from country in countries.ToList()
                select new
                {
                    i = country.Id,
                    cell = new string[] { country.Id.ToString(), 
                            "<a href=\"/Countries/Edit/"+country.Id+"\">" + 
                            country.Name + "</a>",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/Countries/Edit/"+country.Id+"\"><span id=\""+country.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Countries/Details/"+country.Id+"\"><span id=\""+country.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\"href=\"#\"><span id=\""+country.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<Country>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public List<int> GetCountriesByTest(Test Test)
        {
            List<int> countriesId = new List<int>();
            bool location = Test.DemographicsInTests.Where(d => d.Demographic.Name == "Location").Count() > 0;
            if (location)
            {
                try
                {
                    countriesId = Test.Evaluations.Select(l => l.Location.State.Country_Id).Distinct().ToList();
                }
                catch
                {

                }
            }
            return countriesId;
        }

    }
}
