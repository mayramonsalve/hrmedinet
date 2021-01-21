using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MedinetClassLibrary.Services;
using Extreme.Statistics;
using System.Data;
using Extreme.Mathematics;
using Extreme.Mathematics.LinearAlgebra;

namespace MedinetClassLibrary.Models
{
    public class Rankings
    {
        public Test test;
        private Dictionary<string, string> DictionaryColours;
        private User UserLogged;
        private Dictionary<string, string> DemographicNames;
        private Company Company;

        public Rankings()
        {

        }

        public Rankings(Questionnaire Questionnaire, Company Company, User UserLogged)
        {
            this.test = GetLastTestByQuestionnaire(Questionnaire.Id, Company.Id);
            this.Company = Company;
            this.DictionaryColours = CreateDictionaryColours();
            this.UserLogged = UserLogged;
            this.DemographicNames = GetDemographicNames();
        }

        public Rankings(Company Company)
        {
            this.Company = Company;
        }

        private Test GetLastTestByQuestionnaire(int Questionnaire_id, int Company_id)
        {
            return new TestsServices().GetLastTestByCompany(Company_id, Questionnaire_id, null);
        }

        public Dictionary<string, string> GetDemographicNames()
        { 
            Dictionary<string, string> demographics = new Dictionary<string, string>();
            demographics.Add("Country", ViewRes.Views.ChartReport.Graphics.CountryTab);
            if (new RegionsServices().GetByCompany(Company.Id).Count() > 0)
                demographics.Add("Region", ViewRes.Views.ChartReport.Graphics.RegionTab);
            if (new LocationsServices().GetByCompany(Company.Id).Count() > 0)
                demographics.Add("Location", ViewRes.Views.ChartReport.Graphics.LocationTab);
            demographics.Add("AgeRange", ViewRes.Views.ChartReport.Graphics.AgeTab);
            demographics.Add("InstructionLevel", ViewRes.Views.ChartReport.Graphics.InstructionLevelTab);
            demographics.Add("Seniority", ViewRes.Views.ChartReport.Graphics.SeniorityTab);
            demographics.Add("PositionLevel", ViewRes.Views.ChartReport.Graphics.PositionLevelTab);
            demographics.Add("Gender", ViewRes.Views.ChartReport.Graphics.GenderTab);
            if (new PerformanceEvaluationsServices().GetByCompany(Company.Id).Count() > 0)
                demographics.Add("Performance", ViewRes.Views.ChartReport.Graphics.PerformanceTab);
            demographics.Add("FunctionalOrganizationType", "");
            return demographics;
        }

        public string GetColourByClimate(double pctClimate)
        {
            if (pctClimate <= 60)
                return DictionaryColours["Red"];
            else if (pctClimate > 60 && pctClimate <= 80)
                return DictionaryColours["Amber"];
            else if (pctClimate > 80)
                return DictionaryColours["Green"];
            else
                return "";
        }

        private Dictionary<string, string> CreateDictionaryColours()
        {
            Dictionary<string, string> colours = new Dictionary<string, string>();
            colours.Add("Blue", "#0066FF");
            colours.Add("Green", "#66FF00");
            colours.Add("Amber", "#FFFF00");
            colours.Add("Orange", "#FF8C00");
            colours.Add("Red", "#FF0000");
            return colours;
        }

        public double GetGeneralClimate()
        {
                return test.GetGeneralAvgOrMed(true, null, null, null).Values.FirstOrDefault() * 100 / test.Questionnaire.Options.Count;
        }

        public Dictionary<Company, double> GetRankingForCompany(User UserLogged, Test test, int? sector_id, int? questionnaire_id, int? country_id, bool associated)//GetRankingForCompany saca el cuestionario y el sector de la compa;ia
        {
            TestsServices testService = new TestsServices();
            int sector;
            int questionnaire;
            GetQuestionnaireAndSectorByUserLogged(UserLogged, test, sector_id, questionnaire_id, testService, out sector, out questionnaire);
            Dictionary<Company, double> resultsByCompany = new Dictionary<Company, double>();
            IQueryable<Company> Companies = GetCompaniesForRanking(UserLogged, testService, sector, questionnaire, country_id, associated);
            Test companyTest;
            double climate;
            foreach (Company company in Companies)//busca la ultima medicion de cada compañia
            {
                companyTest = testService.GetLastTestByCompany(company.Id, questionnaire, country_id);
                if(country_id.HasValue)
                    climate = companyTest.GetGeneralAvgOrMedByUbication(true, null, null, null, country_id, null, null).Values.FirstOrDefault();
                else
                    climate = companyTest.GetGeneralAvgOrMed(true, null, null, null).Values.FirstOrDefault();
                climate = (double)(climate * 100) / companyTest.Questionnaire.Options.Count;//le busca el clima a cada una y lo pasa a %
                climate = Convert.ToDouble(String.Format("{0:0.##}", climate));
                resultsByCompany.Add(company, climate);
            }
            return resultsByCompany;
        }

        private IQueryable<Company> GetCompaniesForRanking(User UserLogged, TestsServices testService, int sector, int questionnaire, int? country, bool associated)
        {
            if (country.HasValue)
            {
                if (associated)
                    return testService.GetByQuestionnaire(questionnaire).Where(t => t.Evaluations.Count > 0 && t.Evaluations.Select(l => l.Location.State.Country_Id == country.Value).Count() > 0).Select(t => t.Company).Where(c => c.ShowClimate == true && c.CompanySector_Id == sector && c.CompanyAssociated_Id == UserLogged.Company_Id).Distinct();
                else
                    return testService.GetByQuestionnaire(questionnaire).Where(t => t.Evaluations.Count > 0 && t.Evaluations.Select(l => l.Location).Where(lo => lo.State.Country_Id == country.Value).Count() > 0).Select(t => t.Company).Where(c => c.ShowClimate == true && c.CompanySector_Id == sector).Distinct();
            }
            else
            {
                if (associated)
                {
                    IQueryable<Test> auxt = testService.GetByQuestionnaire(questionnaire).Where(t => t.Evaluations.Count > 0);
                    IQueryable<Company> auxc = auxt.Select(c => c.Company);
                    auxc = auxc.Where(c => c.ShowClimate == true && c.CompanySector_Id == sector && c.CompanyAssociated_Id == UserLogged.Company_Id).Distinct();
                    //return testService.GetByQuestionnaire(questionnaire).Where(t => t.Evaluations.Count > 0).Select(t => t.Company).Where(c => c.ShowClimate == true && c.CompanySector_Id == sector && c.CompanyAssociated_Id == UserLogged.Company_Id).Distinct();
                    return auxc;
                }
                else
                    return testService.GetByQuestionnaire(questionnaire).Where(t => t.Evaluations.Count > 0).Select(t => t.Company).Where(c => c.ShowClimate == true && c.CompanySector_Id == sector).Distinct();
            }
        }

        private static void GetQuestionnaireAndSectorByUserLogged(User UserLogged, Test test, int? sector_id, int? questionnaire_id, TestsServices testService, out int sector, out int questionnaire)
        {
            if (UserLogged.Role.Name == "HRAdministrator")
            {
                sector = sector_id.HasValue ? sector_id.Value : test.Company.CompanySector_Id.Value;
                questionnaire = questionnaire_id.HasValue ? questionnaire_id.Value : test.Questionnaire_Id.Value;
            }
            else
            {
                sector = UserLogged.Company.CompanySector_Id.Value;
                questionnaire = questionnaire_id.HasValue ? questionnaire_id.Value : testService.GetLastTestByCompany(UserLogged.Company_Id, null, null).Questionnaire_Id.Value;
            }
        }

        private IQueryable<Country> GetCountries(User UserLogged)
        {
            IQueryable<Test> tests;
            if (UserLogged.Role.Name == "HRAdministrator")
                tests = new TestsServices().GetTestsByAssociated(UserLogged.Company_Id);
            else
                tests = new TestsServices().GetByCompany(UserLogged.Company_Id);
            IQueryable<Country> countries = tests.SelectMany(e => e.Evaluations.Select(l => l.Location.State.Country)).Distinct().AsQueryable();
            return countries;
        }

        public Dictionary<int, string> GetCountriesForDropdownList(User UserLogged)
        {
            IQueryable<Country> countries = GetCountries(UserLogged).Where(c => c != null);
            Dictionary<int, string> countriesDDL = new Dictionary<int, string>();
            foreach (Country c in countries)
            {
                countriesDDL.Add(c.Id, c.Name);
            }
            return countriesDDL;
        }

        #region Internal

        public Dictionary<string, double> GetClimateByDemographic(string demographic, int? fot)
        {
            if (test != null)
            {
                IEnumerable<KeyValuePair<string, double>> aux = GetAvgByDemographic(demographic, fot).OrderByDescending(v => v.Value);
                return OrderByClimate(aux);
            }
            else
                return new Dictionary<string,double>();
        }

        private Dictionary<string, double> OrderByClimate(IEnumerable<KeyValuePair<string, double>> avgs)
        {
            Dictionary<string, double> ordered = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> key in avgs)
            {
                double climate = key.Value * 100 / test.Questionnaire.Options.Count;
                climate = Convert.ToDouble(String.Format("{0:0.##}", climate));
                ordered.Add(key.Key, climate);
            }
            return ordered;
        }

        private Dictionary<string, double> GetAvgByDemographic(string demographic, int? fot)
        {
            switch (demographic)
            {
                case "Country":
                    return test.GetAvgOrMedByCountries(true, null, null, null);
                case "Region":
                    return test.GetAvgOrMedByRegions(true, null, null, null);
                case "Location":
                    return test.GetAvgOrMedByLocations(true, null, null, null, null, null, null);
                case "PositionLevel":
                    return test.GetAvgOrMedByPositionLevels(true, null, null, null);
                case "Seniority":
                    return test.GetAvgOrMedBySeniorities(true, null, null, null);
                case "AgeRange":
                    return test.GetAvgOrMedByAgeRanges(true, null, null, null);
                case "Gender":
                    return test.GetAvgOrMedByGender(true, null, null, null);
                case "InstructionLevel":
                    return test.GetAvgOrMedByInstructionLevels(true, null, null, null);
                case "Performance":
                    return test.GetAvgOrMedByPerformanceEvaluations(true, null, null, null);
                case "FunctionalOrganizationType":
                    return test.GetAvgOrMedByFOs(true, null, null, null, fot.Value);
            }
            return null;
        }

        public Dictionary<string, object> FillRankingDictionary(Dictionary<string, object> ranking, string demographic, Dictionary<string, double> newInfo)
        {
            ranking.Add(demographic, newInfo);
            return ranking;
        }

        #endregion

    }
}
