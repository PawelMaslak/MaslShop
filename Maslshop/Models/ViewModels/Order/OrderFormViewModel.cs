using Maslshop.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels.Order
{
    public class OrderFormViewModel
    {
        public string Heading { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(100)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        [RegularExpression("([A-Za-z][A-Ha-hJ-Yj-y]?[0-9][A-Za-z0-9]? [0-9][A-Za-z]{2}|[Gg][Ii][Rr] 0[Aa]{2})", ErrorMessage = "Enter valid postcode with space between inward and outward parts")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        public double OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }

        public IEnumerable<Core.Delivery> Deliveries { get; set; }

        public IEnumerable<Core.Payment> Payments { get; set; }

        public IEnumerable<OrderStatus> OrderStats { get; set; }

        public ApplicationUser ThisUser { get; set; }

        [Required(ErrorMessage = "Choose delivery type")]
        [Display(Name = "Delivery Type")]
        public int DeliveryId { get; set; }

        [Required(ErrorMessage = "Choose payment type")]
        [Display(Name = "Payment Type")]
        public int PaymentId { get; set; }

        public int StatusId { get; set; }

        public string OrderStatusName { get; set; }

        public bool IsTrue => true;

        [Required]
        [Display(Name = "Accept Terms & Conditions")]
        [Compare(nameof(IsTrue), ErrorMessage = "Please accept Terms & Conditions")]
        public bool TermsAndConditionsAccepted { get; set; }
    }
}