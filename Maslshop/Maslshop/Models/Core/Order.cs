using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole imie jest wymagane")]
        [Display(Name= "Imie")]
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

        [Required(ErrorMessage = "Pole email jest wymagane")]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole miasto jest wymagane")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        public double OrderTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public IEnumerable<Delivery> Deliveries { get; set; }

        [Required]
        public int DeliveryId { get; set; }

        [Required]
        public int OrderStatusId { get; set; }

        [Required]
        public int PaymentTypeId { get; set; }

        public IEnumerable<OrderStatus> OrderStates { get; set; }
    }
}