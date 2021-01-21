using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class FunctionalOrganizationsServices : IRepositoryServices<FunctionalOrganization>
    {
        private IRepository<FunctionalOrganization> _repository;

        public FunctionalOrganizationsServices()
            : this(new Repository<FunctionalOrganization>())
        {
        }

        public FunctionalOrganizationsServices(IRepository<FunctionalOrganization> repository)
        {
            _repository = repository;
        }

        public bool Add(FunctionalOrganization entity)
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

        public IQueryable<FunctionalOrganization> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<FunctionalOrganization> GetByCompany(int? company_id)
        {
            return _repository.GetAllRecords().Where(c => c.FunctionalOrganizationType.Company_Id == company_id);
        }

        public FunctionalOrganization GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int GetFunctionalOrganizationsCount(int type_id)
        {
            return this.GetAllRecords().Where(o => o.Type_Id==type_id).Count();
        }

        public Dictionary<int, string> GetFunctionalOrganizationForDropDownList(int type_id)
        {
            var functionalOrganizations = GetByType(type_id).OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var functionalOrganization in functionalOrganizations)
            {
                Dictionary.Add(functionalOrganization.Id, functionalOrganization.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetFunctionalOrganizationsByTypeForDropDownList(int type_id)
        {
            var functionalOrganizations = GetByType(type_id).OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var functionalOrganization in functionalOrganizations)
            {
                Dictionary.Add(functionalOrganization.Id, functionalOrganization.Name);
            }
            return Dictionary;
        }

        public Dictionary<string, string> GetFunctionalOrganizationsByTypeForDropDownListWithKeyString(int type_id)
        {
            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            foreach (var functionalOrganization in GetFunctionalOrganizationsByTypeForDropDownList(type_id))
            {
                Dictionary.Add(functionalOrganization.Key.ToString(), functionalOrganization.Value);
            }
            return Dictionary;
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public IQueryable<FunctionalOrganization> GetByType(int type_id)
        {
            return _repository.GetAllRecords().Where(c => c.Type_Id == type_id);
        }

        public bool IsNameDuplicated(int type_id, string name)
        {
            return GetByType(type_id).Where(d => d.Name == name).Count() > 0;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int type_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<FunctionalOrganization> functionalOrganizations = GetByType(type_id);
            functionalOrganizations = JqGrid<FunctionalOrganization>.GetFilteredContent(sidx, sord, page, rows, filters, functionalOrganizations, ref totalPages, ref totalRecords);
            var rowsModel = (
                from functionalOrganization in functionalOrganizations.ToList()
                select new
                {
                    i = functionalOrganization.Id,
                    cell = new string[] { functionalOrganization.Id.ToString(), 
                            "<a href=\"/FunctionalOrganizations/Edit/"+functionalOrganization.Id+"\">" + 
                            functionalOrganization.Name + "</a>",
                            functionalOrganization.ShortName,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/FunctionalOrganizations/Edit/"+functionalOrganization.Id+"\"><span id=\""+functionalOrganization.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/FunctionalOrganizations/Details/"+functionalOrganization.Id+"\"><span id=\""+functionalOrganization.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+functionalOrganization.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<FunctionalOrganization>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
