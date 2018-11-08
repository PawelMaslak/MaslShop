using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Delivery;
using System.Collections.Generic;
using System.Linq;

namespace Maslshop.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ApplicationDbContext _context;

        public DeliveryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Delivery> GetDeliveriesOptionsList()
        {
            return _context.Deliveries.ToList();
        }

        public Delivery GetDeliveryById(int id)
        {
            return _context.Deliveries.SingleOrDefault(i => i.Id == id);
        }

        public void AddDeliveryType(DeliveryFormViewModel viewModel)
        {
            _context.Deliveries.Add(new Delivery()
            {
                Name = viewModel.Name,
                Price = viewModel.Price
            });
        }

        public void RemoveDeliveryType(int id)
        {
            _context.Deliveries.Remove(GetDeliveryById(id));
        }
    }
}