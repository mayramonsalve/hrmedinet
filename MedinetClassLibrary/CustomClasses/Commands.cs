using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.CustomClasses;
using ViewRes.Models;

namespace MedinetClassLibrary.Classes
{
    public class Commands
    {
        private string report;
        private string dataType;
        private string demographic;
        private int? fot_id;
        private int? state_id;
        private int? country_id;
        private int? category_id; 

        private int? question_id;
        private int test_id;
        private string command;
        private Dictionary<string, object> parameters;

        public Commands(string report, Dictionary<string, object> parameters)//crea la consulta
        {
            this.report = report;
            this.parameters = parameters;
            this.demographic = parameters["demographic"].ToString();
            GetCommand();
        }

        public void GetCommand()//Crea el comando
        {
            command = "EXECUTE [dbo].[SP";
            switch (report)
            {
                case "Population"://tipo de reporte
                    GetPopulationCommand();
                    break;
                case "Univariate":
                    GetUnivariateCommand();
                    break;
                case "Bivariate":
                    GetBivariateCommand();
                    break;
                case "Result":
                    GetResultCommand();
                    break;
                case "ExecutiveReport":
                    GetExecutiveReportCommand();
                    break;
                case "Ranking":
                    GetRankingCommand();
                    break;
                case "TextAnswer":
                    GetTextAnswerCommand();
                    break;
                case "Category":
                    GetCategoryCommand();
                    break;
                case "Frequency":
                    GetFrequencyCommand();
                    break;
                case "Add":
                    GetAddCommand();
                    break;
                case "Comparative":
                    GetComparativeCommand();
                    break;
                case "Satisfaction":
                    GetSatisfactionCommand();
                    break;
                case "EvaluationsCount":
                    GetEvaluationsCountCommand();
                    break;
                case "Min":
                    GetEvaluationsCountCommand();
                    break;
                case "PositiveAnswers":
                    GetPositiveAnswersCommand();
                    break;
                default:
                    GetEvaluationCommand();
                    break;
            }
        }

        public object ExecuteCommand()//busca cuales son los parametros por los que va a buscar, ejemplo: Media y mediana, Satisfechos y no Satisfechos
        {
            string dataType = parameters.Keys.Contains("dataType") ? parameters["dataType"].ToString() : "";
            bool execDic = report == "Executive" && (dataType == "Positive" ||
                            (dataType == "Climate") && demographic == "General");
            int answersType = parameters.Keys.Contains("all") ? (Int32.Parse(parameters["all"].ToString()) == 0 ? 1 :
                                ((Int32.Parse(parameters["options"].ToString()) % 2 == 0) ?
                                2 : 3)) : 0;
            int avg = parameters.Keys.Contains("avg") ? Int32.Parse(parameters["avg"].ToString()) : 0;
            int med = parameters.Keys.Contains("med") ? Int32.Parse(parameters["med"].ToString()) : 0;
            int countValues = report == "Satisfaction" ? (demographic == "Category" ? 0 : (demographic == "Location") ? 1 : 2)
                : (((avg == 1 && med == 1) || dataType == "SatNotSat") ? 3 :
                ((avg == 1 && med == 0) ? 1 : ((avg == 0 && med == 1) ? 2 : 0)));
            return new FunctionSPConnection(command).ExecuteFunction(report, dataType == "AvgAndMed",
                    countValues, dataType == "Question", answersType, execDic);
        }

        #region ADD
        private void GetAddCommand()
        {
            command = command + "ADD" + demographic + "]"
                + (parameters.Keys.Contains("evaluation") ? " @evaluation= " + parameters["evaluation"] : "")
                + (parameters.Keys.Contains("selectionanswer") ? " @selectionanswer= " + parameters["selectionanswer"] : "")
                + (parameters.Keys.Contains("question") ? ", @question= " + parameters["question"] : "")
                + (parameters.Keys.Contains("option") ? ", @option= " + parameters["option"] : "")
                + (parameters.Keys.Contains("options") ? ", @options= '" + parameters["options"] + "'" : "")
                + (parameters.Keys.Contains("affirmative") ? ", @affirmative= " + parameters["affirmative"] : "")
                + (parameters.Keys.Contains("text") ? ", @text= '" + parameters["text"] + "'" : "");
        }
        #endregion

        #region EVALUATION
        private void GetEvaluationsCountCommand()
        {
            command = command + report + demographic + "] @test= " + parameters["test"]
                + (demographic == "FunctionalOrganizationType" ? " , @fot= " + parameters["fot"] : "")
                + (demographic == "All" ? " , @company= " + parameters["company"] : "");
        }
        public int GetCount()
        {
            return (int)new FunctionSPConnection(command).ExecuteFunction(report, false);
        }
        public Dictionary<string, int> GetAllCount()
        {
            return (Dictionary<string, int>)new FunctionSPConnection(command).ExecuteFunction(report, true);
        }
        private void GetEvaluationCommand()
        {
            command = command + demographic + "] @id= " + parameters["id"];
        }
        public List<object[]> GetData()
        {
            return (List<object[]>)new FunctionSPConnection(command).ExecuteFunction(report, false);
        }
        #endregion

        #region POSITIVEANSWERS
        private void GetPositiveAnswersCommand()
        {
            command = command + report + demographic + "] @test= " + parameters["test"]
            + ", @satisfiedValue= " + parameters["satisfiedValue"]
            + ", @company= " + parameters["company"]
            + (parameters.Keys.Contains("state") ? " , @state= " + parameters["state"] : "")
            + (parameters.Keys.Contains("country") ? " , @country= " + parameters["country"] : "")
            + (parameters.Keys.Contains("region") ? " , @region= " + parameters["region"] : "");
        }
        public Dictionary<string, double> GetDictionary()
        {
            return (Dictionary<string, double>)new FunctionSPConnection(command).ExecuteFunction(report, false);
        }
        #endregion

        #region POPULATION
        private void GetPopulationCommand()//obtiene el comando
        {
            command = command + report + demographic + "] @test= " + parameters["test"]
            + (parameters.Keys.Contains("state") ? " , @state= " + parameters["state"] : "")
            + (parameters.Keys.Contains("country") ? " , @country= " + parameters["country"] : "")
            + (parameters.Keys.Contains("region") ? " , @region= " + parameters["region"] : "")
            + (demographic == "FunctionalOrganizationType" ? " , @fot= " + parameters["fot"] : "");
        }
        #endregion

        #region UNIVARIATE
        private void GetUnivariateCommand()
        {
            //bool minimumPeople = parameters["dataType"].ToString() == "AvgAndMed" &&
            //                    (demographic != "AllTests" || demographic != "Category");
            command = command + report + parameters["dataType"] + demographic + "] @test= " + parameters["test"]
                + (parameters["avg"].ToString() == "0" ? " , @nullAvg= 0" : "")
                + (parameters.Keys.Contains("state") ? " , @state= " + parameters["state"].ToString() : "")
                + (parameters.Keys.Contains("country") ? " , @country= " + parameters["country"].ToString() : "")
                + (parameters.Keys.Contains("questionnaire") ? " , @questionnaire= " + parameters["questionnaire"].ToString() : "")
                + (parameters.Keys.Contains("categorygroup") ? " , @categorygroup= " + parameters["categorygroup"] : "")
                + (parameters.Keys.Contains("category") ? " , @category= " + parameters["category"].ToString() : "")
                + (parameters.Keys.Contains("question") ? " , @question= " + parameters["question"].ToString() : "")
                + (demographic == "FunctionalOrganizationType" ? " , @fot= " + parameters["fot"].ToString() : "")
                + (parameters["dataType"].ToString() == "AvgAndMed" ? " , @med= " + parameters["med"].ToString() : " , @satisfiedValue= " + parameters["satisfiedValue"].ToString())
                + (parameters.Keys.Contains("minimumPeople") ? " , @minimumPeople= " + parameters["minimumPeople"].ToString() : "")
                + (((parameters.Keys.Contains("minimumAnswers") && demographic != "Category") ? " , @minimumAnswers= " + parameters["minimumAnswers"].ToString() : ""));
        }
        #endregion

        #region BIVARIATE
        private void GetBivariateCommand()
        {
            command = command + report + parameters["dataType"] + demographic + parameters["demographic2"]
            + "] @test= " + parameters["test"] + " , @med= " + parameters["med"]
            + " , @minimumAnswers= " + parameters["minimumAnswers"]
            + ((demographic == "FunctionalOrganizationType" || parameters["demographic2"].ToString() == "FunctionalOrganizationType") ? 
            " , @fot= " + parameters["fot"] : "");

        }
        #endregion

        #region RESULT
        private void GetResultCommand()
        {
            command = command + report + demographic + parameters["dataType"]
            + "] @test= " + parameters["test"] + " , @satisfiedValue= " + parameters["satisfiedValue"]
            + (parameters.Keys.Contains("questionnaire") ? ", @questionnaire= " + parameters["questionnaire"] : "")
            + (demographic == "Company" ? ", @all= " + parameters["all"] : "");
        }
        #endregion

        #region EXECUTIVEREPORT
        private void GetExecutiveReportCommand()
        {
            command = command + report + parameters["dataType"] + demographic
            + "] @test= " + parameters["test"]
            + (parameters.Keys.Contains("state") ? " , @state= " + parameters["state"] : "")
            + (parameters.Keys.Contains("country") ? " , @country= " + parameters["country"] : "")
            + (parameters["dataType"] == "Positive" ? " , @satisfiedValue= " + parameters["satisfiedValue"] : "")
            + (demographic == "FunctionalOrganizationType" ? " , @fot= " + parameters["fot"] : "");
        }
        #endregion

        #region RANKING
        private void GetRankingCommand()
        {
            command = command + parameters["dataType"] + demographic
            + "] @questionnaire= " + parameters["questionnaire"]
            + (parameters.Keys.Contains("state") ? " , @state= " + parameters["state"] : "")
            + (parameters.Keys.Contains("country") ? " , @country= " + parameters["country"] : "")
            + (parameters["dataType"] == "External" ? " , @sector= " + parameters["sector"] : " , @company= " + parameters["company"])
            + (parameters.Keys.Contains("associated") ? " , @associated= " + parameters["associated"] : "")
            + (demographic == "FunctionalOrganizationType" ? " , @fot= " + parameters["fot"] : "");
        }
        #endregion

        #region TEXTANSWER
        private void GetTextAnswerCommand()
        {
            command = command + report + demographic + "] @test= " + parameters["test"]
            + (parameters.Keys.Contains("questionnaire") ? " , @questionnaire= " + parameters["questionnaire"].ToString() : "")
            + (parameters.Keys.Contains("categorygroup") ? " , @categorygroup= " + parameters["categorygroup"] : "")
            + (parameters.Keys.Contains("category") ? " , @category= " + parameters["category"] : "")
            + (parameters.Keys.Contains("question") ? " , @question= " + parameters["question"] : "")
            + (demographic == "FunctionalOrganizationType" ? " , @fot= " + parameters["fot"] : "");
        }
        #endregion

        #region CATEGORY
        private void GetCategoryCommand()
        {
            command = command + report + demographic + "] @test= " + parameters["test"]
                + (demographic != "General" && parameters.Keys.Contains("id") ? " , @id= " + parameters["id"] : "")
                + (parameters.Keys.Contains("questionnaire") ? " , @questionnaire= " + parameters["questionnaire"].ToString() : "")
                + (parameters.Keys.Contains("minimumPeople") ? " , @minimumPeople= " + parameters["minimumPeople"] : "");
        }
        #endregion

        #region FREQUENCY
        private void GetFrequencyCommand()
        {
            command = command + report + demographic + "] @test= " + parameters["test"]
            + (demographic == "FunctionalOrganizationType" ? " , @fot= " + parameters["fot"] : "")
            + (demographic == "State" ? " , @country= " + parameters["country"] : "")
            + (parameters.Keys.Contains("questionnaire") ? " , @questionnaire= " + parameters["questionnaire"].ToString() : "")
            + (parameters.Keys.Contains("categorygroup") ? " , @categorygroup= " + parameters["categorygroup"] : "")
            + (parameters.Keys.Contains("category") ? " , @category= " + parameters["category"] : "")
            + (parameters.Keys.Contains("question") ? " , @question= " + parameters["question"] : "")
            + (parameters.Keys.Contains("minimumPeople") ? " , @minimumPeople= " + parameters["minimumPeople"] : "");
        }
        #endregion

        #region COMPARATIVE
        private void GetComparativeCommand()
        {
            command = command + report + demographic + parameters["selector"] + "] @company= " + parameters["company"]
            + (demographic == "FunctionalOrganizationType" ? " , @fot= " + parameters["fot"] : "");
        }
        #endregion

        #region SATISFACTION
        private void GetSatisfactionCommand()
        {
            command = command + report + demographic + "] @test= " + parameters["test"]
            + (parameters.Keys.Contains("id") ? " , @id= " + parameters["id"] : "") +
            " , @minimumPeople= " + parameters["minimumPeople"];
        }
        #endregion
    }
}
