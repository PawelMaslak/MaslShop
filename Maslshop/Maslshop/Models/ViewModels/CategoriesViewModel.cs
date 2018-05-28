using System.Collections.Generic;
using Maslshop.Models.Core;

namespace Maslshop.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public string Heading { get; set; }

        public string SearchTerm { get; set; }
    }
}