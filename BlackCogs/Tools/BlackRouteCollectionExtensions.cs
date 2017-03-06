using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using BlackCogs.Configuration;

namespace BlackCogs.Tools
{
    public static class BlackRouteCollectionExtensions
    {

        static Dictionary<string, Route> Routes = new Dictionary<string, Route>();
        static CommonTools cmtools = new CommonTools();
        static BlackCogsSettingManager setting = new BlackCogsSettingManager();
        public static Route MapRouteWithName(this RouteCollection routes,
        string name, string url, object defaults)//, object constraints)
        {
            try
            {
                if ( ExistsBasedonName(name) !=true)
                    {
                   
                            Route route = routes.MapRoute(name, url, defaults);//, constraints);
                            route.DataTokens = new RouteValueDictionary();
                            route.DataTokens.Add("RouteName", name);
                            routes.Add(name, route);
                            Routes.Add(name, route);

                            return route;
                       
                    
                }
                return null;

            }
            catch (System.ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public static Route MapRouteWithName(this RouteCollection routes,
      string name, string url, object defaults , object constraints)
        {
            try
            {
                if (ExistsBasedonName(name) != true )
                {
                 
                            Route route = routes.MapRoute(name, url, defaults, constraints);
                            route.DataTokens = new RouteValueDictionary();
                            route.DataTokens.Add("RouteName", name);
                            routes.Add(name, route);
                            Routes.Add(name, route);
                            return route;
                      
               
                }
                return null;
            }
            catch(System.ArgumentException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public static Boolean ExistsBasedonName(string  name)
        {
            try
            {
                Boolean ap = false;
                  if ( name !=null)
                {
                    var rt = Routes.FirstOrDefault(x => x.Key == name).Value;
                    if ( rt!=null)
                    {
                        ap = true;
                    }
                    
                }



                return ap;

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return false;
            }
        }
    }
}
