using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(PositionMetadata))]
    public partial class Position
    {
        [Bind(Exclude = "Id")]
        public class PositionMetadata
        {
            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [DisplayName("Chief")]
            public int Chief_Id { get; set; }

            [Required]
            [StringLength(20)]
            public string ShortName { get; set; }
        }
    }
}
