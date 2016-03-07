using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackCogs.Configuration
{
   public class BlackCogsSettingManager
    {
        AppSettingsReader rdr = new AppSettingsReader();
        Boolean searchforbinariesatmodulesfodler;
        public const string BinariesONModulesFolder = "BinariesONModulesFolder";
        public const string ConstExceptionShowingonBrowser = "ExcShownOnBrowser";
        public static Boolean ExceptionShownonBrowser = true;
        public  Boolean IsBinariesEnabledOnModulesFolder()
        {
            try
            {
                Boolean ap=false;
                var val = rdr.GetValue(BinariesONModulesFolder, typeof(Boolean));
                 if ( val !=null)
                {
                    ap = Convert.ToBoolean(val);
                }



                return ap;

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return false;
            }
        }
        public Boolean ExceptionShownOnBrowser()
        {
            try
            {
                Boolean app = true;
                app = Convert.ToBoolean(rdr.GetValue(ConstExceptionShowingonBrowser, typeof(bool)));

                ExceptionShownonBrowser = app;


                return app;

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return true;
            }
        }
    }
}
