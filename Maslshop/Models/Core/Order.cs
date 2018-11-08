using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string City { get; set; }

        public double OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public IEnumerable<Delivery> Deliveries { get; set; }

        [Required]
        public int DeliveryId { get; set; }

        [Required]
        public int OrderStatusId { get; set; }

        [Required]
        public int PaymentTypeId { get; set; }

        public IEnumerable<OrderStatus> OrderStates { get; set; }
    }
}