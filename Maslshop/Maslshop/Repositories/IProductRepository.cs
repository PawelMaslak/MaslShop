using Maslshop.Models.Core;
using Maslshop.Models.ViewModels;
using System.Collections.Generic;
using Maslshop.Models.ViewModels.Product;

namespace Maslshop.Repositories
{
    public interface IProductRepository
    {
        void DeleteProduct(int id);
        void AddProduct(Product product);
        IEnumerable<Category> GetCategories();
        IEnumerable<Product> GetProductsInStockList();
        IEnumerable<ProductsViewModel> GetSearchedProducts(string query, string searchTerm = null);
        IEnumerable<ProductsViewModel> GetProducts();
        IEnumerable<ProductsViewModel> GetLatestThreeProducts();
        Product SelectProductMatchingPhotoId(File photo);
        Product GetProductById(int id);
        CartItem GetProductInTheCart(int id);
    }
}