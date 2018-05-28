using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Maslshop.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Heading { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Hasło {0} musi miec co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required]
        [StringLength(6)]
        [Display(Name = "Kod Pocztowy")]
        public string PostCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Miasto")]
        public string City { get; set; }


        [Display(Name = "Rola")]
        public string Role { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

    }
}