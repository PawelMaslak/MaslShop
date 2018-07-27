using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Order;
using Maslshop.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            var thisUser = _unitOfWork.Admin.GetUserById(HttpContext.User.Identity.GetUserId());

            var viewModel = new OrderFormViewModel()
            {
                Heading = "Zamówienie",
                Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList(),
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

            var orderStatus = _unitOfWork.Orders.GetOrderStatusById(order.OrderStatusId);

            List<OrderDetail> orderDetails = order.OrderDetails;

            List<Product> orderedProducts = new List<Product>();

            foreach (var product in orderDetails)
            {
                var singleProductName = _unitOfWork.Product.GetProductById(product.ProductId);

                orderedProducts.Add(singleProductName);
            }

            string productsList = string.Join(", ", orderedProducts.Select(i => i.Name).ToArray());

            using (MailMessage confrimationEmail = new MailMessage("team.maslshop@gmail.com", user.Email))
            {
                confrimationEmail.Subject = "Potwierdzenie zamówienia nr" + order.OrderId;
                string body = "Dzień dobry " + user.Name + "!";
                body += "<br/ > <br/ >Dziękujemy za złożenie zamówienia. Poinformujemy Cię o zmianie statusu wkrótce.";
                body += "<br/ > <br/ >Obecny status: " + orderStatus.Status;
                body += "<br/ > <br/ >Dane zamówienia znajdziesz poniżej:";
                body += "<br/ > <br/ >Numer zamówienia: " + order.OrderId + "<br/ > Adres wysyłki: " + order.Name + " " + order.Surname + ", " + order.Address + ", " + order.PostCode + " " + order.City + ".";
                body += "<br/ > <br/ >Zamówione produkty: " + productsList;
                body += "<br/ > Kwota zamówienia: " + order.OrderTotal.ToString("C");
                body += "<br/ > <br/ >Pozdrawiamy,";
                body += "<br/ > <br />Zespół Maslshop";
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
    }
}