using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Maslshop.Models.Core;

namespace Maslshop.Models.ViewModels.Product
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name field  cannot be empty")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price field cannot be empty")]
        [Range(Double.Epsilon, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Stock Amount field cannot be empty")]
        [Range(Double.Epsilon, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        [Display(Name = "Stock Amount")]
        public int StockAmount { get; set; }

        [Required(ErrorMessage = "Category field cannot be empty")]
        [Display(Name = "Category")]
        public int Category { get; set; }

        [Required(ErrorMessage = "Description field cannot be empty")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Year field cannot be empty")]
        [Range(Double.Epsilon, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Dimensions field cannot be empty")]
        [Display(Name = "Dimensions")]
        public string Dimensions { get; set; }

        [Required(ErrorMessage = "Manufacturer field cannot be empty")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        public string Heading { get; set; }

        public IEnumerable<Core.Category> Categories { get; set; }

        public List<HttpPostedFileBase> UploadedFiles { get; set; }

        public List<File> Products { get; set; }
    }
}