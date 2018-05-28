using Maslshop.Models.Core;
using Maslshop.Models.ViewModels;
using Maslshop.Persistence;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Maslshop.Controllers
{
    [Authorize(Roles = "Administrator")]
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
                Heading = "Lista kategorii",
                Categories = GetCategories()
            };

            return View(viewModel);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Category.GetCategoriesList();
        }

        public void DeleteCategory(int id)
        {
            _unitOfWork.Category.RemoveCategory(id);
        }

        public ActionResult Delete(int categoryId)
        {
            DeleteCategory(categoryId);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            var viewModel = new CategoryFormViewModel()
            {
                Heading = "Utwórz kategorię"
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
                Heading = "Edytuj kategorię",
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