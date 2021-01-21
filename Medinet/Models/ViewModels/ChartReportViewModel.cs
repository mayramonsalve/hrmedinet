using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using MedinetClassLibrary.Services;
using System.Collections;
using MedinetClassLibrary.CustomClasses;
using System.Drawing;

namespace Medinet.Models.ViewModels
{
    public class ChartReportViewModel
    {
        public SelectList demographicsList1 { get; private set; }
        public SelectList demographicsList2 { get; private set; }
        public SelectList GroupByCategories { get; private set; }
        public SelectList tests { get; private set; }
        public SelectList testsSeveralQuestionnaires { get; private set; }
        public SelectList questionnaires { get; private set; }
        public string chartType { get; set; }
        public Graphic[] graphics { get; private set; }
        public Test test { get; set; }
        public User UserLogged { get; private set; }
        public GraphicDetail[] details { get; private set; }
        public Dictionary<string, int> demographicsCount { get; private set; }
        public List<SummaryTable> listSummary { get; private set; }
        public SelectList question { get; private set; }
        public bool condition { get; private set; }
        public Dictionary<int, string> FO { get; private set; }
        public List<int> countriesId { get; private set; }
        public string title { get; private set; }
        public SelectList testsToCompare { get; private set; }
        public int testCompare { get; set; }
        public Dictionary<string, string> demographicNames { get; private set; }
        public int countTestsInQuestionnaire { get; private set; }
        public Dictionary<string, object> populationComparativeTable { get; private set; }
        public Dictionary<string, int[]> populationComparativeTotal { get; private set; }
        public string demographicSelector { get; private set; }
        public Dictionary<int, string> tabs { get; private set; }
        #region Print
        public string testCompareName { get; private set; }
        public string testName { get; private set; }
        public Graphic graphicPrint { get; private set; }
        public int test_id { get; private set; }
        public ChiSquare[] chiSquarePrint { get; private set; }
        public int? FO_id { get; private set; }
        public int? country_id { get; private set; }
        public int? category_id { get; private set; }
        public string category_name { get; private set; }
        public int? question_id { get; private set; }
        public string question_name { get; private set; }
        public double pValue { get; private set; }
        public int elementsCount { get; private set; }
        public string demographicName1 { get; private set; }
        public int demographicCount1 { get; private set; }
        public string demographicName2 { get; private set; }
        public int demographicCount2 { get; private set; }
        public int optionsCount { get; private set; }
        public string BivariateTitle { get; private set; }
        public Dictionary<string, object> stringObject { get; private set; }
        public bool isTable { get; private set; }
        public FileResult BivariateGraphic { get; private set; }
        #endregion

        //Reports List
        public ChartReportViewModel(SelectList tests, SelectList testsSeveralQuestionnaires, SelectList questionnaires)
        {
            this.tests = tests;
            this.testsSeveralQuestionnaires = testsSeveralQuestionnaires;
            this.questionnaires = questionnaires;
            this.testsToCompare = new SelectList(new TestsServices().GetEmptyDictionary(), "Key", "Value");
        }
        //No Evaluations
        public ChartReportViewModel(string test)
        {
            this.testName = test;
        }

        public ChartReportViewModel(int test_id)
        {
            this.test = new TestsServices().GetById(test_id);
            InitializeDemographicsName();
        }

        private void InitializeDemographicsName()
        {
            demographicNames = new Dictionary<string, string>();
            demographicNames.Add("AgeRange", ViewRes.Views.ChartReport.Graphics.AgeTab);
            demographicNames.Add("Gender", ViewRes.Views.ChartReport.Graphics.GenderTab);
            demographicNames.Add("InstructionLevel", ViewRes.Views.ChartReport.Graphics.InstructionLevelTab);
            demographicNames.Add("Location", ViewRes.Views.ChartReport.Graphics.LocationTab);
            demographicNames.Add("Region", ViewRes.Views.ChartReport.Graphics.RegionTab);
            demographicNames.Add("State", ViewRes.Views.ChartReport.Graphics.StateTab);
            demographicNames.Add("Country", ViewRes.Views.ChartReport.Graphics.CountryTab);
            demographicNames.Add("PositionLevel", ViewRes.Views.ChartReport.Graphics.PositionLevelTab);
            demographicNames.Add("Seniority", ViewRes.Views.ChartReport.Graphics.SeniorityTab);
            demographicNames.Add("Performance", ViewRes.Views.ChartReport.Graphics.PerformanceTab);
            foreach (FunctionalOrganizationType fot in new FunctionalOrganizationTypesServices().GetByCompany(test.Company_Id))
            {
                demographicNames.Add("FunctionalOrganizationType-" + fot.Id, fot.Name);
            }
        }
        
        //Population, Univariate, Edit Univariate
        public ChartReportViewModel(Test test, string chartType, GraphicDetail[] details, Dictionary<string, int> demographicsCount,
            SelectList question, SelectList categories, SelectList questionnaires, bool condition, int testCompare, User UserLogged)
        {
            this.UserLogged = UserLogged;
            this.questionnaires = questionnaires;
            this.GroupByCategories = categories;
            this.graphics = GetGraphics();
            this.details = details;
            this.chartType = chartType;      
            this.test = test;
            this.countTestsInQuestionnaire = test.GetSimilarTestsCount();//test.Questionnaire.Tests.Where(t => t.Company_Id == test.Company_Id && t.Evaluations.Count > 0).Count();
            this.demographicsCount = demographicsCount;
            this.listSummary = new List<SummaryTable>();
            this.question = question;
            this.condition = condition;
            this.optionsCount = new OptionsServices().GetOptionsCount((Int32)test.Questionnaire_Id);
            this.FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(test.Company_Id);
            this.countriesId = new CountriesServices().GetCountriesByTest(test);
            this.testCompare = testCompare;
            if(this.testCompare!=0)
                this.testsToCompare = new SelectList(new TestsServices().GetTestsToCompareForDropdownist(test.Id), "Key", "Value", this.testCompare);
            else
                this.testsToCompare = new SelectList(new TestsServices().GetTestsToCompareForDropdownist(test.Id), "Key", "Value");
        }

        //Print Population&Univariate
        public ChartReportViewModel(int test_id, string testName, Graphic graphic, ChiSquare[] chiSquare, List<SummaryTable> summaryTable,
            int? FO_id, int? country_id, int? category_id, string category_name, int? question_id, string question_name,
            double pValue, int elementsCount, int? compare_id, string testCompareName)
        {
            this.test_id = test_id;
            this.test = new TestsServices().GetById(test_id);
            this.testName = testName;
            this.graphicPrint = graphic;
            this.chiSquarePrint = chiSquare;
            this.listSummary = summaryTable;
            this.FO_id = FO_id;
            this.country_id = country_id;
            this.category_id = category_id;
            this.category_name = category_name;
            this.question_id = question_id;
            this.question_name = question_name;
            this.pValue = pValue;
            this.elementsCount = elementsCount;
            this.testCompare = compare_id.HasValue ? compare_id.Value : 0;
            this.testCompareName = testCompareName;
        }

        public ChartReportViewModel(Test test, Dictionary<string, object> table, Dictionary<string, int[]> total, DemographicsInTest selector,
                                     GraphicDetail[] details, User UserLogged)
        {
            this.UserLogged = UserLogged;
            this.details = details;
            this.test = test;
            this.populationComparativeTable = table;
            this.populationComparativeTotal = total;
            this.graphics = GetGraphics();
            this.demographicSelector = GetParameterNameByDemographic(selector.Demographic.Name, selector.FOT_Id.HasValue ?
                                                                    selector.FOT_Id.Value.ToString() : "");
        }

        public ChartReportViewModel(Test test, Dictionary<int, string> tabs, Dictionary<string, object> table, string titleTable,
                                    GraphicDetail[] details, User UserLogged)
        {
            this.UserLogged = UserLogged;
            this.details = details;
            this.test = test;
            this.populationComparativeTable = table;
            this.graphics = GetGraphics();
            this.tabs = tabs;
            this.demographicSelector = titleTable;
        }

        public string GetFontColorByBackgroundColor(string bgHtml)
        {
            bool black = true;
            Color bgColor = System.Drawing.ColorTranslator.FromHtml(bgHtml);
            float brightness = bgColor.GetBrightness();
            float hue = bgColor.GetHue();
            float saturation = bgColor.GetSaturation();
            if (brightness < 0.5)
                black = false;
            //black = !(bgColor.R < 128) && (bgColor.G < 128) && (bgColor.B < 128);
            return black ? "#000000" : "#FFFFFF";
        }

        public int GetFOCount(int type)//cuenta todas los EvaluationsFO que sean de X id y de X estructura funcional
        {
            return new EvaluationsFOServices().GetByFunctionalOrganizationTypeAndTest(test.Id, type).Select(fo => fo.FunctionalOrganization_Id).Distinct().Count();//devuelve gerencia de sistemas por ejemplo
            //return new FunctionalOrganizationsServices().GetByType(type).Count();
        }

        public string GetTitle(string demographic, string type, string c_fo)//busca el titulo del gráfico,ejemplo:gráfico de muestreo general
        {
            if (type == "Population")
            {
                switch (demographic)
                {
                    case "General":
                        return ViewRes.Views.ChartReport.Graphics.SampleReceived;
                    case "AgeRange":
                        return ViewRes.Controllers.ChartReports.AgePercentage;
                    case "PositionLevel":
                        return ViewRes.Controllers.ChartReports.PositionLevelPercentage;
                    case "Seniority":
                        return ViewRes.Controllers.ChartReports.SeniorityPercentage;
                    case "Country":
                        return ViewRes.Controllers.ChartReports.CountryPercentage;
                    case "Gender":
                        return ViewRes.Controllers.ChartReports.GenderPercentage;
                    case "InstructionLevel":
                        return ViewRes.Controllers.ChartReports.InstructionLevelPercentage;
                    case "Performance":
                        return ViewRes.Controllers.ChartReports.PerformancePercentage;
                    case "Region":
                        return ViewRes.Controllers.ChartReports.RegionPercentage;
                    case "Location":
                        return ViewRes.Controllers.ChartReports.LocationPercentage;
                    case "State":
                        return ViewRes.Controllers.ChartReports.StatePercentage + c_fo;
                    case "FunctionalOrganizationType":
                        return ViewRes.Controllers.ChartReports.PercentageBy + c_fo;
                    default:
                        return "";
                }
            }
            else
            {
                switch (demographic)
                {
                    case "General":
                        return ViewRes.Controllers.ChartReports.GeneralClimate;
                    case "AgeRange":
                        return ViewRes.Controllers.ChartReports.AgeClimate;
                    case "PositionLevel":
                        return ViewRes.Controllers.ChartReports.PositionLevelClimate;
                    case "Seniority":
                        return ViewRes.Controllers.ChartReports.SeniorityClimate;
                    case "Country":
                        return ViewRes.Controllers.ChartReports.CountryClimate;
                    case "Gender":
                        return ViewRes.Controllers.ChartReports.GenderClimate;
                    case "InstructionLevel":
                        return ViewRes.Controllers.ChartReports.InstructionLevelClimate;
                    case "Performance":
                        return ViewRes.Controllers.ChartReports.PerformanceClimate;
                    case "Region":
                        return ViewRes.Controllers.ChartReports.RegionClimate;
                    case "Location":
                        return ViewRes.Controllers.ChartReports.LocationClimate;
                    case "State":
                        return ViewRes.Controllers.ChartReports.StateClimate + c_fo;
                    case "FunctionalOrganizationType":
                        return ViewRes.Controllers.ChartReports.ClimateBy + c_fo;
                    default:
                        return "";
                }
            }
        }

        //Bivariate Graphics
        public ChartReportViewModel(Test test, string chartType ,GraphicDetail[] details, SelectList demographicList)
        {
            this.demographicsList1 = demographicList;
            this.demographicsList2 = demographicList;
            this.graphics = GetGraphics();
            this.details = details;
            this.test = test;
            this.chartType = chartType;
            this.listSummary = new List<SummaryTable>();
        }

        //PrintBivariate Graphics
        public ChartReportViewModel(string test, string demographic1, int count1, string demographic2, int count2, bool isTable,
                                    FileResult graphic, Dictionary<string, object> stringObject, string title, int optionsCount)
        {
            this.testName = test;
            this.demographicName1 = demographic1;
            this.demographicCount1 = count1;
            this.demographicName2 = demographic2;
            this.demographicCount2 = count2;
            this.stringObject = stringObject;
            this.BivariateTitle = title;
            this.optionsCount = optionsCount;
            this.isTable = isTable;
            this.BivariateGraphic = graphic;
        }

        public List<string> GetDemographics(string demographic, int ? id)
        {
            List<string> ls = new List<string>();
            switch (demographic)
            {
                case "General":
                    ls.Add("General");
                    break;
                case "Gender":
                    ls.Add(ViewRes.Views.Shared.Shared.Male);
                    ls.Add(ViewRes.Views.Shared.Shared.Female);
                    break;
                case "PositionLevel":
                     ls = new PositionLevelsServices().GetByCompany(test.Company_Id).OrderBy(m=> m.Name).Select(m => m.Name).ToList();
                    break;

                case "AgeRange":
                    ls = new AgesServices().GetByCompany(test.Company_Id).OrderBy(m => m.Name).Select(m => m.Name).ToList();
                    break;

                case "InstructionLevel":
                    ls = new InstructionLevelsServices().GetByCompany(test.Company_Id).OrderBy(m => m.Name).Select(m => m.Name).ToList();
                    break;

                case "Seniority":
                    ls = new SenioritiesServices().GetByCompany(test.Company_Id).OrderBy(m => m.Name).Select(m => m.Name).ToList();
                    break;

                case "FunctionalOrganizationType":
                    int ii = id.HasValue ? id.Value : 0;
                    if (ii != 0)
                        ls = new FunctionalOrganizationsServices().GetByType(ii).OrderBy(m => m.Name).Select(m => m.Name).ToList();
                    break;

                case "Location":
                    ls = new LocationsServices().GetByCompany(test.Company_Id).OrderBy(m => m.Name).Select(m => m.Name).ToList();
                    break;

                case "Performance":
                    ls = new PerformanceEvaluationsServices().GetByCompany(test.Company_Id).OrderBy(m => m.Name).Select(m => m.Name).ToList();
                    break;

                default:
                    break;
            }
            return ls;
        }

        #region Univariate

        public Graphic[] GetGraphics() {
            GraphicsServices _graphicsServices = new GraphicsServices();
            graphics = new Graphic[_graphicsServices.GetAllRecords().Count()];
            var values =_graphicsServices.GetAllRecords();
            int cont=0;
            foreach(var value in values){
                graphics[cont] = value;
                cont++;
            }
            return graphics;
        }

        public string GetFOTNameById(int type)//busca el nombre de la estructura funcional
        {
            return new FunctionalOrganizationTypesServices().GetById(type).Name;
        }

        public string GetCountryNameById(int id)
        {
            return new CountriesServices().GetById(id).Name;
        }

        public void GetSatisfiedAndNoSatisfiedByDemographic(string demographic, ref Dictionary<string, double> satisfied, ref  Dictionary<string, double> noSatisfied,
                                                            ref string conclusion, ref string ourChi, ref string chiSquareValue, int? FO_id)
        {
            ChiSquare chiSquare = new ChartsServices().GetSatisfiedAndNoSatisfiedDictionary(demographic, test.Id, chartType, 0.05, FO_id);
            conclusion = chiSquare.Conclusion;
            ourChi = chiSquare.OurChiSquare.ToString("f4");
            chiSquareValue = chiSquare.ChiSquareValue.ToString("f4");
            this.GetSatisfied(chiSquare.DataSatisfaction, ref satisfied);
            this.GetNoSatisfied(chiSquare.DataSatisfaction, ref noSatisfied);
        
        }

        public void GetSatisfied(Dictionary<string, object> demograficTable, ref Dictionary<string, double> satisfied)
        {
            IEnumerable Satisfied = (IEnumerable)demograficTable["Satisfied"];
            foreach (KeyValuePair<string, double> sat in Satisfied)
            {
                satisfied.Add(sat.Key, sat.Value);
            }
        }

        public void GetNoSatisfied(Dictionary<string, object> demograficTable, ref Dictionary<string, double> noSatisfied)
        {
            IEnumerable nosatisfied = (IEnumerable)demograficTable["NoSatisfied"];
            foreach (KeyValuePair<string, double> noSat in nosatisfied)
            {
                noSatisfied.Add(noSat.Key, noSat.Value);
            }
        }

        public List<SummaryTable> NameAverageMedianSastNoSast(int? questionnaire_id, int? category_id, int? question_id, double? pValue, int test_id, string demographic, bool condition, int FO_id, int? compare_id) 
        {
            double pvalue;
            if (pValue.HasValue)
                pvalue = pValue.Value;
            else
                pvalue = 0.05;
            return new SummaryTable().UpdateTable(questionnaire_id, category_id, question_id, pvalue, test_id, demographic, condition, FO_id, compare_id);
        }

        public List<SummaryTable> GetTextAnswersList(int test_id, string demographic, int? questionnaire_id, int? category_id, int? question_id, int? FO_id, int? compare_id)
        {
            return new SummaryTable().UpdateTable(questionnaire_id, category_id, question_id, test_id, demographic, FO_id, compare_id);
        }

        public Dictionary<int, string> GetItemsListByDemographic(int test_id, string demographic, int? FO_id)
        {
            return new SummaryTable().GetItemsByDemographic(test_id, demographic, FO_id);
        }

        public Dictionary<string, object> GetFrequencyCategory(int test_id, string type,  string demographic, int? questionnaire_id, int? category_id, int? question_id, int? FO_id, int? compare_id)
        {
            return new TestsServices().GetById(test_id).GetDataFrequencyCategory(type, demographic, questionnaire_id, category_id, question_id, FO_id, compare_id);
        }

        public int GetCategoriesCount(int questionnaire_id)
        {
            return new CategoriesServices().GetCategoriesCount(questionnaire_id);
        }

        public int GetCategoriesCountByTest(int test_id)
        {
            int numberOfCategories = 0;
            Test test = new TestsServices().GetById(test_id);
            if (test.OneQuestionnaire)
            {
                numberOfCategories = test.Questionnaire.Categories.Count();
            }
            else
            {
                numberOfCategories = test.Company.Categories.Where(p => !p.CategoryGroup_Id.HasValue).Count();
            }
            return numberOfCategories;
        }

        public Graphic GetByDemographicAndType(string demographic, string type)
        {
            return new GraphicsServices().GetByDemographicAndType(demographic, type);
        }

        public List<SummaryTable> NameAverageSastNoSastForCategories(int test_id, bool table, int? compare_id, int? questionnaire_id)
        {
            return new SummaryTable().UpdateTable(questionnaire_id, null, null, null, test_id, "Category", table, 0, compare_id);
        }

        public List<SummaryTable> NameAllTestsSastNoSast(int? questionnaire_id, int? category_id, int? question_id, int test_id)
        {
            return new SummaryTable().UpdateTable(questionnaire_id, category_id, question_id, null, test_id, "AllTests", false, null, null);
        }

        public string GetColourByClimate(double avg)
        {
            double pct = ((double)(avg * 100))/test.Questionnaire.Options.Count;
            if (pct <= 60)
                return "#FF0000";
            else if (pct > 60 && pct <= 80)
                return "#FFFF00";
            else if (pct > 80)
                return "#66FF00";
            else
                return "";
        }

        public ChiSquare getChiSquare(int test_id, string Demographic, int? questionnaire_id, int? category_id, int? question_id, int? country_id, int? FO_id, double? pValue)
        {
            ChiSquare chiSquare;
            double pvalue;
            if (pValue.HasValue)
                pvalue = pValue.Value;
            else
                pvalue = 0.05;
            chiSquare = new ChiSquare(new TestsServices().GetById(test_id), Demographic, questionnaire_id, category_id, question_id, country_id, FO_id, pvalue, null, null, null);
            chiSquare.GetAssociation();
            return chiSquare;
        }

        public List<string> GetKeysWhenCompare(List<string> test, List<string> testCompare)
        {
            List<string> keys = test;
            if (keys != testCompare)
                foreach (string key in testCompare)
                {
                    if (!keys.Contains(key))
                        keys.Add(key);
                }
            return keys;
        }

        #endregion

        #region Population

        public string[] GetOrderedDemographic(string demographic_1, string demographic_2, int? fot)//ordena los demográficos de manera vertical u horizontal, el primero que coloca va en vertical
        {
            string[] ordered = new string[2];
            if (GetCountByDemographic(demographic_1, fot) >= GetCountByDemographic(demographic_2, fot))
            {
                ordered[0] = demographic_1;
                ordered[1] = demographic_2;
            }
            else
            {
                ordered[0] = demographic_2;
                ordered[1] = demographic_1;
            }
            return ordered;
        }

        public bool IsTable(string demographic, int? fot)//esta función me da la cantidad de registros demográficos y me dice si es gráfico o tabla, si es menor a 7 es gráfico sino es tabla
        {
            return (GetCountByDemographic(demographic, fot) > 7);
        }

        public int GetCountByDemographic(string demographic, int? fot)//contar por demográfico
        {
            switch (demographic)
            {
                case "Country":
                    return test.Evaluations.Select(d => d.Location.State.Country_Id).Distinct().Count();//se calcula directamente de las evaluaciones,cantidad de paises que se midieron en la  medición 
                case "Region":
                    return test.Evaluations.Select(d => d.Location.Region_Id).Distinct().Count();
                case "State":
                    return test.Evaluations.Select(d => d.Location.State_Id).Distinct().Count();
                case "Location":
                    return test.Evaluations.Select(d => d.Location_Id).Distinct().Count();
                case "PositionLevel":
                    return test.Evaluations.Select(d => d.PositionLevel_Id).Distinct().Count();
                case "Seniority":
                    return test.Evaluations.Select(d => d.Seniority_Id).Distinct().Count();
                case "AgeRange":
                    return test.Evaluations.Select(d => d.Age_Id).Distinct().Count();
                case "InstructionLevel":
                    return test.Evaluations.Select(d => d.InstructionLevel_Id).Distinct().Count();
                case "Performance":
                    return test.Evaluations.Select(d => d.Performance_Id).Distinct().Count();
                case "Gender":
                    return 2;
                case "General":
                    return 1;
                default:// "FunctionalOrganizationType":
                    return GetFOCount(fot.Value);// new EvaluationsFOServices().GetByTest(test.Id).Where(efo => efo.FunctionalOrganization_Id == fot.Value).Select(efo => efo.FunctionalOrganization_Id).Distinct().Count();
            }
        }

        public string GetParameterNameByDemographic(string demographic, string fot_string)
        {
            int fot = fot_string != "" ? Int32.Parse(fot_string) : 0;
            if (demographic != "FunctionalOrganizationType")
                return new DemographicsServices().GetNameInSelectedLanguage(demographic);
            else
                return new FunctionalOrganizationTypesServices().GetById(fot).Name;
        }

        public Dictionary<string, double> GetPopulationTableByDemographic(string demographic, int? fot)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", demographic);
            parameters.Add("test", test.Id);
            if(fot.HasValue)
                parameters.Add("fot", fot.Value);
            return new ChartsServices().GetGraphicDataForPopulation("Population", parameters);
            //switch (demographic)
            //{
            //    case "Country":
            //        return new ChartsServices().GetCountryPctgListByTest(test.Id);
            //    case "Region":
            //        return new ChartsServices().GetRegionPctgListByTest(test.Id);
            //    case "Location":
            //        return new ChartsServices().GetLocationsPctgListByTest(test.Id);
            //    case "PositionLevel":
            //        return new ChartsServices().GetPositionLevelsPctgListByTest(test.Id);
            //    case "Seniority":
            //        return new ChartsServices().GetSenioritiesPctgListByTest(test.Id);
            //    case "AgeRange":
            //        return new ChartsServices().GetAgePctgListByTest(test.Id);
            //    case "InstructionLevel":
            //        return new ChartsServices().GetInstructionLevelsPctgListByTest(test.Id);
            //    case "Performance":
            //        return new ChartsServices().GetPerformancePctgListByTest(test.Id);
            //    case "FunctionalOrganizationType":
            //        return new ChartsServices().GetFunctionalOrganizationPctgListByTestAndType(test.Id, fot.Value);
            //}

            //return null;
        }

        public string GetCategoryShortName(string category)
        {
            string shortname = "";
            string[] catvec = category.Split(' ');
            foreach (string word in catvec)
            {
                if(word!="")
                    shortname = shortname + word.Substring(0, 1);
            }
            return shortname;
        }

        #endregion
    }    
}