using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(QuestionnaireMetadata))]
    public partial class Questionnaire
    {

        [Bind(Exclude = "Id")]
        public class QuestionnaireMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Questionnaire), ErrorMessageResourceName = "InstructionsRequired")]
            [Display(Name = "Instructions", ResourceType = typeof(ViewRes.Models.Questionnaire))]
            public string Instructions { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Questionnaire), ErrorMessageResourceName = "DescriptionRequired")]
            [Display(Name = "Description", ResourceType = typeof(ViewRes.Models.Questionnaire))]
            public string Description { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Questionnaire), ErrorMessageResourceName = "TemplateRequired")]
            [Display(Name = "Template", ResourceType = typeof(ViewRes.Models.Questionnaire))]
            public string Template { get; set; }
        }
    }
}
