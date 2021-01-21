using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extreme.Statistics.Distributions;
using Extreme.Statistics;
using Extreme.Statistics.Tests;
using System.Collections;
using MedinetClassLibrary.Services;
using Extreme.Mathematics;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Models
{
    public class ChiSquare
    {
        public double ChiSquareValue;
        public double OurChiSquare;
        public double OurPValue;
        public string Conclusion;
        public bool Association;
        public bool OneDemographic;
        public double SignificanceLevel;
        public Dictionary<string, object> DataSatisfaction;
        private Test Test;
        private int? Questionnaire_id;
        private int? Category_id;
        private int? Question_id;
        private string Demographic;
        private int VecSize;

        public ChiSquare() { 
        
        }

        public ChiSquare(Test test, string demographic, int? questionnaire_id, int? category_id, int? question_id, double significanceLevel,
                        int? country_id, int? state_id, int? region_id)
        {
            this.SignificanceLevel = significanceLevel;
            InitializeProperties(test, demographic, questionnaire_id, category_id, question_id);
            GetDataSatisfactionDictionary(demographic, questionnaire_id, category_id, question_id, null, null, country_id, state_id, region_id);
            VecSize = GetVectorSize();
        }

        //public ChiSquare(Test test, string demographic, int? category_id, int? question_id, double significanceLevel,
        //                int? country_id, int? state_id, int? region_id)
        //{
        //    this.SignificanceLevel = significanceLevel;
        //    InitializeProperties(test, demographic, category_id, question_id);
        //    Demographic = ViewRes.Views.ChartReport.Graphics.GeneralClimate;
        //    DataSatisfaction = GetGeneralSatAndNoSatByUbication(category_id, question_id, country_id, state_id, region_id);
        //    VecSize = GetVectorSize();
        //}

        public ChiSquare(Test test, string demographic, int? questionnaire_id, int? category_id, int? question_id, int? country_id, int? FOType_id,
                            double significanceLevel, int? countryU_id, int? state_id, int? region_id)
        {
            this.SignificanceLevel = significanceLevel;
            InitializeProperties(test, demographic, questionnaire_id, category_id, question_id);
            GetDataSatisfactionDictionary(demographic, questionnaire_id, category_id, question_id, country_id, FOType_id, countryU_id, state_id, region_id); 
            VecSize = GetVectorSize();
        }

        private void InitializeProperties(Test test, string demographic, int? questionnaire_id, int? category_id, int? question_id)
        {
            Conclusion = ViewRes.Classes.ChiSquare.Conclusion;
            Association = false;//me dice si hay o no asociación
            OneDemographic = false;
            ChiSquareValue = 0;
            OurChiSquare = 0;
            OurPValue = 0;
            this.Test = test;
            this.Questionnaire_id = questionnaire_id;
            this.Category_id = category_id;
            this.Question_id = question_id;
            this.Demographic = demographic;
        }

        private void GetDataSatisfactionDictionary(string demographic, int? questionnaire_id, int? category_id, int? question_id, int? country_id, int? FOType_id,
                                                    int? countryU_id, int? state_id, int? region_id)
        {
            switch (demographic)
            {
                case "General":
                    Demographic = ViewRes.Views.ChartReport.Graphics.GeneralClimate;
                    DataSatisfaction = GetGeneralSatAndNoSat(questionnaire_id, category_id, question_id, countryU_id, state_id, region_id);
                    break;
                case "Category":
                    Demographic = "";
                    DataSatisfaction = GetSatAndNoSatForCategories(questionnaire_id, countryU_id, state_id, region_id);
                    break;
                case "AllTests":
                    Demographic = "";
                    DataSatisfaction = GetAllTestsSatAndNoSat(questionnaire_id, category_id, question_id);
                    break;
                case "AgeRange":
                    Demographic = ViewRes.Views.ChartReport.Graphics.AgeTab;
                    DataSatisfaction = GetSatAndNoSatByAgeRanges(questionnaire_id, category_id, question_id, countryU_id, state_id, region_id);
                    break;
                case "Gender":
                    Demographic = ViewRes.Views.ChartReport.Graphics.GenderTab;//obtiene el nombre que va a salir en la chicuadrada por ejemplo rangos de edad
                    DataSatisfaction = GetSatAndNoSatByGender(questionnaire_id, category_id, question_id, countryU_id, state_id, region_id);
                    break;
                case "InstructionLevel":
                    Demographic = ViewRes.Views.ChartReport.Graphics.InstructionLevelTab;
                    DataSatisfaction = GetSatAndNoSatByInstructionLevels(questionnaire_id, category_id, question_id, countryU_id, state_id, region_id);
                    break;
                case "Location":
                    Demographic = ViewRes.Views.ChartReport.Graphics.LocationTab;
                    DataSatisfaction = GetSatAndNoSatByLocations(questionnaire_id, category_id, question_id, countryU_id, state_id, region_id);
                    break;
                case "Region":
                    Demographic = ViewRes.Views.ChartReport.Graphics.RegionTab;
                    DataSatisfaction = GetSatAndNoSatByRegions(questionnaire_id, category_id, question_id);
                    break;
                case "State":
                    Demographic = ViewRes.Views.ChartReport.Graphics.StateTab;
                    DataSatisfaction = GetSatAndNoSatByStates(questionnaire_id, category_id, question_id);
                    break;
                case "StateByCountry":
                    Demographic = ViewRes.Views.ChartReport.Graphics.StateTab + ViewRes.Views.ChartReport.Graphics.Of + new CountriesServices().GetById(country_id.Value).Name;
                    DataSatisfaction = GetSatAndNoSatByStates(questionnaire_id, category_id, question_id, country_id.Value);
                    break;
                case "Country":
                    Demographic = ViewRes.Views.ChartReport.Graphics.CountryTab;
                    DataSatisfaction = GetSatAndNoSatByCountries(questionnaire_id, category_id, question_id);
                    break;
                case "PositionLevel":
                    Demographic = ViewRes.Views.ChartReport.Graphics.PositionLevelTab;
                    DataSatisfaction = GetSatAndNoSatByPositionLevels(questionnaire_id, category_id, question_id, countryU_id, state_id, region_id);
                    break;
                case "Seniority":
                    Demographic = ViewRes.Views.ChartReport.Graphics.SeniorityTab;
                    DataSatisfaction = GetSatAndNoSatBySeniorities(questionnaire_id, category_id, question_id, countryU_id, state_id, region_id);
                    break;
                case "Performance":
                    Demographic = ViewRes.Views.ChartReport.Graphics.PerformanceTab;
                    DataSatisfaction = GetSatAndNoSatByPerformanceEvaluations(questionnaire_id, category_id, question_id, countryU_id, state_id, region_id);
                    break;
                case "FunctionalOrganizationType":
                    Demographic = new FunctionalOrganizationTypesServices().GetById(FOType_id.Value).Name;
                    DataSatisfaction = GetSatAndNoSatByFunctionalOrganizations(questionnaire_id, category_id, question_id, FOType_id.Value, countryU_id, state_id, region_id);
                    break;
            }
        }

        #region "Satisfaction"

        #region Dictionaries

        private Dictionary<string, object> GetParameters(string demographic, int? questionnaire_id, int? category_id, int? question_id, int? country_id, int? state_id, int? fot_id, bool sat, bool notSat)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("test", Test.Id);
            parameters.Add("minimumPeople", Test.MinimumPeople);//cantidad de personas minimas para mostrar el reporte. en media y mediana se habla de minimas respuestas
            parameters.Add("dataType", "SatNotSat");//me dice si es satnotsat o AvgAndMed(satisfechos y no satisfechos o media y mediana)
            if (country_id.HasValue)
                parameters.Add("country", country_id.Value);
            if (state_id.HasValue)
                parameters.Add("state", state_id.Value);
            if (fot_id.HasValue)
                parameters.Add("fot", fot_id.Value);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (category_id.HasValue)
            {
                if (!Test.OneQuestionnaire && !questionnaire_id.HasValue)
                    parameters.Add("categorygroup", category_id.Value);
                else
                    parameters.Add("category", category_id.Value);
                if (question_id.HasValue)
                    parameters.Add("question", question_id.Value);
            }
            parameters.Add("avg", sat ? 1 : 0);//manda a llamar los satisfechos
            parameters.Add("med", notSat ? 1 : 0);//manda a llamar los no satisfechos
            parameters.Add("satisfiedValue", GetSatisfiedValue());//busca el valor con el que se dice por arriba de él son satisfechos y por debajo de él y él inclusive son no satisfechos
            return parameters;
        }

        public Dictionary<string, object> GetGeneralSatAndNoSat(int? questionnaire_id, int? category_id, int? question_id,
                                            int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("General", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForGeneral(category_id, question_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForGeneral(category_id, question_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //public Dictionary<string, double> InitializeForGeneral(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        List<double> data = new List<double>();
        //        ExtractAnswersValues(category_id, question_id, data, Evaluations, GetSatisfiedValue(), isSatisfied);
        //        results.Add(Test.Name, data.Count);
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetAllTestsSatAndNoSat(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("AllTests", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForAllTests(category_id, question_id, true));
            //SatAndNoSat.Add("NoSatisfied", InitializeForAllTests(category_id, question_id, false));
            return SatAndNoSat;
        }

        //public Dictionary<string, double> InitializeForAllTests(int? category_id, int? question_id, bool isSatisfied)
        //{
        //    if (Test.Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        foreach (Test allTest in Test.Questionnaire.Tests.Where(t => t.Company_Id == Test.Company_Id && t.Evaluations.Count > 0))
        //        {
        //            List<double> data = new List<double>();
        //            ExtractAnswersValues(category_id, question_id, data, allTest.Evaluations, GetSatisfiedValue(), isSatisfied);
        //            results.Add(allTest.Name, data.Count);
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetGeneralSatAndNoSatByUbication(int? questionnaire_id, int? category_id, int? question_id,
                                            int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("General", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForGeneralByUbication(category_id, question_id, true,
            //                                country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForGeneralByUbication(category_id, question_id, false,
            //                                country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //public Dictionary<string, double> InitializeForGeneralByUbication(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        List<double> data = new List<double>();
        //        ExtractAnswersValues(category_id, question_id, data, Evaluations, GetSatisfiedValue(), isSatisfied);
        //        results.Add(Test.Name, data.Count);
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        //private IQueryable<Evaluation> GetEvaluationsByUbication(int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> evaluations = Test.Evaluations.AsQueryable();
        //    if (country_id.HasValue)
        //    {
        //        if (state_id.HasValue)
        //            evaluations = evaluations.Where(l => l.Location.State_Id == state_id.Value);
        //        else
        //            evaluations = evaluations.Where(l => l.Location.State.Country_Id == country_id.Value);
        //    }
        //    else
        //        if (region_id.HasValue)
        //            evaluations = evaluations.Where(l => l.Location.Region_Id == region_id.Value);
        //    return evaluations;
        //}

        public Dictionary<string, object> GetSatAndNoSatForCategories(int? questionnaire_id, int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Category", questionnaire_id, null, null, null, null, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForCategories(true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForCategories(false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //public Dictionary<string, double> InitializeForCategories(bool isSatisfied, int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        foreach (Category cat in Test.Questionnaire.Categories)
        //        {
        //            List<double> data = new List<double>();
        //            ExtractAnswersValues(cat.Id, null, data, Evaluations, GetSatisfiedValue(), isSatisfied);
        //            results.Add(cat.Name, data.Count);
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByAgeRanges(int? questionnaire_id, int? category_id, int? question_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("AgeRange", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForAgeRanges(category_id, question_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForAgeRanges(category_id, question_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //public Dictionary<string, double> InitializeForAgeRanges(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().ShortName, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByGender(int? questionnaire_id, int? category_id, int? question_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Gender", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForGender(category_id, question_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForGender(category_id, question_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForGender(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        List<string> elements = new List<string>();
        //        elements.Add("Male");
        //        elements.Add("Female");
        //        foreach (var element in elements)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Evaluations.Where(s => s.Sex == element);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                if (element == "Male")
        //                    results.Add(ViewRes.Classes.ChiSquare.MaleGender, data.Count);
        //                else
        //                    results.Add(ViewRes.Classes.ChiSquare.FemaleGender, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByInstructionLevels(int? questionnaire_id, int? category_id, int? question_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("InstructionLevel", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForInstructionLevels(category_id, question_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForInstructionLevels(category_id, question_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForInstructionLevels(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().ShortName, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByLocations(int? questionnaire_id, int? category_id, int? question_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Location", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForLocations(category_id, question_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForLocations(category_id, question_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForLocations(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Evaluations.Where(s => s.Location_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().ShortName, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByRegions(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Region", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForRegions(category_id, question_id, true));
            //SatAndNoSat.Add("NoSatisfied", InitializeForRegions(category_id, question_id, false));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForRegions(int? category_id, int? question_id, bool isSatisfied)
        //{
        //    if (Test.Evaluations.Count > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Test.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Test.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().Name, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByStates(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("State", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForStates(category_id, question_id, true));
            //SatAndNoSat.Add("NoSatisfied", InitializeForStates(category_id, question_id, false));
            return SatAndNoSat;
        }

        public Dictionary<string, object> GetSatAndNoSatByStates(int? questionnaire_id, int? category_id, int? question_id, int country_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("State", questionnaire_id, category_id, question_id, country_id, null, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForStates(category_id, question_id, true, country_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForStates(category_id, question_id, false, country_id));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForStates(int? category_id, int? question_id, bool isSatisfied)
        //{
        //    if (Test.Evaluations.Count > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Test.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Test.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().Name, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        //private Dictionary<string, double> InitializeForStates(int? category_id, int? question_id, bool isSatisfied, int? country_id)
        //{
        //    if (Test.Evaluations.Count > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Test.Evaluations.Select(g => g.Location.State).Where(c => c.Country_Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Test.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().Name, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByCountries(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Country", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForCountries(category_id, question_id, true));
            //SatAndNoSat.Add("NoSatisfied", InitializeForCountries(category_id, question_id, false));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForCountries(int? category_id, int? question_id, bool isSatisfied)
        //{
        //    if (Test.Evaluations.Count > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Test.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Test.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().Name, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByPositionLevels(int? questionnaire_id, int? category_id, int? question_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("PositionLevel", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForPositionLevels(category_id, question_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForPositionLevels(category_id, question_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForPositionLevels(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().ShortName, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatBySeniorities(int? questionnaire_id, int? category_id, int? question_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Seniority", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForSeniorities(category_id, question_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForSeniorities(category_id, question_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForSeniorities(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().ShortName, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByPerformanceEvaluations(int? questionnaire_id, int? category_id, int? question_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Performance", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForPerformanceEvaluations(category_id, question_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForPerformanceEvaluations(category_id, question_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //private Dictionary<string, double> InitializeForPerformanceEvaluations(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            List<double> data = new List<double>();
        //            var evaluations = Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            if (evaluations.Count() >= Test.MinimumPeople)
        //            {
        //                ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
        //                results.Add(group.First().ShortName, data.Count);
        //            }
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, object> GetSatAndNoSatByFunctionalOrganizations(int? questionnaire_id, int? category_id, int? question_id, int type_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> SatAndNoSat = (Dictionary<string, object>)new Commands("Univariate", GetParameters("FunctionalOrganizationType", questionnaire_id, category_id, question_id, country_id, state_id, type_id, true, true)).ExecuteCommand();
            //Dictionary<string, object> SatAndNoSat = new Dictionary<string, object>();
            //SatAndNoSat.Add("Satisfied", InitializeForFunctionalOrganizations(category_id, question_id, type_id, true, country_id, state_id, region_id));
            //SatAndNoSat.Add("NoSatisfied", InitializeForFunctionalOrganizations(category_id, question_id, type_id, false, country_id, state_id, region_id));
            return SatAndNoSat;
        }

        //public Dictionary<string, object> GetSatAndNoSatByFOTypes(int? category_id, int? question_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, object> results = new Dictionary<string, object>();
        //        var types = new EvaluationsFOServices().GetByTest(Test.Id).Select(fo => fo.FunctionalOrganization.FunctionalOrganizationType).OrderBy(fo => fo.Name).Distinct();
        //        foreach (var type in types)
        //        {
        //            results.Add(type.Name, InitializeForFunctionalOrganizations(category_id, question_id, type.Id, isSatisfied, country_id, state_id, region_id));
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, double> InitializeForFunctionalOrganizations(int? category_id, int? question_id, int type_id, bool isSatisfied,
        //                                    int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
        //    if (Evaluations.Count() > 0)
        //    {
        //        Dictionary<string, double> results = new Dictionary<string, double>();
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationTypeAndUbication(Evaluations.Select(i=>i.Id).ToList(), type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        //if (groups.Count() > 1)
        //        //{
        //            foreach (var group in groups)
        //            {
        //                List<double> data = new List<double>();
        //                //var evaluationsFO = new EvaluationsFOServices().GetByTest(Test.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //                var evaluationsFO = Evaluations.SelectMany(efo => efo.EvaluationFOs).Where(fo => fo.FunctionalOrganization_Id == group.First().Id); 
        //                if (evaluationsFO.Count() >= Test.MinimumPeople)
        //                {
        //                    ExtractAnswersValuesByEvaluationFO(category_id, question_id, data, evaluationsFO, GetSatisfiedValue(), isSatisfied);
        //                    results.Add(group.First().ShortName, data.Count());
        //                }
        //            }
        //        //}
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        #endregion

        #region Functions

        private static void ExtractAnswersValues(int? category_id, int? question_id, List<double> data, IEnumerable<Evaluation> evaluations, int satisfiedValue, bool isSatisfied)
        {

            IEnumerable<double> answers;
            if (category_id.HasValue)
            {
                if (question_id.HasValue)
                    answers = evaluations.Select(a => a.SelectionAnswers.Where(c => c.Question_Id == question_id.Value).Average(o => o.Option.Value)).AsEnumerable();
                else
                    answers = evaluations.Select(a => a.SelectionAnswers.Where(c => c.Question.Category_Id == category_id.Value).Average(o => o.Option.Value)).AsEnumerable();
            }
            else
                answers = evaluations.Select(a => a.SelectionAnswers.Average(o => o.Option.Value)).AsEnumerable();
            
            if (isSatisfied)
                answers = answers.Where(v => v > satisfiedValue);
            else
                answers = answers.Where(v => v <= satisfiedValue);
            data.AddRange(answers);

            //foreach (Evaluation e in evaluations)
            //{
            //    IQueryable<SelectionAnswer> answers = e.SelectionAnswers.AsQueryable();
            //    if (category_id != null)
            //    {
            //        answers = answers.Where(q => q.Question.Category_Id == category_id);
            //        if (question_id != null)
            //            answers = answers.Where(q => q.Question_Id == question_id);
            //    }
            //    double average = answers.Select(a => a.Option.Value).Count() > 0 ? answers.Select(a => a.Option.Value).Average() : 0;
            //    bool condition;
            //    if (isSatisfied)
            //        condition = average > satisfiedValue;
            //    else
            //        condition = average <= satisfiedValue;

            //    if (condition)
            //        data.Add(average); //al final data debe tener tantos double como satisfechos hayan
            //}
        }

        private void ExtractAnswersValuesByEvaluationFO(int? category_id, int? question_id, List<double> data, IEnumerable<EvaluationFO> evaluationsFO, int satisfiedValue, bool isSatisfied)
        {
            IQueryable<Evaluation> evaluations = Test.Evaluations.Where(e => evaluationsFO.Select(efo => efo.Evaluation_Id).Distinct().Contains(e.Id)).AsQueryable();
            IEnumerable<double> answers;
            if (category_id.HasValue)
            {
                if (question_id.HasValue)
                    answers = evaluations.Select(a => a.SelectionAnswers.Where(c => c.Question_Id == question_id.Value).Average(o => o.Option.Value)).AsEnumerable();
                else
                    answers = evaluations.Select(a => a.SelectionAnswers.Where(c => c.Question.Category_Id == category_id.Value).Average(o => o.Option.Value)).AsEnumerable();
            }
            else
                answers = evaluations.Select(a => a.SelectionAnswers.Average(o => o.Option.Value)).AsEnumerable();

            if (isSatisfied)
                answers = answers.Where(v => v > satisfiedValue);
            else
                answers = answers.Where(v => v <= satisfiedValue);
            data.AddRange(answers);


            //foreach (EvaluationFO efo in evaluationsFO)
            //{
            //    IQueryable<SelectionAnswer> answers = new EvaluationsServices().GetById(efo.Evaluation_Id).SelectionAnswers.AsQueryable();
            //    if (category_id != null)
            //    {
            //        answers = answers.Where(q => q.Question.Category_Id == category_id);
            //        if (question_id != null)
            //            answers = answers.Where(q => q.Question_Id == question_id);
            //    }

            //    double average = answers.Select(a => a.Option.Value).Count() > 0 ? answers.Select(a => a.Option.Value).Average() : 0;

            //    bool condition;
            //    if (isSatisfied)
            //        condition = average > satisfiedValue;
            //    else
            //        condition = average <= satisfiedValue;

            //    if (condition)
            //        data.Add(average);
            //}
        }

        private long GetGlobalSatisfied(int? category_id, int? question_id)
        {
            if (Test.Evaluations.Count > 0)
            {
                return InitializeForGlobal(category_id, question_id, true);
            }
            else
                return 0;
        }

        private long GetGlobalNoSatisfied(int? category_id, int? question_id)
        {
            if (Test.Evaluations.Count > 0)
            {
                return InitializeForGlobal(category_id, question_id, false);
            }
            else
                return 0;
        }

        private long InitializeForGlobal(int? category_id, int? question_id, bool isSatisfied)
        {
            long results = new long();
            List<double> data = new List<double>();
            var evaluations = Test.Evaluations;
            ExtractAnswersValues(category_id, question_id, data, evaluations, GetSatisfiedValue(), isSatisfied);
            results = data.Count;
            return results;
        }

        private int GetSatisfiedValue()
        {
            int valueSatisfied = 0;
            IEnumerable<Option> options = Test.GetOptionsByTest();
            if (options != null)
            {
                valueSatisfied = options.Count() % 2 != 0 ?
                (int)Stats.Percentile(options.Select(o => double.Parse(o.Value.ToString())).ToArray(), 60) ://options.Select(o => double.Parse(o.Value.ToString())).ToArray() devuelve los valores de mis opciones
                (int)Stats.Percentile(options.Select(o => double.Parse(o.Value.ToString())).ToArray(), 50); // y el 50 o 60 es porque estoy buscando el percentil 60 o 50. para números pares es el 50 para impares 60
            }
            return valueSatisfied;
        }

        #endregion

        #endregion

        #region "Chi-Square"

        public void GetAssociation()
        {
            if (Test.Evaluations.Count > 0)
                GetConclusion(ThereIsAssociation());
        }

        private void GetConclusion(bool thereIs)//me da el boolean(thereIs) de que si hay  o no asociación y me da la conclusión(si hay asociacion entre satisfacción y el demográfico)
        {
            Association = thereIs;
            if (!OneDemographic){
                if (thereIs)
                    Conclusion = ViewRes.Classes.ChiSquare.ThereIs + Demographic;
                else
                    Conclusion = ViewRes.Classes.ChiSquare.ThereIsNot + Demographic;
            }
        }

        private bool ThereIsAssociation()
        {
            this.OurChiSquare = GetOurChiSquare();
            if (!OneDemographic)
            {
                this.ChiSquareValue = GetChiSquareValue(GetDegreesOfFreedom(VecSize, 2), SignificanceLevel);//le damos los grados de libertad y el nivel de significancia.VecSize=cantidad de demográficos.SignificanceLevel=0,05
                return (OurChiSquare > ChiSquareValue);
            }
            else
                return false;
        }

        private double GetOurChiSquare()
        {
            string[] Satisfaction = { ViewRes.Classes.ChiSquare.Satisfied, ViewRes.Classes.ChiSquare.NoSatisfied };//tiene los nombres.tabla de contingencia
            string[] Demographic = GetNamesForDemographic();//tiene los nombres de los demográficos que van a estar en la tabla de contingencia
            int[] vecSatisfied = GetNumbersForSatisfied();//la cantidad de demográficos de satisfechos tiene 0, es decir, si hay 5 demográficos habran 5 0 por elemneto. Ejemplo: 0 0 0 0 0, 1 1 1 1 1 es decir hay 5 demográficos y satisfechos(0) y no satisfechos (1)
            int[] vecDemographic = GetNumbersForDemographic();//tenemos 6 demográficos, y se enumeran: 0 1 2 3 4 5, 0 1 2 3 4 5 se repiten 2 veces porque solo tenemos Satisfechos y No Satisfechos si hubiera otra variable se repetiría de nuevo
            double[] values = GetValuesForTable();//son los valores del diccionario de satisfechos y no satisfechos, los primeros son de satisfechos y los segundos de no satisfechos
            CategoricalScale satisfactionScale = new CategoricalScale(Satisfaction);//estas
            CategoricalVariable satisfaction = new CategoricalVariable(satisfactionScale, vecSatisfied);//
            CategoricalScale demographicScale = new CategoricalScale(Demographic);///
            CategoricalVariable demographic = new CategoricalVariable(demographicScale, vecDemographic);/////
            NumericalVariable counts = new NumericalVariable(values);//valores de satisfechos y no satisfechos
            if (Demographic.Count() > 1)
            {
                ContingencyTable ct = new ContingencyTable(demographic, satisfaction, counts);// aquí se crea la tabla de contingencia virtualmente con los datos de demographic, satisfaction, counts
                ct.GetChiSquareTest();//calcula la prueba de chicuadrado y devuelve OurChiSquare
                if (DoubleComplex.IsNaN(ct.ChiSquare))//si ct.ChiSquare no es numerico
                {
                    //OneDemographic = true;
                    return 0;
                }
                //OurPValue
                //double coc = ct.CoefficientOfContingency;
                else
                    return ct.ChiSquare;
            }
            else
            {
                OneDemographic = true;
                return 0;
            }
        }

        private double[] GetValuesForTable()
        {
            int d = 0;
            double[] values = new double[VecSize*2];
            foreach (string key in DataSatisfaction.Keys)
            {
                IEnumerable val = (IEnumerable)DataSatisfaction[key];
                foreach (KeyValuePair<string, double> v in val)
                {
                    values[d] = v.Value;
                    d++;
                }
            }
            return values;
        }

        private string[] GetNamesForDemographic()
        {
            int d = 0;
            string[] Demographics = new string[VecSize];
            foreach (string value in DataSatisfaction.Keys)
            {
                IEnumerable val = (IEnumerable)DataSatisfaction[value];
                foreach (KeyValuePair<string, double> v in val)
                {
                    Demographics[d] = v.Key;
                    d++;
                }
                break;
            }
            return Demographics;
        }

        private int[] GetNumbersForDemographic()
        {
            int[] vecDemographic = new int[VecSize*2];
            int aux = 0;
            for (int i = 0; i < VecSize*2; i++)
            {
                vecDemographic[i] = aux;
                aux++;
                if (aux == VecSize)
                    aux = 0;
            }
            return vecDemographic;
        }

        private int[] GetNumbersForSatisfied()
        {
            int[] vecSatisfied = new int[VecSize*2];
            for (int i = 0; i < VecSize*2; i++)
            {
                if (i < VecSize)
                    vecSatisfied[i] = 0;
                else
                    vecSatisfied[i] = 1;
            }
            return vecSatisfied;
        }

        private int GetVectorSize()//para obtener la acantidad de demográficos que hay dentro del diccionario y asi saber si se va o no hacer el chicuadrado
        {
            int size = 0;
            if (DataSatisfaction != null)
            {
                Dictionary<string, double> vector = new Dictionary<string, double>();
                vector = (Dictionary<string, double>)DataSatisfaction.FirstOrDefault().Value;
                size = vector.Count;
            }
            return size;
        }

        private double GetChiSquareValue(int degreesOfFreedom, double significanceLevel)
        {
            return (double)new ChiSquareDistributionServices().GetChiSquareValue(degreesOfFreedom, significanceLevel).Value;
        }

        private int GetDegreesOfFreedom(int rows, int columns)
        {
            int degrees = (rows - 1) * (columns - 1);
            return degrees > 100 ? 100 : degrees;
        }

        #endregion
    }
}
