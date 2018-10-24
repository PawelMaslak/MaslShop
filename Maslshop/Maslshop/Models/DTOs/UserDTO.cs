using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(100)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "User's Role")]
        public string Role_Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        [RegularExpression("([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? [0-9][A-Za-z]{2}|[Gg][Ii][Rr] 0[Aa]{2})", ErrorMessage = "Enter valid postcode with space between inward and outward parts")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Heading { get; set; }

        public virtual DateTime? RegistrationDate { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}