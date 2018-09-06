using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MEPAC.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Danh sach cac du an",
                url: "danh-sach-du-an",
                defaults: new { controller = "Home", action = "Projects", id = UrlParameter.Optional },
                namespaces: new[] { "MEPAC.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Chi tiet du an",
               url: "chi-tiet-du-an/{id}",
               defaults: new { controller = "Home", action = "ProjectsDetail", id = UrlParameter.Optional },
               namespaces: new[] { "MEPAC.Web.Controllers" }
           );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
