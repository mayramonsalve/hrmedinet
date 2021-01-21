using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class FunctionalOrganizationTypesServices : IRepositoryServices<FunctionalOrganizationType>
    {
        private IRepository<FunctionalOrganizationType> _repository;

        public FunctionalOrganizationTypesServices()
            : this(new Repository<FunctionalOrganizationType>())
        {
        }

        public FunctionalOrganizationTypesServices(IRepository<FunctionalOrganizationType> repository)
        {
            _repository = repository;
        }

        public bool Add(FunctionalOrganizationType entity)
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

        public IQueryable<FunctionalOrganizationType> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<FunctionalOrganizationType> OrderByCompany(IQueryable<FunctionalOrganizationType> list)
        {
            List<FunctionalOrganizationType> orderedTypes = new List<FunctionalOrganizationType>();
            orderedTypes.Add(list.Where(p => p.FOTParent_Id == null).FirstOrDefault());
            foreach (FunctionalOrganizationType fot in list.Where(p => p.FOTParent_Id != null))
            {
                orderedTypes.Add(fot);
            }
            return orderedTypes.AsQueryable();
        }

        public Dictionary<int, string> OrderDictionaryByCompany(Dictionary<int, string> list)
        {
            Dictionary<int, string> orderedTypes = new Dictionary<int,string>();
            FunctionalOrganizationType FOT = GetByCompany(GetById(list.Keys.FirstOrDefault()).Company_Id).Where(p => p.FOTParent_Id == null).FirstOrDefault();
            orderedTypes.Add(FOT.Id, FOT.Name);
            foreach (int key in list.Keys)
            {
                if(key != FOT.Id)
                    orderedTypes.Add(key, list[key]);
            }
            return orderedTypes;
        }

        public Dictionary<int, string> GetChildrenByParent(int parent_id)
        {
            Dictionary<int, string> children = new Dictionary<int, string>();
            foreach(FunctionalOrganizationType fot in GetAllRecords().Where(c => c.FOTParent_Id == parent_id))
            {
                children.Add(fot.Id, fot.Name);
            }
            return children;
        }

        public IQueryable<FunctionalOrganizationType> GetByCompany(int? company_id)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id);
        }

        public IQueryable<FunctionalOrganizationType> GetByCompanyToDoTest(int? company_id)
        {
            return GetByCompany(company_id).Where(fot => !fot.FOTParent_Id.HasValue);
        }

        public FunctionalOrganizationType GetByName(int? company_id, string FO_name)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id && c.Name==FO_name).FirstOrDefault();
        }
        public FunctionalOrganizationType GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int GetFunctionalOrganizationTypesCount(int? company_id)
        {
            return this.GetAllRecords().Where(o => o.Company_Id == company_id).Count();
        }

        public Dictionary<int, string> GetFunctionalOrganizationTypesForDropDownList(int? company_id)
        {
            var  types = GetByCompany(company_id).OrderBy(d => d.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var type in types)
            {
                Dictionary.Add(type.Id, type.Name);
            }

            return Dictionary;
        }

        public void GetChildren(FunctionalOrganizationType type, List<FunctionalOrganizationType> children)
        {
            foreach (FunctionalOrganizationType child in type.FunctionalOrganizationTypes.OrderBy(d => d.Name))
            {
                children.Add(child);
            }
        }

        public Dictionary<int, string> GetFunctionalOrganizationTypesChildrenByTypeForDropDownList(int type_id)
        {
            List<FunctionalOrganizationType> types = new List<FunctionalOrganizationType>();
            FunctionalOrganizationType type = GetById(type_id);
            GetChildren(type, types);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var child in types)
            {
                Dictionary.Add(child.Id, child.Name);
            }

            return Dictionary;
        }

        public String[] GetFunctionalOrganizationTypes(int? company_id)
        {
            String[] FO = new String [this.GetFunctionalOrganizationTypesCount(company_id)];
            var types = GetByCompany(company_id).OrderBy(d => d.Name);
            int pos = 0;
            foreach (var type in types)
            {
                FO[pos] = type.Name;
                pos++;
            }
            return FO;
        }

        public bool IsNameDuplicated(int company_id, string name)
        {
            return GetByCompany(company_id).Where(d => d.Name == name).Count() > 0;
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int company_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<FunctionalOrganizationType> types = GetByCompany(company_id);
            types = JqGrid<FunctionalOrganizationType>.GetFilteredContent(sidx, sord, page, rows, filters, types, ref totalPages, ref totalRecords);
            var rowsModel = (
                from type in types.ToList()
                select new
                {
                    i = type.Id,
                    cell = new string[] { type.Id.ToString(), 
                            "<a href=\"/FunctionalOrganizationTypes/Edit/"+type.Id+"\">" + 
                            type.Name + "</a>",
                            type.ShortName,
                            type.Parent != null ? type.Parent.Name : "-",
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/FunctionalOrganizationTypes/Edit/"+type.Id+"\"><span id=\""+type.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/FunctionalOrganizationTypes/Details/"+type.Id+"\"><span id=\""+type.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+type.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<FunctionalOrganizationType>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
