using Maslshop.Models.ViewModels;
using Maslshop.Persistence;
using System.Web.Mvc;

namespace Maslshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var viewModel = new ProductsListViewModel()
            {
                Heading = "Maslshop",
                Categories = _unitOfWork.Product.GetCategories(),
                Products = _unitOfWork.Product.GetLatestThreeProducts(),
                Files = _unitOfWork.File.GetPhotos()
            };

            return View("Index", viewModel);
        }
    }
}