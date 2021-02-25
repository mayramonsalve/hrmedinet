using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MedinetClassLibrary.Models;

namespace Medinet.Models.ViewModels
{
    public class LoadExcelViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "postedFileRequired")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx|.csv)$", ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "postedFileRequired")]
        [Display(Name = "postedFile", ResourceType = typeof(ViewRes.Models.Shared))]
        public HttpPostedFileBase postedFile { get; set; }

    }
}