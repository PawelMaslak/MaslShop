using Maslshop.Models.Core;
using Maslshop.Repositories;

namespace Maslshop.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public IRoleRepository Roles { get; private set; }
        public IAdminRepository Admin { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IFileRepository File { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IDeliveryRepository Deliveries { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Roles = new RoleRepository(context);
            Admin = new AdminRepository(context);
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            File = new FileRepository(context);
            Orders = new OrderRepository(context);
            Deliveries = new DeliveryRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}