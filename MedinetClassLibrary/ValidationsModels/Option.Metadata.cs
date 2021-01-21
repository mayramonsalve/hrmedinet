using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(OptionMetadata))]
    public partial class Option
    {
        [Bind(Exclude = "Id")]
        public class OptionMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Option), ErrorMessageResourceName = "ValueRequired")]
            [RegularExpression(@"^[1-9]+[0-9]*$", ErrorMessageResourceType = typeof(ViewRes.Models.Option), ErrorMessageResourceName = "ValueInvalid")]  
            [Display(Name = "Value", ResourceType = typeof(ViewRes.Models.Option))]
            public int Value { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Option), ErrorMessageResourceName = "TextRequired")]
            [StringLength(200, ErrorMessageResourceType = typeof(ViewRes.Models.Option), ErrorMessageResourceName = "TextLength")]
            [Display(Name = "Text", ResourceType = typeof(ViewRes.Models.Option))]
            public string Text { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Option), ErrorMessageResourceName = "QuestionnaireRequired")]          
            [Display(Name = "Questionnaire", ResourceType = typeof(ViewRes.Models.Option))]
            public int Questionnaire_Id { get; set; }

            [Display(Name = "Image", ResourceType = typeof(ViewRes.Models.User))]
            public string Image { get; set; }
        }
    }
}
