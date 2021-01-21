using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(RegionMetadata))]
    public partial class Region
    {

        [Bind(Exclude = "Id")]
        public class RegionMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength100")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "CompanyRequired")]
            [Display(Name = "Company", ResourceType = typeof(ViewRes.Models.Shared))]
            public int Company_Id { get; set; }
        }
    }
}
