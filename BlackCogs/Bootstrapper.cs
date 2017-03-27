using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using BlackCogs.Interfaces;
using System.Reflection;
using System.Diagnostics;
using System.Web.Mvc;
using BlackCogs.Configuration;
using System.Web.Routing;

namespace BlackCogs
{
    public class Bootstrapper
    {
        private static CompositionContainer CompositionContainer;
        private static bool IsLoaded = false;
        //   static CommonTools cmTools = new CommonTools();
        static AggregateCatalog Catlgs;
        [ImportMany]
        private static IEnumerable<Lazy<IRouteRegistrar, IRouteRegistrarMetadata>> RouteRegistrars;
        [ImportMany]
        private static IEnumerable<Lazy<IActionVerb, IActionVerbMetadata>> ActionVerbs;
        [ImportMany]
        private static IEnumerable<Lazy<IModuleInfo>> ModuleInfos;

        public static void Compose(List<string> pluginFolders)
        {
            try
            {

                if (IsLoaded) return;

                var catalog = new AggregateCatalog();

               // catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")));

                foreach (var plugin in pluginFolders)
                {
                    var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        "Modules", plugin));
                    RegisterPath(directoryCatalog.FullPath);
                    catalog.Catalogs.Add(directoryCatalog);

                }
                CompositionContainer = new CompositionContainer(catalog);
                   CompositionContainer.ComposeParts();
                ActionVerbs = CompositionContainer.GetExports<IActionVerb, IActionVerbMetadata>();
                ModuleInfos = CompositionContainer.GetExports<IModuleInfo> ();
                RouteRegistrars = CompositionContainer.GetExports<IRouteRegistrar,IRouteRegistrarMetadata>();
                RegisterRoutes();
                Catlgs= catalog;
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
            }
        }
        public static void Compose(List<string> pluginFolders,Boolean binonmodulesdir)
        {
            try
            {
                 

                if (IsLoaded) return;

                var catalog = new AggregateCatalog();
                if (binonmodulesdir != true)
                {

                    catalog.Catalogs.Add(new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin")));
                }
                else {
                    foreach (var plugin in pluginFolders)
                    {
                        var directoryCatalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                            "Modules", plugin));
                        RegisterPath(directoryCatalog.FullPath);
                        catalog.Catalogs.Add(directoryCatalog);

                    }
                }
                CompositionContainer = new CompositionContainer(catalog);
               

                CompositionContainer.ComposeParts();
                ActionVerbs = CompositionContainer.GetExports<IActionVerb, IActionVerbMetadata>();
                ModuleInfos = CompositionContainer.GetExports<IModuleInfo>();
                RouteRegistrars = CompositionContainer.GetExports<IRouteRegistrar, IRouteRegistrarMetadata>();
                RegisterRoutes();
                Catlgs = catalog;
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
            }
        }

        public static T GetInstance<T>(string contractName = null)
        {
            try
            {

                var type = default(T);
               
                if (CompositionContainer == null) return type;

                if (!string.IsNullOrWhiteSpace(contractName))
                {
                    type = CompositionContainer.GetExportedValue<T>(contractName);
                }
                else
                {
                    type = CompositionContainer.GetExportedValue<T>();
                }
              
                   
                return type;
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return default(T);
            }
        }
        public static void RegisterPath(string path)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(path))
                {
                    AppDomain.CurrentDomain.AppendPrivatePath(path);
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
            }

        }
        /// <summary>
        /// Gets the available verbs for the given category.
        /// </summary>
        /// <param name="category">The category of verbs to get.</param>
        /// <returns>An enumerable of verbs.</returns>
        public static IEnumerable<IActionVerb> GetVerbsForCategory(string category)
        {


            return ActionVerbs
                .Where(l => l.Metadata.Category.Equals(category, StringComparison.InvariantCultureIgnoreCase))
                .Select(l => l.Value);
        }
        public static IEnumerable<IModuleInfo> GetAllModulesInfo()
        {
            List<IModuleInfo> ap = new List<IModuleInfo>();

            ap = GetAssembliesInfo();
            
            foreach (var inf in ModuleInfos)
            {
                ap.Add(inf.Value);
            }

            return ap;
        }
        public static string GetApplicationName()
        {
            try
            {
                string ap = null;
                FileVersionInfo finof = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                ap = finof.ProductName;
                ap = AppDomain.CurrentDomain.FriendlyName;


                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public static string GetApplicationName(string projectname)
        {
            try
            {
                string ap = null;
                string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", projectname + ".dll");
                if (CommonTools.isEmpty(projectname) == false && File.Exists(filepath)==true)
                {
                    FileVersionInfo finof = FileVersionInfo.GetVersionInfo(filepath);
                    ap = finof.ProductName;
                }



                return ap;
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }

        /// <summary>
        /// Registers any routes required by the application.
        /// </summary>
        public static void RegisterRoutes()
        {
            try
            {
                if (RouteRegistrars == null || RouteRegistrars.Count() == 0)
                    return;

                var routes = RouteTable.Routes;

                var registrars = RouteRegistrars
                    .OrderBy(lazy => lazy.Metadata.Order)
                    .Select(lazy => lazy.Value).ToList();
                 
              
                registrars.ForEach(r => r.RegisterIgnoreRoutes(routes));
                registrars.ForEach(r => r.RegisterRoutes(routes));
            }
            catch (Exception ex)
            {
                System.ArgumentException et = new ArgumentException();
                if (ex !=et)
                {
                    CommonTools.ErrorReporting(ex);
                }
            }
        }
        public static  List<IModuleInfo> GetAssembliesInfo()
        {
            try
            {
                List<IModuleInfo> ap = new List<IModuleInfo>();

               foreach( var f in Catlgs.ToList())
                {
                  
                }
                var files = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"), "*.dll");
               
                foreach( var   f in files)
                    {
                        var a = GetAssemblyInfo(f);
                    ap.Add(a);
                    }

                return ap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static  IModuleInfo GetAssemblyInfo(string filename)
        {
            try
            {
                IModuleInfo ap =null;
                if ( CommonTools.isEmpty(filename)==false)
                {
                    var asm = Assembly.LoadFrom(filename);
                    var myClassType = asm.GetTypes()
                     .FirstOrDefault(t => t.GetCustomAttributes()
                     .Any(a => a.GetType().Name == "ExportAttribute"));





                    ap = (IModuleInfo)myClassType;
                }



                return ap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    

    }
}
