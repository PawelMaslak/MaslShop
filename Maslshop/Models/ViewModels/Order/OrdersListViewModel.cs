using Maslshop.Models.Core;
using Maslshop.Models.DTOs;
using System.Collections.Generic;

namespace Maslshop.Models.ViewModels.Order
{
    public class OrdersListViewModel
    {
        public string SearchTerm { get; set; }

        public string Heading { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }

        public IEnumerable<Core.Order> OrdersList { get; set; }

        public IEnumerable<Core.Delivery> Deliveries { get; set; }

        public IEnumerable<Payment> Payments { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        public IEnumerable<OrderStatus> OrderStats { get; set; }

        public IEnumerable<UserDTO> Users { get; set; }
    }
}