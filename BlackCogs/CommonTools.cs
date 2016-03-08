using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            BlackCogs.Configuration.BlackCogsSettingManager conf = new Configuration.BlackCogsSettingManager();
            if (ex.GetBaseException() is ValidationException)
            {
                ValidationErrorReporting((ValidationException)ex);


            }
            else
            {

                NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Fatal(ex);
                if (conf.ExceptionShownOnBrowser() == true)
                {

                    throw (ex);
                }
            }

        }
        public static void ValidationErrorReporting(ValidationException ex)
        {
            //throw (ex);
            BlackCogs.Configuration.BlackCogsSettingManager conf = new Configuration.BlackCogsSettingManager();


            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
          
            logger.Info(ex);
            if (conf.ExceptionShownOnBrowser() == true)
            {

                throw (ex);
            }

        }
    }
}
