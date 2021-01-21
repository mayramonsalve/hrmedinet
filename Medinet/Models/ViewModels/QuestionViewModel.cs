using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;
using System.Collections;

namespace Medinet.Models.ViewModels
{
    public class QuestionViewModel
    {
        public Question question { get; private set; }
        public SelectList categoriesList { get; private set; }
        public SelectList questionnairesList { get; private set; }
        public SelectList questionsTypeList { get; private set; }

        public QuestionViewModel(Question question, SelectList categoriesList, SelectList questionnairesList, SelectList questionsTypeList )
        {
            this.question = question;
            this.categoriesList = categoriesList;
            this.questionnairesList = questionnairesList;
            this.questionsTypeList = questionsTypeList;
           
        }
    }
}
