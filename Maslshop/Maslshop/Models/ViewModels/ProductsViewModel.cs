using System;
using System.Collections.Generic;
using Maslshop.Models.Core;

namespace Maslshop.Models.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public virtual List<File> Files { get; set; }

        public string Heading { get; set; }

        public string SearchTerm { get; set; }

        public DateTime? AddedDate { get; set; }
    }
}