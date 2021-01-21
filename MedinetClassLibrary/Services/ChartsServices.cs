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
    public class ChartsServices
    {
        private Dictionary<string, string> ddlDictionary;
        public Dictionary<string, int> demographicsCount;

        public ChartsServices()
        {
            ddlDictionary = new Dictionary<string, string>();
        }

        public List<string> GetUbicationKeys()
        {
            List<string> ubicationList = new List<string>();
            ubicationList.Add("State");
            ubicationList.Add("Location");
            ubicationList.Add("Region");
            ubicationList.Add("Country");
            return ubicationList;
        }

        public Dictionary<string, string> DemographicsDropDownList(Test test)
        {
            //ddlDictionary = new Dictionary<string, string>();
            //Dictionary<string, string> demographics = new GraphicsServices().GetDemographicList(company_id);
            ddlDictionary = new GraphicsServices().GetDemographicList(test);
            //for (int i = 0; i < demographics.Count(); i++) {
            //    if(demographics[i]!=null)
            //        ddlDictionary.Add(i, demographics[i]);
            //}
            //Dictionary<int, string> FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(company_id);
            foreach (var fo in new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(test.Company_Id))
            {
                ddlDictionary.Add("FunctionalOrganizationType-"+fo.Key, fo.Value);
            }
            return ddlDictionary;
        }

        public Dictionary<int, string> DemographicDDLForCategory(Test test, string demographic, int? c_fo, int? questionnaire_id)
        {
            Dictionary<int, string> DDL = new Dictionary<int, string>();
            Dictionary<int, string> dictionary = new Dictionary<int,string>();
            switch (demographic)
            {
                case "AgeRange":
                    dictionary = test.Evaluations.Where(s => s.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.Age_Id.Value, e.Age.Name }).Distinct().ToDictionary(e => e.Value, e => e.Name);
                    break;
                case "Country":
                    dictionary = test.Evaluations.Where(s => s.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.Location.State.Country_Id, e.Location.State.Country.Name }).Distinct().ToDictionary(e => e.Country_Id, e => e.Name);
                    break;
                case "InstructionLevel":
                    dictionary = test.Evaluations.Where(s => s.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.InstructionLevel_Id.Value, e.InstructionLevel.Name }).Distinct().ToDictionary(e => e.Value, e => e.Name);
                    break;
                case "Location":
                    dictionary = test.Evaluations.Where(s => s.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.Location_Id.Value, e.Location.Name }).Distinct().ToDictionary(e => e.Value, e => e.Name);
                    break;
                case "PositionLevel":
                    dictionary = test.Evaluations.Where(s => s.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.PositionLevel_Id.Value, e.PositionLevel.Name }).Distinct().ToDictionary(e => e.Value, e => e.Name);
                    break;
                case "Seniority":
                    dictionary = test.Evaluations.Where(s => s.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.Seniority_Id.Value, e.Seniority.Name }).Distinct().ToDictionary(e => e.Value, e => e.Name);
                    break;
                case "Region":
                    dictionary = test.Evaluations.Where(e => e.Location.Region_Id.HasValue && e.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.Location.Region_Id.Value, e.Location.Region.Name }).Distinct().ToDictionary(e => e.Value, e => e.Name);
                    break;
                case "State":
                    dictionary = test.Evaluations.Where(e => e.Location.State.Country_Id == c_fo.Value && e.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.Location.State_Id, e.Location.State.Name }).Distinct().ToDictionary(e => e.State_Id, e => e.Name);
                    break;
                case "Performance":
                    dictionary = test.Evaluations.Where(s => s.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).Select(e => new { e.Performance_Id.Value, e.PerformanceEvaluation.Name }).Distinct().ToDictionary(e => e.Value, e => e.Name);
                    break;
                case "FunctionalOrganizationType":
                    dictionary = test.Evaluations.Where(s => s.SelectionAnswers.All(q => questionnaire_id.HasValue ? q.Question.Category.Questionnaire_Id == questionnaire_id : true)).SelectMany(e => e.EvaluationFOs.Where(t => t.FunctionalOrganization.Type_Id == c_fo.Value).Select(efo => new { efo.FunctionalOrganization_Id, efo.FunctionalOrganization.Name })).Distinct().ToDictionary(e => e.FunctionalOrganization_Id, e => e.Name);
                    break;
                case "Gender":
                    dictionary.Add(0, ViewRes.Classes.ChiSquare.FemaleGender);
                    dictionary.Add(1, ViewRes.Classes.ChiSquare.MaleGender);
                    break;
            }
            foreach (KeyValuePair<int, string> pair in dictionary)
            {
                DDL.Add(pair.Key, pair.Value);
            }
            return DDL;
        }

        //retorna un Diccionario con la cantidad de elementos de cada demografico dependiendo de la compañia
        //así, si es mayor a 7 muestra la tabla, de lo contrario muestra el gráfico.
        public Dictionary<string, int> GetDemographicsCount(Test test)
        {
            demographicsCount = new DemographicsServices().GetAllDemographicsCountByTest(test.Id, test.Company_Id);
            //IQueryable<FunctionalOrganizationType> FOTypes = new FunctionalOrganizationTypesServices().GetByCompany(test.Company_Id);
            //FunctionalOrganizationsServices FOServices = new FunctionalOrganizationsServices();
            //demographicsCount = new Dictionary<string, int>();
            //demographicsCount.Add("General", 1);
            //demographicsCount.Add("AgeRange", test.Evaluations.Select(e => e.Age_Id).Where(e => e.HasValue).Distinct().Count());
            //demographicsCount.Add("Location", test.Evaluations.Select(e => e.Location_Id).Where(e => e.HasValue).Distinct().Count());
            //demographicsCount.Add("Performance", test.Evaluations.Select(e => e.Performance_Id).Where(e => e.HasValue).Distinct().Count());
            //demographicsCount.Add("Region", demographicsCount["Location"] > 0 ?
            //    test.Evaluations.Select(e => e.Location.Region_Id).Where(e => e.HasValue).Distinct().Count() : 0);
            //demographicsCount.Add("Seniority", test.Evaluations.Select(e => e.Seniority_Id).Where(e => e.HasValue).Distinct().Count());
            //demographicsCount.Add("Country", demographicsCount["Location"] > 0 ?
            //    test.Evaluations.Select(e => e.Location.State.Country_Id).Distinct().Count() : 0);
            //demographicsCount.Add("InstructionLevel", test.Evaluations.Select(e => e.InstructionLevel_Id).Where(e => e.HasValue).Distinct().Count());
            //demographicsCount.Add("Gender", 2);
            //demographicsCount.Add("PositionLevel", test.Evaluations.Select(e => e.PositionLevel_Id).Where(e => e.HasValue).Distinct().Count());
            //foreach(var fot in FOTypes)
            //{
            //    demographicsCount.Add(fot.Name, test.Evaluations.SelectMany(efo => efo.EvaluationFOs.Where(fo => fo.FunctionalOrganization.Type_Id == fot.Id)).Distinct().Count());
            //}
            return demographicsCount;
        }


        public ChiSquare GetSatisfiedAndNoSatisfiedDictionary(string demographic, int test_id, string type, double pValue, int? FO_id)
        {
            Test test = new TestsServices().GetById(test_id);
            ChiSquare chiSquare = new ChiSquare(test, demographic, null, null, null, null, FO_id, pValue, null, null, null);
            chiSquare.GetAssociation();
            return chiSquare;
        }

        #region "Demographic Percentages"

        private object GetGraphicData(string report, Dictionary<string, object> parameters)//
        {
            return new Commands(report, parameters).ExecuteCommand();//Commands: estructura tooodos los comandos
        }

        public Dictionary<string, double> GetGraphicDataForPopulation(string report, Dictionary<string, object> parameters)//
        {
            return (Dictionary<string, double>)GetGraphicData(report, parameters);//lo transforma a String double
        }

        public Dictionary<string, object> GetGraphicDataForFrequencyOrCategory(string report, Dictionary<string, object> parameters)
        {
            return (Dictionary<string, object>)GetGraphicData(report, parameters);
        }

        public Dictionary<string, object> GetGraphicDataForComparative(string report, Dictionary<string, object> parameters)
        {
            return (Dictionary<string, object>)GetGraphicData(report, parameters);
        }

        public Dictionary<string, object> GetGraphicDataForComparativeGraph(string report, Dictionary<string, object> parameters)
        {
            Dictionary<string, int[]> data = (Dictionary<string, int[]>)GetGraphicData(report, parameters);
            Dictionary<string, double> auxInt = data.ToDictionary(k => k.Key, v => Double.Parse(v.Value[0].ToString()));
            Dictionary<string, object> auxObj = new Dictionary<string, object>();
            auxObj.Add("General", auxInt);
            return auxObj;
        }

        public Dictionary<string, double> GetGeneralPctgByTest(Dictionary<string, object> parameters)
        {
            Dictionary<string, double> listedResults = (Dictionary<string, double>)new Commands("Population", parameters).ExecuteCommand();
            //Test test = new TestsServices().GetById(test_id);
            //var evaluations = new EvaluationsServices().GetByTest(test_id);
            //double pct = test.Evaluations.Count(); //* 100 / new TestsServices().GetById(test_id).EvaluationNumber;
            //listedResults.Add(ViewRes.Classes.Services.EvaluationsReceived, pct);
            //listedResults.Add(ViewRes.Classes.Services.EvaluationsNoReceived, test.EvaluationNumber-pct);
            return listedResults;
        }

        public Dictionary<string, double> GetAgePctgListByTest(int test_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();
            var evaluations = new EvaluationsServices().GetByTest(test_id);
            IQueryable<IGrouping<int, Age>> groups = evaluations.Select(c => c.Age).OrderBy(p => p.Name).GroupBy(c => c.Id);
            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.Age_Id == group_id).Count();// *100 / evaluations.Count();
                listedResults.Add(group_name, pct);
            }
            return listedResults;
        }

        public Dictionary<string, double> GetCountryPctgListByTest(int test_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = new EvaluationsServices().GetByTest(test_id);
            IQueryable<IGrouping<int, Country>> groups = evaluations.Select(c => c.Location.State.Country).OrderBy(p => p.Name).GroupBy(c => c.Id);

            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.Location.State.Country_Id == group_id).Count();// *100 / evaluations.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        private IQueryable<Evaluation> GetEvaluationsByUbication(int? test_id, int? country_id,
                                                                int? state_id, int? region_id)
        {
            IQueryable<Evaluation> evaluations = new EvaluationsServices().GetByTest(test_id);
            if (country_id.HasValue)
            {
                if (state_id.HasValue)
                    evaluations = evaluations.Where(l => l.Location.State_Id == state_id.Value);
                else
                    evaluations = evaluations.Where(l => l.Location.State.Country_Id == country_id.Value);
            }
            else
                if (region_id.HasValue)
                    evaluations = evaluations.Where(l => l.Location.Region_Id == region_id.Value);
            return evaluations;
        }


        public Dictionary<string, double> GetGenderPctgListByTest(int test_id, int? country_id,
                                                                    int? state_id, int? region_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = GetEvaluationsByUbication(test_id, country_id, state_id, region_id);

            listedResults.Add(ViewRes.Classes.ChiSquare.FemaleGender, evaluations.Where(e => e.Sex == "Female").Count());// * 100 / evaluations.Count());
            listedResults.Add(ViewRes.Classes.ChiSquare.MaleGender, evaluations.Where(e => e.Sex == "Male").Count());// * 100 / evaluations.Count());

            return listedResults;
        }

        public Dictionary<string, double> GetInstructionLevelsPctgListByTest(int test_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = new EvaluationsServices().GetByTest(test_id);
            IQueryable<IGrouping<int, InstructionLevel>> groups = evaluations.Select(c => c.InstructionLevel).OrderBy(p => p.Name).GroupBy(c => c.Id);

            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.InstructionLevel_Id == group_id).Count();// * 100 / evaluations.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        public Dictionary<string, double> GetLocationsPctgListByTest(int test_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = new EvaluationsServices().GetByTest(test_id);
            IQueryable<IGrouping<int, Location>> groups = evaluations.Select(c => c.Location).OrderBy(p => p.Name).GroupBy(c => c.Id);

            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.Location_Id == group_id).Count();// * 100 / evaluations.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        public Dictionary<string, double> GetPositionLevelsPctgListByTest(int test_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = new EvaluationsServices().GetByTest(test_id);
            IQueryable<IGrouping<int, PositionLevel>> groups = evaluations.Select(c => c.PositionLevel).OrderBy(p => p.Name).GroupBy(c => c.Id);

            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.PositionLevel_Id == group_id).Count();// * 100 / evaluations.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        public Dictionary<string, double> GetRegionPctgListByTest(int test_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = new EvaluationsServices().GetByTest(test_id);
            IQueryable<IGrouping<int, Region>> groups = evaluations.Select(c => c.Location.Region).OrderBy(p => p.Name).GroupBy(c => c.Id);

            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.Location.Region_Id == group_id).Count();// * 100 / evaluations.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        public Dictionary<string, double> GetSenioritiesPctgListByTest(int test_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = new EvaluationsServices().GetByTest(test_id);
            IQueryable<IGrouping<int, Seniority>> groups = evaluations.Select(c => c.Seniority).OrderBy(p => p.Name).GroupBy(c => c.Id);

            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.Seniority_Id == group_id).Count();// * 100 / evaluations.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        public Dictionary<string, double> GetStatePctgListByTest(int test_id, int country_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = new EvaluationsServices().GetByTest(test_id).Where(e=>e.Location.State.Country.Id == country_id);
            IQueryable<IGrouping<int, State>> groups = evaluations.Select(c => c.Location.State).OrderBy(p => p.Name).GroupBy(c => c.Id);
            
            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.Location.State_Id == group_id).Count();// * 100 / evaluations.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        public Dictionary<string, double> GetPerformancePctgListByTest(int test_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluations = new EvaluationsServices().GetByTest(test_id);
            IQueryable<IGrouping<int, PerformanceEvaluation>> groups = evaluations.Select(c => c.PerformanceEvaluation).OrderBy(p => p.Name).GroupBy(c => c.Id);

            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluations.Where(c => c.Performance_Id == group_id).Count();// * 100 / evaluations.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        public Dictionary<string, double> GetFunctionalOrganizationPctgListByTestAndType(int test_id, int type_id)
        {
            Dictionary<string, double> listedResults = new Dictionary<string, double>();

            var evaluationsFO = new EvaluationsFOServices().GetByFunctionalOrganizationTypeAndTest(test_id, type_id);
            IQueryable<IGrouping<int, FunctionalOrganization>> groups = evaluationsFO.Select(c => c.FunctionalOrganization).OrderBy(p => p.Name).GroupBy(c => c.Id);

            foreach (var group in groups)
            {
                int group_id = group.Select(c => c.Id).FirstOrDefault();
                string group_name = group.Select(c => c.Name).FirstOrDefault();
                double pct = evaluationsFO.Where(c => c.FunctionalOrganization_Id == group_id).Count();// * 100 / evaluationsFO.Count();

                listedResults.Add(group_name, pct);
            }

            return listedResults;
        }

        #endregion

    }
}
