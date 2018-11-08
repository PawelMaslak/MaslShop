using System.Collections.Generic;

namespace Maslshop.Models.ViewModels.Delivery
{
    public class DeliveriesViewModel
    {
        public string Heading { get; set; }

        public IEnumerable<Core.Delivery> Deliveries { get; set; }
    }
}