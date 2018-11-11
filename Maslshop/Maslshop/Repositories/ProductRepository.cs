using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Product;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Maslshop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories
                .Where(p => p.Products.Count > 0)
                .ToList();
        }

        public IEnumerable<Product> GetProductsInStockList()
        {
            return _context.Products
                .Include(s => s.Category)
                .Include(s => s.Files).Where(s => s.StockAmount > 0)
                .ToList();
        }

        public CartItem GetProductInTheCart(int id)
        {
            return _context.ShoppingCartItems.SingleOrDefault(c => c.ProductId == id && c.CartId == HttpContext.Current.User.Identity.Name.ToString());
        }

        public IEnumerable<ProductsViewModel> GetSearchedProducts(string query, string searchTerm = null)
        {
            var products = GetProducts();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var term = new SqlParameter("@query", query);

                return _context.Database.SqlQuery<ProductsViewModel>("dbo.FilteredProducts @query", term).ToList();
            }

            return products;
        }

        public IEnumerable<ProductsViewModel> GetProducts()
        {
            var products = _context.Database.SqlQuery<ProductsViewModel>("dbo.GetProducts").ToList();

            return products;
        }

        public IEnumerable<ProductsViewModel> GetLatestThreeProducts()
        {
            var products = _context.Database.SqlQuery<ProductsViewModel>("dbo.GetLatestThreeProducts").ToList().Take(3);

            return products;
        }

        public Product SelectProductMatchingPhotoId(File photo)
        {
            return _context.Products.Single(i => i.Id == photo.ProductId);
        }

        public Product GetProductById(int id)
        {
            return _context.Products.SingleOrDefault(i => i.Id == id);
        }

        public void DeleteProduct(int id)
        {
            _context.Products.Remove(GetProductById(id));
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

    }
}