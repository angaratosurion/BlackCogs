using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Interfaces;

namespace BlackCogs.ModuleInfo
{
    [Export(typeof(IModuleInfo)), ExportMetadata("ID", "3")]
    public class BlackCogsInfo : IModuleInfo
    {
        public string Description
        {
            get
            {
                return "";
            }
        }

        public string Name
        {
            get
            {
                return "BlackCogs";
            }
        }

        public string SourceCode
        {
            get
            {
                return "https://github.com/angaratosurion/BlackCogs";
            }
        }

        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string WebSite
        {
            get
            {
                return "http://pariskoutsioukis.net/blog/";
            }
        }
    }
}
