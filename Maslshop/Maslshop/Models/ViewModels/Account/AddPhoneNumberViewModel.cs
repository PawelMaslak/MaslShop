using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels.Account
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}