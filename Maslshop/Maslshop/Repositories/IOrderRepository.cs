using Maslshop.Models.Core;
using System.Collections.Generic;

namespace Maslshop.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        string GetCartId();
        Delivery GetDeliveryTypeById(int id);
        OrderStatus GetOrderStatusById(int id);
        List<OrderDetail> GetOrderedProductsList(int id);
    }
}