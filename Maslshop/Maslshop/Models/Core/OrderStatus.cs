using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class OrderStatus
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }
    }
}