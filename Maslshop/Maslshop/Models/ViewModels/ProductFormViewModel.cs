using Maslshop.Models.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Maslshop.Models.ViewModels
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole nazwa jest wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Cena jest wymagana i nie może być mniejsza od zera.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Podaj ilość produktów.")]
        public int StockAmount { get; set; }

        [Required(ErrorMessage = "Pole Kategoria nie może pozostać puste.")]
        public int Category { get; set; }

        [Required(ErrorMessage = "Pole opis jest wymagane.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole rok jest wymagane.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Pole waga nie może być puste.")]
        public double Weight { get; set; }

        public double PureGoldWeight { get; set; }

        [Required(ErrorMessage = "Pole próba nie może być puste oraz format musi być następujący - eg. 999,9")]
        public double Fineness { get; set; }

        [Required(ErrorMessage = "Podaj wymiary.")]
        public string Dimensions { get; set; }

        [Required(ErrorMessage = "Pole Wytwórca nie może być puste.")]
        public string Manufacturer { get; set; }

        public string Heading { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public List<HttpPostedFileBase> UploadedFiles { get; set; }

        public List<File> Products { get; set; }
    }
}