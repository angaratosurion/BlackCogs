using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Application;
using BlackCogs.Managers;

namespace BlackCogs
{
   public class CommonTools
    {
        public static ApplicationSignInManager _signInManager;
        public static ApplicationUserManager _userManager;
        public static FeatureManager featuremng = new FeatureManager();
        public static  Boolean isEmpty(string str)
       {
           try
           {
               Boolean ap = true;
               if (str != null && str != String.Empty)
               {
                   ap = false;
               }

               return ap;
           }
           catch (Exception)
           {
               
               throw;
               return true;
           }
       }
       public static void ErrorReporting (Exception ex)
       {
           //throw (ex);
           
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Fatal(ex);
            throw (ex);

        }
    }
}
