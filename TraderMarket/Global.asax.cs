using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Business_Layer;
using Commonlayer;
using Commonlayer.Views;
using System.Security.Principal;

namespace TraderMarket
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            //HtmlAttributeProvider.Register(metadata => metadata.DataTypeName == "Date", "class", "isADate");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Context.User != null)
            {
                Role[] usersRole = new UserService.UserServiceClient().GetUserRoles(Context.User.Identity.Name);

                string[] roles = new string[usersRole.Count()];

                for (int i = 0; i < roles.Length; i++)
                {
                    roles[i] = usersRole.ElementAt(i).Role1;
                }

                GenericPrincipal gp = new GenericPrincipal(Context.User.Identity, roles);
                Context.User = gp;
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Context.User != null)
            {
                RolesView[] usersrole = new UserService.UserServiceClient().GetUserRolesV(Context.User.Identity.Name);
              // Role[] usersRole = new UserService.UserServiceClient().GetUserRoles(Context.User.Identity.Name);

                string[] roles = new string[usersrole.Count()];

                for (int i = 0; i < roles.Length; i++)
                {
                    roles[i] = usersrole.ElementAt(i).Name;
                }

                GenericPrincipal gp = new GenericPrincipal(Context.User.Identity, roles);
                Context.User = gp;
            }
        }
    }
}