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
        IEnumerable<OrderViewModel> GetOrdersList();
        IEnumerable<OrderDetail> GetOrderDetailsList();
        IEnumerable<OrderStatus> GetOrderStatsList();
        IEnumerable<OrderViewModel> GetSearchedOrders(string query, string searchTerm = null);
        Order GetOrderById(int id);
        List<OrderDetail> GetOrderDetailsListByOrderId(int id);
        void RemoveOrderDetail(OrderDetail detail);
        OrderDetail SelectOrderDetail(int id);
        Order SelectOrderMatchingOrderDetailId(OrderDetail detail);
        IEnumerable<OrderViewModel> GetUserOrders(string userId);
        IEnumerable<Order> UserOrders(string userId);
    }
}