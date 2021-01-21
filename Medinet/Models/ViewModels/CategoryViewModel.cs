using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetClassLibrary.Models;
using System.Web.Mvc;

namespace Medinet.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Category category { get; private set; }
        public SelectList questionnairesList { get; private set; }
        public SelectList categoriesList { get; private set; }
        public SelectList companiesList { get; private set; }
        public bool isGroupingCategory { get; private set; }
        public User UserLogged { get; private set; }

        public CategoryViewModel(Category category, SelectList questionnairesList, SelectList categoriesList, SelectList companiesList,
                                    bool isGroupingCategory, User UserLogged)
        {
            this.questionnairesList = questionnairesList;
            this.categoriesList = categoriesList;
            this.category = category;
            this.companiesList = companiesList;
            this.isGroupingCategory = isGroupingCategory;
            this.UserLogged = UserLogged;
        }
    }
}