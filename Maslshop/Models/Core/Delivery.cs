using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class Delivery
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}