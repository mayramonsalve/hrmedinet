using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MedinetClassLibrary.Services;

namespace MedinetClassLibrary.Models
{
   public class DemographicsTables
    {
       public Test test;

       public DemographicsTables() {
           test = new Test();
       }

       public DemographicsTables(int test_id) {
           test = new TestsServices().GetById(test_id);
           
       }

       public Dictionary<string,object> SelectDemographicTable(string demographic) {
           switch (demographic)
           {
               case "AgeRange":
                   return this.GetAgeRangeTable(demographic);
               case "PositionLevel":
                   return this.GetPositionLevelTable(demographic);
               case "Seniority":
                   return this.GetSeniorityTable(demographic);
               case "Country":
                   return this.GetCountryTable(demographic);
               case "Gender":
                   return this.GetGenderTable(demographic);
               case "InstructionLevel":
                   return this.GetInstructionLevelTable(demographic);
               case "Performance":
                   return this.GetPerformanceTable(demographic);
               case "Region":
                   return this.GetRegionTable(demographic);
               case "Location":
                   return this.GetLocationTable(demographic);
               case "State":
                   return this.GetStateTable(demographic);
               default:
                   return this.GetAgeRangeTable(demographic);
           }
       }

       public Dictionary<string, object> GetAgeRangeTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByAgeRanges(null, null, null, null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByAgeRanges(null, null, null, false), dictionaryChi);
       }

       public Dictionary<string, object> GetSeniorityTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatBySeniorities(null, null, null, null, null, null);
           return ConcatDictionary(test.GetAvgAndMedBySeniorities(null, null, null, false), dictionaryChi);
       }

       public Dictionary<string, object> GetRegionTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByRegions(null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByRegions(null, null, null, false), dictionaryChi);
       }

       public Dictionary<string, object> GetCountryTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByCountries(null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByCountries(null, null, null, false), dictionaryChi);
       }

       public Dictionary<string, object> GetGenderTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByGender(null, null, null, null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByGender(null, null, null, false), dictionaryChi);
       }

       public Dictionary<string, object> GetInstructionLevelTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByInstructionLevels(null, null, null, null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByInstructionLevels(null, null, null, false), dictionaryChi);
       }

       public Dictionary<string, object> GetLocationTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByLocations(null, null, null, null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByLocations(null, null, null, false, null, null, null), dictionaryChi);
       }

       public Dictionary<string, object> GetPerformanceTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByPerformanceEvaluations(null, null, null, null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByPerformanceEvaluations(null, null, null, false), dictionaryChi);
       }

       public Dictionary<string, object> GetPositionLevelTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByPositionLevels(null, null, null, null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByPositionLevels(null, null, null, false), dictionaryChi);
       }


       public Dictionary<string, object> GetStateTable(string demographic)
       {
           Dictionary<string, object> dictionaryChi = new ChiSquare(test, demographic, null, null, null, 0.05, null, null, null).GetSatAndNoSatByStates(null, null, null);
           return ConcatDictionary(test.GetAvgAndMedByStates(null, null, null), dictionaryChi);
       }

       private Dictionary<string, object> ConcatDictionary(Dictionary<string, object> dictionary, Dictionary<string, object> dictionaryChi)
       {
           dictionary.Add("Satisfied", dictionaryChi["Satisfied"]);
           dictionary.Add("NoSatisfied", dictionaryChi["NoSatisfied"]);
           return dictionary;
       }

    }
}
