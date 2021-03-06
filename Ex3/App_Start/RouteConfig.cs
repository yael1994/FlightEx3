﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute("display", "display/{ip}/{port}",
           defaults: new { controller = "Flight", action = "display" }
           );
            routes.MapRoute("displayTime", "display/{ip}/{port}/{time}",
           defaults: new { controller = "Flight", action = "displayTime"}
           );

            routes.MapRoute("save", "save/{ip}/{port}/{time}/{during}/{fileName}",
            defaults: new { controller = "Flight", action = "save" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Flight", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
