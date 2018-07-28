using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Order;
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
        IEnumerable<OrderViewmodel> GetOrdersList();
        IEnumerable<OrderDetail> GetOrderDetailsList();
        IEnumerable<OrderStatus> GetOrderStatsList();
        IEnumerable<OrderViewmodel> GetSearchedOrders(string query, string searchTerm = null);
    }
}