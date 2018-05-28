using Maslshop.Models.Core;
using Maslshop.Models.DTOs;
using System.Collections.Generic;

namespace Maslshop.Repositories
{
    public interface IAdminRepository
    {
        List<ApplicationUser> GetUsersList();
        IEnumerable<UserDTO> SelectUsersWithUserRole(List<ApplicationUser> users, string searchTerm = null);
        IEnumerable<UserDTO> GetUsersWithoutAdmin();
        IEnumerable<UserDTO> SelectUsers(string query, string searchTerm = null);
        ApplicationUser GetUserById(string userId);
    }
}