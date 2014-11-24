using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Commonlayer;
using TraderMarket.Models;
using System.Windows.Forms;

namespace TraderMarket.Controllers
{
    public class UserController : BaseController
    {
        /// <summary>
        /// Displays the login view
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Method that runs when the user logs in
        /// </summary>
        /// <param name="data">Form data</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Login(LoginModel data)
        {
            //if user exists, display name in the loginPartial and change url to logoff
            try
            {
                if (new UserService.UserServiceClient().isAuthenticationValid(data.username, data.password))
                {
                    //Display username in the header (_loginPartialView)
                    ViewBag.LoginError = "";
                    FormsAuthentication.RedirectFromLoginPage(data.username, true);
                    return RedirectToAction("Index", "Home");
                }
                else //if user does not exist
                {
                    ViewBag.LoginError = "Could not log in, please make sure that your login details are correct";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.LoginError = "Could not log in, please make sure that your login details are correct";
                return View();
            }
        }

        /// <summary>
        /// Method that runs when the user logs off
        /// </summary>
        /// <returns>ActionResult</returns>
        [Authorize(Roles = "Administrator, Normal User")]
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
            ViewBag.Message = "Enter your details";
            return View();
        }

        /// <summary>
        /// Method that runs when the user hits the submit button of the register form
        /// </summary>
        /// <param name="data">Data in the registration form</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel data)
        {
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
            else
            {
                new UserService.UserServiceClient().AddUser(data.username, data.password, data.email, data.name, data.surname, data.address, data.town, data.country, data.mobile, data.pin);
                ViewBag.Message = "";
                //openForm();
                return RedirectToAction("Login", "User");
            }
        }

        ///// <summary>
        ///// Opens the form of the web application
        ///// </summary>
        //public void openForm()
        //{
        //    Form1 f1 = new Form1();
        //    f1.ShowDialog();
        //}



    }
}
