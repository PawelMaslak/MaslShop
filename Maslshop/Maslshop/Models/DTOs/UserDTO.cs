using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Display(Name = "User's Role")]
        public string Role_Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [StringLength(6)]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        public string Heading { get; set; }

        public virtual DateTime? RegistrationDate { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}