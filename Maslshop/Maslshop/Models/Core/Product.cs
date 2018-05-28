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

        //Added 2018-03-29

        [Required]
        public int Year { get; set; }

        [Required]
        public string Dimensions { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Files == null)
        //    {
        //        yield return new ValidationResult("Dodaj chociaż jedno zdjęcie!");
        //    }
        //}
    }
}