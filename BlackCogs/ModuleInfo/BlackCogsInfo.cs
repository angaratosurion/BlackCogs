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
    [Export(typeof(IModuleInfo)), ExportMetadata("Type", "ModuleInfo1")]
  
    public class BlackCogsInfo : IModuleInfo
    {
        public string Description
        {
            get
            {
                return "";
            }
            set { }
        }

        public string Name
        {
            get
            {
                return "BlackCogs";
            }
            set { }
        }

        public string SourceCode
        {
            get
            {
                return "https://github.com/angaratosurion/BlackCogs";
            }
            set { }
        }

        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            set { }
        }

        public string WebSite
        {
            get
            {
                return "http://pariskoutsioukis.net/blog/";
            }
            set { }
        }
    }
}
