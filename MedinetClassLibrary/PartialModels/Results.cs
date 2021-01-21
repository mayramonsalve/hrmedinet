using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extreme.Statistics;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Models
{
    public class Results
    {
        private Test Test;
        public int satisfiedValue;
        private Questionnaire Questionnaire;

        public Results() { }

        public Results(Test Test)
        {
            this.Test = Test;
            this.satisfiedValue = GetSatisfiedValue();
        }

        public double GetAnswersCount(int? category_id, int? question_id, string answerType)
        {
            if (Test.Evaluations.Count > 0)
            {
                List<double> data = new List<double>();
                GetAnswersCountByAnswerType(Test.Evaluations.AsQueryable(), category_id, question_id, data, answerType);
                double avg = data.Average();
                return avg;
            }
            else
                return 0;
        }

        public double GetPositiveAnswerPctgByEvaluation(Evaluation e, int? category_id)
        {
            if (Test.Evaluations.Count > 0)
            {
                List<double> data = new List<double>();
                GetAnswersCountByAnswerTypeByEvaluation(e.SelectionAnswers.AsQueryable(), category_id, data);
                return data.Count * 100 / e.SelectionAnswers.Count;
            }
            else
                return 0;
        }

        private void GetAnswersCountByAnswerTypeByEvaluation(IQueryable<SelectionAnswer> answers, int? category_id, List<double> data)
        {
            if (category_id != null)
            {
                answers = answers.Where(q => q.Question.Category_Id == category_id);
            }
            int questionsCount = answers.Count();
            if (questionsCount > 0)
            {
                answers = GetAnswersByCondition("Positivas", answers);
                data.Add(answers.Count() * 100 / questionsCount);
            }
        }

        private void GetAnswersCountByAnswerType(IQueryable<Evaluation> Evaluations, int? category_id, int? question_id, List<double> data, string answerType)
        {
            int questionsCount = 0;
            IQueryable<SelectionAnswer> answers = Evaluations.SelectMany(sa => sa.SelectionAnswers);
            //foreach (Evaluation e in Evaluations)
            //{
            //    answers = e.SelectionAnswers.AsQueryable();
                if (category_id != null)
                {
                    answers = answers.Where(q => q.Question.Category_Id == category_id);
                    if (question_id != null)
                        answers = answers.Where(q => q.Question_Id == question_id);
                }
                questionsCount = answers.Count();
            //}
                if (questionsCount > 0)
                {
                    answers = GetAnswersByCondition(answerType, answers);
                    double prom = (double)(answers.Count() * 100) / questionsCount;
                    data.Add((double)prom);
                }
            
        }

        private IQueryable<SelectionAnswer> GetAnswersByCondition(string answerType, IQueryable<SelectionAnswer> answers)
        {
            switch (answerType)
            {
                case "Positivas":
                    answers = answers.Where(o => o.Option.Value > satisfiedValue);
                    break;
                case "Neutras":
                    if(satisfiedValue % 2 != 0)
                        answers = answers.Where(o => o.Option.Value == satisfiedValue);
                    break;
                default:
                    if (satisfiedValue % 2 != 0)
                        answers = answers.Where(o => o.Option.Value < satisfiedValue);
                    else
                        answers = answers.Where(o => o.Option.Value <= satisfiedValue);
                    break;
            }
            return answers;
        }

        private int GetSatisfiedValue()
        {
            int valueSatisfied = 0;
            IEnumerable<Option> options = Test.GetOptionsByTest();
            if (options != null)
            {
                valueSatisfied = options.Count()%2 != 0 ?
                    (int)Stats.Percentile(options.Select(o => double.Parse(o.Value.ToString())).ToArray(), 60):
                    (int)Stats.Percentile(options.Select(o => double.Parse(o.Value.ToString())).ToArray(), 50);
            }
            return valueSatisfied;
        }


        private IQueryable<Evaluation> GetEvaluationsByUbication(int? country_id, int? state_id, int? region_id)
        {
            IQueryable<Evaluation> evaluations = Test.Evaluations.AsQueryable();
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

        public Dictionary<string,double> GetPositiveAnswersPercentageByPositionLevel(int? level, int? notLevel,
                                            int? country_id, int? state_id, int? region_id)
        {
            IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
            if (Evaluations.Count() > 0)
            {
                Dictionary<string, double> results = new Dictionary<string, double>();
                IQueryable<Evaluation> evalGroup = Evaluations;
                string label = "";
                if (level.HasValue)
                {
                    evalGroup = evalGroup.Where(l => l.PositionLevel.Level == level.Value);
                    //label = new EvaluationsServices().GetPositionLevelNameByLevel(level.Value);
                    label = new PositionLevelsServices().GetPositionLevelNameByLevel(level.Value, Test.Company_Id);
                }
                else if (notLevel.HasValue)
                {
                    evalGroup = evalGroup.Where(l => l.PositionLevel.Level != notLevel);
                    //label = "No" + "-" + new EvaluationsServices().GetPositionLevelNameByLevel(notLevel.Value);
                    label = "No" + "-" + new PositionLevelsServices().GetPositionLevelNameByLevel(notLevel.Value, Test.Company_Id);
                }
                if (evalGroup.Count()>0)
                {
                    if (level.HasValue || notLevel.HasValue)
                    {
                        List<double> data = new List<double>();
                        GetAnswersCountByAnswerType(evalGroup, null, null, data, "Positivas");
                        results.Add(label, data != null ? data.Average() : 0);
                    }
                    else
                    {
                        var groups = evalGroup.Select(g => g.PositionLevel).OrderBy(g => g.Name).GroupBy(g => g.Id);
                        foreach (var group in groups)
                        {
                            List<double> data = new List<double>();
                            var evaluations = Test.Evaluations.Where(s => s.PositionLevel_Id == group.First().Id);
                            GetAnswersCountByAnswerType(evaluations.AsQueryable(), null, null, data, "Positivas");
                            results.Add(label, data != null ? data.Average() : 0);
                        }
                    }
                }
                return results;
            }
            return null;
        }
        
        public Dictionary<string[], double> GetPositiveAnswersPercentageByState(int country_id)
        {
            if (Test.Evaluations.Count > 0)
            {
                Dictionary<string[], double> results = new Dictionary<string[], double>();
                IQueryable<Evaluation> evalGroup = Test.Evaluations.Where(s=> s.Location.State.Country_Id == country_id).AsQueryable();
                var groups = evalGroup.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
                foreach (var group in groups)
                {
                    List<double> data = new List<double>();
                    var evaluations = Test.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
                    GetAnswersCountByAnswerType(evaluations.AsQueryable(), null, null, data, "Positivas");
                    results.Add(new string[] { group.First().Code, group.First().Name, "-", group.First().Id.ToString() }, data.Average());
                }
                return results;
            }
            return null;
        }
        public Dictionary<string[], double> GetPositiveAnswersPercentageByState(int country_id, List<int> states)
        {
            if (Test.Evaluations.Count > 0)
            {
                Dictionary<string[], double> results = new Dictionary<string[], double>();
                IQueryable<Evaluation> evalGroup = Test.Evaluations.Where(s => s.Location.State.Country_Id == country_id).AsQueryable();
                var groups = evalGroup.Select(g => g.Location.State).OrderBy(g => g.Name).GroupBy(g => g.Id);
                foreach (var group in groups)
                {
                    if(states.Contains(group.First().Id))
                    {
                        List<double> data = new List<double>();
                        var evaluations = Test.Evaluations.Where(s => s.Location.State_Id == group.First().Id);
                        GetAnswersCountByAnswerType(evaluations.AsQueryable(), null, null, data, "Positivas");
                        results.Add(new string[] { group.First().Code, group.First().Name, "-", group.First().Id.ToString() }, data.Average());
                    }
                }
                return results;
            }
            return null;
        }
        public Dictionary<string[], double> GetPositiveAnswersPercentageByCountry()
        {
            if (Test.Evaluations.Count > 0)
            {
                Dictionary<string[], double> results = new Dictionary<string[], double>();
                IQueryable<Evaluation> evalGroup = Test.Evaluations.AsQueryable();
                var groups = evalGroup.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
                foreach (var group in groups)
                {
                    List<double> data = new List<double>();
                    var evaluations = Test.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
                    GetAnswersCountByAnswerType(evaluations.AsQueryable(), null, null, data, "Positivas");
                    results.Add(new string[] { group.First().Code, group.First().Name, group.First().Map != null ? group.First().Map : "-", group.First().Id.ToString() }, data.Average());
                }
                return results;
            }
            return null;
        }
        public Dictionary<string[], double> GetPositiveAnswersPercentageByCountry(List<int> countries)
        {
            if (Test.Evaluations.Count > 0)
            {
                Dictionary<string[], double> results = new Dictionary<string[], double>();
                IQueryable<Evaluation> evalGroup = Test.Evaluations.AsQueryable();
                var groups = evalGroup.Select(g => g.Location.State.Country).OrderBy(g => g.Name).GroupBy(g => g.Id);
                foreach (var group in groups)
                {
                    if (countries.Contains(group.First().Id))
                    {
                        List<double> data = new List<double>();
                        var evaluations = Test.Evaluations.Where(s => s.Location.State.Country_Id == group.First().Id);
                        GetAnswersCountByAnswerType(evaluations.AsQueryable(), null, null, data, "Positivas");
                        results.Add(new string[] { group.First().Code, group.First().Name, group.First().Map != null ? group.First().Map : "-", group.First().Id.ToString() }, data.Average());
                    }
                }
                return results;
            }
            return null;
        }

        public Dictionary<string, double> GetPositiveAnswersPercentageByFOType(int type,
                                            int? country_id, int? state_id, int? region_id)
        {
            IQueryable<Evaluation> Evaluations = GetEvaluationsByUbication(country_id, state_id, region_id);
            if (Evaluations.Count() > 0)
            {
                Dictionary<string, double> results = new Dictionary<string, double>();
                var groups = new EvaluationsFOServices().GetByFunctionalOrganizationTypeAndUbication(Evaluations.Select(i => i.Id).ToList(), type).Select(fo => fo.FunctionalOrganization).OrderBy(fo => fo.Name).GroupBy(fo => fo.Id);
                if (groups.Count() > 0)
                {
                    List<double> data = new List<double>();
                    foreach (var group in groups)
                    {
                        IQueryable<EvaluationFO> evaluationsFO = Evaluations.SelectMany(efo => efo.EvaluationFOs).Where(fo => fo.FunctionalOrganization_Id == group.First().Id).AsQueryable();
                        //int totalAnswers = GetAnswersCountByTypeByEvaluationFO(data, "Positivas", evaluationsFO);
                        IQueryable<Evaluation> evaluations = evaluationsFO.Select(e => e.Evaluation).AsQueryable();
                        GetAnswersCountByAnswerType(evaluations, null, null,data, "Positivas");
                        results.Add(group.First().Name, data != null ? data.Average() : 0);
                        //results.Add(group.First().Name, data.Sum() * 100 / totalAnswers/**/);
                    }
                }
                return results;
            }
            return null;
        }

        //private int GetAnswersCountByTypeByEvaluationFO(List<double> data, string answerType, IEnumerable<EvaluationFO> evaluationsFO)
        //{
        //    int answersCount = 0;
        //    foreach (EvaluationFO efo in evaluationsFO)
        //    {
        //        IQueryable<SelectionAnswer> answers = new EvaluationsServices().GetById(efo.Evaluation_Id).SelectionAnswers.AsQueryable();
        //        answersCount += answers.Count();
        //        int questionsCount = answers.Count();
        //        if (questionsCount > 0)
        //        {
        //            answers = GetAnswersByCondition(answerType, answers);
        //            data.Add(answers.Count()/* * 100 / questionsCount*/);
        //        }
        //    }
        //    return answersCount;
        //}

        //INCLUIRQUESTIONNAIREID
        public Dictionary<string, double> GetResult1Data(int? questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Company");
            parameters.Add("dataType", "Category");
            parameters.Add("test", Test.Id);
            parameters.Add("satisfiedValue", satisfiedValue);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            parameters.Add("all", 0);
            parameters.Add("options", Test.GetOptionsByTest().Count());
            //ToDictionary(k => k.Key, k => k.Value.ToString())
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Result", parameters).ExecuteCommand();
            //Dictionary<string, double> results = dicObj.ToDictionary(k => k.Key, k => Double.Parse(k.Value.ToString()));
            //results.Add("Total", results.Values.Average());
            return results;
        }

        public Dictionary<string, double[]> GetResult2Data(int? questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Company");
            parameters.Add("dataType", "Category");
            parameters.Add("test", Test.Id);
            parameters.Add("satisfiedValue", satisfiedValue);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            parameters.Add("all", 1);
            parameters.Add("options", Test.GetOptionsByTest().Count());
            Dictionary<string, double[]> results = (Dictionary<string, double[]>)new Commands("Result", parameters).ExecuteCommand();
            return results;
        }

        public Dictionary<string, object> GetResult3Data(int? questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Company");
            parameters.Add("dataType", "Question");
            parameters.Add("test", Test.Id);
            parameters.Add("satisfiedValue", satisfiedValue);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            parameters.Add("all", 0);
            parameters.Add("options", Test.GetOptionsByTest().Count());
            Dictionary<string, object> results = (Dictionary<string, object>)new Commands("Result", parameters).ExecuteCommand();
            return results;
        }

        public Dictionary<string, object> GetResult4Data(int? questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Company");
            parameters.Add("dataType", "Question");
            parameters.Add("test", Test.Id);
            parameters.Add("satisfiedValue", satisfiedValue);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            parameters.Add("all", 1);
            parameters.Add("options", Test.GetOptionsByTest().Count());
            Dictionary<string, object> results = (Dictionary<string, object>)new Commands("Result", parameters).ExecuteCommand();
            return results;
        }

        public Dictionary<string, double> GetResult5Data(int? questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Comparative");
            parameters.Add("dataType", "Category");
            parameters.Add("test", Test.Id);
            parameters.Add("satisfiedValue", satisfiedValue);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            parameters.Add("all", 0);
            parameters.Add("options", Test.GetOptionsByTest().Count());
            Dictionary<string, double> results = (Dictionary<string, double>)new Commands("Result", parameters).ExecuteCommand();
            return results;
        }

        public Dictionary<string, object> GetResult6Data(int? questionnaire_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "Comparative");
            parameters.Add("dataType", "Question");
            parameters.Add("test", Test.Id);
            parameters.Add("satisfiedValue", satisfiedValue);
            if (questionnaire_id.HasValue)
                parameters.Add("questionnaire", questionnaire_id.Value);
            parameters.Add("all", 0);
            parameters.Add("options", Test.GetOptionsByTest().Count());
            Dictionary<string, object> results = (Dictionary<string, object>)new Commands("Result", parameters).ExecuteCommand();
            return results;
        }

    }
}
