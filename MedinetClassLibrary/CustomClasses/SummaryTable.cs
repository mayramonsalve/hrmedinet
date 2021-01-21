using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;

namespace MedinetClassLibrary.CustomClasses
{
    public class SummaryTable
    {
        public string Label;
        public double Average;
        public double Median;
        public double Satisfied;
        public double NotSatisfied;
        public double AverageCompare;
        public double MedianCompare;
        public double SatisfiedCompare;
        public double NotSatisfiedCompare;
        public string TestName;
        public string TestCompareName;
        public string[] TextAnswers;
        public string[] TextAnswersCompare;

        public SummaryTable()
        {
        }
        
        public SummaryTable(string Label,
                            double Average,
                            double Median,
                            double Satisfied,
                            double NotSatisfied)
        {
            this.Label = Label;
            this.Average = Average;
            this.Median = Median;
            this.Satisfied = Satisfied;
            this.NotSatisfied = NotSatisfied;
        }

        public SummaryTable(string label, List<string> textAnswers, List<string> textAnswersCompare,
                            string testName, string testCompareName)
        {
            this.Label = label;
            this.TextAnswers = textAnswers.ToArray();
            this.TextAnswersCompare = textAnswersCompare.ToArray();
            this.TestName = testName;
            this.TestCompareName = testCompareName;
        }

        public SummaryTable(string Label,
                            double Average,
                            double Median,
                            double Satisfied,
                            double NotSatisfied,
                            double AverageCompare,
                            double MedianCompare,
                            double SatisfiedCompare,
                            double NotSatisfiedCompare,
                            string TestName,
                            string TestCompareName)
        {
            this.Label = Label;
            this.Average = Average;
            this.Median = Median;
            this.Satisfied = Satisfied;
            this.NotSatisfied = NotSatisfied;
            this.TestName = TestName;
            this.AverageCompare = AverageCompare;
            this.MedianCompare = MedianCompare;
            this.SatisfiedCompare = SatisfiedCompare;
            this.NotSatisfiedCompare = NotSatisfiedCompare;
            this.TestCompareName = TestCompareName;
        }

        public List<SummaryTable> UpdateTable(int? questionnaire_id, int? category_id, int? question_id, int test_id, string demographic, int? FO_id, int? compare_id)
        {
            Test test = new TestsServices().GetById(test_id);
            Test testCompare = new Test();
            Dictionary<string, List<string>> textAnswers = new Dictionary<string,List<string>>();
            Dictionary<string, List<string>> textAnswersCompare = new Dictionary<string,List<string>>();
            if (compare_id.HasValue)
                testCompare = new TestsServices().GetById(compare_id.Value);
            switch (demographic)
            {
                case "General":
                    textAnswers = test.GetGeneralTextAnswers(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetGeneralTextAnswers(questionnaire_id, category_id, question_id);
                    break;
                case "Location":
                    textAnswers = test.GetTextAnswersByLocations(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByLocations(questionnaire_id, category_id, question_id);
                    break;
                case "AgeRange":
                    textAnswers = test.GetTextAnswersByAgeRanges(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByAgeRanges(questionnaire_id, category_id, question_id);
                    break;
                case "Country":
                    textAnswers = test.GetTextAnswersByCountries(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByCountries(questionnaire_id, category_id, question_id);
                    break;
                case "Region":
                    textAnswers = test.GetTextAnswersByRegions(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByRegions(questionnaire_id, category_id, question_id);
                    break;
                case "InstructionLevel":
                    textAnswers = test.GetTextAnswersByInstructionLevels(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByInstructionLevels(questionnaire_id, category_id, question_id);
                    break;
                case "PositionLevel":
                    textAnswers = test.GetTextAnswersByPositionLevels(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByPositionLevels(questionnaire_id, category_id, question_id);
                    break;
                case "Seniority":
                    textAnswers = test.GetTextAnswersBySeniorities(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersBySeniorities(questionnaire_id, category_id, question_id);
                    break;
                case "Gender":
                    textAnswers = test.GetTextAnswersByGenders(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByGenders(questionnaire_id, category_id, question_id);
                    break;
                case "Performance":
                    textAnswers = test.GetTextAnswersByPerformanceEvaluations(questionnaire_id, category_id, question_id);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByPerformanceEvaluations(questionnaire_id, category_id, question_id);
                    break;
                case "FunctionalOrganizationType":
                    textAnswers = test.GetTextAnswersByFOs(questionnaire_id, category_id, question_id, FO_id.Value);
                    if (compare_id.HasValue)
                        textAnswersCompare = testCompare.GetTextAnswersByFOs(questionnaire_id, category_id, question_id, FO_id.Value);
                    break;
            }
            return FillSumaryTable(textAnswers, textAnswersCompare, test, testCompare);
        }

        public List<SummaryTable> UpdateTable(int? questionnaire_id, int? category_id, int? question_id, double? pValue, int test_id, string demographic, bool condition, int? FO_id, int? compare_id)
        {
            Test test = new TestsServices().GetById(test_id);
            Test testCompare = new Test();
            Dictionary<string, object> dictionarySCompare = null;
            Dictionary<string, object> dictionaryAMCompare = null;
            Dictionary<string, object> dictionaryS = new ChiSquare(test, demographic, questionnaire_id, category_id, question_id, null, FO_id, 0.05, null, null, null).DataSatisfaction;
            Dictionary<string, object> dictionaryAM = new Dictionary<string, object>();
            if (compare_id.HasValue)
            {
                testCompare = new TestsServices().GetById(compare_id.Value);
                dictionarySCompare = new ChiSquare(testCompare, demographic, questionnaire_id, category_id, question_id, null, FO_id, 0.05, null, null, null).DataSatisfaction;
                dictionaryAMCompare = new Dictionary<string, object>();
            }
            if (condition)//número de demográficos mayor a 7
            {
                switch (demographic)
                {
                    case "General":
                        dictionaryAM = test.GetGeneralAvgAndMed(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if(compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetGeneralAvgAndMed(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "Category":
                        dictionaryAM = test.GetCategoryAvgAndMed(compare_id.HasValue && !condition, questionnaire_id, null, null, null);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetCategoryAvgAndMed(compare_id.HasValue && !condition, questionnaire_id, null, null, null);
                        break;
                    case "Location":
                        dictionaryAM = test.GetAvgAndMedByLocations(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition, null, null, null);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByLocations(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition, null, null, null);
                        break;
                    case "AgeRange":
                        dictionaryAM = test.GetAvgAndMedByAgeRanges(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByAgeRanges(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "Country":
                        dictionaryAM = test.GetAvgAndMedByCountries(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByCountries(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "Region":
                        dictionaryAM = test.GetAvgAndMedByRegions(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByRegions(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "InstructionLevel":
                        dictionaryAM = test.GetAvgAndMedByInstructionLevels(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByInstructionLevels(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "PositionLevel":
                        dictionaryAM = test.GetAvgAndMedByPositionLevels(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByPositionLevels(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "Seniority":
                        dictionaryAM = test.GetAvgAndMedBySeniorities(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedBySeniorities(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "Gender":
                        dictionaryAM = test.GetAvgAndMedByGender(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByGender(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "Performance":
                        dictionaryAM = test.GetAvgAndMedByAgeRanges(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByAgeRanges(questionnaire_id, category_id, question_id, compare_id.HasValue && !condition);
                        break;
                    case "FunctionalOrganizationType":
                        dictionaryAM = test.GetAvgAndMedByFOTypes(questionnaire_id, category_id, question_id, FO_id.Value, compare_id.HasValue && !condition);
                        if (compare_id.HasValue)
                            dictionaryAMCompare = testCompare.GetAvgAndMedByFOTypes(questionnaire_id, category_id, question_id, FO_id.Value, compare_id.HasValue && !condition);
                        break;
                }
            }
            if(compare_id.HasValue)
                return FillSumaryTable(dictionaryAM.ToList(), dictionaryS.ToList(), condition, demographic,
                                        dictionaryAMCompare.ToList(), dictionarySCompare.ToList(),
                                        test.Name, testCompare.Name);
            else
                return FillSumaryTable(dictionaryAM.ToList(), dictionaryS.ToList(), condition);
        }

        public List<SummaryTable> FillSumaryTable(Dictionary<string, List<string>> textAnswers,
                                                    Dictionary<string, List<string>> textAnswersCompare,
                                                    Test test, Test testCompare)
        {
            List<SummaryTable> list = new List<SummaryTable>();
            foreach (KeyValuePair<string, List<string>> answer in textAnswers)
            {
                SummaryTable summary = new SummaryTable(answer.Key, answer.Value,
                                                        textAnswersCompare.Keys.Contains(answer.Key) ?
                                                        textAnswersCompare[answer.Key] : new List<string>(),
                                                        test.Name, testCompare.Name == "" ? " " : testCompare.Name);
                list.Add(summary);
            }
            return list;
        }

        public List<SummaryTable> FillSumaryTable(  List<KeyValuePair<string,object>> averageMedian,
                                                List<KeyValuePair<string, object>> satisfiedNotSatisfied,
                                                bool condition)
        {
            List<SummaryTable> list = new List<SummaryTable>();
            if (condition)//si voy a mostrar media y mediana
            {
                Dictionary<string, double> listAvg = (Dictionary<string, double>)averageMedian[0].Value;//devuelve el diccionario
                Dictionary<string, double> listMed = (Dictionary<string, double>)averageMedian[1].Value;
                Dictionary<string, double> listSat = (Dictionary<string, double>)satisfiedNotSatisfied[0].Value;
                Dictionary<string, double> listNSa = (Dictionary<string, double>)satisfiedNotSatisfied[1].Value;
                int size = listAvg.Count();

                foreach (var valor in listAvg)
                { //listAvg.ContainsKey(valor) ? listAvg[valor] : 0
                    SummaryTable summary = new SummaryTable(valor.Key, valor.Value,
                                                            listMed.Keys.Contains(valor.Key) ? listMed[valor.Key] : 0, //listMed[valor.Key],
                                                            listSat.Keys.Contains(valor.Key) ? listSat[valor.Key] : 0,//listSat[valor.Key],
                                                            listNSa.Keys.Contains(valor.Key) ? listNSa[valor.Key] : 0);//listNSa[valor.Key]);

                    list.Add(summary);
                }
            }
            else
            {
                Dictionary<string, double> listSat = (Dictionary<string, double>)satisfiedNotSatisfied[0].Value;
                Dictionary<string, double> listNSa = (Dictionary<string, double>)satisfiedNotSatisfied[1].Value; 
                int size = listSat.Count();

                foreach (var valor in listSat)//SummaryTable tiene valor (ejemplo menor a 18 años), media,mediana,satisfechos,no satisfechos
                {
                    SummaryTable summary = new SummaryTable(valor.Key, 0,0, 
                                                            listSat.Keys.Contains(valor.Key) ? listSat[valor.Key] : 0,//si la lista de satisfechos contiene el valor actual coloque lo que tenga la lista en la posicion del valor actual
                                                            listNSa.Keys.Contains(valor.Key) ? listNSa[valor.Key] : 0);

                    list.Add(summary);
                }
            }

            return list;
        }

        public List<SummaryTable> FillSumaryTable(List<KeyValuePair<string, object>> averageMedian,
                                                  List<KeyValuePair<string, object>> satisfiedNotSatisfied,
                                                  bool condition, string demographic,
                                                  List<KeyValuePair<string, object>> averageMedianCompare,
                                                  List<KeyValuePair<string, object>> satisfiedNotSatisfiedCompare,
                                                  string testName, string testCompareName)
        {
            List<SummaryTable> list = new List<SummaryTable>();
            if (condition)
            {
                Dictionary<string, double> listAvg = (Dictionary<string, double>)averageMedian[0].Value;
                Dictionary<string, double> listMed = (Dictionary<string, double>)averageMedian[1].Value;
                Dictionary<string, double> listSat = (Dictionary<string, double>)satisfiedNotSatisfied[0].Value;
                Dictionary<string, double> listNSa = (Dictionary<string, double>)satisfiedNotSatisfied[1].Value;
                Dictionary<string, double> listAvgCompare = (Dictionary<string, double>)averageMedianCompare[0].Value;
                Dictionary<string, double> listMedCompare = (Dictionary<string, double>)averageMedianCompare[1].Value;
                Dictionary<string, double> listSatCompare = (Dictionary<string, double>)satisfiedNotSatisfiedCompare[0].Value;
                Dictionary<string, double> listNSaCompare = (Dictionary<string, double>)satisfiedNotSatisfiedCompare[1].Value;
                int size = listAvg.Count();
                List<string> keys = GetKeys(listSat, listSatCompare);
                if (demographic == "General")
                {
                    SummaryTable summary = new SummaryTable("General", listAvg.FirstOrDefault().Value,
                                                                                listMed.FirstOrDefault().Value,
                                                                                listSat.FirstOrDefault().Value,
                                                                                listNSa.FirstOrDefault().Value,
                                                                                listAvgCompare.FirstOrDefault().Value,
                                                                                listMedCompare.FirstOrDefault().Value,
                                                                                listSatCompare.FirstOrDefault().Value,
                                                                                listNSaCompare.FirstOrDefault().Value,
                                                                                testName, testCompareName);

                    list.Add(summary);
                }
                else
                {
                    foreach (var valor in keys)
                    {
                        SummaryTable summary = new SummaryTable(valor, listAvg.ContainsKey(valor) ? listAvg[valor] : 0,
                                                                listMed.ContainsKey(valor) ? listMed[valor] : 0,
                                                                listSat.ContainsKey(valor) ? listSat[valor] : 0,
                                                                listNSa.ContainsKey(valor) ? listNSa[valor] : 0,
                                                                listAvgCompare.ContainsKey(valor) ? listAvgCompare[valor] : 0,
                                                                listMedCompare.ContainsKey(valor) ? listMedCompare[valor] : 0,
                                                                listSatCompare.ContainsKey(valor) ? listSatCompare[valor] : 0,
                                                                listNSaCompare.ContainsKey(valor) ? listNSaCompare[valor] : 0,
                                                                testName, testCompareName);

                        list.Add(summary);
                    }
                }
            }
            else
            {
                Dictionary<string, double> listSat = (Dictionary<string, double>)satisfiedNotSatisfied[0].Value;
                Dictionary<string, double> listNSa = (Dictionary<string, double>)satisfiedNotSatisfied[1].Value;
                Dictionary<string, double> listSatCompare = (Dictionary<string, double>)satisfiedNotSatisfiedCompare[0].Value;
                Dictionary<string, double> listNSaCompare = (Dictionary<string, double>)satisfiedNotSatisfiedCompare[1].Value;
                int size = listSat.Count();
                List<string> keys = GetKeys(listSat, listSatCompare);
                if (demographic == "General")
                {
                    SummaryTable summary = new SummaryTable("General", 0, 0,
                                                            listSat.FirstOrDefault().Value,
                                                            listNSa.FirstOrDefault().Value,
                                                            0, 0,
                                                            listSatCompare.FirstOrDefault().Value,
                                                            listNSaCompare.FirstOrDefault().Value,
                                                            testName, testCompareName);

                    list.Add(summary);
                }
                else
                {
                    foreach (var valor in keys)
                    {
                        SummaryTable summary = new SummaryTable(valor, 0, 0,
                                                                listSat.ContainsKey(valor) ? listSat[valor] : 0,
                                                                listNSa.ContainsKey(valor) ? listNSa[valor] : 0,
                                                                0, 0,
                                                                listSatCompare.ContainsKey(valor) ? listSatCompare[valor] : 0,
                                                                listNSaCompare.ContainsKey(valor) ? listNSaCompare[valor] : 0,
                                                                testName, testCompareName);

                        list.Add(summary);
                    }
                }
            }

            return list;
        }

        private static List<string> GetKeys(Dictionary<string, double> listSat, Dictionary<string, double> listSatCompare)
        {
            List<string> keys = listSat.Keys.ToList();
            if (keys != listSatCompare.Keys.ToList())
                foreach (string key in listSatCompare.Keys)
                {
                    if (!keys.Contains(key))
                        keys.Add(key);
                }
            return keys;
        }

        public Dictionary<int, string> GetItemsByDemographic(int test_id, string demographic, int? FO_id)
        {
            Test test = new TestsServices().GetById(test_id);
            Dictionary<int, string> items = new Dictionary<int, string>();
            object aux;
            switch (demographic)
            {
                case "Location":
                    aux = test.Evaluations.Select(i => i.Location).Distinct().OrderBy(l => l.Name).ToList();
                    foreach (Location obj in (List<Location>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
                case "AgeRange":
                    aux = test.Evaluations.Select(i => i.Age).Distinct().OrderBy(l => l.Level).ToList();
                    foreach (Age obj in (List<Age>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
                case "Country":
                    aux = test.Evaluations.Select(i => i.Location.State.Country).Distinct().OrderBy(l => l.Name).ToList();
                    foreach (Country obj in (List<Country>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
                case "Region":
                    aux = test.Evaluations.Select(i => i.Location.Region).Distinct().OrderBy(l => l.Name).ToList();
                    foreach (Region obj in (List<Region>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
                case "InstructionLevel":
                    aux = test.Evaluations.Select(i => i.InstructionLevel).Distinct().OrderBy(l => l.Level).ToList();
                    foreach (InstructionLevel obj in (List<InstructionLevel>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
                case "PositionLevel":
                    aux = test.Evaluations.Select(i => i.PositionLevel).Distinct().OrderBy(l => l.Level).ToList();
                    foreach (PositionLevel obj in (List<PositionLevel>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
                case "Seniority":
                    aux = test.Evaluations.Select(i => i.Seniority).Distinct().OrderBy(l => l.Level).ToList();
                    foreach (Seniority obj in (List<Seniority>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
                case "Gender":
                    aux = test.Evaluations.Select(i => i.Sex).Distinct().OrderBy(l => l).ToList();
                    foreach (string obj in (List<string>)aux)
                        items.Add(obj.ToLower() == "female" ? 1 : 2, obj);
                    break;
                case "Performance":
                    aux = test.Evaluations.Select(i => i.PerformanceEvaluation).Distinct().OrderBy(l => l.Level).ToList();
                    foreach (PerformanceEvaluation obj in (List<PerformanceEvaluation>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
                case "FunctionalOrganizationType":
                    aux = test.Evaluations.Select(i => i.EvaluationFOs.Select(fo => fo.FunctionalOrganization).Where(t => t.Type_Id == FO_id.Value).OrderBy(l => l.Name)).Distinct().ToList();
                    foreach (FunctionalOrganization obj in (List<FunctionalOrganization>)aux)
                        items.Add(obj.Id, obj.Name);
                    break;
            }
            return items;
        }
    }

}
