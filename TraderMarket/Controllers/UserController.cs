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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Google.Apis.Services;
using Google.Apis.Plus.v1;
using Google.Apis.Util.Store;


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
            if (User.Identity.Name == null || User.Identity.Name == "")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LoginWithGoogle(CancellationToken cancellationToken)
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                  new ClientSecrets
                                  {
                                      ClientId = "495570776054-334jadhgre54h0548rp40spl57flgb42.apps.googleusercontent.com",
                                      ClientSecret = "OBVkdnAFcj2dPkk7FYwk4SBI"
                                  },
                                  new[] { "https://mail.google.com/ email" },
                                  "Vanessa",
                                  CancellationToken.None,
                                 null
                                  ).Result;

            //var result = await new AuthorizationCodeMvcApp(this, new AuthRepository()).
            //                                          AuthorizeAsync(cancellationToken);

            if (credential != null)
            {
                var service = new PlusService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "ASP.NET MVC Sample"
                });

                Google.Apis.Plus.v1.Data.Person me = service.People.Get("me").Execute();
                Google.Apis.Plus.v1.Data.Person.EmailsData myAccountEmail = me.Emails.Where(a => a.Type == "account").FirstOrDefault();
                try
                {
                    bool u = new UserService.UserServiceClient().DoesEmailExist(myAccountEmail.Value);
                    if (u == true)
                    {
                        FormsAuthentication.RedirectFromLoginPage(myAccountEmail.Value, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("Login");
                    }
                }
                catch
                {
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
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

