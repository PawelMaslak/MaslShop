using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Order;
using Maslshop.Persistence;
using System.Web.Mvc;

namespace Maslshop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public ActionResult Checkout()
        {
            var viewModel = new OrderFormViewModel()
            {
                Heading = "Zamówienie",
                Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Checkout(OrderFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList();
                return View(viewModel); 
            }

            var order = new Order()
            {
                Name = viewModel.Name,
                Surname = viewModel.Surname,
                Address = viewModel.Address,
                City = viewModel.City,
                PostCode = viewModel.PostCode,
                Email = viewModel.Email,
                DeliveryId = viewModel.DeliveryId,
                OrderStatusId = 1
            };

            _unitOfWork.Orders.CreateOrder(order);

            Session.RemoveAll();

            return RedirectToAction("CheckoutComplete");
        }

        public ActionResult CheckoutComplete()
        {
            return View();
        }
    }
}