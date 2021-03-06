﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(ClimateScaleMetadata))]
    public partial class ClimateScale
    {

        [Bind(Exclude = "Id")]
        public class ClimateScaleMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Questionnaire), ErrorMessageResourceName = "DescriptionRequired")]
            [Display(Name = "Description", ResourceType = typeof(ViewRes.Models.Questionnaire))]
            public string Description { get; set; }

        }
    }
}
