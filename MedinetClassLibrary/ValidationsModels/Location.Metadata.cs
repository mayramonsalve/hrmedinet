using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location
    {
        [Bind(Exclude = "Id")]
        public class LocationMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength100")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "ShortNameRequired")]
            [StringLength(10, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "ShortNameLength")]
            [Display(Name = "ShortName", ResourceType = typeof(ViewRes.Models.Shared))]
            public string ShortName { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Location), ErrorMessageResourceName = "StateRequired")]
            [Display(Name = "State", ResourceType = typeof(ViewRes.Models.Location))]
            public int State_Id { get; set; }

            //[Required(ErrorMessageResourceType = typeof(ViewRes.Models.Location), ErrorMessageResourceName = "RegionRequired")]
            [Display(Name = "Region", ResourceType = typeof(ViewRes.Models.Location))]
            public int Region_Id { get; set; }

        }
    }
}
