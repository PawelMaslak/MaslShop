using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Order;
using Maslshop.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        //public string ShoppingCartId { get; set; }

        //public const string CartSessionKey = "CartId";

        public ActionResult Checkout()
        {
            var thisUser = _unitOfWork.Admin.GetUserById(HttpContext.User.Identity.GetUserId());

            var viewModel = new OrderFormViewModel()
            {
                Heading = "Zamówienie",
                Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList(),
                Payments = _unitOfWork.Payments.GetPaymentTypes(),
                ThisUser = thisUser,
                Name = thisUser.Name,
                Surname = thisUser.Surname,
                Address = thisUser.Address,
                City = thisUser.City,
                PostCode = thisUser.PostCode,
                Email = thisUser.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Checkout(OrderFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList();
                viewModel.ThisUser = _unitOfWork.Admin.GetUserById(HttpContext.User.Identity.GetUserId());
                viewModel.Payments = _unitOfWork.Payments.GetPaymentTypes();
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
                OrderStatusId = 1,
                PaymentTypeId = viewModel.PaymentId
            };

            _unitOfWork.Orders.CreateOrder(order);

            SendConfirmationEmail(order);

            Session.RemoveAll();

            return RedirectToAction("CheckoutComplete");
        }

        public ActionResult CheckoutComplete()
        {
            return View();
        }

        private void SendConfirmationEmail(Order order)
        {
            var user = _unitOfWork.Admin.GetUserById(HttpContext.User.Identity.GetUserId());

            using (MailMessage confrimationEmail = new MailMessage("team.maslshop@gmail.com", user.Email))
            {
                confrimationEmail.Subject = "Maslshop - Potwierdzenie zamówienia nr " + order.OrderId;
                string body = CreateBody(order);
                confrimationEmail.Body = body;
                confrimationEmail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    EnableSsl = true
                };
                NetworkCredential networkCred = new NetworkCredential("team.maslshop@gmail.com", "Maslshop123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCred;
                smtp.Port = 587;
                smtp.Send(confrimationEmail);
            }
        }

        private string CreateBody(Order order)
        {
            List<OrderDetail> orderDetails = order.OrderDetails;

            var deliveryType = _unitOfWork.Deliveries.GetDeliveryById(order.DeliveryId);

            var paymentType = _unitOfWork.Payments.GetPaymentTypeById(order.PaymentTypeId);

            var orderStatus = _unitOfWork.Orders.GetOrderStatusById(order.OrderStatusId);

            string body = string.Empty;

            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Emails/ConfirmationEmail.cshtml")))
            {
                body = reader.ReadToEnd();
            }

            StringBuilder productNames = new StringBuilder();
            
            foreach (var product in orderDetails)
            {
                var singleProduct = _unitOfWork.Product.GetProductById(product.ProductId);

                productNames.AppendFormat("<br/>{0}", singleProduct.Name);
            }

            StringBuilder productsQuantity = new StringBuilder();

            foreach (var product in orderDetails)
            {
                productsQuantity.AppendFormat("<br/>{0}", product.Quantity);
            }

            StringBuilder productsSubtotal = new StringBuilder();

            foreach (var product in orderDetails)
            {
                productsSubtotal.AppendFormat("<br/>{0:C}", (product.Quantity * product.Price));
            }

            body = body.Replace("{pName}", productNames.ToString());
            body = body.Replace("{pQuantity}", productsQuantity.ToString());
            body = body.Replace("{pSubtotal}", productsSubtotal.ToString());
            body = body.Replace("{pDeliveryPrice}", deliveryType.Price.ToString("C"));
            body = body.Replace("{pDeliveryType}", deliveryType.Name);
            body = body.Replace("{pTotal}", order.OrderTotal.ToString("C"));
            body = body.Replace("{userName}", order.Name);
            body = body.Replace("{orderId}", order.OrderId.ToString());
            body = body.Replace("{orderStatus}", orderStatus.Status);
            body = body.Replace("{userSurname}", order.Surname);
            body = body.Replace("{userAddress}", order.Address);
            body = body.Replace("{userPostCode}", order.PostCode);
            body = body.Replace("{userCity}", order.City);
            body = body.Replace("{pPaymentType}", paymentType.Name);

            return body;
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult ViewOrders(string query = null)
        {
            if (query != null)
            {
                var viewmodel = new OrdersListViewModel()
                {
                    Heading = "List wyszukanych zamówień",
                    Orders = _unitOfWork.Orders.GetSearchedOrders(query),
                    Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList(),
                    Payments = _unitOfWork.Payments.GetPaymentTypes(),
                    OrderDetails = _unitOfWork.Orders.GetOrderDetailsList(),
                    OrderStats = _unitOfWork.Orders.GetOrderStatsList(),
                    SearchTerm = query
                };

                return View(viewmodel);
            }
            var viewModel = new OrdersListViewModel()
            {
                Heading = "List zamówień",
                Orders = _unitOfWork.Orders.GetOrdersList(),
                Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList(),
                Payments = _unitOfWork.Payments.GetPaymentTypes(),
                OrderDetails = _unitOfWork.Orders.GetOrderDetailsList(),
                OrderStats = _unitOfWork.Orders.GetOrderStatsList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SearchOrder(OrdersListViewModel viewModel)
        {
            return RedirectToAction("ViewOrders", new { query = viewModel.SearchTerm });
        }
    }
}