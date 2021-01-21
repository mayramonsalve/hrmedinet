using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MedinetClassLibrary.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }

        [Bind(Exclude = "Id")]
        public class UserMetadata
        {
            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "UserNameRequired")]
            [StringLength(15, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "UserNameLength")]
            [Display(Name = "UserName", ResourceType = typeof(ViewRes.Models.User))]
            public string UserName { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "PasswordRequired")]
            [StringLength(15, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "PasswordLength")]
            [Display(Name = "Password", ResourceType = typeof(ViewRes.Models.User))]
            public string Password { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "ConfirmPasswordRequired")]
            [StringLength(15, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "ConfirmPasswordLength")]
            [Display(Name = "ConfirmPassword", ResourceType = typeof(ViewRes.Models.User))]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "FirstNameRequired")]
            [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "FirstNameLength")]
            [Display(Name = "FirstName", ResourceType = typeof(ViewRes.Models.User))]
            public string FirstName { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "LastNameRequired")]
            [StringLength(100, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "LastNameLength")]
            [Display(Name = "LastName", ResourceType = typeof(ViewRes.Models.User))]
            public string LastName { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "EmailRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "EmailLength")]
            [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "EmailInvalid")]
            //[DataType(DataType.EmailAddress)]
            [Display(Name = "Email", ResourceType = typeof(ViewRes.Models.User))]
            public string Email { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "ContactPhoneRequired")]
            [StringLength(20, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "ContactPhoneLength")]
            //[InternationalPhone]
            [RegularExpression(@"^(\+?\-? *[0-9]+)([0-9 ])*$", ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "ContactPhoneInvalid")]
            [Display(Name = "ContactPhone", ResourceType = typeof(ViewRes.Models.User))]
            public string ContactPhone { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "AddressRequired")]
            [StringLength(500, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "AddressLength")]
            [Display(Name = "Address", ResourceType = typeof(ViewRes.Models.User))]
            public string Address { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "IdNumberRequired")]
            [StringLength(50, ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "IdNumberLength")]
            [Display(Name = "IdNumber", ResourceType = typeof(ViewRes.Models.User))]
            public string IdNumber { get; set; }

            [Required(ErrorMessageResourceType = typeof(ViewRes.Models.User), ErrorMessageResourceName = "RoleRequired")]
            [Display(Name = "Role", ResourceType = typeof(ViewRes.Models.User))]
            public int Role_Id { get; set; }

            [Display(Name = "Company", ResourceType = typeof(ViewRes.Models.Shared))]
            public int Company_Id { get; set; }

            [Display(Name = "Location", ResourceType = typeof(ViewRes.Models.User))]
            public int Location_Id { get; set; }

            [Display(Name = "Image", ResourceType = typeof(ViewRes.Models.User))]
            public string Image { get; set; }

        }
    }
}
