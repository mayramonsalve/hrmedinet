using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.CustomClasses;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Models
{
    public class BivariateCharts
    {
        public double significanceLevel = 0.05;
        Dictionary<string, object> parameters;
        Dictionary<string, object> dictionaryData;
        public BivariateCharts() {
            parameters = new Dictionary<string, object>();
            dictionaryData = new Dictionary<string, object>();
        }

        public string[] SetTitles(string title, string axisX, string axisY, string legendName) {
            string[] Titles = new string[4];
            Titles[0] = title;
            Titles[1] = axisX;
            Titles[2] = axisY;
            Titles[3] = legendName;
            return Titles;
        }

        #region Select Bivariate Chart
        public object SelectBivariateGraphic(int test_id, string demographic_1, string demographic_2, bool chart)
        {//INCLUIRQUESTIONNAIREID
            int company_id = new TestsServices().GetById(test_id).Company_Id;
            GetParameters(test_id, demographic_1, demographic_2);
            dictionaryData = (Dictionary<string, object>)new Commands("Bivariate", parameters).ExecuteCommand();
            switch (demographic_1)
            {
                case "Country": //Country:
                    return CountriesChart(test_id, demographic_2, company_id, chart);
                case "Region": //"Regions":
                    return RegionsChart(test_id, demographic_2, company_id, chart);
                case "State":// "States":
                    return StatesChart(test_id, demographic_2, company_id, chart);
                case "Location": //"Locations":
                    return LocationsChart(test_id, demographic_2, company_id, chart);
                case "PositionLevel": //"PositionLevels":
                    return PositionLevelsChart(test_id, demographic_2, company_id, chart);
                case "Seniority": //"Seniorities":
                    return SenioritiesChart(test_id, demographic_2, company_id, chart);
                case "AgeRange": //"AgeRanges":
                    return AgeRangesChart(test_id, demographic_2, company_id, chart);
                case "Gender": //"Gender":
                    return GendersChart(test_id, demographic_2, company_id, chart);
                case "InstructionLevel": //"InstructionLevel":
                    return InstructionLevelsChart(test_id, demographic_2, company_id, chart);
                case "Performance": //"Performance Evaluation":
                    return PerformanceChart(test_id, demographic_2, company_id, chart);
                default: //"Functional Organization Type":
                    return FunctionalOrganizationTypesChart(test_id, demographic_1, demographic_2, company_id, chart);
            }
        }

        private void GetParameters(int test_id, string demographic_1, string demographic_2)
        {
            Test test = new TestsServices().GetById(test_id);
            //parameters.Add("minimumAnswers", test.MinimumPeople * test.Questionnaire.Categories.SelectMany(q => q.Questions).Count());
            parameters.Add("minimumAnswers", test.GetMinimumAnswers("", null, null, null, false));
            parameters.Add("demographic", demographic_1);
            parameters.Add("demographic2", demographic_2);
            parameters.Add("test", test_id);
            parameters.Add("dataType", "AvgAndMed");
            parameters.Add("med", 0);
            if (demographic_1.Contains("FunctionalOrganizationType"))
            {
                parameters.Add("fot", Int32.Parse(demographic_1.Split('-')[1]));
                parameters["demographic"] = demographic_1.Split('-')[0];
            }
            else if (demographic_2.Contains("FunctionalOrganizationType"))
            {
                parameters.Add("fot", Int32.Parse(demographic_2.Split('-')[1]));
                parameters["demographic2"] = parameters["demographic"];
                parameters["demographic"] = demographic_2.Split('-')[0];
            }
        }

        private object FunctionalOrganizationTypesChart(int test_id, string demographic_1, string demographic_2, int company_id, bool chart)
        {
            int FOT_id = Int32.Parse(demographic_1.Split('-')[1]);
            switch (demographic_2)
            {
                case "Country": //"Country":
                    return this.FOAndCountryChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "País": //"Country":
                //    return this.FOAndCountryChart("BigScreen", "Column", test_id, null, null, FOT_id);
                case "Region": // "Regions":
                    return this.FOAndRegionChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "Región": // "Regions":
                //    return this.FOAndRegionChart("BigScreen", "Column", test_id, null, null, FOT_id);
                case "State": //"State":
                    return this.FOAndStateChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "Estado": //"State":
                //    return this.FOAndStateChart("BigScreen", "Column", test_id, null, null, FOT_id);
                case "Location": //"Locations":
                    return this.FOAndLocationsChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "Sucursal": //"Locations":
                //    return this.FOAndLocationsChart("BigScreen", "Column", test_id, null, null, FOT_id);
                case "PositionLevel": //"PositionLevels":
                    return this.FOAndPositionLevelChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.FOAndPositionLevelChart("BigScreen", "Column", test_id, null, null, FOT_id);
                case "Seniority": //"Seniorities":
                    return this.FOAndSeniorityChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.FOAndSeniorityChart("BigScreen", "Column", test_id, null, null, FOT_id);
                case "AgeRange": //"AgeRanges":
                    return this.FOAndAgeRangeChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.FOAndAgeRangeChart("BigScreen", "Column", test_id, null, null, FOT_id);
                case "Gender": //"Gender":
                    return this.FOAndGenderChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "Género": //"Gender":
                //    return this.FOAndGenderChart("BigScreen", "Column", test_id, null, null, FOT_id);
                case "InstructionLevel": //"instruction Levels":
                    return this.FOAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.FOAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, FOT_id);
                default:
                    //int FO_id = new FunctionalOrganizationTypesServices().GetByName(company_id, demographic_2).Id;
                    return this.FOAndPerformanceChart("BigScreen", "Column", test_id, null, null, FOT_id, chart);
            }
        }

        private object PerformanceChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "Country": //"Country":
                    return this.PerformanceAndCountryChart("BigScreen", "Column", test_id, null, null, chart);
                //case "País": //"Country":
                //    return this.PerformanceAndCountryChart("BigScreen", "Column", test_id, null, null);
                case "Region": // "Regions":
                    return this.PerformanceAndRegionChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Región": // "Regions":
                //    return this.PerformanceAndRegionChart("BigScreen", "Column", test_id, null, null);
                case "State": //"State":
                    return this.PerformanceAndStateChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Estado": //"State":
                //    return this.PerformanceAndStateChart("BigScreen", "Column", test_id, null, null);
                case "Location": //"Locations":
                    return this.PerformanceAndLocationsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Sucursal": //"Locations":
                //    return this.PerformanceAndLocationsChart("BigScreen", "Column", test_id, null, null);
                case "PositionLevel": //"PositionLevels":
                    return this.PerformanceAndPositionLevelChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.PerformanceAndPositionLevelChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.PerformanceAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.PerformanceAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.PerformanceAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.PerformanceAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.PerformanceAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.PerformanceAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.PerformanceAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.PerformanceAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndPerformanceChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object InstructionLevelsChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "Country": //"Country":
                    return this.InstructionLevelsAndCountriesChart("BigScreen", "Column", test_id, null, null, chart);
                //case "País": //"Country":
                //    return this.InstructionLevelsAndCountriesChart("BigScreen", "Column", test_id, null, null);
                case "Region": // "Regions":
                    return this.InstructionLevelsAndRegionChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Región": // "Regions":
                //    return this.InstructionLevelsAndRegionChart("BigScreen", "Column", test_id, null, null);
                case "State": //"State":
                    return this.InstructionLevelsAndStateChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Estado": //"State":
                //    return this.InstructionLevelsAndStateChart("BigScreen", "Column", test_id, null, null);
                case "Location": //"Locations":
                    return this.InstructionLevelsAndLocationsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Sucursal": //"Locations":
                //    return this.InstructionLevelsAndLocationsChart("BigScreen", "Column", test_id, null, null);
                case "PositionLevel": //"PositionLevels":
                    return this.InstructionLevelsAndPositionLevelChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.InstructionLevelsAndPositionLevelChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.InstructionLevelsAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.InstructionLevelsAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.InstructionLevelsAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.InstructionLevelsAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.InstructionLevelsAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.InstructionLevelsAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.InstructionLevelsAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.InstructionLevelsAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object GendersChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "Country": //"Country":
                    return this.GenderAndCountryChart("BigScreen", "Column", test_id, null, null, chart);
                //case "País": //"Country":
                //    return this.GenderAndCountryChart("BigScreen", "Column", test_id, null, null);
                case "Region": // "Regions":
                    return this.GenderAndRegionChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Región": // "Regions":
                //    return this.GenderAndRegionChart("BigScreen", "Column", test_id, null, null);
                case "State": //"State":
                    return this.GenderAndStateChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Estado": //"State":
                //    return this.GenderAndStateChart("BigScreen", "Column", test_id, null, null);
                case "Location": //"Locations":
                    return this.GenderAndLocationsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Sucursal": //"Locations":
                //    return this.GenderAndLocationsChart("BigScreen", "Column", test_id, null, null);
                case "PositionLevel": //"PositionLevels":
                    return this.GenderAndPositionLevelChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.GenderAndPositionLevelChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.GenderAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.GenderAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.GenderAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.GenderAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.GenderAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.GenderAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.GenderAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.GenderAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndGenderChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object AgeRangesChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "Country": //"Country":
                    return this.AgeRangeAndCountriesChart("BigScreen", "Column", test_id, null, null, chart);
                //case "País": //"Country":
                //    return this.AgeRangeAndCountriesChart("BigScreen", "Column", test_id, null, null);
                case "Region": // "Regions":
                    return this.AgeRangeAndRegionChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Región": // "Regions":
                //    return this.AgeRangeAndRegionChart("BigScreen", "Column", test_id, null, null);
                case "State": //"State":
                    return this.AgeRangeAndStateChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Estado": //"State":
                //    return this.AgeRangeAndStateChart("BigScreen", "Column", test_id, null, null);
                case "Location": //"Locations":
                    return this.AgeRangeAndLocationChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Sucursal": //"Locations":
                //    return this.AgeRangeAndLocationChart("BigScreen", "Column", test_id, null, null);
                case "PositionLevel": //"PositionLevels":
                    return this.AgeRangeAndPositionLevelChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.AgeRangeAndPositionLevelChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.AgeRangeAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.AgeRangeAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.AgeRangeAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.AgeRangeAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.AgeRangeAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.AgeRangeAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.AgeRangeAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.AgeRangeAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndAgeRangeChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object SenioritiesChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "Country": //"Country":
                    return this.SeniorityAndCountriesChart("BigScreen", "Column", test_id, null, null, chart);
                //case "País": //"Country":
                //    return this.SeniorityAndCountriesChart("BigScreen", "Column", test_id, null, null);
                case "Region": // "Regions":
                    return this.SeniorityAndRegionChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Región": // "Regions":
                //    return this.SeniorityAndRegionChart("BigScreen", "Column", test_id, null, null);
                case "State": //"State":
                    return this.SeniorityAndStateChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Estado": //"State":
                //    return this.SeniorityAndStateChart("BigScreen", "Column", test_id, null, null);
                case "Location": //"Locations":
                    return this.SeniorityAndLocationChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Sucursal": //"Locations":
                //    return this.SeniorityAndLocationChart("BigScreen", "Column", test_id, null, null);
                case "PositionLevel": //"PositionLevels":
                    return this.SeniorityAndPositionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.SeniorityAndPositionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.SeniorityAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.SeniorityAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.SeniorityAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.SeniorityAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.SeniorityAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.SeniorityAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.SeniorityAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.SeniorityAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndSeniorityChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object PositionLevelsChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "Country": //"Country":
                    return this.PositionLevelAndCountriesChart("BigScreen", "Column", test_id, null, null, chart);
                //case "País": //"Country":
                //    return this.PositionLevelAndCountriesChart("BigScreen", "Column", test_id, null, null);
                case "Region": // "Regions":
                    return this.PositionLevelAndRegionChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Región": // "Regions":
                //    return this.PositionLevelAndRegionChart("BigScreen", "Column", test_id, null, null);
                case "State": //"State":
                    return this.PositionLevelAndStateChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Estado": //"State":
                //    return this.PositionLevelAndStateChart("BigScreen", "Column", test_id, null, null);
                case "Location": //"Locations":
                    return this.PositionLevelAndLocationsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Sucursal": //"Locations":
                //    return this.PositionLevelAndLocationsChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.PositionLevelAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.PositionLevelAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.PositionLevelAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.PositionLevelAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.PositionLevelAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.PositionLevelAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.PositionLevelAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.PositionLevelAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.PositionLevelsAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.PositionLevelsAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndPositionLevelChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object LocationsChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "PositionLevel": //"PositionLevels":
                    return this.LocationAndPositionLevelChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.LocationAndPositionLevelChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.LocationAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.LocationAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.LocationAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.LocationAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.LocationAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.LocationAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.LocationAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.LocationAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.LocationAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.LocationAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndLocationsChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object StatesChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "PositionLevel": //"PositionLevels":
                    return this.StateAndPositionLevelChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.StateAndPositionLevelChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.StateAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.StateAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.StateAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.StateAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.StateAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.StateAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.StateAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.StateAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.StateAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.StateAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndStateChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object RegionsChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "PositionLevel": //"PositionLevels":
                    return this.RegionAndPositionLevelChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.RegionAndPositionLevelChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.RegionAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.RegionAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.RegionAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.RegionAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.RegionAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.RegionAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.RegionAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.RegionAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.RegionAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.RegionAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndRegionChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }

        private object CountriesChart(int test_id, string demographic_2, int company_id, bool chart)
        {
            switch (demographic_2)
            {
                case "PositionLevel": //"PositionLevels":
                    return this.CountryAndPositionLevelChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de cargo": //"PositionLevels":
                //    return this.CountryAndPositionLevelChart("BigScreen", "Column", test_id, null, null);
                case "Seniority": //"Seniorities":
                    return this.CountryAndSeniorityChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Antigüedad": //"Seniorities":
                //    return this.CountryAndSeniorityChart("BigScreen", "Column", test_id, null, null);
                case "AgeRange": //"AgeRanges":
                    return this.CountryAndAgeRangeChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Rango de edad": //"AgeRanges":
                //    return this.CountryAndAgeRangeChart("BigScreen", "Column", test_id, null, null);
                case "Gender": //"Gender":
                    return this.CountryAndGenderChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Género": //"Gender":
                //    return this.CountryAndGenderChart("BigScreen", "Column", test_id, null, null);
                case "InstructionLevel": //"instruction Levels":
                    return this.CountryAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Nivel de instrucción": //"instruction Levels":
                //    return this.CountryAndInstructionLevelsChart("BigScreen", "Column", test_id, null, null);
                case "Performance": //"Performance":
                    return this.CountryAndPerformanceChart("BigScreen", "Column", test_id, null, null, chart);
                //case "Desempeño": //"Performance":
                //    return this.CountryAndPerformanceChart("BigScreen", "Column", test_id, null, null);
                default:
                    int FO_id = Int32.Parse(demographic_2.Split('-')[1]);
                    return this.FOAndCountryChart("BigScreen", "Column", test_id, null, null, FO_id, chart);
            }
        }
        #endregion

        #region Age Range
        public object AgeRangeAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndGender(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                            + ViewRes.Classes.BivariateCharts.And
                            + ViewRes.Classes.BivariateCharts.Genders;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //FileContentResult file = new FileContentResult(ms.GetBuffer(), @"image/png");
                //return file;
            }
            else
                return dictionaryDataObject;
        } 
        
        public object AgeRangeAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                            + ViewRes.Classes.BivariateCharts.And
                            + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                   dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
               // return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object AgeRangeAndCountriesChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndCountries(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                            + ViewRes.Classes.BivariateCharts.And
                            + ViewRes.Classes.BivariateCharts.Countries;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Countries;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object AgeRangeAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                          + ViewRes.Classes.BivariateCharts.And
                          + ViewRes.Classes.BivariateCharts.InstructionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                   dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object AgeRangeAndLocationChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndLocations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Locations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);           
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Locations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object AgeRangeAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object AgeRangeAndRegionChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndRegions(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Regions;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);         
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Regions;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms; 
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
       
        public object AgeRangeAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object AgeRangeAndStateChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByAgeRangesAndStates(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.AgeRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.States;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);        
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.AgeRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        #endregion Age Ranges

        #region Gender
        public object GenderAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object GenderAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object GenderAndCountryChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndCountries(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Countries;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);           
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Countries;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object GenderAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object GenderAndLocationsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndLocations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Locations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);            
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Locations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object GenderAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object GenderAndRegionChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndRegions(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Regions;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);         
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Regions;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object GenderAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object GenderAndStateChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByGenderAndStates(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Genders
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.States;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Genders;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        #endregion Gender

        #region Seniority
        public object SeniorityAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);           
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object SeniorityAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object SeniorityAndCountriesChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndCountries(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Countries;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Countries;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object SeniorityAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object SeniorityAndLocationChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndLocations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Locations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);           
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Locations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object SeniorityAndPositionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object SeniorityAndRegionChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndRegions(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Regions;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);         
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Regions;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object SeniorityAndStateChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndStates(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.States;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);        
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object SeniorityAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedBySenioritiesAndGender(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.SeniorityRanges
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Genders;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);         
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        #endregion Seniority

        #region Location
        public object LocationAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByLocationsAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Locations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Locations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object LocationAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByLocationsAndGenders(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Locations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Genders;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);            
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Locations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object LocationAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByLocationsAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Locations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);           
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Locations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object LocationAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByLocationsAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Locations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Locations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object LocationAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByLocationsAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Locations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Locations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object LocationAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByLocationsAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Locations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Locations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        #endregion Location

        #region Region
        public object RegionAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByRegionsAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Regions
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);           
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Regions;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object RegionAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByRegionsAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Regions
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Regions;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object RegionAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByRegionsAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Regions
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Regions;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object RegionAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByRegionsAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Regions
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Regions;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object RegionAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByRegionsAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Regions
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Regions;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object RegionAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedRegionsAndGender(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Regions
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Genders;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);            
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Regions;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                // return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        #endregion Region

        #region State
        public object StateAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByStatesAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.States
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.States;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object StateAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByStatesAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.States
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.States;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object StateAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByStatesAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.States
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object StateAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByStatesAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.States
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object StateAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByStatesAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.States
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object StateAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByStatesAndGenders(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.States
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Genders;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.States;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        #endregion State

        #region Country
        public object CountryAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByCountriesAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Countries
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Countries;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object CountryAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByCountriesAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Countries
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Countries;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                // return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object CountryAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByCountriesAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Countries
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Countries;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object CountryAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            ////Dictionary<string, object> dictionaryData = test.GetAvgOrMedByCountriesAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Countries
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Countries;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return File(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object CountryAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByCountriesAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Countries
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Countries;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object CountryAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedCountriesAndGender(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.Countries
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Genders;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.Countries;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        #endregion Country

        #region Instruction Level
        public object InstructionLevelsAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByInstructionLevelsAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object InstructionLevelsAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByInstructionLevelsAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object InstructionLevelsAndCountriesChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByInstructionLevelsAndCountries(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Countries;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Countries;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object InstructionLevelsAndLocationsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByInstructionLevelsAndLocations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Locations;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Locations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object InstructionLevelsAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByInstructionLevelsAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object InstructionLevelsAndRegionChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByInstructionLevelsAndRegions(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Regions;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Regions;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                // return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object InstructionLevelsAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByInstructionLevelsAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object InstructionLevelsAndStateChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByInstructionLevelsAndStates(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.States;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object InstructionLevelsAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedInstructionLevelsAndGender(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.InstructionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Genders;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.InstructionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        #endregion Instruction Level

        #region Position Level
        public object PositionLevelAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPositionLevelsAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                // return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PositionLevelsAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPositionLevelsAndPerformanceEvaluations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PositionLevelAndCountriesChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPositionLevelsAndCountries(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Countries;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Countries;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PositionLevelAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPositionLevelsAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PositionLevelAndLocationsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPositionLevelsAndLocations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Locations;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Locations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PositionLevelAndRegionChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPositionLevelsAndRegions(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Regions;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Regions;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PositionLevelAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPositionLevelsAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                // return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PositionLevelAndStateChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPositionLevelsAndStates(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.States;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PositionLevelAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedPositionLevelsAndGender(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PositionLevels
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Genders;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PositionLevels;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        #endregion Position Level

        #region Performance
        public object PerformanceAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndAgeRanges(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);
                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PerformanceAndCountryChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndCountries(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Countries;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Countries;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PerformanceAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndInstructionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PerformanceAndLocationsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndLocations(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Locations;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Locations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PerformanceAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndPositionLevels(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PerformanceAndRegionChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndRegions(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Regions;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Regions;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PerformanceAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndSeniorities(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PerformanceAndStateChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndStates(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.States;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object PerformanceAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByPerformanceEvaluationsAndGender(true, category_id, question_id);
            string newTitle = ViewRes.Classes.BivariateCharts.PerformanceEvaluations
            + ViewRes.Classes.BivariateCharts.And
            + ViewRes.Classes.BivariateCharts.Genders;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        #endregion Performance

        #region Functional Organization Type
        public object FOAndAgeRangeChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndAgeRanges(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.AgeRanges;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.AgeRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return File(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object FOAndCountryChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndCountries(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Countries;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Countries;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object FOAndInstructionLevelsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndInstructionLevels(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.InstructionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.InstructionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object FOAndLocationsChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndLocations(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Locations;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Locations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object FOAndPositionLevelChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndPositionLevels(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.PositionLevels;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PositionLevels;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object FOAndRegionChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndRegions(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Regions;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Regions;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object FOAndSeniorityChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndSeniorities(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.SeniorityRanges;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.SeniorityRanges;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object FOAndStateChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndStates(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.States;            
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.States;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        public object FOAndGenderChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndGender(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;
            string newTitle = FO
                + ViewRes.Classes.BivariateCharts.And
                + ViewRes.Classes.BivariateCharts.Genders;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.Genders;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }
        
        public object FOAndPerformanceChart(string chartSize, string chartType, int test_id, int? category_id, int? question_id, int FO_id, bool chart)
        {
            Test test = new TestsServices().GetById(test_id);
            //Dictionary<string, object> dictionaryData = test.GetAvgOrMedByFunctionalOrganizationsAndPerformanceEvaluations(true, category_id, question_id, FO_id);
            string FO = new FunctionalOrganizationTypesServices().GetById(FO_id).Name;            
            string newTitle = FO
            + ViewRes.Classes.BivariateCharts.And
            + ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
            List<object> dictionaryDataObject = new List<object>(); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle); dictionaryDataObject.Add(dictionaryData); dictionaryDataObject.Add(newTitle);
            if (chart)
            {
                string axisX = ViewRes.Classes.BivariateCharts.PerformanceEvaluations;
                string axisY = ViewRes.Classes.BivariateCharts.Average;
                string legend = FO;
                ChartDetails cd = new ChartDetails(chartSize, chartType, false, SetTitles(newTitle, axisX, axisY, legend), 2,
                                  dictionaryData, test_id, false);

                MemoryStream ms = new MemoryStream();
                cd.chart.SaveImage(ms);
                return ms;
                //return new FileContentResult(ms.GetBuffer(), @"image/png");
            }
            else
                return dictionaryDataObject;
        }

        #endregion Functional Organization Type

    }
}
