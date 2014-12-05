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
            if (ModelState.IsValid) { 
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
                if (data.iban != null)
                {
                    string comm;
                    if ((data.commissionp == "Percentage") && (data.commissionff == null))
                    {
                        comm = "Percentage";
                    }
                    else if ((data.commissionp == null) && (data.commissionff == "FixedFee"))
                    {
                        comm = "FixedFee";
                    }
                    else
                    {
                       comm = "both";
                    }
                    
                    if (data.handlesdel == "Yes")
                    {
                        new UserService.UserServiceClient().AddUser(data.username, data.password, data.email, data.name, data.surname, data.postcode, data.town, Convert.ToInt32(data.contactno), data.residence, data.street, data.country, true, Convert.ToInt64(data.iban),comm);
                        ViewBag.Message = "";
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {                       
                        
                            new UserService.UserServiceClient().AddUser(data.username, data.password, data.email, data.name, data.surname, data.postcode, data.town, Convert.ToInt32(data.contactno), data.residence, data.street, data.country, false, Convert.ToInt64(data.iban), comm);
                            ViewBag.Message = "";
                            return RedirectToAction("Login", "User");
                    }
                   
                }
                else
                {
                    new UserService.UserServiceClient().AddUser(data.username, data.password, data.email, data.name, data.surname, data.postcode, data.town, Convert.ToInt32(data.contactno), data.residence, data.street, data.country, false, 0, "no");
                    new UserService.UserServiceClient().AddCreditCard(data.username, data.cardtype, data.cvv, data.cardowner, data.cardnumber);
                    ViewBag.Message = "";
                    return RedirectToAction("Login", "User");
                    
                }
            }
            }
            else
            {
                return View();
            }   
                
            
        }

        



    }
}
