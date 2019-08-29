using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "FEAccomadations",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Accomadations", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "HMS.Controllers" } // have to use namespace as we have a controller with the same name in the Areas section
        );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
