using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class CompaniesServices //: IRepositoryServices<Company>
    {
        private IRepository<Company> _repository;

        public CompaniesServices()
            : this(new Repository<Company>())
        {
        }

        public CompaniesServices(IRepository<Company> repository)
        {
            _repository = repository;
        }

        public bool Add(Company entity)
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

        public IQueryable<Company> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Company GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Dictionary<int, string> GetCompaniesForDropDownList()
        {
            var companies = _repository.GetAllRecords().OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var company in companies)
            {
                Dictionary.Add(company.Id, company.Name);
            }

            return Dictionary;
        }

        public IQueryable<Company> GetCustomersByAssociated(int company_id)
        {
            return _repository.GetAllRecords().Where(c => c.CompanyAssociated_Id == company_id
                && c.CompaniesType.Name == "Customer");
        }

        public IQueryable<Company> GetCustomersBySector(int sector_id)
        {
            return _repository.GetAllRecords().Where(c => c.CompanySector_Id == sector_id
                && c.CompaniesType.Name == "Customer");
        }

        public IQueryable<Company> GetAssociatedCompanies()
        {
            return _repository.GetAllRecords().Where(c=> c.CompaniesType.Name == "Associated");
        }

        public Dictionary<int, string> GetCustomersByAssociatedAndQuestionnaireForDropDownList(int company_id, int questionnaire_id)
        {
            IQueryable<Company> companies = GetCustomersByAssociated(company_id).Where(t => t.Tests.Select(q => q.Questionnaire_Id == questionnaire_id).Count() > 0 ).OrderBy(c => c.Name);
            return ConvertIQueryableToDictionary(companies);
        }

        public Dictionary<int, string> GetCustomersByAssociatedForDropDownList(int company_id)
        {
            IQueryable<Company> companies = GetCustomersByAssociated(company_id).OrderBy(c => c.Name);
            return ConvertIQueryableToDictionary(companies);
        }

        private Dictionary<int, string> ConvertIQueryableToDictionary(IQueryable<Company> companies)
        {
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var company in companies)
            {
                Dictionary.Add(company.Id, company.Name);
            }
            return Dictionary;
        }

        public Dictionary<int, string> GetCustomersBySectorForDropDownList(int sector_id)
        {
            var companies = GetCustomersBySector(sector_id).OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var company in companies)
            {
                Dictionary.Add(company.Id, company.Name);
            }
            return Dictionary;
        }

        public Dictionary<int, string> GetCompaniesByAssociatedForDropDownList(int company_id)
        {
            var companies = _repository.GetAllRecords().Where(c => c.Id==company_id 
                || c.CompanyAssociated_Id==company_id).OrderBy(c => c.Name);
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var company in companies)
            {
                Dictionary.Add(company.Id, company.Name);
            }

            return Dictionary;
        }

        public Dictionary<int, string> GetCompaniesByAssociatedAndRoleForDropDownList(int company_id, int role_id)
        {
            string role = new RolesServices().GetById(role_id).Name;
            IQueryable<Company> companies = _repository.GetAllRecords().Where(c => c.Id == company_id
                || c.CompanyAssociated_Id == company_id).OrderBy(c => c.Name);
            if (role == "Administrator" || role == "HRAdministrator")
                companies = companies.Where(c => c.CompaniesType.Name != "Customer");
            else
                companies = companies.Where(c => c.CompaniesType.Name == "Customer");
            Dictionary<int, string> Dictionary = new Dictionary<int, string>();
            foreach (var company in companies)
            {
                Dictionary.Add(company.Id, company.Name);
            }

            return Dictionary;
        }

        public Dictionary<string, string> GetEmptyDictionary()
        {
            return _repository.GetEmptyDictionary();
        }

        public bool IsNameDuplicated(string name)
        {
            Company company = _repository.GetAllRecords().Where(r => r.Name == name).FirstOrDefault();
            
            return company != null;
        }

        public bool IsPhoneDuplicated(string phone)
        {
            Company company = _repository.GetAllRecords().Where(r => r.Phone == phone).FirstOrDefault();

            return company != null;
        }

        public bool IsUrlDuplicated(string url)
        {
            Company company = _repository.GetAllRecords().Where(r => r.Url == url &&
                url != null).FirstOrDefault();

            return company != null;
        }

        public bool IsNumberDuplicated(string number)
        {
            Company company = _repository.GetAllRecords().Where(r => r.Number == number).FirstOrDefault();

            return company != null;
        }

        public object RequestList(User userLogged, string sidx, string sord, int page, int rows, string filters)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<Company> companies;
            if (userLogged.Company.CompaniesType.Name == "Owner")
                companies = GetAllRecords();
            else
                companies = GetAllRecords().Where(c => c.Id == userLogged.Company_Id ||
                                        c.CompanyAssociated_Id == userLogged.Company_Id);

            companies = JqGrid<Company>.GetFilteredContent(sidx, sord, page, rows, filters, companies, ref totalPages, ref totalRecords);
            var rowsModel = (
                from company in companies.ToList()
                select new
                {
                    i = company.Id,
                    cell = new string[] { company.Id.ToString(), 
                            "<a href=\"/Companies/Edit/"+company.Id+"\">" + 
                            company.Name + "</a>",
                            company.CompanyAssociated == null ? "-" : company.CompanyAssociated.Name,
                            company.Contact,
                            company.Phone,
                            company.CompaniesType.Name,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\"href=\"/Companies/Edit/"+company.Id+"\"><span id=\""+company.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/Companies/Details/"+company.Id+"\"><span id=\""+company.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\"href=\"#\"><span id=\""+company.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>",
                            "<a title=\""+"Crear Demográficos"+"\"href=\"#\"><span id=\""+company.Id+"\" class=\"ui-icon ui-icon-copy\"></span></a>"}

                }).ToArray();
            return JqGrid<Company>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
