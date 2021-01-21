using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(FeatureMetadata))]
    public partial class Feature
    {
        [Bind(Exclude = "Id")]
        public class FeatureMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Feature), ErrorMessageResourceName = "FeedbackTypeRequired")]
            [Display(Name = "FeedbackType", ResourceType = typeof(ViewRes.Models.Feature))]
            public int FeedbackType_Id { get; set; }
        }
    }
}