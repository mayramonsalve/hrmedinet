using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(CompanyMetadata))]
    public partial class Company
    {
        [Bind(Exclude = "Id")]
        public class CompanyMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength100")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "NumberRequired")]
            [StringLength(20, ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "NumberLength")]
            [Display(Name = "Number", ResourceType = typeof(ViewRes.Models.Company))]
            public string Number { get; set; }

            [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "UrlLength")]
            //[Url]
            [RegularExpression(@"^(www\.|http:\/\/|https:\/\/|http:\/\/www\.|https:\/\/www\.)*[a-z0-9]+\.[a-z]{2,6}(\.[a-z]{2,6})*$", ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "UrlInvalid")]
            public string Url { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "PhoneRequired")]
            [StringLength(20, ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "PhoneLength")]
            //[InternationalPhone]
            [RegularExpression(@"^(\+?\-? *[0-9]+)([0-9 ])*$", ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "PhoneInvalid")]
            [Display(Name = "Phone", ResourceType = typeof(ViewRes.Models.Company))]
            public string Phone { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "ContactRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "ContactLength")]
            [Display(Name = "Contact", ResourceType = typeof(ViewRes.Models.Company))]
            public string Contact { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "AddressRequired")]
            [StringLength(500, ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "AddressLength")]
            [Display(Name = "Address", ResourceType = typeof(ViewRes.Models.Company))]
            public string Address { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "CompanyTypeRequired")]
            [Display(Name = "CompanyType", ResourceType = typeof(ViewRes.Models.Company))]
            public int CompanyType_Id { get; set; }

            [Display(Name = "Image", ResourceType = typeof(ViewRes.Models.Company))]
            public string Image { get; set; }

            //[Required(ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "CompanySectorRequired")]
            [Display(Name = "CompanySector", ResourceType = typeof(ViewRes.Models.Company))]
            public int CompanySector_Id { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Company), ErrorMessageResourceName = "ShowClimateRequired")]
            [Display(Name = "ShowClimate", ResourceType = typeof(ViewRes.Models.Company))]
            public bool ShowClimate { get; set; }

        }

    }
}