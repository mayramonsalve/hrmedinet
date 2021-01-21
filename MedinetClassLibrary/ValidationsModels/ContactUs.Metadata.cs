using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(ContactUsMetadata))]
    public partial class ContactUs
    {
        [Bind(Exclude = "Id")]
        public class ContactUsMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "NameLength")]
            [Display(Name = "Name", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Name { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "CompanyRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.Shared), ErrorMessageResourceName = "CompanyLength")]
            [Display(Name = "Company", ResourceType = typeof(ViewRes.Models.Shared))]
            public string Company { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "EmailRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "EmailLength")]
            [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "EmailInvalid")]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email", ResourceType = typeof(ViewRes.Models.User))]
            public string Email { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.ContactUs), ErrorMessageResourceName = "PhoneRequired")]
            [StringLength(20, ErrorMessageResourceType = typeof(ViewRes.Models.ContactUs), ErrorMessageResourceName = "PhoneLength")]
            [InternationalPhone]
            //[DataType(DataType.PhoneNumber, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "ContactInvalid")]
            [Display(Name = "Phone", ResourceType = typeof(ViewRes.Models.ContactUs))]
            public string Phone { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.ContactUs), ErrorMessageResourceName = "AddressRequired")]
            [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.Models.ContactUs), ErrorMessageResourceName = "AddressLength")]
            [Display(Name = "Address", ResourceType = typeof(ViewRes.Models.ContactUs))]
            public string Address { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.ContactUs), ErrorMessageResourceName = "DescriptionRequired")]
            [Display(Name = "Description", ResourceType = typeof(ViewRes.Models.ContactUs))]
            public string Description { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.State), ErrorMessageResourceName = "CountryRequired")]
            [Display(Name = "Country", ResourceType = typeof(ViewRes.Models.State))]
            public int Country_Id { get; set; }

            [Display(Name = "Date", ResourceType = typeof(ViewRes.Models.ContactUs))]
            public string Date { get; set; }
        }
    }
}