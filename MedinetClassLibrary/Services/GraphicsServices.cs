using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Repositories;
using MedinetClassLibrary.Classes;
using System.Web.Mvc;


namespace MedinetClassLibrary.Services
{
    public class GraphicsServices : IRepositoryServices<Graphic>
    {
        private IRepository<Graphic> _repository;

        public GraphicsServices()
            : this(new Repository<Graphic>())
        {
        }

        public GraphicsServices(IRepository<Graphic> repository)
        {
            _repository = repository;
        }

        public bool Add(Graphic entity)
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

        public IQueryable<Graphic> GetAllRecords()
        {
            return _repository.GetAllRecords();
        }

        public Graphic GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

        public Graphic GetByDemographicAndType(string demographic, string type)
        {
            return _repository.GetAllRecords().Where(g => g.Demographic == demographic && g.Type == type).FirstOrDefault();
        }

        public string GetSrcByDemographic(string demographic, string type)
        {
            return GetAllRecords().Where(d => d.Demographic == demographic && d.Type == type).Select(s => s.Source).FirstOrDefault();
        }

        public Dictionary<string, string> GetDemographicList(Test test) {

            //List<string> demog = _repository.GetAllRecords().Where(c => c.Demographic != "FunctionalOrganizationType" && c.Demographic != "General" && c.Demographic != "Category").OrderBy(d => d.Demographic).Select(c => c.Demographic).Distinct().ToList();
            int company_id = test.Company_Id;
            List<string> demog = test.DemographicsInTests.Where(f => !f.FOT_Id.HasValue).Select(n => n.Demographic.Name).ToList();
            Dictionary<string, string> demographics = new Dictionary<string,string>();
            int i = 0;
            foreach (var dem in demog)
            {
                switch (dem)
                {
                    case "AgeRange":
                        demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.AgeRange);
                        break;
                    case "Gender":
                        demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.Gender);
                        break;
                    case "InstructionLevel":
                        demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.InstructionLevel);
                        break;
                    case "Location":
                        if (new LocationsServices().GetByCompany(company_id).Count() > 0)
                            demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.Location);
                        break;
                    case "Region":
                        if (new RegionsServices().GetByCompany(company_id).Count() > 0)
                            demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.Region);
                        break;
                    case "State":
                        demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.State);
                        break;
                    case "Country":
                        demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.Country);
                        break;
                    case "PositionLevel":
                        demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.PositionLevel);
                        break;
                    case "Seniority":
                        demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.Seniority);
                        break;
                    case "Performance":
                        if (new PerformanceEvaluationsServices().GetByCompany(company_id).Count() > 0)
                            demographics.Add(dem, ViewRes.Views.ChartReport.Demographics.Performance);
                        break;
                }
                i++;
            }
            return demographics;
        }
    }
}
