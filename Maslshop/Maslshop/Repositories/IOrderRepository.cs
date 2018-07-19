using Maslshop.Models.Core;

namespace Maslshop.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        string GetCartId();
        Delivery GetDeliveryTypeById(int id);
    }
}