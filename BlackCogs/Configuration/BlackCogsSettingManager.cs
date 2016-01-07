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
    }
}
