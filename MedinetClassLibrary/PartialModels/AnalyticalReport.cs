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
using MedinetClassLibrary.Classes;

namespace MedinetClassLibrary.Models
{
    public class AnalyticalReport
    {
        public Test Test;
        private Dictionary<string, string> DictionaryColours;
        public User UserLogged;
        private Results ResultClass;
        private Dictionary<string, string> DemographicNames;
        private Dictionary<string, int> DemographicsCount;
        public int? country;
        public int? state;
        public int? region;
        private int optionsCount;
        public bool genderPopulation;
        public int evaluationsByUbicationCount;
        private int satisfiedValue;

        public AnalyticalReport(Test Test, User UserLogged, int? country, int? state, int? region)
        {
            this.Test = Test;
            this.DictionaryColours = CreateDictionaryColours();
            this.UserLogged = UserLogged;
            this.ResultClass = new Results(Test);//calcula las respuestas positivas
            this.country = country;
            this.state = state;
            this.region = region;
            this.DemographicNames = GetDemographicNames();
            this.genderPopulation = this.DemographicNames.ContainsKey("Gender");//me dice si entre los demograficos q voy a medir se encuentra el demografico genero,y luego me dice si existe el grafico genderpopulation
            this.optionsCount = Test.GetOptionsByTest().Select(v=> v.Value).Max();//es para sacar el pocentaje final//busca el value de la opcion mas alta
            this.evaluationsByUbicationCount = GetEvaluationsCountByUbication(country, state, region);//porcentaje de recibidos y no recibidos
            this.satisfiedValue = GetSatisfiedValue();//valor de satisfaccion
        }

        public double GetSatisfiedCountPercentage()
        {
            ChiSquare cs = new ChiSquare(Test, "General", null, null, null, 0.05, country, state, region);
            Dictionary<string, double> satisfied = (Dictionary<string, double>)cs.DataSatisfaction["Satisfied"];
            if (satisfied.Count > 0)
            {
                int sat = (int)satisfied[Test.Name];
                double pct = (double)(sat * 100) / evaluationsByUbicationCount; // GetEvaluationsByUbication(country, state, region).Count();
                return pct;
            }
            else
                return 0;
        }

        public string GetColourByClimate(double pctClimate)
        {
            if (Test.ClimateScale_Id.HasValue)
            {
                ClimateRange range = Test.ClimateScale.ClimateRanges.Where(r => r.MinValue <= (decimal)pctClimate && r.MaxValue >= (decimal)pctClimate).OrderBy(r => r.MaxValue).FirstOrDefault();
                //return System.Drawing.ColorTranslator.FromHtml(range.Color);
                return range.Color;
            }
            else
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
        }

        private Dictionary<string, string> CreateDictionaryColours()
        {
            Dictionary<string, string> colours = new Dictionary<string, string>();
            colours.Add("Green", "#00B386");
            colours.Add("Amber", "#FECE00");
            colours.Add("Red", "#FF004C");
            return colours;
        }

        public double GetGeneralClimate()
        {
            if (country.HasValue || region.HasValue)
            {
                double avg = Test.GetGeneralAvgOrMedByUbication(true, null, null, null, country, state, region).Values.FirstOrDefault();// * 100 / optionsCount;//Test.GetOptionsByTest().Count(); //Test.Questionnaire.Options.Count;
                //avg = Test.ResultBasedOn100 ? avg : avg * 100 / optionsCount;//MODIFICAR:dejar solo lo que esta despues de los puntos
                avg = avg * 100 / optionsCount;
                return avg;
            }
            else
            {
                double avg = Test.GetGeneralAvgOrMed(true, null, null, null).Values.FirstOrDefault();// *100 / optionsCount;//Test.GetOptionsByTest().Count();//Test.Questionnaire.Options.Count;
                //avg = Test.ResultBasedOn100 ? avg : avg * 100 / optionsCount;
                avg = avg * 100 / optionsCount;
                return avg;
            }
        }

        public Dictionary<string, double> GetClimateByCategories()
        {
            //Dictionary<string, double> ClimateByCategory = new Dictionary<string, double>();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "General");
            parameters.Add("test", Test.Id);
            parameters.Add("minimumPeople", this.Test.MinimumPeople);
            if (this.country.HasValue)
                parameters.Add("country", this.country.Value);
            if (this.state.HasValue)
                parameters.Add("state", this.state.Value);
            if (this.region.HasValue)
                parameters.Add("region", this.region.Value);
            Dictionary<string, object> aux = (Dictionary<string, object>)new Commands("Category", parameters).ExecuteCommand();
            Dictionary<string, double> ClimateByCategoryAux = (Dictionary<string, double>)aux[""];
            Dictionary<string, double> ClimateByCategory = new Dictionary<string, double>();
            //List<Category> categories = (Test.OneQuestionnaire ? Test.Questionnaire.Categories : Test.Company.Categories).ToList();

            if (ClimateByCategoryAux.Count >= 4)
            {
            //    double climate;
            //    //int optionsCount = Test.OneQuestionnaire ? Test.Questionnaire.Options.Count : Test.DemographicSelectorDetails.FirstOrDefault().Questionnaire.Options.Count;
            //    foreach (Category cat in categories)
            //    {
            //        if (country.HasValue || region.HasValue)
            //        {
            //            climate = Test.GetGeneralAvgOrMedByUbication(true, null, cat.Id, null, country, state, region).Values.FirstOrDefault();// * 100 / optionsCount;
            //            climate = Test.ResultBasedOn100 ? climate : climate * 100 / optionsCount;
            //        }
            //        else
            //        {
            //            climate = Test.GetGeneralAvgOrMed(true, null, cat.Id, null).Values.FirstOrDefault();// *100 / optionsCount;
            //            climate = Test.ResultBasedOn100 ? climate : climate * 100 / optionsCount;
            //        }
            //        ClimateByCategory.Add(cat.Name, climate);
            //    }
                
                foreach (string key in ClimateByCategoryAux.Keys)//esto hacerlo igual para las sucursales
                {
                    ClimateByCategory.Add(key, ClimateByCategoryAux[key] * 100 / optionsCount);
                }

                ClimateByCategory = GetBestAndWorstCategories(ClimateByCategory);//esta funcion me coloca la B o la W a los mejores y peores respectivamente    
            }
            return ClimateByCategory;
        }

        private Dictionary<string,double> GetBestAndWorstCategories(Dictionary<string, double> ClimateByCategory)
        {
            int count=0;
            double current=0;
            int last = 3;
            List<Category> categories = (Test.OneQuestionnaire ? Test.Questionnaire.Categories : Test.Company.Categories).ToList();
            if (categories.Count == 4)
                last = 2;
            Dictionary<string, double> BestAndWorstCategories = new Dictionary<string, double>();
            foreach(KeyValuePair<string, double> cbc in ClimateByCategory.OrderByDescending(c => c.Value))
            {
                count++;
                if (count <= last)
                {
                    if (count == 3 && current != cbc.Value)
                    {
                        BestAndWorstCategories.Add("B", 0);
                        break;
                    }
                    else
                    {
                        if (!BestAndWorstCategories.ContainsKey(cbc.Key))
                            BestAndWorstCategories.Add(cbc.Key, cbc.Value);
                        else
                            count--;
                    }
                }
                else
                    break;
                current = cbc.Value;
            }
            if (last == 2)
                BestAndWorstCategories.Add("B", 0);
            count = 0;
            current = 0;
            foreach (KeyValuePair<string, double> cbc in ClimateByCategory.OrderBy(c => c.Value))
            {
                count++;
                if (count <= last)
                {
                    if (count == 3 && current != cbc.Value)
                    {
                        BestAndWorstCategories.Add("W", 0);
                        break;
                    }
                    else
                    {
                        if (!BestAndWorstCategories.ContainsKey(cbc.Key))
                            BestAndWorstCategories.Add(cbc.Key, cbc.Value);
                        else
                            count--;
                    }
                }
                else
                    break;
                current = cbc.Value;
            }
            if(BestAndWorstCategories.Count!=6)
                BestAndWorstCategories.Add("W", 0);
            return BestAndWorstCategories;
        }

        //#DIT
        public Dictionary<string, double> GetPositiveAnswersByPositionLevels()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "PositionLevel");
            parameters.Add("test", Test.Id);
            parameters.Add("company", Test.Company_Id);
            parameters.Add("satisfiedValue", this.satisfiedValue);
            if(this.country.HasValue)
                parameters.Add("country", this.country.Value);
            if (this.state.HasValue)
                parameters.Add("state", this.state.Value);
            if (this.region.HasValue)
                parameters.Add("region", this.region.Value);
            Dictionary<string, double> PositiveAnswers = (Dictionary<string, double>)new Commands("PositiveAnswers", parameters).GetDictionary();
            //int greaterLevel = GetGreaterLevel();
            //if (greaterLevel != 0)
            //{
            //    Dictionary<string, double> values = ResultClass.GetPositiveAnswersPercentageByPositionLevel(greaterLevel, null, country, state, region);
            //    if (values.Count > 0)
            //    {
            //        string key = values.Keys.FirstOrDefault();
            //        PositiveAnswers.Add(key, values[key]);
            //        values = ResultClass.GetPositiveAnswersPercentageByPositionLevel(null, greaterLevel, country, state, region);
            //        if (values.Count > 0)
            //        {
            //            key = values.Keys.FirstOrDefault();
            //            PositiveAnswers.Add(key, values[key]);
            //            return PositiveAnswers;
            //        }
            //    }
            //}
            return PositiveAnswers;
        }

        private int GetEvaluationsCountByUbication(int? country_id, int? state_id, int? region_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "General");
            parameters.Add("test", Test.Id);
            if(country_id.HasValue)
                parameters.Add("country", country_id.Value);
            if (state_id.HasValue)
                parameters.Add("state", state_id.Value);
            if (region_id.HasValue)
                parameters.Add("region", region_id.Value);
            Dictionary<string, double> data = (Dictionary<string, double>)new Commands("Population", parameters).ExecuteCommand();
            return (int)data.FirstOrDefault().Value;
        }

        private int GetMinLevelByUbication()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "PositionLevel");
            parameters.Add("test", Test.Id);
            if (this.country.HasValue)
                parameters.Add("country", country.Value);
            if (this.state.HasValue)
                parameters.Add("state", state.Value);
            if (this.region.HasValue)
                parameters.Add("region", region.Value);
            return (int)new Commands("Min", parameters).GetCount();
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

        private int GetGreaterLevel()
        {
            int graterLevel = 0;
            PositionLevelsServices pls = new PositionLevelsServices();
            if(pls.GetByCompany(Test.Company_Id).Count() > 1)
                graterLevel = GetMinLevelByUbication();// GetEvaluationsByUbication(country, state, region).Max(p => p.PositionLevel.Level);
            return graterLevel;
        }

        private int GetSatisfiedValue()
        {
            int valueSatisfied = 0;
            IEnumerable<Option> options = Test.GetOptionsByTest();
            if (options != null)
            {
                valueSatisfied = options.Count() % 2 != 0 ?
                (int)Stats.Percentile(options.Select(o => double.Parse(o.Value.ToString())).ToArray(), 60) :
                (int)Stats.Percentile(options.Select(o => double.Parse(o.Value.ToString())).ToArray(), 50);
            }
            return valueSatisfied;
        }

        //#DIT
        public Dictionary<string, double> GetPositiveAnswersByFOTypes() //FOT de mayor rango
        {
            //Dictionary<string, double> ClimateByFO = ResultClass.GetPositiveAnswersPercentageByFOType(GetSeniorFOType(), country, state, region);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("demographic", "FunctionalOrganizationType");
            parameters.Add("test", Test.Id);
            parameters.Add("company", Test.Company_Id);
            parameters.Add("satisfiedValue", this.satisfiedValue);
            if (this.country.HasValue)
                parameters.Add("country", this.country.Value);
            if (this.state.HasValue)
                parameters.Add("state", this.state.Value);
            if (this.region.HasValue)
                parameters.Add("region", this.region.Value);
            Dictionary<string, double> ClimateByFO = (Dictionary<string, double>)new Commands("PositiveAnswers", parameters).GetDictionary();
            Dictionary<string, double> aux = new Dictionary<string, double>();

            foreach (string key in ClimateByFO.Keys)//esto hacerlo igual para las sucursales
            {
                aux.Add(key, ClimateByFO[key]);//Aqui se saca el porcentaje
                //aux.Add(key, ClimateByFO[key] * 100 / optionsCount);//Aqui se saca el porcentaje
                //ClimateByCategory.Add(key, ClimateByCategoryAux[key] * 100 / optionsCount);
            }

            if (aux.Count > 3)//sacar el % como se saco en clima por categorias
                return GetBestAndWorstFO(aux);
            else
                return null;


            /*if (ClimateByFO.Count > 3)//sacar el % como se saco en clima por categorias
                return GetBestAndWorstFO(ClimateByFO);
            else
                return null;*/

        }

        private Dictionary<string, double> GetBestAndWorstFO(Dictionary<string, double> ClimateByFO)
        {
            int count = 0;
            double current = 0;
            int last = 3;
            if (ClimateByFO.Count == 4)
                last = 2;
            Dictionary<string, double> BestAndWorstFO = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> cbc in ClimateByFO.OrderByDescending(c => c.Value))
            {
                count++;
                if (count <= last)
                {
                    if (count == 3 && current != cbc.Value)
                    {
                        BestAndWorstFO.Add("B", 0);
                        break;
                    }
                    else
                    {
                        if (!BestAndWorstFO.ContainsKey(cbc.Key))
                            BestAndWorstFO.Add(cbc.Key, cbc.Value);
                        else
                            count--;
                    }
                }
                current = cbc.Value;
            }
            if(last == 2)
                BestAndWorstFO.Add("B", 0);
            count = 0;
            current = 0;
            foreach (KeyValuePair<string, double> cbc in ClimateByFO.OrderBy(c => c.Value))
            {
                count++;
                if (count <= last)
                {
                    if (count == 3 && current != cbc.Value)
                    {
                        BestAndWorstFO.Add("W", 0);
                        break;
                    }
                    else
                    {
                        if (!BestAndWorstFO.ContainsKey(cbc.Key))
                            BestAndWorstFO.Add(cbc.Key, cbc.Value);
                        else
                            count--;
                    }
                }
                current = cbc.Value;
            }
            if (BestAndWorstFO.Count != 6)
                BestAndWorstFO.Add("W", 0);
            return BestAndWorstFO;
        }

        private int GetSeniorFOType()
        {
            return new FunctionalOrganizationTypesServices().GetByCompany(Test.Company_Id).Where(p => p.Parent == null).Select(id => id.Id).FirstOrDefault();            
        }

        public int[] GetPositionAndCompaniesCount()
        {
            if (Test.OneQuestionnaire)
            {
                Dictionary<Company, double> CompanyRanking = new Rankings().GetRankingForCompany(UserLogged, Test, null, null, null, false);
                int pos = 0;
                int[] PosAndCount = new int[2];
                foreach (KeyValuePair<Company, double> companyResult in CompanyRanking.OrderByDescending(key => key.Value))
                {
                    pos++;
                    if (companyResult.Key.Name == Test.Company.Name)
                    {
                        PosAndCount[0] = pos;//posicion de la empresa en el ranking
                        break;
                    }
                }
                PosAndCount[1] = CompanyRanking.Count;//cantidad de empresas
                return PosAndCount;
            }
            else
                return null;
        }

        public List<string> GetDemographicsWhereThereIsAssociation()
        {
            List<string> demographicsWithAssociation = new List<string>();
            List<int> FOT = new FunctionalOrganizationTypesServices().GetByCompany(Test.Company_Id).Select(fot => fot.Id).ToList();
            //List<int> countriesId = new CountriesServices().GetCountriesByTest(Test);
            CountriesServices countryService = new CountriesServices();
            FunctionalOrganizationTypesServices FOTService = new FunctionalOrganizationTypesServices();
            ChiSquare cs;
            foreach(string demographic in DemographicNames.Keys)//recorro los demograficos
            {
                if (demographic == "FunctionalOrganizationType")//si el demografico actual es una estructura funcional
                {
                    foreach (int type in FOT)
                    {
                        cs = new ChiSquare(Test, demographic, null, null, null, null, type, 0.05, country, state, region);
                        GetConclusion(demographicsWithAssociation, cs, demographic, FOTService.GetById(type).Name);
                    }
                }
                else
                {
                    cs = new ChiSquare(Test, demographic, null, null, null, 0.05, country, state, region);
                    GetConclusion(demographicsWithAssociation, cs, demographic, "");
                }
            }
            if (demographicsWithAssociation.Count > 0)
                return demographicsWithAssociation;
            else
                return null;
        }

        private void GetConclusion(List<string> demographicsWithAssociation, ChiSquare cs, string demographic, string name)
        {
            Dictionary<string, double> sat = (Dictionary<string, double>)cs.DataSatisfaction["Satisfied"];//diccionario de satisfechos y no satisfechos
            if(sat.Count > 1)
                cs.GetAssociation();//aqui realmente realiza lo de chicuadrado y trae los valores de:la tabla, de nosotros y la conclusion
            if (cs.Association)
            {
                if (demographic=="FunctionalOrganizationType")
                    demographicsWithAssociation.Add(name);//agreega el nombre de la estructura funcional o el nombre del demografico
                else
                    demographicsWithAssociation.Add(DemographicNames[demographic]);
            }
        }
        
        private Dictionary<string,string> GetDemographicNames()
        {
            Dictionary<string,string> demographics = new Dictionary<string,string>();
            if(!country.HasValue)
                demographics.Add("Country", ViewRes.Views.ChartReport.Graphics.CountryTab);
            if(new RegionsServices().GetByCompany(Test.Company_Id).Count() > 0 && !region.HasValue && !country.HasValue && !state.HasValue)
                demographics.Add("Region", ViewRes.Views.ChartReport.Graphics.RegionTab);
            if (new LocationsServices().GetByCompany(Test.Company_Id).Count() > 0)
                demographics.Add("Location", ViewRes.Views.ChartReport.Graphics.LocationTab);
            demographics.Add("AgeRange", ViewRes.Views.ChartReport.Graphics.AgeTab);
            demographics.Add("InstructionLevel", ViewRes.Views.ChartReport.Graphics.InstructionLevelTab);
            demographics.Add("Seniority", ViewRes.Views.ChartReport.Graphics.SeniorityTab);
            demographics.Add("PositionLevel", ViewRes.Views.ChartReport.Graphics.PositionLevelTab);
            demographics.Add("Gender", ViewRes.Views.ChartReport.Graphics.GenderTab);
            if (new PerformanceEvaluationsServices().GetByCompany(Test.Company_Id).Count() > 0)
                demographics.Add("Performance", ViewRes.Views.ChartReport.Graphics.PerformanceTab);
            demographics.Add("FunctionalOrganizationType", "");
            RemoveKeys0(demographics);
            return demographics;
        }

        private void RemoveKeys0(Dictionary<string, string> demographics)
        {
            this.DemographicsCount = new DemographicsServices().GetAllDemographicsCountByTest(Test.Id, Test.Company_Id);
            foreach (KeyValuePair<string, int> kvp in DemographicsCount)
            {
                if (kvp.Value == 0)
                {
                    demographics.Remove(kvp.Key);
                }
            }
        }

        public Dictionary<string, string> GetChartSources()
        {
            GraphicsServices gs = new GraphicsServices();
            Dictionary<string, string> chartSources = new Dictionary<string,string>();
            chartSources.Add("Gender", gs.GetSrcByDemographic(this.genderPopulation ? "Gender" : "General", "Population"));
            chartSources.Add("General", gs.GetSrcByDemographic("General", "Univariate"));
            chartSources.Add("Category", gs.GetSrcByDemographic("Category", "Univariate"));
            chartSources.Add("Location", gs.GetSrcByDemographic("Location", "Univariate"));
            return chartSources;
        }

        //#DIT
        public Dictionary<string, double> GetClimateByBranches()//obtiene el clima por sucursal
        {
            Dictionary<string, double> ClimateByBranch = new Dictionary<string, double>();
            Dictionary<string, double> ClimateByBranch2 = new Dictionary<string, double>();
            if (DemographicsCount["Location"] >= 4)
            {
                ClimateByBranch = Test.GetAvgOrMedByLocations(true, null, null, null, country, state, region);//GetAvgOrMedByLocations trae solo el promedio por sucursal
                if (ClimateByBranch.Count >= 4)
                {
                    foreach (string key in ClimateByBranch.Keys)
                    {
                        ClimateByBranch2.Add(key, ClimateByBranch[key] * 100 / optionsCount);//Aqui se saca el porcentaje
                    }

                    ClimateByBranch = GetBestAndWorstByDemographic(ClimateByBranch2);//GetBestAndWorstBranches mejores y peores sucursales.esto es para clima por sucursal(resultados por sucursal)
                }
                else
                    return new Dictionary<string, double>();
            }
            return ClimateByBranch;
        }
        
        //#DIT
        public Dictionary<string, double> GetClimateByFOTypes(int fot_id, string fot_name)
        {
            Dictionary<string, double> ClimateByFOType = new Dictionary<string, double>();
            Dictionary<string, double> ClimateByFOType2 = new Dictionary<string, double>();
            if (DemographicsCount[fot_name] >= 4)
            {
                ClimateByFOType = Test.GetAvgOrMedByFOs(true, null, null, null, fot_id);
                if (ClimateByFOType.Count >= 4)
                {
                    foreach (string key in ClimateByFOType.Keys)
                    {
                        ClimateByFOType2.Add(key, ClimateByFOType[key] * 100 / optionsCount);
                    }
                    ClimateByFOType = GetBestAndWorstByDemographic(ClimateByFOType2);
                }
                else
                    return new Dictionary<string, double>();
            }
            return ClimateByFOType;
        }

        //#DIT
        public Dictionary<string, double> GetClimateByAgeRanges()
        {
            Dictionary<string, double> ClimateByAgeRange = new Dictionary<string, double>();
            Dictionary<string, double> ClimateByAgeRange2 = new Dictionary<string, double>();
            if (DemographicsCount["AgeRange"] >= 4)
            {
                ClimateByAgeRange = Test.GetAvgOrMedByAgeRanges(true, null, null, null);
                if (ClimateByAgeRange.Count >= 4)
                {
                    foreach (string key in ClimateByAgeRange.Keys)
                    {
                        ClimateByAgeRange2.Add(key, ClimateByAgeRange[key] * 100 / optionsCount);
                    }
                    ClimateByAgeRange = GetBestAndWorstByDemographic(ClimateByAgeRange2);
                }
                else
                    return new Dictionary<string, double>();
            }
            return ClimateByAgeRange;
        }

        private IQueryable<Location> GetBranches()
        {
            IQueryable<Location> branches = Test.Evaluations.Select(b => b.Location).Distinct().AsQueryable();
            if (country.HasValue)
            {
                if (state.HasValue)
                    branches = branches.Where(b => b.State_Id == state.Value).Distinct();
                else
                    branches = branches.Where(b => b.State.Country_Id == country.Value).Distinct();
            }
            else if (region.HasValue)
                branches = branches.Where(b => b.Region_Id == region.Value).Distinct();

            return branches;
        }

        private Dictionary<string, double> GetBestAndWorstByDemographic(Dictionary<string, double> ClimateByDemographic)
        {
            int count = 0;
            double current = 0;
            int last = 3;
            if (ClimateByDemographic.Count() == 4)
                last = 2;
            Dictionary<string, double> BestAndWorstDemographic = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> cbc in ClimateByDemographic.OrderByDescending(c => c.Value))
            {
                count++;
                if (count <= last)
                {
                    if (count == 3 && current != cbc.Value)
                    {
                        BestAndWorstDemographic.Add("B", 0);
                        break;
                    }
                    else
                    {
                        if (!BestAndWorstDemographic.ContainsKey(cbc.Key))
                            BestAndWorstDemographic.Add(cbc.Key, cbc.Value);
                        else
                            count--;
                    }
                }
                else
                    break;
                current = cbc.Value;
            }
            if(last == 2)
                BestAndWorstDemographic.Add("B", 0);
            count = 0;
            current = 0;
            foreach (KeyValuePair<string, double> cbc in ClimateByDemographic.OrderBy(c => c.Value))
            {
                count++;
                if (count <= last)
                {
                    if (count == 3 && current != cbc.Value)
                    {
                        BestAndWorstDemographic.Add("W", 0);
                        break;
                    }
                    else
                    {
                        if (!BestAndWorstDemographic.ContainsKey(cbc.Key))
                            BestAndWorstDemographic.Add(cbc.Key, cbc.Value);
                        else
                            count--;
                    }
                }
                else
                    break;
                current = cbc.Value;
            }
            if (BestAndWorstDemographic.Count != 6)
                BestAndWorstDemographic.Add("W", 0);
            return BestAndWorstDemographic;
        }

        //#DIT
        public Dictionary<string, object> GetSatisfiedAndNonSatisfied(int? fot)
        {
            Dictionary<string, object> SatisfiedAndNonSatisfied = new Dictionary<string, object>();
            SatisfiedAndNonSatisfied.Add("General", GetSatisfiedAndNonSatisfiedByDemographic("General"));
            SatisfiedAndNonSatisfied.Add("Category", GetSatisfiedAndNonSatisfiedByDemographic("Category"));
            SatisfiedAndNonSatisfied.Add("Location", GetSatisfiedAndNonSatisfiedByDemographic("Location"));
            SatisfiedAndNonSatisfied.Add("FunctionalOrganizationType", GetSatisfiedAndNonSatisfiedByDemographic("FunctionalOrganizationType", fot.Value));
            SatisfiedAndNonSatisfied.Add("AgeRange", GetSatisfiedAndNonSatisfiedByDemographic("AgeRange"));
            return SatisfiedAndNonSatisfied;
        }

        private Dictionary<string, double[]> GetSatisfiedAndNonSatisfiedByDemographic(string demographic, int? fot = null)
        {
            double EvaluationsByUbication = this.evaluationsByUbicationCount;// GetEvaluationsByUbication(country, state, region).Count();
            Dictionary<string, double[]> data = new Dictionary<string, double[]>();
            ChiSquare cs = new ChiSquare(Test, demographic, null, null, null, null, fot, 0.05, country, state, region);
            Dictionary<string, double> satisfied = (Dictionary<string, double>)cs.DataSatisfaction["Satisfied"];
            Dictionary<string, double> nonSatisfied = (Dictionary<string, double>)cs.DataSatisfaction["NoSatisfied"];
            Dictionary<string, object> AvgAndMed = new Dictionary<string, object>();            
            Dictionary<string, double> average = new Dictionary<string, double>();
            Dictionary<string, double> median = new Dictionary<string, double>();
            List<string> keys = satisfied.Keys.ToList();
            bool table = false;
            if (keys.Count > 10)
            {
                if(demographic == "Location")
                    AvgAndMed = Test.GetAvgAndMedByLocations(null, null, null, false, country, state, region);
                else if (demographic == "FunctionalOrganizationType")
                    AvgAndMed = Test.GetAvgAndMedByFOTypes(null, null, null, fot.Value, false, country, state, region);
                else if (demographic == "AgeRange")
                    AvgAndMed = Test.GetAvgAndMedByAgeRanges(null, null, null, false, country, state, region);
                else
                    AvgAndMed = Test.GetCategoryAvgAndMed(false, null, country, state, region);
                average = (Dictionary<string, double>)AvgAndMed["Average"];
                median = (Dictionary<string, double>)AvgAndMed["Median"];
                table = true;
            }            
            
            foreach (string key in keys)
            {
                double sat = satisfied[key];
                double nonSat = nonSatisfied[key];
                if (demographic == "Location")
                    EvaluationsByUbication = sat + nonSat;
                double pctSat = sat * 100 / EvaluationsByUbication;
                double pctNonSat = nonSat * 100 / EvaluationsByUbication;
                if (table)
                {
                    double avg = average[key];
                    double med = median[key];
                    data.Add(key, new double[] { sat, pctSat, nonSat, pctNonSat, avg, med });
                }
                else
                    data.Add(key, new double[] { sat, pctSat, nonSat, pctNonSat });
            }
            return data;
        }

        public Dictionary<string, string> GetUbication()
        {
            Dictionary<string, string> Ubications = new Dictionary<string, string>();
            if (country.HasValue)
            {
                Ubications.Add("Country", new CountriesServices().GetById(country.Value).Name);
                if (state.HasValue)
                {
                    Ubications.Add("State", new StatesServices().GetById(state.Value).Name);
                }

            }
            else if (region.HasValue)
                Ubications.Add("Region", new RegionsServices().GetById(region.Value).Name);
            else
                Ubications.Add("General", "General");

            return Ubications;
        }

        #region Stepwise

        /*
         * 1. Get%PositiveByEvaluation y GetVectorForEvaluations
         * 2. Get%PositiveByEvaluationByEachCategory
         * 3. GetVectorForCategoriesByEvaluation
         * 4. GetMatrixForCategoriesByEachEvaluation
         */

        private/* NumericalVariable*/ double[] GetVectorDependentVariable()
        {
            double[] dependentVariable = new double[Test.Evaluations.Count];
            int c = 0;
            foreach(Evaluation evaluation in Test.Evaluations)
            {
                dependentVariable[c] = ResultClass.GetPositiveAnswerPctgByEvaluation(evaluation,null);
                c++;
            }
            return dependentVariable;/*new NumericalVariable("Positive answers percentage by evaluation", dependentVariable);*/
        }

        private NumericalVariable[] GetMatrixIndependentVariables()
        {
            NumericalVariable[] independentVariables = new NumericalVariable[Test.Evaluations.Count];
            double[] dependentVariable;
            int c, e=0;
            double[] eval = GetVectorDependentVariable();
            foreach (Evaluation evaluation in Test.Evaluations)
            {
                c = 1;
                dependentVariable = new double[Test.Questionnaire.Categories.Count+2];
                dependentVariable[0] = evaluation.Id;
                foreach (Category category in Test.Questionnaire.Categories)
                {
                    dependentVariable[c] = ResultClass.GetPositiveAnswerPctgByEvaluation(evaluation, category.Id);
                    c++;
                }
                dependentVariable[c] = eval[e];
                independentVariables[e] = new NumericalVariable(evaluation.Id.ToString(), dependentVariable);
                e++;
            }
            return independentVariables;
        }
        
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable("Stepwise");
            dt.Columns.Add("Key",typeof(string));
            foreach(string cat in GetCategories())
            {
                dt.Columns.Add(cat,typeof(double));
            }
            dt.Columns.Add("Evaluations",typeof(double));
            
            NumericalVariable[] independentVariables = new NumericalVariable[Test.Evaluations.Count];
            double[] dependentVariable = new double[Test.Questionnaire.Categories.Count + 2];
            int c, e = 0;
            double[] eval = GetVectorDependentVariable();
            object[] dr;
            foreach (Evaluation evaluation in Test.Evaluations)
            {
                c = 1;
                dr = new object[Test.Questionnaire.Categories.Count + 2];
                dr[0] = evaluation.Id;
                foreach (Category category in Test.Questionnaire.Categories)
                {
                    dr[c] = ResultClass.GetPositiveAnswerPctgByEvaluation(evaluation, category.Id);
                    c++;
                }
                dr[c] = eval[e];
                e++;
                dt.Rows.Add(dr);
            }     
            return dt;
        }

        private string[] GetCategories()
        {
            string[] categories = new string[Test.Questionnaire.Categories.Count];
            int c = 0;
            foreach (Category cat in Test.Questionnaire.Categories)
            {
                categories[c] = cat.Name;
                c++;
            }
            return categories;
        }

        public Dictionary<string, double> GetStepwiseValues()
        {
            //if (Test.Evaluations.Count > 7 && Test.Evaluations.Count > Test.Questionnaire.Categories.Count)
            //if(false)
            //{
            //    VariableCollection vc = new VariableCollection(GetDataTable());
            //    string dependent = "Evaluations";
            //    string[] independents = GetCategories();
            //    LinearRegressionModel regression = new LinearRegressionModel(vc, dependent, independents);
            //    StepwiseOptions stepwise = new StepwiseOptions();
            //    stepwise.Criterion = StepwiseCriterion.PValue;
            //    stepwise.Method = StepwiseRegressionMethod.AllVariables;
            //    stepwise.ToEnterPValueThreshold = 0.05;
            //    stepwise.ToRemovePValueThreshold = 0.1;
            //    regression.StepwiseOptions = stepwise;
            //    regression.NoIntercept = true;
            //    regression.Compute();
            //    SymmetricMatrix sum = regression.GetSumOfSquaresMatrix();
            //    DataTable anova = regression.AnovaTable.ToDataTable();
            //    string best = regression.BestFitParameters.ToString();
            //    SymmetricMatrix corr = regression.GetCorrelationMatrix();
            //    SymmetricMatrix cov = regression.GetCovarianceMatrix();
            //    Vector resi = regression.GetStudentizedResiduals();
            //    DenseMatrix obs = regression.ObservationMatrix;
            //    DenseVector stre = regression.GetStudentizedResiduals();
            //    DenseVector ddd = regression.GetStudentizedDeletedResiduals();
            //    foreach (Parameter parameter in regression.Parameters)
            //    {

            //    }
            //}
            return null;
        }

        #endregion

    }
}
