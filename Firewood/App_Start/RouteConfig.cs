using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Firewood
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //picture pool
            routes.MapRoute(
                "PicPool",
                "PicPool/Type/{type}/Id/{id}/Size/{size}",
                new { controller = "PicPool", action = "Get" }
            );
            routes.MapRoute(
                "PicPoolWidth",
                "PicPool/Type/{type}/Id/{id}/Width/{width}",
                new { controller = "PicPool", action = "Get" }
            );
            routes.MapRoute(
                "PicPoolHeight",
                "PicPool/Type/{type}/Id/{id}/Height/{height}",
                new { controller = "PicPool", action = "Get" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}