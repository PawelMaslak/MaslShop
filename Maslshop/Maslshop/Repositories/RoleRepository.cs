using Maslshop.Models.Core;
using Maslshop.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace Maslshop.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<IdentityRole> GetRolesList()
        {
            return _context.Roles.ToList();
        }

        public IdentityRole GetRole(string roleId)
        {
            return _context.Roles.SingleOrDefault(r => r.Id == roleId);
        }

        public string GetUserRoleName(string roleID)
        {
            return _context.Roles.Single(r => r.Id == roleID).Name;
        }

        public List<IdentityRole> GetRolesListWithoutAdmin()
        {
            return _context.Roles.Where(r => r.Name != "Administrator").ToList();
        }

        public void DeleteRole(string roleId)
        {
            _context.Roles.Remove(GetRole(roleId));
        }

        public void CreateRole(RoleFormViewModel viewModel)
        {
            _context.Roles.Add(new IdentityRole()
            {
                Name = viewModel.RoleName
            });
        }
    }
}