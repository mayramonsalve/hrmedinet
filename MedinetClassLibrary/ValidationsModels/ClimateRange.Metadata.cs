using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(ClimateRangeMetadata))]
    public partial class ClimateRange
    {
        [Bind(Exclude = "Id")]
        public class ClimateRangeMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.ClimateRange), ErrorMessageResourceName = "MinValueRequired")]
            [RegularExpression(@"^[0-9]{0,2}(\.[0-9]{1,2})?$|^-?(100)(\.[0]{1,2})?$", ErrorMessageResourceType = typeof(ViewRes.Models.ClimateRange), ErrorMessageResourceName = "MinValueInvalid")]
            [Display(Name = "MinValue", ResourceType = typeof(ViewRes.Models.ClimateRange))]
            public decimal MinValue { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.ClimateRange), ErrorMessageResourceName = "MaxValueRequired")]
            [RegularExpression(@"^[0-9]{0,2}(\.[0-9]{1,2})?$|^-?(100)(\.[0]{1,2})?$", ErrorMessageResourceType = typeof(ViewRes.Models.ClimateRange), ErrorMessageResourceName = "MaxValueInvalid")]
            [Display(Name = "MaxValue", ResourceType = typeof(ViewRes.Models.ClimateRange))]
            public decimal MaxValue { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.ClimateRange), ErrorMessageResourceName = "ColorRequired")]
            [Display(Name = "Color", ResourceType = typeof(ViewRes.Models.ClimateRange))]
            public string Color { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.ClimateRange), ErrorMessageResourceName = "ActionRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.ClimateRange), ErrorMessageResourceName = "ActionLength")]
            [Display(Name = "Action", ResourceType = typeof(ViewRes.Models.ClimateRange))]
            public string Action { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.ClimateRange), ErrorMessageResourceName = "ClimateScaleRequired")]          
            [Display(Name = "ClimateScale", ResourceType = typeof(ViewRes.Models.ClimateRange))]
            public int ClimateScale_Id { get; set; }

        }
    }
}
