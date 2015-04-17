using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TraderMarket
{
    public class AuthorizeBuyer: AuthorizeAttribute
    {
        private readonly bool _authorize;

	    public AuthorizeBuyer()
	    {
		    _authorize = true;
	    }

        public AuthorizeBuyer(bool authorize)
	    {
		    _authorize = authorize;
	    }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            else
            {
                TraderMarket.UserService.RolesView[] rv = new UserService.UserServiceClient().GetUserRolesV(httpContext.User.Identity.Name);
                foreach (TraderMarket.UserService.RolesView r in rv)
                {
                    if (r.ID == 3)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}