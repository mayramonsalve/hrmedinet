using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MedinetClassLibrary.Classes;
using Medinet.Models;
using System.Globalization;

namespace Medinet.Controllers
{
    public class AccountController : Controller
    { 
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        //public ActionResult LogOn()
        //{
        //    return View();
        //}
        public ActionResult LogOn()
        {
            string returnUrl = Request.QueryString["ReturnUrl"];
            if ((returnUrl != null) && returnUrl.StartsWith("/Mobile/", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction("LogOn", "Account", new {Area = "Mobile", ReturnUrl = returnUrl});

            return View();
        }


        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                { 
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", ViewRes.Controllers.Account.UserNamePassError);
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult MobileLogOn()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult MobileLogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);

                    if (model.RememberMe)
                    {
                        Response.Cookies["UserName"].Value = model.UserName;
                        Response.Cookies["Password"].Value = model.Password;
                        Response.Cookies["UserName"].Expires = DateTime.Now.AddMonths(12);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddMonths(12);
                    }

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddMonths(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddMonths(-1);
                    ViewData["UserNamePassError"] = ViewRes.Controllers.Account.UserNamePassError;
                }

            }

            // If we got this far, something failed, redisplay form
            return View("/Views/Home/Index.Mobile.aspx",model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
        // **************************************
        // URL: /Account/PasswordRecovery
        // **************************************
        public ActionResult PasswordRecovery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordRecovery(FormCollection collection)
        {
            string username = collection["Username"];        
            try
            {
                MembershipUser user = MembershipService.GetUserByLogin(username);
                string newPassword = user.ResetPassword();
                EmailBroadcaster.SendEmail(ViewRes.Controllers.Account.PassRecoveryNew, ViewRes.Controllers.Account.PassRecoveryPass + newPassword, user.Email);
                return RedirectToAction("PasswordRecoverySucceeded");
            }
            catch
            {
                try
                {
                    MembershipUser user = MembershipService.GetUserByLogin(MembershipService.GetUserByEmail(username));
                    string newPassword = user.ResetPassword();
                    EmailBroadcaster.SendEmail(ViewRes.Controllers.Account.PassRecoveryNew, ViewRes.Controllers.Account.PassRecoveryPass + newPassword, user.Email);
                    return RedirectToAction("PasswordRecoverySucceeded");
                }
                catch {
                    ModelState.AddModelError(ViewRes.Controllers.Account.UserName, new Exception());
                    return View();
                }
            }
        }

        public ActionResult PasswordRecoverySucceeded()
        {
            return View();
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            string currentPassword = collection["CurrentPassword"];
            string newPassword = collection["NewPassword"];
            string repeatNewPassword = collection["RepeatNewPassword"];
            try
            {
                
                if (newPassword.CompareTo(repeatNewPassword) == 0)
                {
                    if (MembershipService.ChangePassword(User.Identity.Name, collection["CurrentPassword"], collection["NewPassword"]))      
                    {
                        return RedirectToAction("ChangePasswordSucceeded");
                    }
                    else
                    {
                        ModelState.AddModelError(ViewRes.Controllers.Account.CurrentPassword, new Exception());
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError(ViewRes.Controllers.Account.RepeatNewPassword, new Exception());
                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError(ViewRes.Controllers.Account.NewPassword, new Exception());
                return View();
            }
        }

        public ActionResult ChangePasswordSucceeded()
        {
            return View();
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
