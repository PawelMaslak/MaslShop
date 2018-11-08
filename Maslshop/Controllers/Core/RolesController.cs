using Maslshop.Models.ViewModels.Roles;
using Maslshop.Persistence;
using System.Web.Mvc;

namespace Maslshop.Controllers.Core
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
           var viewModel = new RolesListViewModel()
            {
                Roles = _unitOfWork.Roles.GetRolesList(),
                Heading = "Maslshop - Roles List"
            };

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new RoleFormViewModel()
            {
                Heading = "Maslshop - Create New Role"
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoleFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Roles.CreateRole(viewModel);

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }
                return View(viewModel);
        }

        public ActionResult Delete(string roleId)
        {
            _unitOfWork.Roles.DeleteRole(roleId);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string roleId)
        {
            if (_unitOfWork.Roles.GetRole(roleId) == null)
                return HttpNotFound();

            var viewModel = new RoleFormViewModel
            {
                Heading = "Maslshop - Edit Role",
                RoleId = roleId,
                RoleName = _unitOfWork.Roles.GetRole(roleId).Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoleFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Roles.GetRole(viewModel.RoleId).Name = viewModel.RoleName;

                _unitOfWork.Complete();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}