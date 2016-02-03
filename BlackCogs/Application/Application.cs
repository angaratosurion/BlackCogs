using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BlackCogs.Application;
using BlackCogs.Configuration;
using BlackCogs.Controllers.Factory;
using BlackCogs.Views.Engines;

namespace BlackCogs.Application
{
    public class Application : System.Web.HttpApplication
    {
        static BlackCogsSettingManager confmngr = new BlackCogsSettingManager();
        //[Import]
        //private CustomControllerFactory ControllerFactory;
        public static void BootStrap()
        {
            try
            {
                var pluginFolders = new List<string>();

                var plugins = Directory.GetDirectories(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Modules")).ToList();


                plugins.ForEach(s =>
                {
                    var di = new DirectoryInfo(s);
                    pluginFolders.Add(di.Name);
                });

               
                if (plugins.Count > 0 &&confmngr.IsBinariesEnabledOnModulesFolder() == true )
                {
                    Bootstrapper.Compose(pluginFolders);
                }
                //else if (confmngr.IsBinariesEnabledOnModulesFolder() != true)
                //{
                //    Bootstrapper.Compose(pluginFolders, true);
                //}
                else
                {
                    Bootstrapper.Compose(pluginFolders, confmngr.IsBinariesEnabledOnModulesFolder());
                }
                ControllerBuilder.Current.SetControllerFactory(new CustomControllerFactory());
                ViewEngines.Engines.Add(new CustomViewEngine(pluginFolders));
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }

        protected void Application_Start()
        {
            try
            {
              
              
                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
              BootStrap();
          
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }
        }
        protected void Application_Error()
        {
            Exception lastException = Server.GetLastError();
            CommonTools.ErrorReporting(lastException);
        }

    }
}
