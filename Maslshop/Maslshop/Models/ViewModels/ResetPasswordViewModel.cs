using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Hasło {0} musi miec co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}