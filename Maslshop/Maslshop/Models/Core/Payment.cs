using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class Payment
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole nazwa nie może być puste")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}