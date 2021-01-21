using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(TestMetadata))]
    public partial class Test
    {
        [Bind(Exclude = "Id")]
        public class TestMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength100")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

           /* [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "StartDateRequired")]
            [Display(Name = "StartDate", ResourceType = typeof(ViewRes.Models.Test))]
            public DateTime StartDate { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "EndDateRequired")]
            [Display(Name = "EndDate", ResourceType = typeof(ViewRes.Models.Test))]
            public DateTime EndDate { get; set; }*/

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "EvaluationNumberRequired")]
            [RegularExpression(@"^[1-9]+[0-9]*$", ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "EvaluationNumberInvalid")]
            [Display(Name = "EvaluationNumber", ResourceType = typeof(ViewRes.Models.Test))]
            public int EvaluationNumber { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "TextRequired")]
            [Display(Name = "Text", ResourceType = typeof(ViewRes.Models.Test))]
            public string Text { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "OneQuestionnaireRequired")]
            [Display(Name = "OneQuestionnaire", ResourceType = typeof(ViewRes.Models.Test))]
            public bool OneQuestionnaire { get; set; }

            //[Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "QuestionnaireRequired")]
            [Display(Name = "Questionnaire", ResourceType = typeof(ViewRes.Models.Test))]
            public int Questionnaire_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "ClimateScaleRequired")]
            [Display(Name = "ClimateScale", ResourceType = typeof(ViewRes.Models.Test))]
            public int ClimateScale_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "GroupByCategoriesRequired")]
            [Display(Name = "GroupByCategories", ResourceType = typeof(ViewRes.Models.Test))]
            public bool GroupByCategories { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "RecordsPerPageRequired")]
            [RegularExpression(@"^[1-9]+[0-9]*$", ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "RecordsPerPageInvalid")]
            [Display(Name = "RecordsPerPage", ResourceType = typeof(ViewRes.Models.Test))]
            public int RecordsPerPage { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "CompanyRequired")]
            [Display(Name = "Company", ResourceType = typeof(ViewRes.Models.Shared))]
            public int Company_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "MinimumPeopleRequired")]
            [RegularExpression(@"^[1-9]+[0-9]*$", ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "MinimumPeopleInvalid")]
            [Display(Name = "MinimumPeople", ResourceType = typeof(ViewRes.Models.Test))]
            public int MinimumPeople { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "ResultBasedOn100Required")]
            [Display(Name = "ResultBasedOn100", ResourceType = typeof(ViewRes.Models.Test))]
            public bool ResultBasedOn100 { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "WeightedRequired")]
            [Display(Name = "Weighted", ResourceType = typeof(ViewRes.Models.Test))]
            public bool Weighted { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Test), ErrorMessageResourceName = "DisorderedRequired")]
            [Display(Name = "Disordered", ResourceType = typeof(ViewRes.Models.Test))]
            public bool Disordered { get; set; }

            [Display(Name = "ConfidenceLevel", ResourceType = typeof(ViewRes.Models.Test))]
            public bool ConfidenceLevel { get; set; }

            [Display(Name = "StandardError", ResourceType = typeof(ViewRes.Models.Test))]
            public bool StandardError { get; set; }

            [Display(Name = "NumberOfEmployees", ResourceType = typeof(ViewRes.Models.Test))]
            public int NumberOfEmployees { get; set; }

            [Display(Name = "PreviousTest", ResourceType = typeof(ViewRes.Models.Test))]
            public int PreviousTest_Id { get; set; }
         }
    }
}
