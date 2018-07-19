using Maslshop.Models.DTOs;
using System.Collections.Generic;

namespace Maslshop.Models.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }

        public string SearchTerm { get; set; }
    }
}