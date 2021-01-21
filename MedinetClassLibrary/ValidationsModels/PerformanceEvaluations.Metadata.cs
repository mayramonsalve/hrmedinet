using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{

    [MetadataType(typeof(PerformanceEvaluationMetadata))]
    public partial class PerformanceEvaluation
    {
        [Bind(Exclude = "Id")]
        public class PerformanceEvaluationMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(30, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength30")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "ShortNameRequired")]
            [StringLength(10, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "ShortNameLength")]
            [Display(Name = "ShortName", ResourceType = typeof(ViewRes.Models.Shared))]
            public string ShortName { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.PositionLevel), ErrorMessageResourceName = "LevelRequired")]
            [RegularExpression(@"^[1-9]+[0-9]*$", ErrorMessageResourceType = typeof(ViewRes.Models.PositionLevel), ErrorMessageResourceName = "LevelInvalid")]
            [Display(Name = "Level", ResourceType = typeof(ViewRes.Models.PositionLevel))]
            public int Level { get; set; }
        }
    }
}
