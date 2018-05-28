using Maslshop.Models.Core;
using System.Collections.Generic;

namespace Maslshop.Repositories
{
    public interface IProductRepository
    {
        void DeleteProduct(int id);
        void AddProduct(Product product);
        IEnumerable<Category> GetCategories();
        IEnumerable<Product> GetProductsInStockList();
        IEnumerable<Product> GetSearchedProducts(string searchTerm = null);
        IEnumerable<Product> GetProducts();
        Product SelectProductMatchingPhotoId(File photo);
        Product GetProductById(int id);
    }
}