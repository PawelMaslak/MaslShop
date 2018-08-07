using Maslshop.Models.Core;
using System;
using System.Collections.Generic;

namespace Maslshop.Models.ViewModels.Order
{
    public class OrderViewModel
    {

        public string Heading { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public int OrderId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public double OrderTotal { get; set; }

        public DateTime? OrderDate { get; set; }

        public string OrderStatusName { get; set; }

        public int OrderStatusId { get; set; }

        public string PaymentTypeName { get; set; }

        public string DeliveryTypeName { get; set; }

        public double DeliveryPrice { get; set; }

        public int StatusId { get; set; }

        public int Quantity { get; set; }

        public string DeliveryAddress { get; set; }

        public string DeliveryPostCode { get; set; }

        public string DeliveryCity { get; set; }

        public string BillingAddress { get; set; }

        public string BillingName { get; set; }

        public string BillingSurname { get; set; }

        public string BillingCity { get; set; }

        public string BillingPostCode { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        public IEnumerable<Core.Delivery> Deliveries { get; set; }

        public IEnumerable<Core.Payment> Payments { get; set; }

        public IEnumerable<OrderStatus> OrderStats { get; set; }
    }
}