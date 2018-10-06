using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class Product /*: IValidatableObject*/
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public int Id { get; set; }

        [Required]
        public int StockAmount { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [StringLength(1024)]
        public string Description { get; set; }

        public Category Category { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public virtual DateTime? AddedDate { get; set; }

        public virtual List<File> Files { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Dimensions { get; set; }

        [Required]
        public string Manufacturer { get; set; }
    }
}