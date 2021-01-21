using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedinetClassLibrary.Services;
using MedinetClassLibrary.Models;
using Medinet.Models.ViewModels;
using MedinetClassLibrary.AuthorizationModels;
using MedinetClassLibrary.Classes;

namespace Medinet.Controllers
{
    [HandleError]
    [Authorize(Roles = "Administrator, CompanyAppManager")]
    public class UsersController : Controller
    {
        private UsersServices _userService;
        private UserViewModel _userViewModel;

         public UsersController()
        {
            _userService = new UsersServices();
        }

         public UsersController(UsersServices _userService)
        {
            this._userService = _userService;
        }

         private bool GetAuthorization(User user)
         {
             return new UsersAuthorization(new UsersServices().GetByUserName(User.Identity.Name),
                 user.Role_Id, new CompaniesServices().GetById((int)user.Company_Id)).isAuthorizated(); 
         }

         public ActionResult Create()
         {
             InitializeViews(null);
             return View(_userViewModel);
         }

         [HttpPost]
         public ActionResult Create(User user)
         {
             if (new UsersServices().GetByUserName(User.Identity.Name).Company.CompaniesType.Name == "Customer")
                 user.Company_Id = new UsersServices().GetByUserName(User.Identity.Name).Company_Id;
             if (GetAuthorization(user))
             {
                 ValidateUserModel(user);
                 ImageFileValidation();
                 if (ModelState.IsValid)
                 {
                     if (_userService.Add(user))
                     {
                         UploadImageToServer(user.Id);
                         _userService.SaveChanges();
                         EmailBroadcaster.SendEmail(ViewRes.Controllers.Users.UserWelcome, ViewRes.Controllers.Users.Message, user.Email);
                         return RedirectToAction("Index");
                     }
                 }
                 InitializeViews(null);
                 return View(_userViewModel);
             }
             else
                 return RedirectToLogOn();
         }

         [HttpPost]
         public ActionResult Delete(int id, FormCollection collection)
         {
             if (GetAuthorization(_userService.GetById(id)))
             {
                 try
                 {
                     _userService.Delete(id);
                     _userService.SaveChanges();
                     return RedirectToAction("Index");
                 }
                 catch
                 {
                     InitializeViews(id);
                     return RedirectToAction("Index");
                 }
             }
             else
                 return RedirectToLogOn();
         }

         public ActionResult Details(int id)
         {
             if (GetAuthorization(_userService.GetById(id)))
             {
                 InitializeViews(id);
                 return View(_userViewModel);
             }
             else
                 return RedirectToLogOn();
         }

         public ActionResult Edit(int id)
         {
             if (GetAuthorization(_userService.GetById(id)))
             {
                 InitializeViews(id);
                 return View(_userViewModel);
             }
             else
                 return RedirectToLogOn();
         }

         [HttpPost]
         public ActionResult Edit(int id, User User)
         {
             if (GetAuthorization(_userService.GetById(id)))
             {
                 User user = _userService.GetById(id);
                 try
                 {
                     user.FirstName = User.FirstName;
                     user.LastName = User.LastName;
                     user.Email = User.Email;
                     user.ContactPhone = User.ContactPhone;
                     user.Address = User.Address;
                     user.IdNumber = User.IdNumber;
                     user.Company_Id = User.Company_Id;
                     user.Location_Id = User.Location_Id;
                     if (User.Image != null)
                     {
                         ImageFileValidation();
                         UploadImageToServer(user.Id);
                     }
                     _userService.SaveChanges();
                     return RedirectToAction("Index");
                 }
                 catch
                 {
                     InitializeViews(id);
                     return View(_userViewModel);
                 }
             }
             else
                 return RedirectToLogOn();
         }

         private ActionResult RedirectToLogOn()
         {
             ModelState.AddModelError(ViewRes.Controllers.Shared.UnauthorizedAccess, ViewRes.Controllers.Shared.UnauthorizedText);
             return RedirectToAction("LogOn", "Account");
         }

         public ActionResult GridData(string sidx, string sord, int page, int rows, string filters)
         {
             object resultado = _userService.RequestList(_userService.GetByUserName(User.Identity.Name),sidx, sord, page, rows, filters);
             return Json(resultado);
         }
        
         public ActionResult Index()
         {
             InitializeViews(null);
             return View(_userViewModel);
         }

         public JsonResult GetCompaniesByRole(int role_id)
         {
             int company_id = _userService.GetByUserName(User.Identity.Name).Company_Id;
             List<object> companies = new List<object>();
             foreach (var company in  new CompaniesServices().GetCompaniesByAssociatedAndRoleForDropDownList(company_id, role_id))
             {
                 companies.Add(
                     new
                     {
                         optionValue = company.Key,
                         optionDisplay = company.Value
                     });
             }

             return Json(companies);
         }

        private void InitializeViews(int? user_id)
        {
            User user;
            User userLogged = _userService.GetByUserName(User.Identity.Name);
            SelectList companiesList; 
            SelectList locationsList; 
            SelectList rolesList;
            string companyType = userLogged.Company.CompaniesType.Name;
            
            if (user_id != null)
            {
                user = _userService.GetById((int)user_id);               
                companiesList = new SelectList(new CompaniesServices().GetCompaniesByAssociatedForDropDownList(userLogged.Company_Id), "Key", "Value", user.Company_Id);
                locationsList = new SelectList(new LocationsServices().GetLocationsForDropDownList(user.Company_Id), "Key", "Value", user.Location_Id);
                if (userLogged.Role.Name == "Administrator")
                    rolesList = new SelectList(new RolesServices().GetRolesForAdministrator(), "Key", "Value", user.Role_Id);
                else
                    rolesList = new SelectList(new RolesServices().GetRolesForCompanyAppManager(), "Key", "Value", user.Role_Id);

            }
            else
            {
                user = new User();
                companiesList = new SelectList(new CompaniesServices().GetCompaniesByAssociatedForDropDownList(userLogged.Company_Id), "Key", "Value");
                if (userLogged.Role.Name == "Administrator")
                {
                    locationsList = new SelectList(new LocationsServices().GetEmptyDictionary(), "Key", "Value");
                    rolesList = new SelectList(new RolesServices().GetRolesForAdministrator(), "Key", "Value");
                }
                else
                {
                    rolesList = new SelectList(new RolesServices().GetRolesForCompanyAppManager(), "Key", "Value");
                    locationsList = new SelectList(new LocationsServices().GetLocationsForDropDownList(userLogged.Company_Id), "Key", "Value");
                }
            }

            _userViewModel = new UserViewModel(user, companiesList, locationsList, rolesList, companyType);
        }

        private void ValidateUserModel(User user)
        {
            if (user.Password != user.ConfirmPassword)
                ModelState.AddModelError(ViewRes.Controllers.Users.Password, ViewRes.Controllers.Users.PasswordText);
            if (_userService.IsUserNameDuplicated(user.UserName))
                ModelState.AddModelError(ViewRes.Controllers.Users.UserName, ViewRes.Controllers.Users.UserNameText);
            if (_userService.IsEmailDuplicated(user.Email))
                ModelState.AddModelError(ViewRes.Controllers.Users.Email, ViewRes.Controllers.Users.EmailText);
        }

        private void UploadImageToServer(int user_id)
        {
            User user = _userService.GetById((int)user_id);

            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];
                string name = DateTime.Now.ToString("yyyyMMddhhmmss") + user.Id;
                if (file.ContentLength > 0)
                {
                    string[] extension = file.FileName.ToString().Split('.');
                    string filePath = Request.MapPath("~/Content/Images/Users/" + name + "." + extension.Last().ToString());
                    file.SaveAs(filePath);
                    string uImage = name + '.' + extension.Last().ToString();
                    user.Image = uImage;
                }
            }
        }

        private void ImageFileValidation()
        {
            string[] formatos = { "image/pjpeg", "image/jpeg", "image/gif", "image/png" };

            foreach (string inputTagName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[inputTagName];

                if (file.ContentLength > 0)
                {
                    var ctype = file.ContentType;
                    int tamano = file.ContentLength / 1024;

                    if (tamano > 500)
                    {
                        ModelState.AddModelError(ViewRes.Controllers.Users.Photo, ViewRes.Controllers.Users.PhotoSizeText);
                    }
                    else if (!formatos.Contains(ctype))
                    {
                        ModelState.AddModelError(ViewRes.Controllers.Users.Photo, ViewRes.Controllers.Users.PhotoFormatText);
                    }
                }
            }
        }
    }
}
