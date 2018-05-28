using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels
{
    public class RoleFormViewModel
    {
        [Required]
        public string RoleName { get; set; }

        public string Heading { get; set; }

        public string RoleId { get; set; }
    }
}