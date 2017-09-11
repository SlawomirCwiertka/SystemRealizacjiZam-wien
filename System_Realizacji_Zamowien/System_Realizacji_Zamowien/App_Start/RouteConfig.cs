using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace System_Realizacji_Zamowien
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Order",
                url: "{controller}/{action}/{id}/{messageId}",
                defaults: new { controller = "Order", action = "Order", id = UrlParameter.Optional, messageId = UrlParameter.Optional }
            );
            routes.MapRoute(
             name: "Pagination",
             url: "{controller}/{action}/{page}/{category}/{count}/{searchText}",
             defaults: new { controller = "Products", action = "Products", count = UrlParameter.Optional, page = UrlParameter.Optional, category = UrlParameter.Optional, searchText = UrlParameter.Optional }
         );
        }
    }
}
