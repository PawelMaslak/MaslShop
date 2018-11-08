using Maslshop.Models.Core;
using Maslshop.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Maslshop.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserDTO> SelectUsersWithUserRole(List<ApplicationUser> users, string searchTerm = null)
        {
            var usersWithRole = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Address = user.Address,
                PostCode = user.PostCode,
                Role_Name = GetUserRole(user),
                City = user.City,
                Name = user.Name,
                Surname = user.Surname,
                RegistrationDate = user.RegistrationDate
            }).Where(u => u.Role_Name != "Administrator")
                .OrderByDescending(u => u.RegistrationDate).ToList();

            if (!String.IsNullOrEmpty(searchTerm))
            {
                return usersWithRole
                    .Where(u =>
                        u.Name.ToLower().Contains(searchTerm.ToLower()) ||
                        u.Surname.ToLower().Contains(searchTerm.ToLower()) ||
                        u.Email.ToLower().Contains(searchTerm.ToLower()) ||
                        u.Address.ToLower().Contains(searchTerm.ToLower()) ||
                        u.City.ToLower().Contains(searchTerm.ToLower()) ||
                        u.PostCode.Contains(searchTerm));
            }

            return usersWithRole;
        }

 
        public IEnumerable<UserDTO> SelectUsers(string query, string searchTerm = null)
        {
            var users = GetUsersWithoutAdmin();

            if (!String.IsNullOrWhiteSpace(query))
            {
                var term = new SqlParameter("@query", query);

                return _context.Database.SqlQuery<UserDTO>("dbo.FilteredUsers @query", term).ToList();
            }

            return users;
        }

        public IEnumerable<UserDTO> GetUsersWithoutAdmin()
        {
            var parameter = new SqlParameter("@P", "Administrator");

            var users = _context.Database.SqlQuery<UserDTO>("dbo.GetUsersWithoutAdmin @P", parameter);

            return users;
        }

        public List<ApplicationUser> GetUsersList()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);;
        }

        private string GetUserRole(ApplicationUser user)
        {
            var userRole = user.Roles.FirstOrDefault();
            var userRoleName = string.Empty;

            if (userRole != null)
            {
                var roleID = userRole.RoleId;
                userRoleName = _context.Roles.Single(r => r.Id == roleID).Name;
            }

            return userRoleName;
        }
    }
}