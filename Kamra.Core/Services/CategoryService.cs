using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using Kamra.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private List<Category> _categories = new List<Category>();

        public void AddCategory(Category category)
        {
            CategoryValidator.ValidateCategoryInput(category, _categories);
            _categories.Add(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categories.AsReadOnly();
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _categories.FirstOrDefault(p => p.Name == categoryName);
        }

        public void RemoveCategory(Category category)
        {
            CategoryValidator.ValidateCategoryExists(category, _categories);
            _categories.Remove(category);
        }
    }
}
