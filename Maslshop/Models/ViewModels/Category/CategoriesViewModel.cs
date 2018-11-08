using System.Collections.Generic;
using Maslshop.Models.Core;

namespace Maslshop.Models.ViewModels.Category
{
    public class CategoriesViewModel
    {
        public IEnumerable<Core.Category> Categories { get; set; }

        public IEnumerable<Core.Product> Products { get; set; }

        public string Heading { get; set; }

        public string SearchTerm { get; set; }
    }
}