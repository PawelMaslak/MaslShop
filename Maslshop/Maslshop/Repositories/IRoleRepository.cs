using Maslshop.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Maslshop.Models.ViewModels.Roles;

namespace Maslshop.Repositories
{
    public interface IRoleRepository
    {
        void CreateRole(RoleFormViewModel viewModel);
        void DeleteRole(string roleId);
        IdentityRole GetRole(string roleId);
        IEnumerable<IdentityRole> GetRolesList();
        List<IdentityRole> GetRolesListWithoutAdmin();
        string GetUserRoleName(string roleID);
    }
}