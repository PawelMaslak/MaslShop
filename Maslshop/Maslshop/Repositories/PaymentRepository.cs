using Maslshop.Models.Core;
using System.Collections.Generic;
using System.Linq;

namespace Maslshop.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetPaymentTypes()
        {
            return _context.Payments.ToList();
        }

        public Payment GetPaymentTypeById(int id)
        {
            return _context.Payments.SingleOrDefault(i => i.Id == id);
        }
    }
}