using Maslshop.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Maslshop.Models.Core;

namespace Maslshop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategoriesList()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.SingleOrDefault(c => c.Id == id);
        }

        public void RemoveCategory(int id)
        {
            _context.Categories.Remove(GetCategoryById(id));
        }

        public void AddCategory(CategoryFormViewModel viewModel)
        {
            _context.Categories.Add(new Category()
            {
                Name = viewModel.Name
            });
        }
    }
}