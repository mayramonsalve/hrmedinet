using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(LoadExcelMetadata))]
    public partial class LoadExcel
    {
        [Bind]
        public class LoadExcelMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "postedFileRequired")]
            [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx|.csv)$", ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "postedFileRequired")]
            [Display(Name = "postedFile", ResourceType = typeof(ViewRes.Models.Shared))]
            public HttpPostedFileBase postedFile { get; set; }
        }
    }
}
