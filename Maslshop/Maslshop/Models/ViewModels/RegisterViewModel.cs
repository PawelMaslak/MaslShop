using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Heading { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password {0} has to consist at least of {2} letters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "Enter valid post code - maximum 6 letters")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Post Code")]
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