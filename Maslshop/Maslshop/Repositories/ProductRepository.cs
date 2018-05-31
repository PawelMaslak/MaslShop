using Maslshop.Models.Core;
using Maslshop.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

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
            return _context.Categories.ToList();
        }



        public IEnumerable<Product> GetProductsInStockList()
        {
            return _context.Products
                .Include(s => s.Category)
                .Include(s => s.Files).Where(s => s.StockAmount > 0)
                .ToList();
        }

        //public IEnumerable<Product> GetSearchedProducts(string searchTerm = null)
        //{
        //    var products = GetProductsInStockList();

        //    if (!string.IsNullOrWhiteSpace(searchTerm))
        //    {
        //        return products.Where(
        //            s => s.Name.ToLower().Contains(searchTerm.ToLower()) ||
        //                 s.Category.Name.ToLower().Contains(searchTerm.ToLower()));
        //    }

        //    return products;
        //}

        //public IEnumerable<Product> GetProducts()
        //{
        //    var products = _context.Products
        //        .Include(s => s.Category)
        //        .Include(s => s.Files).Where(s => s.StockAmount > 0)
        //        .OrderByDescending(u => u.AddedDate)
        //        .ToList().Take(3);

        //    return products;
        //}

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