using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class StatesServices : IRepositoryServices<State>
    {
        private IRepository<State> _repository;

        public StatesServices()
            : this(new Repository<State>())
        {
        }

        public StatesServices(IRepository<State> repository)
        {
            _repository = repository;
        }

        public bool Add(State entity)
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

        public IQueryable<State> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public IQueryable<State> GetByCountry(int country_id)
        {
            return _repository.GetAllRecords().Where(c=>c.Country_Id == country_id);
        }

        public State GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetStatesForDropDownList(int country_id)
        {
            var states = GetByCountry(country_id).OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var state in states)
            {
                Dictionary.Add(state.Id, state.Name);
            }
            return Dictionary;
        }

        public Dictionary<int, string> GetStatesForDropDownList()
        {
            var states = GetAllRecords().OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var state in states)
            {
                Dictionary.Add(state.Id, state.Name);
            }

            return Dictionary;
        }

        public bool IsNameDuplicated(int country_id, string name)
        {
            return GetByCountry(country_id).Where(r => r.Name == name).Count() > 0;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<State> states = GetAllRecords();
            states = JqGrid<State>.GetFilteredContent(sidx, sord, page, rows, filters, states, ref totalPages, ref totalRecords);
            var rowsModel = (
                from state in states.ToList()
                select new
                {
                    i = state.Id,
                    cell = new string[] { state.Id.ToString(), 
                            "<a href=\"/States/Edit/"+state.Id+"\">" + 
                            state.Name + "</a>",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/States/Edit/"+state.Id+"\"><span id=\""+state.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/States/Details/"+state.Id+"\"><span id=\""+state.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+state.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<State>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public object RequestList(int country_id, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<State> states = GetByCountry(country_id);
            states = JqGrid<State>.GetFilteredContent(sidx, sord, page, rows, filters, states, ref totalPages, ref totalRecords);
            var rowsModel = (
                from state in states.ToList()
                select new
                {
                    i = state.Id,
                    cell = new string[] { state.Id.ToString(), 
                            "<a href=\"/States/Edit/"+state.Id+"\">" + 
                            state.Name + "</a>",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/States/Edit/"+state.Id+"\"><span id=\""+state.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/States/Details/"+state.Id+"\"><span id=\""+state.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+state.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<State>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}
