using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels.Delivery
{
    public class DeliveryFormViewModel
    {
        [Display(Name = "Delivery Type Name")]
        [Required(ErrorMessage = "Delivery Type field cannot be empty")]
        [StringLength(50, ErrorMessage = "Delivery type name is too long")]
        public string Name { get; set; }

        [Display(Name = "Delivery Type Price")]
        [Required(ErrorMessage = "Price field cannot be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger or equal to 0")]
        public double Price { get; set; }

        public string Heading { get; set; }

        public int Id { get; set; }
    }
}