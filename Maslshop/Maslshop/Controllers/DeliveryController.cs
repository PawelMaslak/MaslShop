using Maslshop.Models.ViewModels.Delivery;
using Maslshop.Persistence;
using System.Web.Mvc;

namespace Maslshop.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeliveryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Deliveries()
        {
            var viewModel = new DeliveriesViewModel()
            {
                Heading = "Lista Opcji Przesyłki",
                Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList()
            };

            return View(viewModel);
        }

        public ActionResult AddDeliveryType()
        {
            var viewModel = new DeliveryFormViewModel()
            {
                Heading = "Dodaj nowy typ przesyłki",
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddDeliveryType(DeliveryFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            _unitOfWork.Deliveries.AddDeliveryType(viewModel);

            _unitOfWork.Complete();

            return RedirectToAction("Deliveries");
        }

        public ActionResult EditDeliveryType(int deliveryId)
        {
            var deliveryType = _unitOfWork.Deliveries.GetDeliveryById(deliveryId);

            if (deliveryType == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DeliveryFormViewModel()
            {
                Heading = "Edytuj typ przesyłki",
                Id = deliveryId,
                Name = deliveryType.Name,
                Price = deliveryType.Price
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditDeliveryType(DeliveryFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var deliveryType = _unitOfWork.Deliveries.GetDeliveryById(viewModel.Id);

                deliveryType.Name = viewModel.Name;
                deliveryType.Price = viewModel.Price;

                _unitOfWork.Complete();

                return RedirectToAction("Deliveries");
            }
            return View(viewModel);
        }

        public ActionResult RemoveDeliveryType(int deliveryId)
        {
            _unitOfWork.Deliveries.RemoveDeliveryType(deliveryId);

            _unitOfWork.Complete();

            return RedirectToAction("Deliveries");
        }
    }
}