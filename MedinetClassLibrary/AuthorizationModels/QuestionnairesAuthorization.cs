using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using MedinetClassLibrary.Services;

namespace MedinetClassLibrary.AuthorizationModels
{
    public class QuestionnairesAuthorization
    {
        private User userLogged;
        private Company company; // compañia de la persona q creó el questionnaire

        public QuestionnairesAuthorization(User userLogged, Questionnaire questionnaire)
        {
            this.userLogged = userLogged;
            this.company = new CompaniesServices().GetById(new UsersServices().GetById(questionnaire.User_Id).Company_Id);
        }

        public bool isAuthorizated()
        {
            return company.Id == userLogged.Company_Id; // usuario de la misma compañia q creó el questionnaire
        }

    }
}
