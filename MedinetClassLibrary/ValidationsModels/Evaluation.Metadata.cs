using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(EvaluationMetadata))]
    public partial class Evaluation
    {
        [Bind(Exclude = "Id")]
        public class EvaluationMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Evaluation), ErrorMessageResourceName = "SexRequired")]
            [StringLength(10, ErrorMessageResourceType = typeof(ViewRes.Models.Evaluation), ErrorMessageResourceName = "SexLength")]
            [Display(Name = "Sex", ResourceType = typeof(ViewRes.Models.Evaluation))]
            public string Sex { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Evaluation), ErrorMessageResourceName = "AgeRangeRequired")]
            [Display(Name = "AgeRange", ResourceType = typeof(ViewRes.Models.Evaluation))]
            public int Age_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Evaluation), ErrorMessageResourceName = "InstructionLevelRequired")]
            [Display(Name = "InstructionLevel", ResourceType = typeof(ViewRes.Models.Evaluation))]
            public int InstructionLevel_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Evaluation), ErrorMessageResourceName = "PositionLevelRequired")]
            [Display(Name = "PositionLevel", ResourceType = typeof(ViewRes.Models.Evaluation))]
            public int PositionLevel_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Evaluation), ErrorMessageResourceName = "SeniorityRangeRequired")]
            [Display(Name = "SeniorityRange", ResourceType = typeof(ViewRes.Models.Evaluation))]
            public int Seniority_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Evaluation), ErrorMessageResourceName = "LocationRequired")]
            [Display(Name = "Location", ResourceType = typeof(ViewRes.Models.Evaluation))]
            public int Location_Id { get; set; }

            //[Required(ErrorMessageResourceType = typeof(ViewRes.Models.Evaluation), ErrorMessageResourceName = "PerformanceEvaluationRequired")]
            [Display(Name = "PerformanceEvaluation", ResourceType = typeof(ViewRes.Models.Evaluation))]
            public int Performance_Id { get; set; }
        }

    }
}
