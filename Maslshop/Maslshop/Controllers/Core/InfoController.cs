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
                Heading = "Maslshop - About Us"
            };

            return View(viewModel);
        }
    }

}