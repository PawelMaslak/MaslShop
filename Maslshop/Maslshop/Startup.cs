using Maslshop.Models.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Maslshop.Startup))]
namespace Maslshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
            CreateDeliveryTypes();
            CreateOrderStatus();
            CreatePaymentTypes();
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
                    PostCode = "SL2 2BL",
                    City = "Admin",
                    RegistrationDate = DateTime.Now,
                    UserName = "admin@maslshop.com",
                    Email = "admin@maslshop.com",
                    EmailConfirmed = true
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

        private void CreateDeliveryTypes()
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var deliveriesTypesList = _context.Deliveries.ToList();

            if (deliveriesTypesList.Count == 0)
            {
                var deliveryType = new Delivery()
                {
                    Name = "Collection",
                    Price = 0
                };

                _context.Deliveries.Add(deliveryType);

                var deliveryType1 = new Delivery()
                {
                    Name = "Courier",
                    Price = 25
                };

                _context.Deliveries.Add(deliveryType1);

                var deliveryType2 = new Delivery()
                {
                    Name = "Royal Mail Parcel",
                    Price = 12
                };

                _context.Deliveries.Add(deliveryType2);

                _context.SaveChanges();
            }
        }

        private void CreateOrderStatus()
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var statusList = _context.OrderStates.ToList();

            if (statusList.Count == 0)
            {
                var orderStatus = new OrderStatus()
                {
                    Status = "Pending"
                };

                _context.OrderStates.Add(orderStatus);

                var orderStatus2 = new OrderStatus()
                {
                    Status = "Payment Received"
                };

                _context.OrderStates.Add(orderStatus2);

                var orderStatus3 = new OrderStatus()
                {
                    Status = "Dispatched"
                };

                _context.OrderStates.Add(orderStatus3);

                var orderStatus4 = new OrderStatus()
                {
                    Status = "Delivered"
                };

                _context.OrderStates.Add(orderStatus4);

                var orderStatus5 = new OrderStatus()
                {
                    Status = "Cancelled"
                };

                _context.OrderStates.Add(orderStatus5);

                _context.SaveChanges();
            }
        }

        private void CreatePaymentTypes()
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            var paymentTypesList = _context.Payments.ToList();

            if (paymentTypesList.Count == 0)
            {
                var paymentType = new Payment()
                {
                    Name = "Pay On Order"
                };

                _context.Payments.Add(paymentType);

                var paymentType2 = new Payment()
                {
                    Name = "Pay On Delivery"
                };

                _context.Payments.Add(paymentType2);

                _context.SaveChanges();
            }
        }
    }
}
