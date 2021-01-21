using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using System.Web;

namespace Medinet.Models.ViewModels
{
    public class EvaluationViewModel
    {
        public Evaluation evaluation { get; private set; }
        public SelectList agesList { get; private set; }
        public SelectList senioritiesList { get; private set; }
        public SelectList instructionLevelsList { get; private set; }
        public SelectList positionLevelsList { get; private set; }
        public SelectList locationsList { get; private set; }
        public IQueryable<FunctionalOrganizationType> FOTypes { get; private set; }
        public SelectList FOrganizations { get; private set; }
        public SelectList performanceEvaluationsList { get; private set; }
        public Test test { get; private set; }
        public int progress { get; private set; }
        public Dictionary<int, string> FO { get; private set; }

        public int IdQuestionnaireToUse { get; private set; }
        public Questionnaire QuestionnaireToUse { get; private set; }
        public int IdSelectorValue { get; set; }

        public Evaluation previousEvaluation { get; private set; }

        public List<Questionnaire> listOfQuestionnaires { get; private set; }
        public Dictionary<int, int> DicOfQuestionnairesAndSelectorValues { get; private set; }

        public int[][] Selected_FO { get; private set; }

        public Boolean conferencia;

        public EvaluationViewModel(Test test, Evaluation evaluation, int[][] Selected_FO, int IdQuestionnaireToUse, Questionnaire QuestionnaireToUse)
        {
            agesList = null;
            senioritiesList = null;
            instructionLevelsList = null;
            positionLevelsList = null;
            locationsList = null;
            positionLevelsList = null;
            FOTypes = null;
            FOrganizations = null;
            performanceEvaluationsList = null;
            progress = 0;
            FO = null;
            previousEvaluation = null;
            IdSelectorValue = 0;
            listOfQuestionnaires = null;
            DicOfQuestionnairesAndSelectorValues = null;
            this.IdQuestionnaireToUse = IdQuestionnaireToUse;
            this.QuestionnaireToUse = QuestionnaireToUse;
            this.test = test;
            this.evaluation = evaluation;
            this.Selected_FO = Selected_FO;
        }
        public EvaluationViewModel(Evaluation previousEvaluation)
        {
            this.previousEvaluation = previousEvaluation;
        }

        public EvaluationViewModel(Test test, int IdSelectorValue)
        {
            this.test = test;
            this.IdSelectorValue = IdSelectorValue;
            this.IdQuestionnaireToUse = (test.OneQuestionnaire) ? test.Questionnaire_Id.Value : GetQuestionnaireIdByRequest();
            this.QuestionnaireToUse = new QuestionnairesServices().GetById(IdQuestionnaireToUse);
            this.progress = GetProgress();
        }

        public EvaluationViewModel(Evaluation evaluation, SelectList agesList, SelectList senioritiesList,
                                    SelectList instructionLevelsList, SelectList positionLevelsList,SelectList locationsList,
                                    IQueryable<FunctionalOrganizationType> FOTypes, SelectList FOrganizaions,
                                    SelectList performanceEvaluationsList, Test test, Evaluation previousEvaluation)
        {
            this.evaluation = evaluation;
            this.locationsList = locationsList;
            this.agesList = agesList;
            this.senioritiesList = senioritiesList;
            this.instructionLevelsList = instructionLevelsList;
            this.positionLevelsList = positionLevelsList;
            this.test = test;
            this.FOTypes = FOTypes;
            this.FOrganizations = FOrganizations;
            this.performanceEvaluationsList = performanceEvaluationsList;
            this.FO = new FunctionalOrganizationTypesServices().GetFunctionalOrganizationTypesForDropDownList(test.Company_Id);
            this.FO = new FunctionalOrganizationTypesServices().OrderDictionaryByCompany(this.FO);
            this.IdQuestionnaireToUse = test.Questionnaire_Id.HasValue ? test.Questionnaire_Id.Value :
                                        ((test.PreviousTest_Id.HasValue) ? ((test.OneQuestionnaire) ?
                                        test.Questionnaire_Id.Value : GetQuestionnaireIdByRequest()) : 0);
            this.QuestionnaireToUse = (IdQuestionnaireToUse != 0) ?
                                        new QuestionnairesServices().GetById(IdQuestionnaireToUse) : new Questionnaire();
            this.progress = (IdQuestionnaireToUse != 0) ? GetProgress() : 100;
            this.previousEvaluation = previousEvaluation;
            GetDicOfQuestionnairesAndSelectorValuesByTest();
            Selected_FO = new int[2][];
        }

        public EvaluationViewModel(Test test)
        {
            this.test = test;
        }

        private void GetDicOfQuestionnairesAndSelectorValuesByTest()
        {
            if (test.OneQuestionnaire)
            {
                this.DicOfQuestionnairesAndSelectorValues = new Dictionary<int, int>(); ;
            }
            else
            {
                this.DicOfQuestionnairesAndSelectorValues = test.GetDicOfQuestionnairesAndSelectorValuesByTest();
            }
        }

        public int GetProgress()
        {
            int result;
            int p = Math.DivRem(GetQuestionsCountByQuestionnaire(), test.RecordsPerPage, out result);
            if (result > 0)
                p++;
            return Math.DivRem(100, ++p, out result);
        }

        private int GetQuestionsCountByQuestionnaire()
        {
            //int questionnaire = (test.OneQuestionnaire) ? test.Questionnaire_Id.Value : GetQuestionnaireIdByRequest();
            return new QuestionsServices().GetQuestionsCountByQuestionnaire(QuestionnaireToUse.Id);
            //int q=0;
            //foreach (Category cat in QuestionnaireToUse.Categories)
            //{
            //    q = q + cat.Questions.Count;
            //}
            //return q;
        }

        public List<object[]> GetQuestionsByCategory(int category_id)
        {
            return new QuestionsServices().GetQuestionsInfoByCategory(category_id);//.GetQuestions(category_id);
        }

        public IQueryable<Option> GetOptions(int? question_id)
        {
            //int questionnaire = (test.OneQuestionnaire) ? test.Questionnaire_Id.Value : GetQuestionnaireIdByRequest();
            return question_id.HasValue ? new QuestionsServices().GetById(question_id.Value).Options.AsQueryable() : new OptionsServices().GetByQuestionnaire(IdQuestionnaireToUse);
        }

        public IQueryable<Option> GetNegativeOptions()
        {
            IQueryable<Option> negative_options = GetOptions(null);
            var negative_images = negative_options.Select(o => o.Image).ToArray();
            int ni = negative_images.Length-1;
            foreach(Option neg_opt in negative_options)
            {
                neg_opt.Image = negative_images[ni--];
            }
            return negative_options;
        }

        private int GetQuestionnaireIdByRequest()
        {
            DemographicsInTest demographicInTest = new DemographicsInTestsServices().GetSelector(test.Id);
            return new DemographicSelectorDetailsServices().GetQuestionnaireIdByDemographicSelectorDetailValues(test.Id, demographicInTest.Demographic_Id, IdSelectorValue);
        }

        private int GetSelectorId(Evaluation evaluation, Test test, DemographicsInTest demographicInTest)
        {
            switch (demographicInTest.Demographic.Name)
            {
                case "AgeRange":
                    return evaluation.Age_Id.Value;
                case "InstructionLevel":
                    return evaluation.InstructionLevel_Id.Value;
                case "Location":
                    return evaluation.Location_Id.Value;
                case "Performance":
                    return evaluation.Performance_Id.Value;
                case "PositionLevel":
                    return evaluation.PositionLevel_Id.Value;
                case "Seniority":
                    return evaluation.Seniority_Id.Value;
                case "Country":
                    return evaluation.Location.State.Country_Id;
                case "State":
                    return evaluation.Location.State_Id;
                case "Region":
                    return evaluation.Location.Region_Id.Value;
                case "FunctionalOrganizationType":
                    return evaluation.EvaluationFOs.Where(f => f.FunctionalOrganization.Type_Id == demographicInTest.FOT_Id.Value).FirstOrDefault().FunctionalOrganization_Id;
                default:
                    return 0;
            }
        }

        public List<object[]> GetQuestions(bool disorder)
        {
            //int questionnaire = (test.OneQuestionnaire) ? test.Questionnaire_Id.Value : GetQuestionnaireIdByRequest();
            List<object[]> _questions = new QuestionsServices().GetQuestionsInfoByQuestionnaire(QuestionnaireToUse.Id);
            //List<object[]> questions = _questions;//new Question[this.GetQuestionsCount()];
            //int pos=0;
            //foreach (var q in _questions) {
            //    questions[pos]= q ;
            //    pos++;
            //}
            if (disorder)
                _questions = disorderQuestions(_questions);
            return _questions;
       }

        private List<object[]> disorderQuestions(List<object[]> orderedQuest)
        {
            Random r = new Random();
            int q = orderedQuest.Count();
            List<object[]> disorderedQuest = new List<object[]>();
            bool[] used = new bool[q];
            for (int i=0; i < q; i++)
                used[i]=false;
            int index=0;
            int times;
            int lastUsed = 0;
            for (int i=0; i < q; i++){
                times = 0;
                do
                {
                    index = (r.Next(q - 1));
                    if (times == 3)
                        lastUsed = index = searchNotUsed(used, lastUsed);
                    times++;
                } while (used[index]);
                disorderedQuest.Add(orderedQuest[index]);
                used[index]=true;
            }
            return disorderedQuest;
        }

        private int searchNotUsed(bool[] used, int lastUsed)
        {
            int notUsed = lastUsed;
            while (used[notUsed])
            {
                notUsed++;
            }
            return notUsed;
        }

        public List<object[]> GetCategories()
        {
            //int questionnaire = (this.test.OneQuestionnaire) ? this.test.Questionnaire_Id.Value : GetQuestionnaireIdByRequest();
            return new CategoriesServices().GetCategoriesInfo(QuestionnaireToUse.Id);//.GetCategories(IdQuestionnaireToUse);
        }

        public List<object[]> GetQuestions(int category_id)
        {
            return new QuestionsServices().GetQuestionsInfoByCategory(category_id);               
        }

        public Dictionary<int, string> GetQuestionTypeByQuestion(int question)
        {
            return new QuestionsServices().GetById(question).QuestionTypeByQuestions.ToDictionary(k => k.QuestionType_Id, v => v.AuxiliarText);
        }

        public int GetQuestionsCount() {
            //int questionnaire = (test.OneQuestionnaire) ? test.Questionnaire_Id.Value : GetQuestionnaireIdByRequest();
            return new QuestionsServices().GetQuestionsCountByQuestionnaire(QuestionnaireToUse.Id);//.GetAllRecords().Where(o => o.Category.Questionnaire.Id == IdQuestionnaireToUse).Count();
        }

        public int GetCategoriesCount()
        {
            //int questionnaire = (this.test.Questionnaire_Id.HasValue) ? this.test.Questionnaire_Id.Value : GetQuestionnaireIdByRequest();
            return new CategoriesServices().GetCategoriesCount(IdQuestionnaireToUse);
        }

        public int GetQuestionsCountByCategory(int category_id)
        {
            return new QuestionsServices().GetQuestionsCount(category_id);
        }

        public IQueryable<Age> GetAges()
        {
            return new AgesServices().GetByCompany(this.test.Company_Id);
        }
        public IQueryable<Seniority> GetSeniorities()
        {
            return new SenioritiesServices().GetByCompany(this.test.Company_Id);
        }
        public IQueryable<InstructionLevel> GetInstructionLevels()
        {
            return new InstructionLevelsServices().GetByCompany(this.test.Company_Id);
            //return new EvaluationsServices().GetAllInstructionLevels();
        }
        public IQueryable<PositionLevel> GetPositionLevels()
        {
            return new PositionLevelsServices().GetByCompany(this.test.Company_Id);
            //return new EvaluationsServices().GetAllPositionLevels();
        }
        public IQueryable<Location> GetLocations()
        {
            return new LocationsServices().GetByCompany(this.test.Company_Id);
        }
        public IQueryable<FunctionalOrganizationType> GetFOTypes()
        {
            return new FunctionalOrganizationTypesServices().GetByCompany(this.test.Company_Id);
        }
        public SelectList GetFOrganizationsByType(int type_id)
        {
            return new SelectList(new FunctionalOrganizationsServices().GetFunctionalOrganizationForDropDownList(type_id), "Key", "Value") ;
        }
        public IQueryable<FunctionalOrganization> GetFOrganizations(int type_id)
        {
            return new FunctionalOrganizationsServices().GetByType(type_id);
        }
        public IQueryable<PerformanceEvaluation> GetPerformanceEvaluations()
        {
            return new PerformanceEvaluationsServices().GetByCompany(this.test.Company_Id);
        }
        public void SetEvaluation(Evaluation evaluation)
        {
            this.evaluation = evaluation;
        }

        public void SetSelectedFo(int[][] Selected_FO)
        {
            this.Selected_FO = Selected_FO;
        }
    }
}

