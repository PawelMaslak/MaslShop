using Maslshop.Models.Core;
using System;
using System.Collections.Generic;

namespace Maslshop.Models.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public string SearchTerm { get; set; }

        public string Heading { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<File> Files { get; set; }

        public Category Category {get; set;}

        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int StockAmount { get; set; }

        public string FileName { get; set; }

        public string CategoryName { get; set; }

        public DateTime? AddedDate { get; set; }
    }
}