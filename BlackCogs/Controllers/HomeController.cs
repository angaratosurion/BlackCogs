using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using BlackCogs.Configuration;

namespace BlackCogs.Controllers
{
    [Export("Home", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class HomeController : Controller
    {

        BlackCogsSettingManager set = new BlackCogsSettingManager();
        public ActionResult Index()
        {

            string cont = set.DefaultController();
            string act= set.DefaultAction();

           

            return RedirectToAction(act, cont);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
