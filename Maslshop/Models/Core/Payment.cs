using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.Core
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}