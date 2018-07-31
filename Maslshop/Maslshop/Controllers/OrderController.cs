﻿using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Order;
using Maslshop.Persistence;
using Microsoft.AspNet.Identity;
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

            if (order.OrderStatusId != 1)
            {
                user.Email = order.Email;
            }

            using (MailMessage confrimationEmail = new MailMessage("team.maslshop@gmail.com", user.Email))
            {
                switch (order.OrderStatusId)
                {
                    case 1: // Order created
                        confrimationEmail.Subject = "Maslshop - Potwierdzenie zamówienia nr " + order.OrderId;
                        string body = CreateBody(order);
                        confrimationEmail.Body = body;
                        break;
                    case 2: // Payment received
                        confrimationEmail.Subject = "Maslshop - Zmiana statusu zamówienia nr " + order.OrderId;
                        string body1 = CreateBody(order);
                        confrimationEmail.Body = body1;
                        break;
                    case 3: // Order shipped
                        confrimationEmail.Subject = "Maslshop - TEST TEST Doręczono zamówienie nr " + order.OrderId;
                        string body2 = CreateBody(order);
                        confrimationEmail.Body = body2;
                        break;
                        //case 4: // Order delivered
                        //    break;
                        //case 5: // Order cancelled 
                        //    break;
                }
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

        //private void SendOrderUpdateEmail(Order order)
        //{
        //    var userEmail = order.Email;

        //    using (MailMessage confrimationEmail = new MailMessage("team.maslshop@gmail.com", userEmail))
        //    {

        //        confrimationEmail.Subject = "Maslshop - Zmiana statusu zamówienia nr " + order.OrderId;
        //        string body = CreateBodyUpdateEmail(order);
        //        confrimationEmail.Body = body;
        //        confrimationEmail.IsBodyHtml = true;
        //        SmtpClient smtp = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            EnableSsl = true
        //        };
        //        NetworkCredential networkCred = new NetworkCredential("team.maslshop@gmail.com", "Maslshop123");
        //        smtp.UseDefaultCredentials = true;
        //        smtp.Credentials = networkCred;
        //        smtp.Port = 587;
        //        smtp.Send(confrimationEmail);
        //    }
        //}

        private string CreateBody(Order order)
        {
            var orderDetails = _unitOfWork.Orders.GetOrderDetailsListByOrderId(order.OrderId);

            var deliveryType = _unitOfWork.Deliveries.GetDeliveryById(order.DeliveryId);

            var paymentType = _unitOfWork.Payments.GetPaymentTypeById(order.PaymentTypeId);

            var orderStatus = _unitOfWork.Orders.GetOrderStatusById(order.OrderStatusId);

            string body = string.Empty;

            switch (order.OrderStatusId)
            {
                case 1:
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Emails/ConfirmationEmail.cshtml")))
                    {
                        body = reader.ReadToEnd();
                    }
                    break;
                case 2:
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Emails/ChangeStatusConfirmationEmail.cshtml")))
                    {
                        body = reader.ReadToEnd();
                    }
                    break;
                case 3:
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Emails/ChangeStatusConfirmationEmail.cshtml")))
                    {
                        body = reader.ReadToEnd();
                    }
                    break;
                case 4:

                    break;
                case 5:

                    break;
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

        //private string CreateBodyUpdateEmail(Order order)
        //{
        //    var orderDetails = _unitOfWork.Orders.GetOrderDetailsListByOrderId(order.OrderId);

        //    var deliveryType = _unitOfWork.Deliveries.GetDeliveryById(order.DeliveryId);

        //    var paymentType = _unitOfWork.Payments.GetPaymentTypeById(order.PaymentTypeId);

        //    var orderStatus = _unitOfWork.Orders.GetOrderStatusById(order.OrderStatusId);

        //    string body = string.Empty;

        //    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Emails/ChangeStatusConfirmationEmail.cshtml")))
        //    {
        //        body = reader.ReadToEnd();
        //    }

        //    StringBuilder productNames = new StringBuilder();

        //    foreach (var product in orderDetails)
        //    {
        //        var singleProduct = _unitOfWork.Product.GetProductById(product.ProductId);

        //        productNames.AppendFormat("<br/>{0}", singleProduct.Name);
        //    }

        //    StringBuilder productsQuantity = new StringBuilder();

        //    foreach (var product in orderDetails)
        //    {
        //        productsQuantity.AppendFormat("<br/>{0}", product.Quantity);
        //    }

        //    StringBuilder productsSubtotal = new StringBuilder();

        //    foreach (var product in orderDetails)
        //    {
        //        productsSubtotal.AppendFormat("<br/>{0:C}", (product.Quantity * product.Price));
        //    }

        //    body = body.Replace("{pName}", productNames.ToString());
        //    body = body.Replace("{pQuantity}", productsQuantity.ToString());
        //    body = body.Replace("{pSubtotal}", productsSubtotal.ToString());
        //    body = body.Replace("{pDeliveryPrice}", deliveryType.Price.ToString("C"));
        //    body = body.Replace("{pDeliveryType}", deliveryType.Name);
        //    body = body.Replace("{pTotal}", order.OrderTotal.ToString("C"));
        //    body = body.Replace("{userName}", order.Name);
        //    body = body.Replace("{orderId}", order.OrderId.ToString());
        //    body = body.Replace("{orderStatus}", orderStatus.Status);
        //    body = body.Replace("{userSurname}", order.Surname);
        //    body = body.Replace("{userAddress}", order.Address);
        //    body = body.Replace("{userPostCode}", order.PostCode);
        //    body = body.Replace("{userCity}", order.City);
        //    body = body.Replace("{pPaymentType}", paymentType.Name);

        //    return body;
        //}

        [Authorize(Roles = "Administrator")]
        public ActionResult ViewOrders(string query = null)
        {
            if (query != null)
            {
                var viewmodel = new OrdersListViewModel()
                {
                    Heading = "Lista wyszukanych zamówień",
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
                Heading = "Lista zamówień",
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

        public ActionResult EditOrder(int orderId)
        {
            var order = _unitOfWork.Orders.GetOrderById(orderId);

            var statusName = _unitOfWork.Orders.GetOrderStatusById(order.OrderStatusId).Status;

            var viewModel = new EditOrderFormViewModel()
            {
                OrderId = orderId,
                Heading = "Edytuj zamówienie",
                Name = order.Name,
                Surname = order.Surname,
                Address = order.Address,
                PostCode = order.PostCode,
                City = order.City,
                DeliveryId = order.DeliveryId,
                StatusId = order.OrderStatusId,
                PaymentId = order.PaymentTypeId,
                OrderStatusName = statusName,
                OrderStats = _unitOfWork.Orders.GetOrderStatsList(),
                Payments = _unitOfWork.Payments.GetPaymentTypes(),
                Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList(),
                OrderDetails = _unitOfWork.Orders.GetOrderDetailsListByOrderId(order.OrderId)
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditOrder(EditOrderFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Deliveries = _unitOfWork.Deliveries.GetDeliveriesOptionsList();
                viewModel.Payments = _unitOfWork.Payments.GetPaymentTypes();
                viewModel.OrderStats = _unitOfWork.Orders.GetOrderStatsList();

                return View(viewModel);
            }

            var order = _unitOfWork.Orders.GetOrderById(viewModel.OrderId);

            var delivery = _unitOfWork.Deliveries.GetDeliveryById(order.DeliveryId);

            var orderDetails = _unitOfWork.Orders.GetOrderDetailsListByOrderId(viewModel.OrderId);

            order.OrderTotal = 0;

            foreach (var detail in orderDetails)
            {
                order.OrderTotal += (detail.Quantity * detail.Price);
            }

            var total = order.OrderTotal + delivery.Price;

            order.Name = viewModel.Name;
            order.Surname = viewModel.Surname;
            order.Address = viewModel.Address;
            order.PostCode = viewModel.PostCode;
            order.City = viewModel.City;
            order.OrderTotal = total;

            if (viewModel.StatusId != order.OrderStatusId)
            {
                order.OrderStatusId = viewModel.StatusId;
                SendConfirmationEmail(order);  
            }

            _unitOfWork.Complete();

            return RedirectToAction("ViewOrders");
        }

        public ActionResult DeleteOrderDetailEntry(int orderDetailId)
        {

            var detail = _unitOfWork.Orders.SelectOrderDetail(orderDetailId);

            var order = _unitOfWork.Orders.SelectOrderMatchingOrderDetailId(detail);

            _unitOfWork.Orders.RemoveOrderDetail(detail);

            _unitOfWork.Complete();

            return RedirectToAction("EditOrder", new {orderId = order.OrderId});
        }

        //[HttpPost]
        //public ActionResult UpdateOrderStatus(EditOrderStatusViewModel viewModel)
        //{
        //    var order = _unitOfWork.Orders.GetOrderById(viewModel.OrderId);

        //    var errors = ModelState.Values.Select(v => v.Errors);

        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOrders");
        //    }

        //    order.OrderStatusId = viewModel.StatusId;

        //    var orders = _unitOfWork.Orders.GetOrdersList();

        //    foreach (var order in orders)
        //    {
        //        var currentOrder = _unitOfWork.Orders.GetOrderById(order.OrderId);

        //        var getOrderStatusId = _unitOfWork.Orders.GetOrderStatusById(order.OrderId);

        //        currentOrder.OrderStatusId = viewModel.OrderStatusId;

        //        _unitOfWork.Complete();
        //    }

        //    return RedirectToAction("ViewOrders");
        //}
    }
}