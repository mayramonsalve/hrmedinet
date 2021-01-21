using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using MedinetClassLibrary.Services;

namespace Medinet.Models.ViewModels
{
    public class AnalyticalReportViewModel {
        public Test Test { get; private set; }
        public bool Print { get; private set; }
        public double SatisfiedCountPercentage { get; private set; }
        public string ColourByClimate { get; private set; }
        public double GeneralClimate { get; private set; }
        public Dictionary<string, double> ClimateByCategories { get; private set; }
        public Dictionary<string, double> PositiveAnswersByPositionLevels { get; private set; }
        public Dictionary<string, double> PositiveAnswersByFOTypes { get; private set; }
        public int[] PositionAndCompaniesCount { get; private set; }
        public Dictionary<string, double> StepwiseValues { get; private set; }
        public List<string> DemographicsWhereThereIsAssociation { get; private set; }
        public Dictionary<string, string> ChartSources { get; private set; }
        public Dictionary<string, double> ClimateByBranches { get; private set; }
        public Dictionary<string, double> ClimateByFOTypes { get; private set; }
        public Dictionary<string, double> ClimateByAgeRanges { get; private set; }
        public Dictionary<string, object> SatNotSat { get; private set; }
        public Dictionary<string, string> Ubication { get; private set; }
        public string FOTName { get; private set; }
        public int? country_id { get; private set; }
        public int? state_id { get; private set; }
        public int? region_id { get; private set; }
        public int graphicIdPopulationGender { get; private set; }
        public int optionsByTest { get; private set; }
        public int optionsCount { get; private set; }

        public AnalyticalReportViewModel(Test Test, bool? Print)
        {
            this.Test = Test;
            if (Print.HasValue)
                this.Print = Print.Value;
            else
                this.Print = false;
        }

        public AnalyticalReportViewModel(Test Test, bool? Print, double SatisfiedCountPercentage,
            string ColourByClimate, double GeneralClimate, Dictionary<string, double> ClimateByCategories,
            Dictionary<string, double> PositiveAnswersByPositionLevels,
            Dictionary<string, double> PositiveAnswersByFOTypes, int[] PositionAndCompaniesCount,
            Dictionary<string, double> StepwiseValues, List<string> DemographicsWhereThereIsAssociation,
            Dictionary<string, string> ChartSources, Dictionary<string, double> ClimateByBranches,
            Dictionary<string, double> ClimateByFOTypes, Dictionary<string, double> ClimateByAgeRanges,
            Dictionary<string, object> SatNotSat, Dictionary<string, string> Ubication,
            string FOTName, int? country_id, int? state_id, int? region_id, int graphicId)
        {
            this.Test = Test;
            if (Print.HasValue)
                this.Print = Print.Value;
            else
                this.Print = false;
            this.FOTName = FOTName;
            this.SatisfiedCountPercentage = SatisfiedCountPercentage;
            this.ColourByClimate = ColourByClimate;
            this.GeneralClimate = GeneralClimate;
            this.ClimateByCategories = ClimateByCategories;
            this.PositiveAnswersByPositionLevels = PositiveAnswersByPositionLevels;
            this.PositiveAnswersByFOTypes = PositiveAnswersByFOTypes;
            this.PositionAndCompaniesCount = PositionAndCompaniesCount;
            this.StepwiseValues = StepwiseValues;
            this.DemographicsWhereThereIsAssociation = DemographicsWhereThereIsAssociation;
            this.ChartSources = ChartSources;
            this.ClimateByBranches = ClimateByBranches;
            this.ClimateByFOTypes = ClimateByFOTypes;
            this.ClimateByAgeRanges = ClimateByAgeRanges;
            this.SatNotSat = SatNotSat;
            this.Ubication = Ubication;
            this.country_id = country_id;
            this.state_id = state_id;
            this.region_id = region_id;
            this.graphicIdPopulationGender = graphicId;
            this.optionsCount = new OptionsServices().GetOptionsCount((Int32)Test.Questionnaire_Id);
            this.optionsByTest = Test.ResultBasedOn100 ? 1 : Test.GetOptionsByTest().Count();
        }

        public string GetColourByClimate(double avg)
        {
            //double pct = ((double)(avg * 100)) / Test.GetOptionsByTest().Count();//.Questionnaire.Options.Count;
            double pct = Test.ResultBasedOn100 ? avg : ((double)(avg * 100)) / optionsByTest;
            if (Test.ClimateScale_Id.HasValue)
            {
                ClimateRange range = Test.ClimateScale.ClimateRanges.Where(r => r.MinValue <= (decimal)pct && r.MaxValue >= (decimal)pct).OrderBy(r => r.MaxValue).FirstOrDefault();
                return range.Color;
            }
            else
            {
                if (pct <= 60)
                    return "#FF004C";
                else if (pct > 60 && pct <= 80)
                    return "#FECE00";
                else if (pct > 80)
                    return "#00B386";
                else
                    return "";
            }
        }

    }
}
    
