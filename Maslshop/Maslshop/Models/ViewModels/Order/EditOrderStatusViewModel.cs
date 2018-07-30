using Maslshop.Models.Core;
using System.Collections.Generic;

namespace Maslshop.Models.ViewModels.Order
{
    public class EditOrderStatusViewModel
    {
        public int OrderId { get; set; }

        public int OrderStatusId { get; set; }

        public IEnumerable<OrderStatus> OrderStats { get; set; }
    }
}