using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;
using System.Web;

namespace MedinetClassLibrary.Services
{
    public class ClimateScalesServices : IRepositoryServices<ClimateScale>
    {
        private IRepository<ClimateScale> _repository;

        public ClimateScalesServices()
            : this(new Repository<ClimateScale>())
        {
        }

        public ClimateScalesServices(IRepository<ClimateScale> repository)
        {
            _repository = repository;
        }

        public bool Add(ClimateScale entity)
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

        public IQueryable<ClimateScale> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public IQueryable<ClimateScale> GetClimateScalesByCompany(int company_id)
        {
            return _repository.GetAllRecords().Where(c => c.Company_Id == company_id);
        }

        public Dictionary<int, string> GetClimateScalesForDropDownListByCompany(int company_id)
        {
            Dictionary<int, string> scales = new Dictionary<int, string>();
            foreach (ClimateScale scale in GetClimateScalesByCompany(company_id))
            {
                scales.Add(scale.Id, scale.Name);
            }
            return scales;
        }

        public IQueryable<ClimateScale> GetClimateScalesByCompanyAndAssociated(int company_id, User user)
        {
            ////////////////
            int nummber=0;
            int companyid = 0;
            CompaniesServices comp = new CompaniesServices();
            List<Company> List_User_DB = comp.GetAllRecords().ToList();
            if (user.Role.Name == "HRCompany")
            {
                var sql =
                    (from cp in List_User_DB where cp.Id == company_id select new { number = cp.CompanyAssociated_Id }).ToList();///se obtiene el id de la compa;ia que ha creado el hrcompany (seria el campo CompanyAssociated de la tabla Companies)

                var countsc = _repository.GetAllRecords().Where(c => c.Company_Id == company_id || c.Company.CompanyAssociated_Id == company_id).Count();//aqui se obtiene si la consulta original (de mayra) trae algun valor
                if (countsc == 0)//si no trae algun valor-->esto quiere decir que la consulta anterior no consigue datos ya que la compa;ia HRCompany no posee escalas de clima
                {
                    var scalec = comp.GetAllRecords().Where(c => c.Id == company_id).ToArray();//trae todos los valores de las escalas de clima creadas para la compa;ia X

                    foreach (var rec in sql)
                    {
                        companyid = (int)rec.number;
                    }

                    return _repository.GetAllRecords().Where(c => c.Company_Id == companyid || c.Company.CompanyAssociated_Id == companyid);
                    // string sdsd = "fdfdfs";
                }
                else//si trae valores
                {
                    return _repository.GetAllRecords().Where(c => c.Company_Id == company_id || c.Company.CompanyAssociated_Id == company_id);
                }
            }
            else {
                return _repository.GetAllRecords().Where(c => c.Company_Id == company_id || c.Company.CompanyAssociated_Id == company_id);
            
            }
            
            
        
        }

        public Dictionary<int, string> GetClimateScalesForDropDownListByCompanyAndAssociated(int company_id, User user)
        {
            Dictionary<int, string> scales = new Dictionary<int, string>();
            foreach (ClimateScale scale in GetClimateScalesByCompanyAndAssociated(company_id,user))
            {
                scales.Add(scale.Id, scale.Name);
            }
            return scales;
        }

        public ClimateScale GetById(int id)
        {
            return _repository.GetById(id);
        }

        public object RequestList(string sidx, string sord, int page, int rows, string filters, int company_id)
        {
            int totalPages = 0;
            int totalRecords = 0;

            IQueryable<ClimateScale> climateScales = GetClimateScalesByCompany(company_id);
            climateScales = JqGrid<ClimateScale>.GetFilteredContent(sidx, sord, page, rows, filters, climateScales, ref totalPages, ref totalRecords);
            var rowsModel = (
                from climateScale in climateScales.ToList()
                select new
                {
                    i = climateScale.Id,
                    cell = new string[] { climateScale.Id.ToString(), 
                            "<a href=\"/ClimateScales/Edit/"+climateScale.Id+"\">" + 
                            climateScale.Name + "</a>",
                            climateScale.Description,
                            "<a title=\""+ViewRes.Classes.Services.Modify+"\" href=\"/ClimateScales/Edit/"+climateScale.Id+"\"><span id=\""+climateScale.Id+"\" class=\"ui-icon ui-icon-pencil\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Details+"\" href=\"/ClimateScales/Details/"+climateScale.Id+"\"><span id=\""+climateScale.Id+"\" class=\"ui-icon ui-icon-search\"></span></a>",
                            "<a title=\""+ViewRes.Classes.Services.Delete+"\" href=\"#\"><span id=\""+climateScale.Id+"\" class=\"ui-icon ui-icon-trash\"></span></a>" }
                }).ToArray();

            return JqGrid<ClimateScale>.SetJsonData(totalPages, totalRecords, page, rowsModel);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public bool IsNameDuplicated(int company_id, string name)
        {
            ClimateScale climateScale = GetClimateScalesByCompany(company_id).Where(q => q.Name == name).FirstOrDefault();
            return climateScale != null;
        }

    }
}
