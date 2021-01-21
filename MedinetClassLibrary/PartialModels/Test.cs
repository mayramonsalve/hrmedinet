using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Services;
using Extreme.Statistics;
using Extreme.Mathematics;
using Extreme.Statistics.Tests;
using System.Collections;
using Extreme.Statistics.Distributions;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Models
{
    public partial class Test
    {
        #region "Properties"

        public double Average
        { 
            get  { return GetGlobalAverage(); }
        }

        public double Median
        {
            get { return GetGlobalMedian(); }
        }

        #endregion

        #region "Text Answers"

        public Dictionary<string, object> GetParametersForTextAnswers(string demographic, int? questionnaire, int category, int question, int? fot)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("test", this.Id);
            parameters.Add("demographic", demographic);
            if(questionnaire.HasValue)
                parameters.Add("questionnaire", questionnaire.Value);
            if (!this.OneQuestionnaire && !questionnaire.HasValue)
                parameters.Add("categorygroup", category);
            else
                parameters.Add("category", category);
            parameters.Add("question", question);
            if(fot.HasValue)
                parameters.Add("fot", fot.Value);
            return parameters;
        }

        public Dictionary<string, List<string>> GetGeneralTextAnswers(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("General", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //if (this.Evaluations != null)
            //{
            //    List<string> text = new List<string>();
            //    ExtractAnswersTexts(category_id, question_id, text, this.Evaluations);
            //    results.Add("General", text);
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByAgeRanges(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("AgeRange", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Level).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().ShortName, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByInstructionLevels(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("InstructionLevel", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Level).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().ShortName, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByPositionLevels(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("PositionLevel", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Level).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().ShortName, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByLocations(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("Location", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location).OrderBy(g => g.Name).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.Location_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().ShortName, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByRegions(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("Region", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location.Region).OrderBy(g => g.Name).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().Name, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByStates(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("State", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().Name, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByCountries(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("Country", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().Name, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersBySeniorities(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("Seniority", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Level).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().ShortName, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByPerformanceEvaluations(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("Performance", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).OrderBy(g => g.Level).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            results.Add(group.First().ShortName, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByGenders(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("Gender", questionnaire_id, category_id.Value, question_id.Value, null)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    List<string> elements = new List<string>();
            //    elements.Add("Female");
            //    elements.Add("Male");
            //    foreach (var element in elements)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluations = this.Evaluations.Where(s => s.Sex == element);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTexts(category_id, question_id, text, evaluations);
            //            if (element == "Male")
            //                results.Add(ViewRes.Classes.ChiSquare.MaleGender, text);
            //            else
            //                results.Add(ViewRes.Classes.ChiSquare.FemaleGender, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, List<string>> GetTextAnswersByFOs(int? questionnaire_id, int? category_id, int? question_id, int type_id)
        {
            Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            results = (Dictionary<string, List<string>>)new Commands("TextAnswers", GetParametersForTextAnswers("FunctionalOrganizationType", questionnaire_id, category_id.Value, question_id.Value, type_id)).ExecuteCommand();
            return results;
            //Dictionary<string, List<string>> results = new Dictionary<string, List<string>>();
            //if (this.Evaluations != null)
            //{
            //    var groups = new EvaluationsFOServices().GetByFunctionalOrganizationTypeAndTest(this.Id, type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
            //    foreach (var group in groups)
            //    {
            //        List<string> text = new List<string>();
            //        var evaluationsFO = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
            //        if (evaluationsFO.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersTextsByEvaluationFO(category_id, question_id, text, evaluationsFO);
            //            results.Add(group.First().ShortName, text);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }
        #endregion

        #region FC

        private Dictionary<string, object> GetParametersFC(string type, string demographic, int? questionnaire_id, int? category_id, int? question_id, int? fot_id, bool f)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("test", this.Id);
            string aux = "minimumPeople";// type == "Category" ? "minimumPeople" : "minimumAnswers";
            parameters.Add(aux, GetMinimumAnswers(demographic, questionnaire_id, category_id, question_id, f));
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (category_id.HasValue)
            {
                if (!this.OneQuestionnaire && !questionnaire_id.HasValue)
                    parameters.Add("categorygroup", category_id.Value);
                else
                    parameters.Add("category", category_id.Value);
                if (question_id.HasValue)
                    parameters.Add("question", question_id.Value);
            }
            if (fot_id.HasValue)
                parameters.Add("fot", fot_id.Value);
            return parameters;
        }

        public Dictionary<string, object> GetDataFrequencyCategory(string type, string demographic, int? questionnaire_id, int? category_id, int? question_id, int? fot_id, int? compare)
        {
            Dictionary<string, object> results = (Dictionary<string, object>)new Commands(type, GetParametersFC(type, demographic, questionnaire_id, category_id, question_id, fot_id, type == "Frequency")).ExecuteCommand();
            return results;
        }

        public int GetQuestionsType()//obtener tipo de cuestionario
        {
            if (this.OneQuestionnaire)
            {
                return this.Questionnaire.Categories.SelectMany(q => q.Questions.Select(t => t.QuestionType_Id)).Distinct().FirstOrDefault();
            }
            else
            {
                return this.GetQuestionnairesByTest().SelectMany(c => c.Categories.SelectMany(q => q.Questions.Select(t => t.QuestionType_Id))).Distinct().FirstOrDefault();
            }
        }

        #endregion

        #region "Average and Median"

        public Dictionary<string, object> GetGeneralAvgAndMed(int? questionnaire_id, int? category_id, int? question_id, bool compare)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            try
            {
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("General", questionnaire_id, category_id, question_id, null, null, null, true, !compare)).ExecuteCommand();
            }
            catch
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add("Average", (Dictionary<string, double>)new Commands("Univariate", GetParameters("General", questionnaire_id, category_id, question_id, null, null, null, true, !compare)).ExecuteCommand());
            }
            if (compare)
            {
                object aux = AvgAndMed["Average"];
                AvgAndMed.Remove("Average");
                AvgAndMed.Add(label, aux);
            }
            //AvgAndMed.Add(label, GetGeneralAvgOrMed(true, category_id, question_id));
            //if(!compare)
                //AvgAndMed.Add("Median", GetGeneralAvgOrMed(false, category_id, question_id));
            return AvgAndMed;
        }

        public double GetAvg(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("General", questionnaire_id, category_id, question_id, null, null, null, true, false)).ExecuteCommand();
            return results.Values.FirstOrDefault();
            //Dictionary<string, double> results = new Dictionary<string, double>();
            //if (this.Evaluations != null)
            //{
            //    List<double> data = new List<double>();
            //    ExtractAnswersValues(category_id, question_id, data, this.Evaluations);
            //    AddResult(true, results, this.Name, data);
            //    return results.Values.FirstOrDefault();
            //}
            //else
            //    return 0;
        }

        public Dictionary<string, object> GetCategoryAvgAndMed(bool compare, int? questionnaire_id, int? country_id,
                                                                int? state_id, int? region_id)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Category", questionnaire_id, null, null, country_id, state_id, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetCategoryAvgOrMed(true, questionnaire_id, country_id, state_id, region_id));
            }
            //return results;
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetCategoryAvgOrMed(true, country_id, state_id, region_id));
            //if (!compare)
                //AvgAndMed.Add("Median", GetCategoryAvgOrMed(false, country_id, state_id, region_id));
            return AvgAndMed;
        }

        public Dictionary<string, double> GetCategoryAvgOrMed(bool Average, int? questionnaire_id, int? country_id,
                                                                int? state_id, int? region_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("Category", questionnaire_id, null, null, country_id, state_id, null, Average, !Average)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string,double>();
            //IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
            //if (Evaluations.Count() > 0)
            //{
            //    foreach (Category cat in this.Questionnaire.Categories)
            //    {
            //        List<double> data = new List<double>();
            //        ExtractAnswersValues(cat.Id, null, data, Evaluations);
            //        AddResult(Average, results, cat.Name, data);
            //    }
            //    Dictionary<string, double> catResults = new Dictionary<string, double>();
            //    foreach (KeyValuePair<string, double> cat in (IEnumerable)results.OrderByDescending(key => key.Value))
            //    {
            //        catResults.Add(cat.Key, results[cat.Key]);
            //    }
            //    return catResults;
            //    //return (Dictionary<string, double>)results.OrderByDescending(a => a.Value);

            //    //IEnumerable<Dictionary<string, double>> aux = results.OrderByDescending(a => a.Value).ToArray();
            //}
            //else
            //    return null;
        }

        private string GetLabel(bool compare)
        {
            string label;
            if (compare)
                label = this.Name;
            else
                label = "Average";
            return label;
        }

        public Dictionary<string, object> GetGeneralAvgAndMedByUbication(int? questionnaire_id, int? category_id, int? question_id,
                                            int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("General", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            //AvgAndMed.Add("Average", GetGeneralAvgOrMedByUbication(true, category_id, question_id,
            //                                                        country_id, state_id, region_id));
            //AvgAndMed.Add("Median", GetGeneralAvgOrMedByUbication(false, category_id, question_id,
            //                                                        country_id, state_id, region_id));
            return AvgAndMed;
        }

        private Dictionary<string, object> GetParameters(string demographic, int? questionnaire_id, int? category_id, int? question_id, int? country_id, int? state_id, int? fot_id, bool avg, bool med)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("test", this.Id);
            parameters.Add("dataType", "AvgAndMed");
            if (country_id.HasValue)
                parameters.Add("country", country_id.Value);
            if (state_id.HasValue)
                parameters.Add("state", state_id.Value);
            bool minimumPeople = demographic == "Category";
            string aux = minimumPeople ? "minimumPeople" : "minimumAnswers";
            parameters.Add(aux, GetMinimumAnswers(demographic, questionnaire_id, category_id, question_id, minimumPeople));
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            if (category_id.HasValue)
            {
                if (!this.OneQuestionnaire && !questionnaire_id.HasValue)
                    parameters.Add("categorygroup", category_id.Value);
                else
                    parameters.Add("category", category_id.Value);
                if (question_id.HasValue)
                    parameters.Add("question", question_id.Value);
            }
            if (fot_id.HasValue)
                parameters.Add("fot", fot_id.Value);
            parameters.Add("avg", avg ? 1 : 0);
            parameters.Add("med", med ? 1 : 0);
            //parameters.Add("minimumPeople", this.MinimumPeople);
            return parameters;
        }

        public Dictionary<string, double> GetGeneralAvgOrMedByUbication(bool isAverage, int? questionnaire_id, int? category_id, int? question_id,
                                            int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("General", questionnaire_id, category_id, question_id, country_id, state_id, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
            //Dictionary<string, double> results = new Dictionary<string, double>();
            //if (Evaluations.Count() > 0)
            //{
            //    List<double> data = new List<double>();
            //    ExtractAnswersValues(category_id, question_id, data, Evaluations);
            //    AddResult(isAverage, results, this.Name, data);
            //    return results;
            //}
            //else
            //    return null;
        }

        //private IQueryable<Evaluation> GetEvaluationsByUbication(int? country_id, int? state_id, int? region_id)
        //{
        //    IQueryable<Evaluation> evaluations = this.Evaluations.AsQueryable();
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

        public Dictionary<string, double> GetGeneralAvgOrMed(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("General", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            //Dictionary<string, double> aux = results.ToDictionary(k => k.Key, k => Double.Parse(k.Value.ToString()));
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();
            //if (this.Evaluations != null)
            //{
            //    List<double> data = new List<double>();
            //    ExtractAnswersValues(category_id, question_id, data,this.Evaluations);
            //    AddResult(isAverage, results, this.Name, data);
            //    return results;
            //}
            //else
            //    return null;
        }
        
        //ENLAZAR CON SP
        public Dictionary<string[], double> GetGeneralPctgByCountry(List<int> countriesId)
        {
            if (this.Evaluations != null)
            {
                Dictionary<string[], double> results = new Dictionary<string[], double>();
                foreach (int id in countriesId)
                {
                    Country country = new CountriesServices().GetById(id);
                    Dictionary<string, double> resultsAux = new Dictionary<string, double>();
                    resultsAux = (Dictionary<string, double>)new Commands("Univariate", GetParameters("General", null, null, null, id, null, null, true, false)).ExecuteCommand();
                    results.Add(new string[] { country.Code, country.Name, country.Map != null ? country.Map : "-", country.Id.ToString() }, resultsAux.Values.FirstOrDefault() * 100 / this.Questionnaire.Options.Count);
                }

                //var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
                //foreach (var group in groups)
                //{
                //    if (countriesId.Contains(group.First().Id))
                //    {
                //        List<double> data = new List<double>();
                //        var evaluations = this.Evaluations.Where(c => c.Location.State.Country_Id == group.First().Id);
                //        ExtractAnswersValues(null, null, data, evaluations);
                //        //AddResult(true, results, group.First().Name, data);
                //        results.Add(new string[] { group.First().Code, group.First().Name, group.First().Map != null ? group.First().Map : "-", group.First().Id.ToString() }, data.Average() * 100 / this.Questionnaire.Options.Count);
                //    }
                //}
                return results;
            }
            else
                return null;
        }
        
        //ENLAZAR CON SP
        public Dictionary<string[], double> GetGeneralPctgByState(int country_id, List<int> statesId)
        {
            Dictionary<string[], double> results = new Dictionary<string[], double>();
            if (this.Evaluations != null)
            {
                IQueryable<Evaluation> evalGroup = this.Evaluations.Where(s => s.Location.State.Country_Id == country_id).AsQueryable();
                var groups = evalGroup.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
                foreach (var group in groups)
                {
                    if (statesId.Contains(group.First().Id))
                    {
                        List<double> data = new List<double>();
                        var evaluations = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
                        ExtractAnswersValues(null, null, data, evaluations);
                        results.Add(new string[] { group.First().Code, group.First().Name, "-", group.First().Id.ToString() }, data.Average() * 100 / this.Questionnaire.Options.Count);
                    }
                }
                return results;
            }
            else
                return null;
        }
        
        //ENLAZAR CON SP
        public Dictionary<string, double> GetAvgByBranch(int branch_id, string branch_name)
        {
            Dictionary<string, double> results = new Dictionary<string, double>();
            IQueryable<Evaluation> Evaluations = this.Evaluations.Where(b => b.Location_Id == branch_id).AsQueryable();
            if (Evaluations.Count() > 0)
            {
                List<double> data = new List<double>();
                ExtractAnswersValues(null, null, data, Evaluations);
                AddResult(true, results, branch_name, data);
            }
            return results;
        }

        public Dictionary<string, object> GetAvgAndMedByFOTypes(int? questionnaire_id, int? category_id, int? question_id, int type_id, bool compare,
                                                                int? country_id = null, int? state_id = null, int? region_id = null)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("FunctionalOrganizationType", questionnaire_id, category_id, question_id, country_id, state_id, type_id, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByFOs(true, questionnaire_id, category_id, question_id, type_id));
            }
            
            //AvgAndMed.Add(label, GetAvgOrMedByFOs(true, category_id, question_id, type_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByFOs(false, category_id, question_id, type_id));
            return AvgAndMed;
        }

        //public Dictionary<string, object> GetAvgOrMedByFOTypes(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> results = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var types = new EvaluationsFOServices().GetByTest(this.Id).Select(fo => fo.FunctionalOrganization.FunctionalOrganizationType).OrderBy(fo => fo.Name).Distinct();
        //        foreach (var type in types)
        //        {
        //            results.Add(type.Name, GetAvgOrMedByFOs(isAverage, category_id, question_id, type.Id));
        //        }
        //        return results;
        //    }
        //    else
        //        return null;
        //}

        public Dictionary<string, double> GetAvgOrMedByFOs(bool isAverage, int? questionnaire_id, int? category_id, int? question_id, int type_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("FunctionalOrganizationType", questionnaire_id, category_id, question_id, null, null, type_id, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = new EvaluationsFOServices().GetByFunctionalOrganizationTypeAndTest(this.Id,type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
                
            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluationsFO = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
            //        if (evaluationsFO.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValuesByEvaluationFO(category_id, question_id, data, evaluationsFO);
            //            AddResult(isAverage, results, group.First().ShortName, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByAgeRanges(int? questionnaire_id, int? category_id, int? question_id, bool compare,
                                                                int? country_id = null, int? state_id = null, int? region_id = null)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("AgeRange", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByAgeRanges(true, questionnaire_id, category_id, question_id));
            }
            return AvgAndMed;
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedByAgeRanges(true, category_id, question_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByAgeRanges(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByAgeRanges(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("AgeRange", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string,double> results = new Dictionary<string,double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().ShortName, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByGender(int? questionnaire_id, int? category_id, int? question_id, bool compare)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Gender", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByGender(true, questionnaire_id, category_id, question_id));
            }
            return AvgAndMed;
            //string label = GetLabel(compare);
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedByGender(true, category_id, question_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByGender(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByGender(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("Gender", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    List<string> elements = new List<string>();
            //    elements.Add("Female");
            //    elements.Add("Male");

            //    foreach (var element in elements)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.Sex == element);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            if (element == "Male")
            //                AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
            //            else
            //                AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
                        
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByInstructionLevels(int? questionnaire_id, int? category_id, int? question_id, bool compare)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("InstructionLevel", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByInstructionLevels(true, questionnaire_id, category_id, question_id));
            }
            return AvgAndMed;
            //string label = GetLabel(compare);
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedByInstructionLevels(true, category_id, question_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByInstructionLevels(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByInstructionLevels(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("InstructionLevel", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().ShortName, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByLocations(int? questionnaire_id, int? category_id, int? question_id, bool compare,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Location", questionnaire_id, category_id, question_id, country_id, state_id, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByLocations(true, questionnaire_id, category_id, question_id, country_id, state_id, region_id));
            }
            return AvgAndMed;
            //string label = GetLabel(compare);
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedByLocations(true, category_id, question_id, country_id, state_id, region_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByLocations(false, category_id, question_id, country_id, state_id, region_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByLocations(bool isAverage, int? questionnaire_id, int? category_id, int? question_id,
                                                                    int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("Location", questionnaire_id, category_id, question_id, country_id, state_id, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();
            //IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
            //if (Evaluations.Count() > 0)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = Evaluations.Where(s => s.Location_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().ShortName, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByRegions(int? questionnaire_id, int? category_id, int? question_id, bool compare)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Region", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByRegions(true, questionnaire_id, category_id, question_id));
            }
            return AvgAndMed;
            //string label = GetLabel(compare);
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedByRegions(true, category_id, question_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByRegions(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByRegions(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("Region", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().Name, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByStates(int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, object> AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("State", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            return AvgAndMed;
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add("Average", GetAvgOrMedByStates(true, category_id, question_id));
            //AvgAndMed.Add("Median", GetAvgOrMedByStates(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByStates(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("State", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().Name, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByStates(int? questionnaire_id, int? category_id, int? question_id, int country_id)
        {
            Dictionary<string, object> AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("State", questionnaire_id, category_id, question_id, country_id, null, null, true, true)).ExecuteCommand();
            return AvgAndMed;
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add("Average", GetAvgOrMedByStates(true, category_id,question_id ,country_id));
            //AvgAndMed.Add("Median", GetAvgOrMedByStates(false, category_id,question_id, country_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByStates(bool isAverage, int? questionnaire_id, int? category_id, int? question_id, int country_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("State", questionnaire_id, category_id, question_id, country_id, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location.State).Where(c => c.Country_Id==country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().Name, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByCountries(int? questionnaire_id, int? category_id, int? question_id, bool compare)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Country", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByCountries(true, questionnaire_id, category_id, question_id));
            }
            return AvgAndMed;
            //string label = GetLabel(compare);
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedByCountries(true, category_id, question_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByCountries(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByCountries(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("Country", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().Name, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByPositionLevels(int? questionnaire_id, int? category_id, int? question_id, bool compare)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("PositionLevel", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByPositionLevels(true, questionnaire_id, category_id, question_id));
            }
            return AvgAndMed;
            //string label = GetLabel(compare);
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedByPositionLevels(true, category_id, question_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByPositionLevels(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByPositionLevels(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("PositionLevel", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().ShortName, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedBySeniorities(int? questionnaire_id, int? category_id, int? question_id, bool compare)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Seniority", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedBySeniorities(true, questionnaire_id, category_id, question_id));
            }
            return AvgAndMed;
            //string label = GetLabel(compare);
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedBySeniorities(true, category_id, question_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedBySeniorities(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedBySeniorities(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("Seniority", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
            //        if (evaluations.Count() >= this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().ShortName, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        public Dictionary<string, object> GetAvgAndMedByPerformanceEvaluations(int? questionnaire_id, int? category_id, int? question_id, bool compare)
        {
            string label = GetLabel(compare);
            Dictionary<string, object> AvgAndMed;
            if (!compare)
                AvgAndMed = (Dictionary<string, object>)new Commands("Univariate", GetParameters("Performance", questionnaire_id, category_id, question_id, null, null, null, true, true)).ExecuteCommand();
            else
            {
                AvgAndMed = new Dictionary<string, object>();
                AvgAndMed.Add(label, GetAvgOrMedByPerformanceEvaluations(true, questionnaire_id, category_id, question_id));
            }
            return AvgAndMed;
            //string label = GetLabel(compare);
            //Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();
            //AvgAndMed.Add(label, GetAvgOrMedByPerformanceEvaluations(true, category_id, question_id));
            //if(!compare)
            //    AvgAndMed.Add("Median", GetAvgOrMedByPerformanceEvaluations(false, category_id, question_id));
            //return AvgAndMed;
        }

        public Dictionary<string, double> GetAvgOrMedByPerformanceEvaluations(bool isAverage, int? questionnaire_id, int? category_id, int? question_id)
        {
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Univariate", GetParameters("Performance", questionnaire_id, category_id, question_id, null, null, null, isAverage, !isAverage)).ExecuteCommand();
            return results;
            //Dictionary<string, double> results = new Dictionary<string, double>();

            //if (this.Evaluations != null)
            //{
            //    var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);

            //    foreach (var group in groups)
            //    {
            //        List<double> data = new List<double>();
            //        var evaluations = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
            //        if (evaluations!=null && evaluations.Count()>=this.MinimumPeople)
            //        {
            //            ExtractAnswersValues(category_id, question_id, data, evaluations);
            //            AddResult(isAverage, results, group.First().ShortName, data);
            //        }
            //    }
            //    return results;
            //}
            //else
            //    return null;
        }

        #endregion

        //#region "Bivariables"

        //#region AgeRanges

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndGender(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndLocations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndRegions(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.Region_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndStates(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndStates(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country_Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndCountries(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State.Country_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByAgeRangesAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Age_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region Gender

        //public Dictionary<string, object> GetAvgOrMedByGenderAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}
        
        //public Dictionary<string, object> GetAvgOrMedByGenderAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByGenderAndLocations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByGenderAndRegions(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.Region_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByGenderAndStates(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByGenderAndStates(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country_Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByGenderAndCountries(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State.Country_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //                if (group == "Male")
        //                    objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //                else
        //                    objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //            }
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByGenderAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByGenderAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByGenderAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        List<string> groups = new List<string>();
        //        groups.Add("Male");
        //        groups.Add("Female");
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Sex == group);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            if (group == "Male")
        //                objects.Add(ViewRes.Classes.ChiSquare.MaleGender, results);
        //            else
        //                objects.Add(ViewRes.Classes.ChiSquare.FemaleGender, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region InstructionLevels

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedInstructionLevelsAndGender(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndLocations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndRegions(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.Region_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndStates(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndStates(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country_Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndCountries(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State.Country_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByInstructionLevelsAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.InstructionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region Locations

        //public Dictionary<string, object> GetAvgOrMedByLocationsAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByLocationsAndGenders(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByLocationsAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByLocationsAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByLocationsAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByLocationsAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region Regions

        //public Dictionary<string, object> GetAvgOrMedByRegionsAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedRegionsAndGender(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByRegionsAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByRegionsAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByRegionsAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByRegionsAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.Region_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region States

        //public Dictionary<string, object> GetAvgOrMedByStatesAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndAgeRanges(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country.Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndGenders(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndGenders(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country.Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndInstructionLevels(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country.Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndPositionLevels(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country.Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndSeniorities(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country.Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByStatesAndPerformanceEvaluations(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country.Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region Countries

        //public Dictionary<string, object> GetAvgOrMedByCountriesAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedCountriesAndGender(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByCountriesAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByCountriesAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByCountriesAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByCountriesAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().Name, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region PositionLevels

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedPositionLevelsAndGender(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndLocations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndRegions(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.Region_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndStates(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndStates(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country_Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndCountries(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State.Country_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPositionLevelsAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region Seniorities

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndGender(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndLocations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndRegions(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.Region_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndStates(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndStates(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country_Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndCountries(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State.Country_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedBySenioritiesAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Seniority_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region PerformanceEvaluations

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndAgeRanges(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndGender(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if (element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndInstructionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndLocations(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndRegions(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.Region_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndStates(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndStates(bool isAverage, int? category_id, int country_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country_Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndCountries(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State.Country_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndPositionLevels(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByPerformanceEvaluationsAndSeniorities(bool isAverage, int? category_id, int? question_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsGroup = this.Evaluations.Where(s => s.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#region FunctionalOrganizations

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndAgeRanges(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        //var groups = this.Evaluations.Select(g => g.PerformanceEvaluation).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo =>fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            //var evaluationsGroup = this.Evaluations.Where(e => e.Performance_Id == group.First().Id);
        //            var elements = this.Evaluations.Select(g => g.Age).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Age_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndGender(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id); 
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup); 
        //            List<string> elements = new List<string>();
        //            elements.Add("Male");
        //            elements.Add("Female");
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Sex == element);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                if(element == "Male")
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.MaleGender, data);
        //                else
        //                    AddResult(isAverage, results, ViewRes.Classes.ChiSquare.FemaleGender, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndInstructionLevels(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id); 
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.InstructionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.InstructionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndLocations(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.Location).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndRegions(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.Location.Region).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.Region_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndStates(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndStates(bool isAverage, int? category_id, int country_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.Location.State).Where(g => g.Country_Id == country_id).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndCountries(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Location.State.Country_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().Name, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndPositionLevels(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.PositionLevel_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndSeniorities(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.Seniority).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Seniority_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //public Dictionary<string, object> GetAvgOrMedByFunctionalOrganizationsAndPerformanceEvaluations(bool isAverage, int? category_id, int? question_id, int type_id)
        //{
        //    Dictionary<string, object> objects = new Dictionary<string, object>();
        //    if (this.Evaluations != null)
        //    {
        //        var groups = new EvaluationsFOServices().GetByFunctionalOrganizationType(type_id).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
        //        foreach (var group in groups)
        //        {
        //            Dictionary<string, double> results = new Dictionary<string, double>();
        //            var evaluationsFOGroup = new EvaluationsFOServices().GetByTest(this.Id).Where(fo => fo.FunctionalOrganization_Id == group.First().Id);
        //            var evaluationsGroup = GetEvaluationsFromEvaluationsFO(evaluationsFOGroup);
        //            var elements = this.Evaluations.Select(g => g.PerformanceEvaluation).Where(n => n != null).OrderBy(g => g.Name).GroupBy(g => g.Id);
        //            foreach (var element in elements)
        //            {
        //                List<double> data = new List<double>();
        //                var evaluations = evaluationsGroup.Where(s => s.Performance_Id == element.First().Id);
        //                ExtractAnswersValues(category_id, null, data, evaluations);
        //                AddResult(isAverage, results, element.First().ShortName, data);
        //            }
        //            objects.Add(group.First().ShortName, results);
        //        }
        //        return objects;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

        //#endregion

        #region "Private Methods"

        private static IEnumerable<Evaluation> GetEvaluationsFromEvaluationsFO(IEnumerable<EvaluationFO> evaluationsFO)
        {
            List<Evaluation> evaluations = new List<Evaluation>();
            Evaluation e;
            foreach (EvaluationFO efo in evaluationsFO)
            {
                e = new EvaluationsServices().GetById(efo.Evaluation_Id);
                evaluations.Add(e);
            }
            return evaluations.Distinct();
        }

        private static void AddResult(bool isAverage, Dictionary<string, double> results, string name, List<double> data)
        {
            if (isAverage)
                results.Add(name, data.Count > 0 ? Convert.ToDouble(String.Format("{0:0.##}",data.Average())) : 0);
            else
                results.Add(name, Stats.Median(data.ToArray<double>()));
        }

        private void ExtractAnswersValuesByEvaluationFO(int? category_id, int? question_id, List<double> data, IEnumerable<EvaluationFO> evaluationsFO)
        {
            IQueryable<Evaluation> evaluations = this.Evaluations.Where(e => evaluationsFO.Select(efo => efo.Evaluation_Id).Distinct().Contains(e.Id)).AsQueryable();
            IEnumerable<double> answers;
            WeighingsServices ws = new WeighingsServices();
            if (this.Weighted)
            {
                if (category_id.HasValue)
                {
                    if (question_id.HasValue)
                        answers = evaluations.SelectMany(a => a.SelectionAnswers.Where(c => c.Question_Id == question_id.Value).Select(o => (double)o.Option.Value * (double)ws.GetValueByTestAndCategory(this.Id, o.Question.Category_Id) / 100)).AsEnumerable();
                    else
                        answers = evaluations.SelectMany(a => a.SelectionAnswers.Where(c => c.Question.Category_Id == category_id.Value).Select(o => (double)o.Option.Value * (double)ws.GetValueByTestAndCategory(this.Id, o.Question.Category_Id) / 100)).AsEnumerable();
                }
                else
                    answers = evaluations.SelectMany(a => a.SelectionAnswers.Select(o => (double)o.Option.Value * (double)ws.GetValueByTestAndCategory(this.Id, o.Question.Category_Id) / 100)).AsEnumerable();
            }
            else
            {
                if (category_id.HasValue)
                {
                    if (question_id.HasValue)
                        answers = evaluations.SelectMany(a => a.SelectionAnswers.Where(c => c.Question_Id == question_id.Value).Select(o => (double)o.Option.Value)).AsEnumerable();
                    else
                        answers = evaluations.SelectMany(a => a.SelectionAnswers.Where(c => c.Question.Category_Id == category_id.Value).Select(o => (double)o.Option.Value)).AsEnumerable();
                }
                else
                    answers = evaluations.SelectMany(a => a.SelectionAnswers.Select(o => (double)o.Option.Value)).AsEnumerable();
            }
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

            //    WeighingsServices weighingService = new WeighingsServices();

            //    foreach (var answer in answers)
            //    {
            //        if (efo.Evaluation.Test.Weighted)
            //            data.Add(answer.Option.Value * (double)weighingService.GetValueByTestAndCategory(efo.Evaluation.Test_Id, answer.Question.Category_Id) / 100);
            //        else
            //            data.Add(answer.Option.Value);
            //    }
            //}
        }

        private void ExtractAnswersTextsByEvaluationFO(int? category_id, int? question_id, List<string> text, IEnumerable<EvaluationFO> evaluationsFO)
        {
            IQueryable<Evaluation> evaluations = this.Evaluations.Where(e => evaluationsFO.Select(efo => efo.Evaluation_Id).Distinct().Contains(e.Id)).AsQueryable();
            List<string> answers;
            if (category_id.HasValue)
            {
                if (question_id.HasValue)
                    answers = evaluations.SelectMany(a => a.TextAnswers.Where(c => c.Question_Id == question_id.Value).Select(t => t.Text)).ToList();
                else
                    answers = evaluations.SelectMany(a => a.TextAnswers.Where(c => c.Question.Category_Id == category_id.Value).Select(t => t.Text)).ToList();
            }
            else
                answers = evaluations.SelectMany(a => a.TextAnswers.Select(t => t.Text)).ToList();
            text.AddRange(answers);
        }

        private void ExtractAnswersValues(int? category_id, int? question_id, List<double> data, IEnumerable<Evaluation> evaluations)
        {
            IEnumerable<double> answers;
            WeighingsServices ws = new WeighingsServices();
            if (this.Weighted)
            {
                if (category_id.HasValue)
                {
                    if (question_id.HasValue)
                        answers = evaluations.SelectMany(a => a.SelectionAnswers.Where(c => c.Question_Id == question_id.Value).Select(o => (double)o.Option.Value * (double)ws.GetValueByTestAndCategory(this.Id, o.Question.Category_Id) / 100)).AsEnumerable();
                    else
                        answers = evaluations.SelectMany(a => a.SelectionAnswers.Where(c => c.Question.Category_Id == category_id.Value).Select(o => (double)o.Option.Value * (double)ws.GetValueByTestAndCategory(this.Id, o.Question.Category_Id) / 100)).AsEnumerable();
                }
                else
                    answers = evaluations.SelectMany(a => a.SelectionAnswers.Select(o => (double)o.Option.Value * (double)ws.GetValueByTestAndCategory(this.Id, o.Question.Category_Id) / 100)).AsEnumerable();
            }
            else
            {
                if (category_id.HasValue)
                {
                    if (question_id.HasValue)
                        answers = evaluations.SelectMany(a => a.SelectionAnswers.Where(c => c.Question_Id == question_id.Value).Select(o => (double)o.Option.Value)).AsEnumerable();
                    else
                        answers = evaluations.SelectMany(a => a.SelectionAnswers.Where(c => c.Question.Category_Id == category_id.Value).Select(o => (double)o.Option.Value)).AsEnumerable();
                }
                else
                    answers = evaluations.SelectMany(a => a.SelectionAnswers.Select(o => (double)o.Option.Value)).AsEnumerable();
            }
            data.AddRange(answers);
            //if (this.Weighted)
            //    data.Add((double)(answer.Option.Value * (double)weighingService.GetValueByTestAndCategory(e.Test_Id, answer.Question.Category_Id) / 100));
            //else
            //foreach (Evaluation e in evaluations)
            //{
            //    //IQueryable<SelectionAnswer> answers = e.SelectionAnswers.AsQueryable();
            //    if (category_id != null)
            //    {
            //        answers = answers.Where(q => q.Question.Category_Id == category_id);
            //        if (question_id != null)
            //            answers = answers.Where(q => q.Question_Id == question_id);
            //    }
            //    //WeighingsServices weighingService = new WeighingsServices();
            //    foreach (var answer in answers)
            //    {
            //        if (e.Test.Weighted)
            //            data.Add((double)(answer.Option.Value * (double)weighingService.GetValueByTestAndCategory(e.Test_Id, answer.Question.Category_Id) / 100));
            //        else
            //            data.Add(answer.Option.Value);
            //    }
            //}
        }

        private void ExtractAnswersTexts(int? category_id, int? question_id, List<string> texts, IEnumerable<Evaluation> evaluations)
        {
            List<string> answers;
            if (category_id.HasValue)
            {
                if (question_id.HasValue)
                    answers = evaluations.SelectMany(a => a.TextAnswers.Where(c => c.Question_Id == question_id.Value).Select(t => t.Text)).ToList();
                else
                    answers = evaluations.SelectMany(a => a.TextAnswers.Where(c => c.Question.Category_Id == category_id.Value).Select(t => t.Text)).ToList();
            }
            else
                answers = evaluations.SelectMany(a => a.TextAnswers.Select(t => t.Text)).ToList();
            texts.AddRange(answers);
        }

        private double GetGlobalAverage()
        {
            if (this.Evaluations.Count > 0)
            {
                List<double> data = new List<double>();
                var evaluations = this.Evaluations;

                foreach (Evaluation e in evaluations)
                {
                    foreach (var answer in e.SelectionAnswers)
                    {
                        data.Add(answer.Option.Value);
                    }
                }
                return data.Average();
            }
            else
                return 0;
        }

        private double GetGlobalMedian()
        {
            if (this.Evaluations != null)
            {
                var evaluations = this.Evaluations;
                List<double> data = new List<double>();

                WeighingsServices weighingService = new WeighingsServices();

                foreach (Evaluation e in evaluations)
                {
                    foreach (var answer in e.SelectionAnswers)
                    {
                        if (this.Weighted)
                            data.Add(answer.Option.Value * (double)weighingService.GetValueByTestAndCategory(e.Test_Id,answer.Question.Category_Id) / 100);
                        else
                            data.Add(answer.Option.Value);
                    }
                }
                return Stats.Median(data.ToArray<double>());
            }
            else
                return 0;
        }

        private string GetNameSelectorByIdId(int id)
        {
            string demo = GetDemographicSelector();
            switch (demo)
            {
                case "AgeRange":
                    return new AgesServices().GetById(id).Name;
                case "InstructionLevel":
                    return new InstructionLevelsServices().GetById(id).Name;
                case "Location":
                    return new LocationsServices().GetById(id).Name;
                case "Performance":
                    return new PerformanceEvaluationsServices().GetById(id).Name;
                case "PositionLevel":
                    return new PositionLevelsServices().GetById(id).Name;
                case "Seniority":
                    return new SenioritiesServices().GetById(id).Name;
                case "Country":
                    return new CountriesServices().GetById(id).Name;
                case "State":
                    return new StatesServices().GetById(id).Name;
                case "Region":
                    return new RegionsServices().GetById(id).Name;
                case "FunctionalOrganizationType":
                    return new FunctionalOrganizationsServices().GetById(id).Name;
                case "Gender":
                    if (id == 0)
                    {
                        return ViewRes.Classes.ChiSquare.FemaleGender;
                    }
                    else {

                        return ViewRes.Classes.ChiSquare.MaleGender;
                    }
                default:
                    return "";
            }
        }

        #endregion

        public List<string> GetQuestionnairesNameByTest()
        {
            IQueryable<Questionnaire> questionnaires = new DemographicSelectorDetailsServices().GetByTest(this.Id).Select(q => q.Questionnaire).Distinct();
            return questionnaires.Select(n => n.Name).ToList();
        }

        public List<Questionnaire> GetQuestionnairesByTest()
        {
           return new DemographicSelectorDetailsServices().GetByTest(this.Id).Select(q => q.Questionnaire).OrderBy(q => q.Id).ToList();
        }

        public Dictionary<int, int> GetDicOfQuestionnairesAndSelectorValuesByTest()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "QUESTIONNAIRESANDSELECTORVALUESBYTESTID");
            parameters.Add("id", this.Id);
            List<object[]> data = new Commands("QuestionnairesSelectorValues", parameters).GetData();
            Dictionary<int, int> quest_selval = new Dictionary<int, int>();
            foreach(object[] item in data)
            {
                quest_selval.Add((int)item[0], (int)item[1]);
            }
            return quest_selval;
        }

        public string GetDemographicSelector()
        {
            DemographicsInTest dit = new DemographicsInTestsServices().GetSelector(this.Id);
            return dit != null ? (dit.FOT_Id.HasValue ? dit.FunctionalOrganizationType.Name : dit.Demographic.Name) : "";
            //return "Demografico Selector";
        }

        public Dictionary<string, string> GetMatching()
        {
            Dictionary<string, string> matching = new Dictionary<string, string>();
            IQueryable<DemographicSelectorDetail> details = new DemographicSelectorDetailsServices().GetByTest(this.Id);
            //var details=new DemographicSelectorDetailsServices().GetByTest(this.Id).FirstOrDefault();
           foreach (DemographicSelectorDetail detail in details)
           {
                
                matching.Add(detail.Questionnaire.Name, GetNameSelectorByIdId(detail.SelectorValue_Id));
           }
            return matching;
        }

        public List<string> GetDemographicsInTest()
        {
            IQueryable<DemographicSelectorDetail> details = new DemographicSelectorDetailsServices().GetByTest(this.Id);
            foreach (DemographicSelectorDetail detail in details)
            {

                GetNameSelectorByIdId(detail.SelectorValue_Id);
            }
            return new DemographicsInTestsServices().GetByTest(this.Id).Select(n => n.Demographic.Name).ToList();
        }

        public int GetSimilarTestsCount()
        {
            if (this.OneQuestionnaire)
            {
                return this.Questionnaire.Tests.Where(t => t.Company_Id == this.Company_Id && t.Evaluations.Count > 0).Count();
            }
            else
            {
                return this.Company.Tests.Where(o => !o.OneQuestionnaire && o.Evaluations.Count > 0 && o.GetQuestionnairesByTest() == this.GetQuestionnairesByTest()).Count();
            }
        }

        public Questionnaire GetQuestionnaireForMinimumPeople(int? questionnaire_id)
        {
            if (this.OneQuestionnaire)
                return this.Questionnaire;
            else if (questionnaire_id.HasValue)
                return new QuestionnairesServices().GetById(questionnaire_id.Value);
            else
            {
                List<Questionnaire> questionnaires = this.GetQuestionnairesByTest();
                int minValue = questionnaires.SelectMany(cc => cc.Categories.Select(c => c.Questions.Count)).Min();
                return questionnaires.Where(q => q.Categories.Select(c => c.Questions.Count).Min() == minValue).FirstOrDefault();
            }
        }

        public IEnumerable<Option> GetOptionsByTest()
        {
            IEnumerable<Option> options = new List<Option>().AsEnumerable();
            if (this.OneQuestionnaire)
            {
                options = this.Questionnaire.Options.OrderBy(v => v.Value);
            }
            else
            {
                List<Questionnaire> questionnaires = this.GetQuestionnairesByTest();
                int maxValue = questionnaires.SelectMany(oo => oo.Options.Select(v => v.Value)).Max();
                options = questionnaires.Where(o => o.Options.Select(v => v.Value).Max() == maxValue).FirstOrDefault().Options.OrderBy(v => v.Value);
            }
            return options;
        }

        public int GetMinimumAnswers(string demographic, int? questionnaire_id, int? category_id, int? question_id, bool minimumPeople)
        {
            if (minimumPeople)//cuando busca cantidad de personas
                return this.MinimumPeople;
            else
            {
                Questionnaire questionnaire = GetQuestionnaireForMinimumPeople(questionnaire_id);
                if (category_id.HasValue)
                {
                    if (this.OneQuestionnaire)
                    {
                        if (question_id.HasValue)
                            return this.MinimumPeople;
                        else
                            return (this.MinimumPeople * questionnaire.Categories.Where(c => c.Id == category_id.Value).FirstOrDefault().Questions.Count);//5personas minimas*50preguntas=250 respuestas. return 250
                    }
                    else
                    {
                        Category category = new CategoriesServices().GetById(category_id.Value);
                        return (this.MinimumPeople * category.Categories.SelectMany(q => q.Questions).Count());
                    }
                }
                else
                {
                    return (this.MinimumPeople * questionnaire.Categories.SelectMany(q => q.Questions).Count());//this.MinimumPeople;
                }
            }
        }

    }
}
