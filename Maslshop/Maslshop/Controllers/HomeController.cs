using Maslshop.Models.ViewModels;
using Maslshop.Persistence;
using System.Web.Mvc;

namespace Maslshop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Index(string query = null)
        {
            if (query != null)
            {
                var viewModel = new ProductsListViewModel()
                {
                    Heading = "Lista wyszukanych produktów",
                    Categories = _unitOfWork.Product.GetCategories(),
                    Products = _unitOfWork.Product.GetSearchedProducts(query),
                    Files = _unitOfWork.File.GetPhotos(),
                    SearchTerm = query
                };

                return View("Index", viewModel);
            }
            else
            {
                var viewModel = new ProductsListViewModel()
                {
                    Heading = "Lista produktów - ostatnio dodane",
                    Categories = _unitOfWork.Product.GetCategories(),
                    Products = _unitOfWork.Product.GetLatestThreeProducts(),
                    Files = _unitOfWork.File.GetPhotos()
                };

                return View("Index", viewModel);
            }
        }

        [HttpPost]
        public ActionResult SearchProduct(ProductsViewModel viewModel)
        {
            return RedirectToAction("Index", new { query = viewModel.SearchTerm });
        }
    }
}