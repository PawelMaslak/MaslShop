using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Maslshop.Models.Core;

namespace Maslshop.Models.ViewModels
{
    public class CategoryFormViewModel
    {
        [Required]
        public string Name { get; set; }

        public int Id { get; set; }

        public string Heading { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public int NumberOfProducts { get; set; }
    }
}