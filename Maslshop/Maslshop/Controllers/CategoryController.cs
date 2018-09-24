using Maslshop.Models.ViewModels;
using Maslshop.Persistence;
using System.Linq;
using System.Web.Mvc;

namespace Maslshop.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Category
        public ActionResult Index()
        {
            var viewModel = new CategoriesViewModel()
            {
                Heading = "Maslshop - Categories List",
                Categories = _unitOfWork.Category.GetCategoriesList()
            };

            return View(viewModel);
        }

        public ActionResult CategoriesList()
        {
            var categoriesWithProducts = _unitOfWork.Category.GetCategoriesList().Where(i => i.Products.Count > 0);

            var viewModel = new CategoriesViewModel()
            {
                Categories = categoriesWithProducts
            };

            return PartialView(viewModel);
        }

        public ActionResult Delete(int categoryId)
        {
            _unitOfWork.Category.RemoveCategory(categoryId);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            var viewModel = new CategoryFormViewModel()
            {
                Heading = "Create New Category"
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(CategoryFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.AddCategory(viewModel);

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public ActionResult Edit(int categoryId)
        {
            if (_unitOfWork.Category.GetCategoryById(categoryId) == null)
                return HttpNotFound();

            var viewModel = new CategoryFormViewModel()
            {
                Heading = "Edit Category",
                Id = categoryId,
                Name = _unitOfWork.Category.GetCategoryById(categoryId).Name
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var category = _unitOfWork.Category.GetCategoryById(viewModel.Id);

                category.Name = viewModel.Name;

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }



    }
}