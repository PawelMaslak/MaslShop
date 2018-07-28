using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Maslshop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateOrder(Order order)
        {
            ShoppingCartId = GetCartId();

            order.OrderDate = DateTime.Now;

            order.UserName = HttpContext.Current.User.Identity.Name;

            _context.Orders.Add(order);

            var deliveryType = GetDeliveryTypeById(order.DeliveryId); 

            var shoppingCartItems = (List<CartItem>)HttpContext.Current.Session[ShoppingCartId];

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    OrderId = order.OrderId
                };
                order.OrderTotal += (item.Quantity * item.Product.Price);

                _context.OrderDetails.Add(orderDetail);
            }

            var total = order.OrderTotal + deliveryType.Price;

            order.OrderTotal = total;

            _context.SaveChanges();
        }

        public Delivery GetDeliveryTypeById(int id)
        {
            return _context.Deliveries.SingleOrDefault(i => i.Id == id);
        }

        public OrderStatus GetOrderStatusById(int id)
        {
            return _context.OrderStates.SingleOrDefault(i => i.Id == id);
        }

        public List<OrderDetail> GetOrderedProductsList(int id)
        {
            return _context.OrderDetails.Where(i => i.OrderId == id).ToList();
        }

        public string GetCartId()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null || HttpContext.Current.Session[CartSessionKey].ToString() != HttpContext.Current.User.Identity.Name)
            {
                HttpContext.Current.Session[CartSessionKey] = HttpContext.Current.User.Identity.Name;
            }

            return HttpContext.Current.Session[CartSessionKey].ToString();
        }

        public IEnumerable<OrderViewmodel> GetOrdersList()
        {
            var orders = _context.Database.SqlQuery<OrderViewmodel>("dbo.GetOrders").ToList();

            return orders;
        }

        public IEnumerable<OrderViewmodel> GetSearchedOrders(string query, string searchTerm = null)
        {
            var products = GetOrdersList();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var term = new SqlParameter("@query", query);

                return _context.Database.SqlQuery<OrderViewmodel>("dbo.FilteredOrders @query", term).ToList();
            }

            return products;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsList()
        {
            return _context.OrderDetails.ToList();
        }

        public IEnumerable<OrderStatus> GetOrderStatsList()
        {
            return _context.OrderStates.ToList();
        }
    }
}