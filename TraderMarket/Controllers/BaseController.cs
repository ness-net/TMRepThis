using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commonlayer;

namespace TraderMarket.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.Menus = new MenuService.MenuServiceClient().GetMainMenus1(User.Identity.Name);
            }
            else
            {
                ViewBag.Menus = new MenuService.MenuServiceClient().GetMainMenuV(1);
            }

            base.OnActionExecuting(filterContext);

        }
    }
}

