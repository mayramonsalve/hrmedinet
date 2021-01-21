using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Services
{
    public class DemographicsServices: IRepositoryServices<Demographic>
    {
        private IRepository<Demographic> _repository;

        public DemographicsServices()
            : this(new Repository<Demographic>())
        {
        }

        public DemographicsServices(IRepository<Demographic> repository)
        {
            _repository = repository;
        }

        public bool Add(Demographic entity)
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

        public IQueryable<Demographic> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Demographic GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int GetDemographicsCountByTest(int test_id, string demographic, int? fot)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("test", test_id);
            if(fot.HasValue)
                parameters.Add("fot", fot.Value);
            int count = new Commands("EvaluationsCount", parameters).GetCount();
            return count;
        }

        public Dictionary<string, int> GetAllDemographicsCountByTest(int test_id, int company_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "All");
            parameters.Add("test", test_id);
            parameters.Add("company", company_id);
            Dictionary<string, int> count = new Commands("EvaluationsCount", parameters).GetAllCount();
            return count;
        }

        public Dictionary<string, string> GetDemographicsForDropDownList()
        {
            List<string> demoNot = new List<string>();
            demoNot.Add("AllTests");
            demoNot.Add("Category");
            demoNot.Add("FunctionalOrganizationType");
            demoNot.Add("General");
            var demographics = GetAllRecords().Where(d => !demoNot.Contains(d.Name)).OrderBy(d => d.Name);
            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            foreach (var demo in demographics)
            {
                Dictionary.Add(demo.Id.ToString(), GetNameInSelectedLanguage(demo.Name));
            }
            return Dictionary;
        }

        public string GetNameInSelectedLanguage(string dem)
        {
            switch (dem)
            {
                case "AgeRange":
                    return ViewRes.Views.ChartReport.Demographics.AgeRange;
                case "Gender":
                    return ViewRes.Views.ChartReport.Demographics.Gender;
                case "InstructionLevel":
                    return ViewRes.Views.ChartReport.Demographics.InstructionLevel;
                case "Location":
                    return ViewRes.Views.ChartReport.Demographics.Location;
                case "Region":
                    return ViewRes.Views.ChartReport.Demographics.Region;
                case "State":
                    return ViewRes.Views.ChartReport.Demographics.State;
                case "Country":
                    return ViewRes.Views.ChartReport.Demographics.Country;
                case "PositionLevel":
                    return ViewRes.Views.ChartReport.Demographics.PositionLevel;
                case "Seniority":
                    return ViewRes.Views.ChartReport.Demographics.Seniority;
                case "Performance":
                    return ViewRes.Views.ChartReport.Demographics.Performance;
                default:
                    return "";
            }
        }

        public int GetIdFromDemographicName(string name)
        {
            return GetAllRecords().Where(n => n.Name == name).FirstOrDefault().Id;
        }

        public Dictionary<string, string> GetDemographicsByCompanyForDropDownList(int company_id)
        {
            Dictionary<string, string> Dictionary = GetDemographicsForDropDownList();
            foreach (var fot in new FunctionalOrganizationTypesServices().GetByCompany(company_id).Where(fot => !fot.FOTParent_Id.HasValue))
            {
                Dictionary.Add("fot-" + fot.Id, fot.Name);
            }
            return Dictionary;
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }



    }
}
