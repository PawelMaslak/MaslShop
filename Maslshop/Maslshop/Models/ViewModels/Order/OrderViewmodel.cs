using System;

namespace Maslshop.Models.ViewModels.Order
{
    public class OrderViewmodel
    {
        public string UserName { get; set; }

        public int OrderId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public double OrderTotal { get; set; }

        public DateTime? OrderDate { get; set; }

        public string OrderStatusName { get; set; }

        public string PaymentTypeName { get; set; }

        public string DeliveryTypeName { get; set; }

        public int Quantity { get; set; }
    }
}