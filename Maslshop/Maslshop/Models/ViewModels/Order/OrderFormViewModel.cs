using Maslshop.Models.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels.Order
{
    public class OrderFormViewModel
    {
        public int OrderId { get; set; }

        public string UserName { get; set; }

        public string Heading { get; set; }

        [Required(ErrorMessage = "Pole imie jest wymagane")]
        [Display(Name = "Imie")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Pole adres jest wymagane")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Pole kod pocztowy jest wymagane")]
        [Display(Name = "Kod Pocztowy")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Pole email pocztowy jest wymagane")]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole miasto jest wymagane")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        public double OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public IEnumerable<Core.Delivery> Deliveries { get; set; }

        public IEnumerable<Core.Payment> Payments { get; set; }

        public ApplicationUser ThisUser { get; set; }

        [Required(ErrorMessage = "Wybierz opcje przesyłki")]
        [Display(Name = "Typ przesyłki")]
        public int DeliveryId { get; set; }

        [Required(ErrorMessage = "Wybierz rodzaj płatności")]
        [Display(Name = "Rodzaj płatności")]
        public int PaymentId { get; set; }

        public bool IsTrue => true;

        [Required]
        [Display(Name = "Akceptuję regulamin")]
        [Compare(nameof(IsTrue), ErrorMessage = "Proszę zaakceptować regulamin sklepu")]
        public bool TermsAndConditionsAccepted { get; set; }
    }
}