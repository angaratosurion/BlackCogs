using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BlackCogs.Interfaces;

namespace BlackCogs.Application
{
    [Export(typeof(IRouteRegistrar)), ExportMetadata("Order", 100)]

    public class RouteConfig : IRouteRegistrar
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
           

            
        //}

        public void RegisterIgnoreRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
