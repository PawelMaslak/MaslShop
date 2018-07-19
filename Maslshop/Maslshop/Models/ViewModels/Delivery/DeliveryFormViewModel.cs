using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels.Delivery
{
    public class DeliveryFormViewModel
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole nazwa nie może być puste")]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Proszę podać cenę")]
        public double Price { get; set; }

        public string Heading { get; set; }

        public int Id { get; set; }
    }
}