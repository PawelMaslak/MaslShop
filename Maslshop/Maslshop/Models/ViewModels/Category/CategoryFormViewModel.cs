using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maslshop.Models.ViewModels.Category
{
    public class CategoryFormViewModel
    {
        [Required(ErrorMessage = "Field cannot be empty")]
        public string Name { get; set; }

        public int Id { get; set; }

        public string Heading { get; set; }

        public IEnumerable<Core.Category> Categories { get; set; }

        public int NumberOfProducts { get; set; }
    }
}