using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class QuestionnaireViewModel
    {
        public Questionnaire questionnaire { get; private set; }
        public SelectList templatesList { get; private set; }
        public ListQuestionnaire Templates { get; private set; }
        public string role { get; private set; }

        public QuestionnaireViewModel(Questionnaire questionnaire , SelectList templatesList, string role)
        {
            this.questionnaire = questionnaire;
            this.templatesList = templatesList;
            this.Templates = new ListQuestionnaire();
            this.role = role;
        }

        public QuestionnaireViewModel(Questionnaire questionnaire)
        {
            this.questionnaire = questionnaire;
        }

    }
}
