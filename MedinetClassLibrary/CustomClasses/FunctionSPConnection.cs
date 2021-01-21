using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MedinetClassLibrary.CustomClasses
{
    public class FunctionSPConnection
    {
        private string connectionString;
        private string command;

        public FunctionSPConnection(string command)
        {//ejecuta el comando y recibe lo que sea que este recibiendo
            this.connectionString = ConfigurationManager.ConnectionStrings["MEDINET_DBConnectionString"].ConnectionString;
            this.command = command;
        }

        public object ExecuteFunction(string report, bool all)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            object data;
            switch (report)
            {
                case "Min":
                    data = GetEvaluationsCount(reader);
                    break;
                case "PositiveAnswers":
                    data = GetPositiveAnswers(reader);
                    break;
                case "EvaluationsCount":
                    if (all)
                        data = GetAllEvaluationsCount(reader);
                    else
                        data = GetEvaluationsCount(reader);
                    break;
                case "QuestionsCount":
                    data = GetQuestionsCount(reader);
                    break;
                case "Questions":
                    data = GetQuestions(reader);
                    break;
                case "Categories":
                    data = GetCategories(reader);
                    break;
                case "QuestionnairesSelectorValues":
                    data = GetQuestionnairesSelectorValues(reader);
                    break;
                default:
                    data = new List<object[]>();
                    break;
            }
            reader.Close();
            conn.Close();
            return data;
        }

        public object ExecuteFunction(string report, bool AvgAndMed, int countValues, bool CatAndQue, int answersType, bool execDic)//crea la conexion a la BD, abre la conexion
        {
            //SqlConnection conn = new SqlConnection("Data Source=RAMAR07;Initial Catalog=HrMedinetWAW;Persist Security Info=True;User ID=app_hrmedinet; Password=hrmedinet_app");
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(command, conn);
            cmd.CommandTimeout = 300;//aumenta el tiempo de espera por la consulta
            SqlDataReader reader = cmd.ExecuteReader();//cmd.ExecuteReader:ejecuta el comando y guarda lo que obtuvimos como resultado en reader
            object data;
            switch(report)
            {
                case "Population":
                    data = GetPopulationResult(reader);
                    break;
                case "Univariate":
                    data = GetUnivariateResult(AvgAndMed, countValues, reader);
                    break;
                case "Bivariate":
                    data = GetBivariateResult(reader);
                    break;
                case "Result":
                    try
                    {
                        if (CatAndQue)
                        {
                            if (answersType == 1)
                                data = GetPosCategoryAndQuestionPercentageResult(answersType, reader);
                            else if (answersType == 2)
                                data = GetPosNegCategoryAndQuestionPercentageResult(answersType, reader);
                            else
                                data = GetPosNeuNegCategoryAndQuestionPercentageResult(answersType, reader);
                        }
                        else
                        {
                            if (answersType == 1)
                                data = GetPosCategoryPercentageResult(answersType, reader);
                            else if (answersType == 2)
                                data = GetPosNegCategoryPercentageResult(answersType, reader);
                            else
                                data = GetPosNeuNegCategoryPercentageResult(answersType, reader);
                        }
                    }
                    catch
                    {
                        data = null;
                    }
                    break;
                case "Executive":
                    if (execDic)
                        data = GetDictionaryExecutiveResult(reader);
                    else
                        data = GetDecimalExecutiveResult(reader);
                    break;
                case "Ranking":
                    data = GetRankingResult(reader);
                    break;
                case "TextAnswer":
                    data = GetTextAnswerResult(reader);
                    break;
                case "Frequency":
                    data = GetFrequencyResult(reader);
                    break;
                case "Category":
                    data = GetCategoryResult(reader);
                    break;
                case "Comparative":
                    data = GetComparativeResult(reader);
                    break;
                case "Satisfaction":
                    data = GetSatisfactionResult(reader, countValues);
                    break;
                default:
                    data = null;
                    break;
            }
            reader.Close();
            conn.Close();
            return data;
        }

        private Dictionary<string, double> GetPositiveAnswers(SqlDataReader reader)
        {
            Dictionary<string, double> positiveDic = new Dictionary<string, double>();
            while (reader.Read())
            {
                positiveDic.Add(reader[0].ToString(), Double.Parse(reader[1].ToString()));
            }
            return positiveDic;
        }

        private object GetSatisfactionResult(SqlDataReader reader, int type) //0: category, 1:location, 2:fot 
        {
            switch(type)
            {
                case 0:
                    return GetCategorySatisfactionResult(reader);
                case 1:
                    return GetLocationSatisfactionResult(reader);
                case 2:
                    return GetFunctionalOrganizationTypeSatisfactionResult(reader);
                default:
                    return null;
            }
        }

        private object GetCategorySatisfactionResult(SqlDataReader reader)
        {
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            Dictionary<string, double> auxResult = new Dictionary<string, double>();
            string demographic = "";
            while (reader.Read())
            {
                if (reader[0].ToString() != demographic && demographic != "")
                {
                    resultDic.Add(demographic, auxResult);
                    auxResult = new Dictionary<string, double>();
                    demographic = reader[0].ToString();
                }
                else if (demographic == "")
                    demographic = reader[0].ToString();
                auxResult.Add(reader[1].ToString(), Double.Parse(reader[2].ToString()));
            }
            resultDic.Add(demographic, auxResult);
            return resultDic;
        }

        private object GetLocationSatisfactionResult(SqlDataReader reader)
        {
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            Dictionary<string, double[]> auxResult = new Dictionary<string, double[]>();
            string demographic = "";
            while (reader.Read())
            {
                if (reader[0].ToString() != demographic && demographic != "")
                {
                    resultDic.Add(demographic, auxResult);
                    auxResult = new Dictionary<string, double[]>();
                    demographic = reader[0].ToString();
                }
                else if (demographic == "")
                    demographic = reader[0].ToString();
                auxResult.Add(reader[1].ToString(), new double[] { Double.Parse(reader[2].ToString()), Double.Parse(reader[3].ToString()) });
            }
            resultDic.Add(demographic, auxResult);
            return resultDic;
        }

        private object GetFunctionalOrganizationTypeSatisfactionResult(SqlDataReader reader)
        {
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            Dictionary<string, object> resultDicAux = new Dictionary<string, object>();
            Dictionary<string, double[]> auxResult = new Dictionary<string, double[]>();
            Dictionary<string, double[]> auxTotalResult = new Dictionary<string, double[]>();
            Dictionary<string, int> auxCount = new Dictionary<string,int>();
            string demographic = "";
            string foDemographic = "";
            //int count = 0, sumPob = 0;
            //double sumAvg = 0;
            while (reader.Read())
            {
                if (reader[0].ToString() != foDemographic && foDemographic != "")
                {
                    resultDic.Add(demographic, auxResult);
                    auxResult = new Dictionary<string, double[]>();
                    demographic = reader[1].ToString();
                    GetTotalsFOT(resultDic, auxTotalResult, auxCount);
                    resultDicAux.Add("-" + foDemographic, auxTotalResult);
                    resultDicAux = resultDicAux.Concat(resultDic).ToDictionary(x => x.Key, x => x.Value);;
                    foDemographic = reader[0].ToString();
                    resultDic = new Dictionary<string,object>();
                    auxCount = new Dictionary<string, int>();
                    auxTotalResult = new Dictionary<string, double[]>();
                }
                else if (foDemographic == "")
                    foDemographic = reader[0].ToString();

                if (reader[1].ToString() != demographic && demographic != "")
                {
                    resultDic.Add(demographic, auxResult);
                    auxResult = new Dictionary<string, double[]>();
                    demographic = reader[1].ToString();
                }
                else if (demographic == "")
                    demographic = reader[1].ToString();
                auxResult.Add(reader[2].ToString(), new double[] { Double.Parse(reader[3].ToString()), Double.Parse(reader[4].ToString())});
            }
            resultDic.Add(demographic, auxResult);
            GetTotalsFOT(resultDic, auxTotalResult, auxCount);
            resultDicAux.Add("-" + foDemographic, auxTotalResult);
            resultDicAux = resultDicAux.Concat(resultDic).ToDictionary(x => x.Key, x => x.Value); ;
            return resultDicAux;
        }

        private void GetTotalsFOT(Dictionary<string, object> resultDic, Dictionary<string, double[]> auxTotalResult,
                                    Dictionary<string, int> auxCount)
        {
            int sumPob = 0;
            double sumAvg = 0;
            double[] vec;
            foreach (KeyValuePair<string, object> pair in resultDic)
            {
                foreach (KeyValuePair<string, double[]> year in (Dictionary<string, double[]>)pair.Value)
                {
                    if (!auxTotalResult.Keys.Contains(year.Key))
                    {
                        auxTotalResult.Add(year.Key, year.Value);
                        auxCount.Add(year.Key, 1);
                    }
                    else
                    {
                        vec = auxTotalResult[year.Key];
                        sumPob = (int)vec[0];
                        sumAvg = vec[1];
                        auxTotalResult[year.Key] = new double[] { vec[0] + year.Value[0], vec[1] + year.Value[1] };
                        auxCount[year.Key]++;
                    }
                }
            }
            foreach (KeyValuePair<string, int> yearCount in auxCount)
            {
                vec = auxTotalResult[yearCount.Key];
                sumAvg = vec[1] / yearCount.Value;
                auxTotalResult[yearCount.Key] = new double[] { vec[0], sumAvg };
            }
        }

        private object GetComparativeResult(SqlDataReader reader)
        {
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            Dictionary<string, double> auxResult = new Dictionary<string, double>();
            Dictionary<string, int[]> auxResultVec = new Dictionary<string, int[]>();
            string demographic = "";
            while (reader.Read())
            {
                if (reader[0].ToString() != demographic && demographic != "" && demographic != "General")
                {
                    resultDic.Add(demographic, auxResult);
                    auxResult = new Dictionary<string, double>();
                    auxResultVec = new Dictionary<string, int[]>();
                    demographic = reader[0].ToString();
                }
                else if (demographic == "")
                    demographic = reader[0].ToString();
                if(demographic == "General")
                    auxResultVec.Add(reader[1].ToString(), new int[] { Int32.Parse(reader[2].ToString()), Int32.Parse(reader[3].ToString())});
                else
                    auxResult.Add(reader[1].ToString(), Double.Parse(reader[2].ToString()));
            }
            if (demographic != "General")
            {
                resultDic.Add(demographic, auxResult);
                return resultDic;
            }
            else
                return auxResultVec;
        }

        private int GetEvaluationsCount(SqlDataReader reader)
        {
            int data = 0;
            while (reader.Read())
            {
                data = Int32.Parse(reader[0].ToString());
            }
            return data;
        }

        private Dictionary<string, int> GetAllEvaluationsCount(SqlDataReader reader)
        {
            Dictionary<string, int> data = new Dictionary<string,int>();
            while (reader.Read())
            {
                data.Add(reader[0].ToString(), Int32.Parse(reader[1].ToString()));
            }
            return data;
        }

        private List<object[]> GetQuestionsCount(SqlDataReader reader)
        {
            List<object[]> data = new List<object[]>();
            while (reader.Read())
            {
                data.Add(new object[] {Int32.Parse(reader[0].ToString())});
            }
            return data;
        }

        private List<object[]> GetQuestions(SqlDataReader reader)
        {
            List<object[]> data = new List<object[]>();
            while (reader.Read())
            {
                data.Add(new object[] {
                    Int32.Parse(reader[0].ToString()),
                    reader[1].ToString(),
                    Int32.Parse(reader[2].ToString()),
                    Int32.Parse(reader[3].ToString()),
                    reader[4].ToString(),
                    Boolean.Parse(reader[5].ToString()) ? 1 : 0,
                    Boolean.Parse(reader[6].ToString()) ? 1 : 0
                });
            }
            return data;
        }

        private List<object[]> GetCategories(SqlDataReader reader)
        {
            List<object[]> data = new List<object[]>();
            while (reader.Read())
            {
                data.Add(new object []
                {
                    Int32.Parse(reader[0].ToString()),
                    reader[1].ToString(),
                    reader[2].ToString()
                });
            }
            return data;
        }

        private List<object[]> GetQuestionnairesSelectorValues(SqlDataReader reader)
        {
            List<object[]> data = new List<object[]>();
            while (reader.Read())
            {
                data.Add(new object []
                {
                    Int32.Parse(reader[0].ToString()),
                    Int32.Parse(reader[1].ToString())
                });
            }
            return data;
        }

        private object GetCategoryResult(SqlDataReader reader)
        {
            Dictionary<string, object> categoryDic = new Dictionary<string, object>();
            Dictionary<string, double> aux = new Dictionary<string, double>();
            while (reader.Read())
            {
                aux.Add(reader[0].ToString(), Double.Parse(reader[1].ToString()));
            }
            categoryDic.Add("", aux);//.OrderByDescending(v => v.Value));
            return categoryDic;
        }

        private object GetFrequencyResult(SqlDataReader reader)
        {
            Dictionary<string, object> frequencyDic = new Dictionary<string, object>();
            Dictionary<string, double> aux = new Dictionary<string, double>();
            string name1 = "";
            string name2 = "";
            while (reader.Read())
            {
                if (reader[0].ToString() != name1 && name1 != "")
                {
                    if (name1 == "True")
                        name1 = ViewRes.Views.Evaluation.AnswerTest.Yes;
                    else if (name1 == "False")
                        name1 = ViewRes.Views.Evaluation.AnswerTest.No;
                    else if (name1 == "Female")
                        name1 = ViewRes.Classes.ChiSquare.FemaleGender;
                    else if (name1 == "Male")
                        name1 = ViewRes.Classes.ChiSquare.MaleGender;
                    frequencyDic.Add(name1, aux);
                    aux = new Dictionary<string, double>();
                    name1 = reader[0].ToString();
                }
                else if (name1 == "")
                    name1 = reader[0].ToString();
                if (reader[1].ToString() == "True")
                    name2 = ViewRes.Views.Evaluation.AnswerTest.Yes;
                else if (reader[1].ToString() == "False")
                    name2 = ViewRes.Views.Evaluation.AnswerTest.No;
                else if (reader[1].ToString() == "Female")
                    name2 = ViewRes.Classes.ChiSquare.FemaleGender;
                else if (reader[1].ToString() == "Male")
                    name2 = ViewRes.Classes.ChiSquare.MaleGender;
                else
                    name2 = reader[1].ToString();
                aux.Add(name2, Int32.Parse(reader[2].ToString()));
            }
            if (name1 == "True")
                name1 = ViewRes.Views.Evaluation.AnswerTest.Yes;
            else if (name1 == "False")
                name1 = ViewRes.Views.Evaluation.AnswerTest.No;
            else if (name1 == "Female")
                name1 = ViewRes.Classes.ChiSquare.FemaleGender;
            else if (name1 == "Male")
                name1 = ViewRes.Classes.ChiSquare.MaleGender;
            frequencyDic.Add(name1, aux);
            return frequencyDic;
        }

        private object GetTextAnswerResult(SqlDataReader reader)
        {
            Dictionary<string, List<string>> textAnswerDic = new Dictionary<string, List<string>>();
            List<string> text = new List<string>();
            string nameText = "";
            string auxText;
            while (reader.Read())
            {
                if (reader[0].ToString() != nameText && nameText != "")
                {
                    auxText = (nameText == "Female" ? ViewRes.Classes.ChiSquare.FemaleGender : (nameText == "Male" ? ViewRes.Classes.ChiSquare.MaleGender : nameText));
                    textAnswerDic.Add(auxText, text);
                    text = new List<string>();
                    nameText = reader[0].ToString();
                }
                else if (nameText == "")
                    nameText = reader[0].ToString();
                text.Add(reader[1].ToString());
            }
            auxText = (nameText == "Female" ? ViewRes.Classes.ChiSquare.FemaleGender : (nameText == "Male" ? ViewRes.Classes.ChiSquare.MaleGender : nameText));
            textAnswerDic.Add(auxText, text);
            return textAnswerDic;
        }

        private static Dictionary<string, decimal> GetRankingResult(SqlDataReader reader)
        {
            Dictionary<string, decimal> rankingDic = new Dictionary<string, decimal>();
            while (reader.Read())
            {
                rankingDic.Add(reader[0].ToString(), Int32.Parse(reader[1].ToString()));
            }
            return rankingDic;
        }

        private object GetDecimalExecutiveResult(SqlDataReader reader)
        {
            decimal executiveDec = 0;
            while (reader.Read())
            {
                executiveDec = Decimal.Parse(reader[0].ToString());
            }
            return executiveDec;
        }

        private object GetDictionaryExecutiveResult(SqlDataReader reader)
        {
            Dictionary<string, decimal> executiveDic = new Dictionary<string, decimal>();
            while (reader.Read())
            {
                executiveDic.Add(reader[0].ToString(), Int32.Parse(reader[1].ToString()));
            }
            return executiveDic;
        }

        private object GetPosCategoryAndQuestionPercentageResult(int answersType, SqlDataReader reader)
        {
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            Dictionary<string, double> auxResult = new Dictionary<string, double>();
            string category = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (category == "")
                        category = reader[0].ToString();
                    if (reader[0].ToString() != category && category != "" && !resultDic.ContainsKey(reader[0].ToString()))
                    {
                        auxResult.Add("Total", auxResult.Values.Average());
                        resultDic.Add(category, auxResult);
                        auxResult = new Dictionary<string, double>();
                        category = reader[0].ToString();
                    }

                    auxResult.Add(reader[1].ToString(), Double.Parse(reader[2].ToString()));
                }
                resultDic.Add(category, auxResult);
            }
            return resultDic;
        }

        private object GetPosNegCategoryAndQuestionPercentageResult(int answersType, SqlDataReader reader)
        {
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            Dictionary<string, double[]> auxResult = new Dictionary<string, double[]>();
            double total_pos = 0, total_neg = 0;
            int count = 0;
            string category = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader[0].ToString() != category && category != "")
                    {
                        auxResult.Add("Total", new double[] { total_pos / count, total_neg / count });
                        resultDic.Add(category, auxResult);
                        auxResult = new Dictionary<string, double[]>();
                        category = reader[0].ToString();
                        total_pos = 0; total_neg = 0;
                        count = 0;
                    }
                    else if (category == "")
                        category = reader[0].ToString();
                    auxResult.Add(reader[1].ToString(), new double[] { Double.Parse(reader[2].ToString()), Double.Parse(reader[4].ToString()) });
                    total_pos += Double.Parse(reader[2].ToString());
                    total_neg += Double.Parse(reader[4].ToString());
                    count++;
                }
                resultDic.Add(category, auxResult);
            }
            return resultDic;
        }

        private object GetPosNeuNegCategoryAndQuestionPercentageResult(int answersType, SqlDataReader reader)
        {
            Dictionary<string, object> resultDic = new Dictionary<string, object>();
            Dictionary<string, double[]> auxResult = new Dictionary<string, double[]>();
            double total_pos=0, total_neu=0, total_neg=0;
            int count=0;
            string category = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader[0].ToString() != category && category != "")
                    {
                        auxResult.Add("Total", new double[] { total_pos / count, total_neu / count, total_neg / count });
                        resultDic.Add(category, auxResult);
                        auxResult = new Dictionary<string, double[]>();
                        category = reader[0].ToString();
                        total_pos = 0; total_neu = 0; total_neg = 0;
                        count = 0;
                    }
                    else if (category == "")
                        category = reader[0].ToString();
                    auxResult.Add(reader[1].ToString(), new double[] { Double.Parse(reader[2].ToString()), Double.Parse(reader[3].ToString()), Double.Parse(reader[4].ToString()) });
                    total_pos += Double.Parse(reader[2].ToString());
                    total_neu += Double.Parse(reader[3].ToString());
                    total_neg += Double.Parse(reader[4].ToString());
                    count++;
                }
                resultDic.Add(category, auxResult);
            }
            return resultDic;
        }

        private object GetPosCategoryPercentageResult(int answersType, SqlDataReader reader)
        {
            Dictionary<string, double> resultDic = new Dictionary<string, double>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    resultDic.Add(reader[0].ToString(), Double.Parse(reader[1].ToString()));
                }
                resultDic.Add("Total", resultDic.Values.Average());
            }
            return resultDic;
        }

        private object GetPosNeuNegCategoryPercentageResult(int answersType, SqlDataReader reader)
        {
            Dictionary<string, double[]> resultDic = new Dictionary<string, double[]>();
            double total_pos = 0, total_neu = 0, total_neg = 0;
            int count = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    resultDic.Add(reader[0].ToString(), new double[] { Double.Parse(reader[1].ToString()), Double.Parse(reader[2].ToString()), Double.Parse(reader[3].ToString()) });
                    total_pos += Double.Parse(reader[1].ToString());
                    total_neu += Double.Parse(reader[2].ToString());
                    total_neg += Double.Parse(reader[3].ToString());
                    count++;
                }
                resultDic.Add("Total", new double[] { total_pos / count, total_neu / count, total_neg / count });
            }
            return resultDic;
        }

        private object GetPosNegCategoryPercentageResult(int answersType, SqlDataReader reader)
        {
            Dictionary<string, double[]> resultDic = new Dictionary<string, double[]>();
            double total_pos = 0, total_neg = 0;
            int count = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    resultDic.Add(reader[0].ToString(), new double[] { Double.Parse(reader[1].ToString()), Double.Parse(reader[3].ToString()) });
                    total_pos += Double.Parse(reader[1].ToString());
                    total_neg += Double.Parse(reader[3].ToString());
                    count++;
                }
                resultDic.Add("Total", new double[] { total_pos / count, total_neg / count });
            }
            return resultDic;
        }

        private object GetBivariateResult(SqlDataReader reader)
        {
            Dictionary<string, object> bivariateDic = new Dictionary<string, object>();
            Dictionary<string, double> aux = new Dictionary<string, double>();
            string name1 = "";
            string name2 = "";
            while (reader.Read())
            {
                if (reader[0].ToString() != name1 && name1 != "")
                {
                    if (name1 == "Female")
                        name1 = ViewRes.Classes.ChiSquare.FemaleGender;
                    else if (name1 == "Male")
                        name1 = ViewRes.Classes.ChiSquare.MaleGender;
                    bivariateDic.Add(name1, aux);
                    aux = new Dictionary<string, double>();
                    name1 = reader[0].ToString();
                }
                else if (name1 == "")
                    name1 = reader[0].ToString();
                if (reader[1].ToString() == "Female")
                    name2 = ViewRes.Classes.ChiSquare.FemaleGender;
                else if (reader[1].ToString() == "Male")
                    name2 = ViewRes.Classes.ChiSquare.MaleGender;
                else
                    name2 = reader[1].ToString();
                aux.Add(name2, Double.Parse(reader[2].ToString()));
            }
            bivariateDic.Add(name1, aux);
            return bivariateDic;
        }

        private object GetUnivariateResult(bool AvgAndMed, int countValues, SqlDataReader reader)
        {
            object data;
            Dictionary<string, object> univariateDic = new Dictionary<string, object>();//si countvalues=3 lleno este diccionario
            Dictionary<string, double> averageDic = new Dictionary<string, double>();//si countvalues=1 lleno este diccionario
            Dictionary<string, double> medianDic = new Dictionary<string, double>();//si countvalues=2 lleno este diccionario
            string name;
            while (reader.Read())
            {
                if (reader[0].ToString() == "Female")
                    name = ViewRes.Classes.ChiSquare.FemaleGender;
                else if (reader[0].ToString() == "Male")
                    name = ViewRes.Classes.ChiSquare.MaleGender;
                else
                    name = reader[0].ToString();
                averageDic.Add(name, Double.Parse(reader[1].ToString()));
                if (countValues > 1)
                    medianDic.Add(name, Double.Parse(reader[2].ToString()));
            }
            switch(countValues)
            {
                case 3:
                    univariateDic.Add(AvgAndMed ? "Average" : "Satisfied", averageDic);
                    univariateDic.Add(AvgAndMed ? "Median" : "NoSatisfied", medianDic);
                    data = univariateDic;
                    break;
                case 2:
                    //univariateDic.Add(AvgAndMed ? "Median" : "NoSatisfied", medianDic);
                    data = medianDic;
                    break;
                case 1:
                    //univariateDic.Add(AvgAndMed ? "Average" : "Satisfied", averageDic);
                    data = averageDic;
                    break;
                default:
                    data = null;
                    break;
            }
            return data;
        }

        private object GetPopulationResult(SqlDataReader reader)
        {
            Dictionary<string, double> populationDic = new Dictionary<string, double>();
            string viewres;
            while (reader.Read())//se posiciona en cada registro del vector ejemplo:Recibido,195. el primer numerito 1,2 solito lo recorre el while
            {
                /*if (reader[0].ToString() == "Female")//reader[0]?Recibido
                    viewres = ViewRes.Classes.ChiSquare.FemaleGender;
                else if (reader[0].ToString() == "Male")
                    viewres = ViewRes.Classes.ChiSquare.MaleGender;
                else if (reader[0].ToString() == "Received")
                    viewres = ViewRes.Controllers.ChartReports.Received;
                else if (reader[0].ToString() == "NotReceived")
                    viewres = ViewRes.Controllers.ChartReports.NotReceived;
                else
                    viewres = reader[0].ToString();*/
                populationDic.Add(reader[0].ToString(), Int32.Parse(reader[1].ToString()));//reader[1]=195
            }
            return populationDic;
        }
    }
}
