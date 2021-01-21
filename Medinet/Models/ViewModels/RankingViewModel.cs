using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using MedinetClassLibrary.Services;

namespace Medinet.Models.ViewModels
{
    public class RankingViewModel {
        public Dictionary<string, double> ranking { get; private set; }
        public Dictionary<string, object> rankingPrint { get; private set; }
        public Dictionary<string, int> demographicsCount { get; private set; }
        public Dictionary<int, string> FO { get; private set; }
        public Test test { get; private set; }
        public User UserLogged { get; private set; }
        public bool print { get; private set; }
        public int resultType { get; private set; }
        public int questionnaireId { get; private set; }
        public int companyId { get; private set; }
        public int foId { get; private set; }
        public SelectList sectorsList { get; private set; }
        public SelectList questionnairesList { get; private set; }
        public string sector { get; private set; }
        public string questionnaire { get; private set; }
        public string country { get; private set; }
        public string company { get; private set; }
        public string title { get; private set; }
        public string nameTH { get; private set; }
        public SelectList countriesList { get; private set; }
        public SelectList companiesList { get; private set; }

        public RankingViewModel(Test test, bool print, int type)
        {
            this.test = test;
            this.print = print;
            this.resultType = type;
        }

        public RankingViewModel(int questionnaire, SelectList sectorsList,
                                SelectList companiesList, SelectList countriesList,
                                Dictionary<string,int> demographicsCount, Dictionary<int,string> FO,
                                User UserLogged, Dictionary<string, double> ranking, int company, int foId)
        {
            this.questionnaireId = questionnaire;
            this.sectorsList = sectorsList;
            this.companiesList = companiesList;
            this.countriesList = countriesList;
            this.demographicsCount = demographicsCount;
            this.FO = FO;
            this.UserLogged = UserLogged;
            this.ranking = ranking;
            this.companyId = company;
            this.foId = foId;
        }

        public int GetFOCount(int type)
        {
            return new FunctionalOrganizationsServices().GetByType(type).Count();
        }

        #region print
        public RankingViewModel(string questionnaire, string sector, string country, string company, string title, string nameTH,
                                Dictionary<string, object> ranking, User UserLogged)
        {
            this.questionnaire = questionnaire;
            this.sector = sector;
            this.country = country;
            this.company = company;
            this.title = title;
            this.rankingPrint = ranking;
            this.nameTH = nameTH;
            this.UserLogged = UserLogged;
        }
        #endregion

        private Dictionary<string, string> GetEmptyDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string,string>();
            dictionary.Add("","");
            return dictionary;
        }

    }
}
    
