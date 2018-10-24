using System.Collections.Generic;
using Maslshop.Models.DTOs;

namespace Maslshop.Models.ViewModels.Users
{
    public class UserListViewModel
    {
        public IEnumerable<UserDTO> Users { get; set; }

        public string SearchTerm { get; set; }
    }
}