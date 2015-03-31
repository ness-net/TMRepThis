using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Commonlayer;
using TraderMarket.Models;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.AspNet;

namespace TraderMarket.Controllers
{
    public class UserController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the login view
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Login()
        {

            //var openid = new OpenIdRelyingParty();
            //IAuthenticationResponse response = openid.GetResponse();

            //if (response != null)
            //{
            //    switch (response.Status)
            //    {
            //        case AuthenticationStatus.Authenticated:
            //            FormsAuthentication.RedirectFromLoginPage(
            //                response.ClaimedIdentifier, false);
            //            break;
            //        case AuthenticationStatus.Canceled:
            //            ModelState.AddModelError("loginIdentifier",
            //                "Login was cancelled at the provider");
            //            break;
            //        case AuthenticationStatus.Failed:
            //            ModelState.AddModelError("loginIdentifier",
            //                "Login failed using the provided OpenID identifier");
            //            break;
            //    }
            //}           

            if (User.Identity.Name == null || User.Identity.Name == "")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Method that runs when the user logs in
        /// </summary>
        /// <param name="data">Form data</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Login(LoginModel data)
        {
            try
            {
                if (new UserService.UserServiceClient().isAuthenticationValid(data.email, data.password))
                {
                    ViewBag.LoginError = "";
                    FormsAuthentication.RedirectFromLoginPage(data.email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginError = "Could not log in, please make sure that your login details are correct";
                    return View();
                }
            }
            catch
            {
                ViewBag.LoginError = "Could not log in, please make sure that your login details are correct";
                return View();
            }
        }

        /// <summary>
        /// Method that runs when the user logs off
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Method to display the register page
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Register()
        {
            if (User.Identity.Name == null || User.Identity.Name == "")
            {
                ViewBag.Message = "Enter your details";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Method that runs when the user hits the submit button of the register form
        /// </summary>
        /// <param name="data">Data in the registration form</param>
        /// <returns>ActionResult</returns>
        [Recaptcha.RecaptchaControlMvc.CaptchaValidator]
        [HttpPost]
        public ActionResult Register(RegisterModel data, bool captchaValid)
        {
            if (!captchaValid)
            {
                ModelState.AddModelError("captcha", "You did not type the verification word correctly. Please try again.");
                return View();
            }
            else if (ModelState.IsValid)
            {
                Regex regex = new Regex(@"^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$");
                if ((new UserService.UserServiceClient().DoesUsernameExist(data.username)) == true)
                {
                    ViewBag.Message = "Username already exists";
                    return View();
                }
                else if ((new UserService.UserServiceClient().DoesEmailExist(data.email)) == true)
                {
                    ViewBag.Message = "Email already exists";
                    return View();
                }
                else if (regex.IsMatch(data.password) == false)
                {
                    ViewBag.Message = "Password must be at least 4 characters, no more than 8 characters, and must include at least one upper case letter, one lower case letter, and one numeric digit.";
                    return View();
                }
                else
                {
                    new UserService.UserServiceClient().AddUser(data.username, data.password, data.email, data.name, data.surname, Convert.ToInt64(data.contactno), data.buyer, data.seller);
                    return RedirectToAction("Login", "User");

                }

               
            }
            return View();
        }

    }
}

