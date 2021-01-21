using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(ListQuestionnaireMetadata))]
    public class ListQuestionnaire
    {
        public string Templates { get; private set; }

        public ListQuestionnaire()
        {
            this.Templates = "";
        }

        public ListQuestionnaire(string templates)
        {
            this.Templates = templates;
        }

        [Bind(Exclude = "Id")]
        public class ListQuestionnaireMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Questionnaire), ErrorMessageResourceName = "TemplatesRequired")]
            [Display(Name = "Template", ResourceType = typeof(ViewRes.Models.Questionnaire))]
            public string Templates { get; set; }
        }
    }
}
