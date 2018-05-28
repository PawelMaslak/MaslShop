using Maslshop.Models.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Maslshop.Startup))]
namespace Maslshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };

                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    Name = "Admin",
                    Surname = "Admin",
                    Address = "Admin 1/3",
                    PostCode = "00-000",
                    City = "Admin",
                    RegistrationDate = DateTime.Now,
                    UserName = "admin@maslshop.com",
                    Email = "admin@maslshop.com"
                };

                var password = "Admin123";

                var checkUser = userManager.Create(user, password);

                if (checkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Administrator");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };

                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole
                {
                    Name = "Manager"
                };

                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Super User"))
            {
                var role = new IdentityRole
                {
                    Name = "Super User"
                };

                roleManager.Create(role);
            }

        }
    }
}
