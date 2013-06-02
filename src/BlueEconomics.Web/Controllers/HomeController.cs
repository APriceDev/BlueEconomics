using System.Web.Mvc;
using BlueEconomics.Web.ViewModels;

namespace BlueEconomics.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            return View(model);
        }


    }
}
