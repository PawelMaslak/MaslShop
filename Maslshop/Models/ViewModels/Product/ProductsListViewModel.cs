using System.Collections.Generic;
using Maslshop.Models.Core;

namespace Maslshop.Models.ViewModels.Product
{
    public class ProductsListViewModel
    {
        public IEnumerable<ProductsViewModel> Products { get; set; }

        public string SearchTerm { get; set; }

        public string Heading { get; set; }

        public IEnumerable<Core.Category> Categories { get; set; }

        public IEnumerable<File> Files { get; set; }
    }
}