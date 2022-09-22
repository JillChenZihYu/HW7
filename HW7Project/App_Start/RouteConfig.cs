using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HW7Project
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Cart",
                url: "購物車",
                defaults: new { controller = "Home", action = "MyCart" }
            );
            routes.MapRoute(
                name: "Cart2",
                url: "Cart",
                defaults: new { controller = "Home", action = "MyCart" }
            );
            routes.MapRoute(
                name: "Cart3",
                url: "MyCart",
                defaults: new { controller = "Home", action = "MyCart" }
            );

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
