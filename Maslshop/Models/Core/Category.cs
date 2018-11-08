using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}