using Maslshop.Models.ViewModels.Delivery;
using Maslshop.Persistence;
using System.Web.Mvc;

namespace Maslshop.Controllers
{
    [Authorize(Roles = "Administrator")]
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
                Heading = "Maslshop - Deliveries Panel",
                Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList(),
            };

            return View(viewModel);
        }

        
        public ActionResult AddDeliveryType()
        {
            var viewModel = new DeliveryFormViewModel()
            {
                Heading = "Maslshop - Add New Delivery Type",
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                Heading = "Maslshop - Edit Delivery Type",
                Id = deliveryId,
                Name = deliveryType.Name,
                Price = deliveryType.Price
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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