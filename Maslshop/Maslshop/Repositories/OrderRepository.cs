using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Maslshop.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        private string ShoppingCartId { get; set; }

        private const string CartSessionKey = "CartId";

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderDetail SelectOrderDetail(int id)
        {
            return _context.OrderDetails.Single(i => i.OrderDetailId == id);
        }

        public void RemoveOrderDetail(OrderDetail detail)
        {
            _context.OrderDetails.Remove(detail);
        }

        public Order SelectOrderMatchingOrderDetailId(OrderDetail detail)
        {
            return _context.Orders.SingleOrDefault(i => i.OrderId == detail.OrderId);
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
                var checkedProduct = _context.Products.SingleOrDefault(i => i.Id == item.ProductId);

                var orderDetail = new OrderDetail()
                {
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                    OrderId = order.OrderId
                };

                if (orderDetail.Quantity > checkedProduct.StockAmount)
                {
                    orderDetail.Quantity = checkedProduct.StockAmount;
                }

                order.OrderTotal += (orderDetail.Quantity * item.Product.Price);

                _context.OrderDetails.Add(orderDetail);
            }

            _context.SaveChanges();

            var orderDetailsList = _context.OrderDetails.Where(i => i.OrderId == order.OrderId).ToList();

            foreach (var orderedProduct in orderDetailsList)
            {
                var product = _context.Products.SingleOrDefault(i => i.Id == orderedProduct.ProductId);

                product.StockAmount = product.StockAmount - orderedProduct.Quantity;

                _context.SaveChanges();
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

        public IEnumerable<OrderViewModel> GetOrdersList()
        {
            var orders = _context.Database.SqlQuery<OrderViewModel>("dbo.GetOrders").ToList();

            return orders;
        }

        public IEnumerable<OrderViewModel> GetUserOrders(string userId)
        {
            var parameter = new SqlParameter("@P", userId);

            var orders = _context.Database.SqlQuery<OrderViewModel>("dbo.GetUserOrders @P", parameter);

            return orders;
        }

        public IEnumerable<Order> UserOrders(string userId)
        {
            return _context.Orders.Where(i => i.UserId == userId);
        }

        public IEnumerable<OrderViewModel> GetSearchedOrders(string query, string searchTerm = null)
        {
            var orders = GetOrdersList();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var term = new SqlParameter("@query", query);

                return _context.Database.SqlQuery<OrderViewModel>("dbo.FilteredOrders @query", term).ToList();
            }

            return orders;
        }

        public IEnumerable<OrderDetail> GetOrderDetailsList()
        {
            return _context.OrderDetails.ToList();
        }

        public IEnumerable<OrderStatus> GetOrderStatsList()
        {
            return _context.OrderStates.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.SingleOrDefault(i => i.OrderId == id);
        }

        public List<OrderDetail> GetOrderDetailsListByOrderId(int id)
        {
            return _context.OrderDetails.Where(i => i.OrderId == id).ToList();
        }
    }
}