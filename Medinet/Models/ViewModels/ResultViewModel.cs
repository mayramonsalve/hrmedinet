using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using MedinetClassLibrary.Services;

namespace Medinet.Models.ViewModels
{
    public class ResultViewModel {
        public Dictionary<string,double> stringDouble { get; private set; }
        public Dictionary<string, double[]> stringDoubleVector { get; private set; }
        public Dictionary<string, object> stringObject { get; private set; }
        public Dictionary<Company, double> companyDouble{ get; private set; }
        public Test test { get; private set; }
        public User UserLogged { get; private set; }
        public int optionsCount { get; private set; }
        public bool print { get; private set; }
        public int resultType { get; private set; }
        public SelectList sectorsList { get; private set; }
        public SelectList questionnairesList { get; private set; }
        public string sector { get; private set; }
        public string questionnaire { get; private set; }
        public int questionnaireToUse { get; private set; }
        public List<Questionnaire> questionnairesInTest { get; private set; }

        public ResultViewModel(Test test, bool print, int type)
        {
            this.test = test;
            this.print = print;
            this.resultType = type;
        }

        //Report1
        public ResultViewModel(Dictionary<string, double> result, Test test, int? questionnaire, bool print, int type)
        {
            this.stringDouble= result;
            this.test = test;
            this.print = print;
            this.resultType = type;
            if (this.test.OneQuestionnaire)
                this.questionnaireToUse = this.test.Questionnaire.Id;
            else
            {
                this.questionnaireToUse = questionnaire.HasValue ? questionnaire.Value : 0;
                this.questionnairesInTest = this.test.GetQuestionnairesByTest();
            }
        }

        //Report2 & Report5
        public ResultViewModel(Dictionary<string, double[]> result, Test test, int? questionnaire, bool print, int type)
        {
            this.stringDoubleVector = result;
            this.test = test;
            this.print = print;
            this.resultType = type;
            this.optionsCount = questionnaire.HasValue ? new QuestionnairesServices().GetById(questionnaire.Value).Options.Count
                                : test.GetOptionsByTest().Count();
            if (this.test.OneQuestionnaire)
                this.questionnaireToUse = this.test.Questionnaire.Id;
            else
            {
                this.questionnaireToUse = questionnaire.HasValue ? questionnaire.Value : 0;
                this.questionnairesInTest = this.test.GetQuestionnairesByTest();
            }
        }
        
        //Report3 & Report4 & Report6
        public ResultViewModel(Dictionary<string, object> result, Test test, int? questionnaire, bool print, int type)
        {
            this.stringObject = result;
            this.test = test;
            this.print = print;
            this.resultType = type;
            this.optionsCount = questionnaire.HasValue ? new QuestionnairesServices().GetById(questionnaire.Value).Options.Count
                                : test.GetOptionsByTest().Count();
            if (this.test.OneQuestionnaire)
            {
                this.questionnaireToUse = this.test.Questionnaire.Id;
                this.optionsCount = test.Questionnaire.Options.Count;
            }
            else
            {
                this.questionnairesInTest = this.test.GetQuestionnairesByTest();
                this.questionnaireToUse = questionnaire.HasValue ? questionnaire.Value :
                    (type == 3 || type == 4 ? questionnairesInTest.First().Id : 0);
            }
        }



    }
}
    
