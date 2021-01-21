using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(FunctionalOrganizationTypeMetadata))]
    public partial class FunctionalOrganizationType
    {
        [Bind(Exclude = "Id")]
        public class FunctionalOrganizationTypeMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "ShortNameRequired")]
            [StringLength(10, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "ShortNameLength")]
            [Display(Name = "ShortName", ResourceType = typeof(ViewRes.Models.Shared))]
            public string ShortName { get; set; }

            [Display(Name = "FOTParent", ResourceType = typeof(ViewRes.Models.FOrganization))]
            public int FOTParent_Id { get; set; }
        }
    }
}