using Maslshop.Models.Core;
using System.Collections.Generic;

namespace Maslshop.Repositories
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetPaymentTypes();
        Payment GetPaymentTypeById(int id);
    }
}