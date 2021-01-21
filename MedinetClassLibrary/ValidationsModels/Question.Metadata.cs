using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(QuestionMetadata))]
    public partial class Question
    {
        [Bind(Exclude = "Id")]
        public class QuestionMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Question), ErrorMessageResourceName = "TextRequired")]
            [Display(Name = "Text", ResourceType = typeof(ViewRes.Models.Question))]
            public string Text { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Question), ErrorMessageResourceName = "QuestionTypeRequired")]
            [Display(Name = "QuestionType", ResourceType = typeof(ViewRes.Models.Question))]
            public int? QuestionType_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Question), ErrorMessageResourceName = "CategoryRequired")]
            [Display(Name = "Category", ResourceType = typeof(ViewRes.Models.Question))]
            public int? Category_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Question), ErrorMessageResourceName = "SortOrderRequired")]
            [RegularExpression(@"^[1-9]+[0-9]*$", ErrorMessageResourceType = typeof(ViewRes.Models.Question), ErrorMessageResourceName = "SortOrderInvalid")]
            [Display(Name = "SortOrder", ResourceType = typeof(ViewRes.Models.Question))]
            public int? SortOrder { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Question), ErrorMessageResourceName = "PositiveRequired")]
            [Display(Name = "Positive", ResourceType = typeof(ViewRes.Models.Question))]
            public bool Positive { get; set; }
        }
    }
}
