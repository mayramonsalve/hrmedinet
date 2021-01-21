using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category
    {
        [Bind(Exclude = "Id")]
        public class CategoryMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Category), ErrorMessageResourceName = "DescriptionRequired")]
            [Display(Name = "Description", ResourceType = typeof(ViewRes.Models.Category))]
            public string Description { get; set; }

            //[Required(ErrorMessageResourceType = typeof(ViewRes.Models.Category), ErrorMessageResourceName = "QuestionnaireRequired")]
            [Display(Name = "Questionnaire", ResourceType = typeof(ViewRes.Models.Category))]
            public int Questionnaire_Id { get; set; }

            [Display(Name = "GroupingCategory", ResourceType = typeof(ViewRes.Models.Category))]
            public int CategoryGroup_Id { get; set; }

            [Display(Name = "Company", ResourceType = typeof(ViewRes.Models.Shared))]
            public int Company_Id { get; set; }

        }
    }
}
