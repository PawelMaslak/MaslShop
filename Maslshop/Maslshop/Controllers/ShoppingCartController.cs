using Maslshop.Models.Core;
using Maslshop.Persistence;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Maslshop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Product GetProduct(int id)
        {
            return _unitOfWork.Product.GetProductById(id);
        }

        public ActionResult AddToCart(int id)
        {
            ShoppingCartId = GetCartId();

            var product = GetProduct(id);

            if (Session[ShoppingCartId] == null)
            {
                var cart = new List<CartItem>
                {
                    new CartItem()
                    {
                        ProductId = id,
                        Product = product,
                        Quantity = 1,
                        DateCreated = DateTime.Now
                    }
                };

                Session[ShoppingCartId] = cart;
            }
            else
            {
                var cart = (List<CartItem>)Session[ShoppingCartId];

                var index = IsExist(id);

                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem()
                    {
                        ProductId = id,
                        Product = product,
                        Quantity = 1,
                        DateCreated = DateTime.Now
                    });
                }
                Session[ShoppingCartId] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            ShoppingCartId = GetCartId();

            var cart = (List<CartItem>)Session[ShoppingCartId];

            var index = IsExist(id);
            cart.RemoveAt(index);
            Session[ShoppingCartId] = cart;

            if (cart.Count < 1)
            {
                Session.RemoveAll();

                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "ShoppingCart");
        }

        private int IsExist(int id)
        {
            var cart = (List<CartItem>)Session[ShoppingCartId];

            for (var i = 0; i < cart.Count; i++)
                if (cart[i].Product.Id == id)
                {
                    return i;
                }
            return -1;
        }

        public string GetCartId()
        {
            if (HttpContext.Session[CartSessionKey] == null || HttpContext.Session[CartSessionKey].ToString() != HttpContext.User.Identity.Name)
            {
                HttpContext.Session[CartSessionKey] = HttpContext.User.Identity.Name;
            }

            return HttpContext.Session[CartSessionKey].ToString();
        }

        public ActionResult Index()
        {
            var basket = (List<CartItem>)Session[System.Web.HttpContext.Current.User.Identity.Name];

            if (basket != null)
            {
                return View();
            }
                return View("Empty Basket");
        }
    }
}