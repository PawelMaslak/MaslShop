using Maslshop.Repositories;

namespace Maslshop.Controllers
{
    using Models.ViewModels;
    using System.Web.Mvc;

    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public ActionResult Index(string query = null)
        {
            var viewModel = new UserListViewModel()
            {
                Users = _adminRepository.SelectUsers(query),
                SearchTerm = query
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchUser(UserListViewModel viewModel)
        {
            return RedirectToAction("Index", new { query = viewModel.SearchTerm });
        }
    }
}