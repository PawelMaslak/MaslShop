using Maslshop.Models.ViewModels;
using System.Collections.Generic;
using Maslshop.Models.Core;
using Maslshop.Models.ViewModels.Category;

namespace Maslshop.Repositories
{
    public interface ICategoryRepository
    {
        void AddCategory(CategoryFormViewModel viewModel);
        IEnumerable<Category> GetCategoriesList();
        Category GetCategoryById(int id);
        void RemoveCategory(int id);
    }
}