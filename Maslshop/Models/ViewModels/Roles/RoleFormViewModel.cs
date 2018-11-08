using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels.Roles
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "Role Name field cannot be empty")]
        public string RoleName { get; set; }

        public string Heading { get; set; }

        public string RoleId { get; set; }
    }
}