using Maslshop.Models.ViewModels.Info;
using System.Web.Mvc;

namespace Maslshop.Controllers.Core
{
    [AllowAnonymous]
    public class InfoController : Controller
    {
        public ActionResult About()
        {
            var viewModel = new AboutViewModel()
            {
                Heading = "About Us"
            };

            return View(viewModel);
        }

        public ActionResult Faq()
        {
            return View();
        }
    }

}