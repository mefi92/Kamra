using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using Kamra.Core.Validators;

namespace Kamra.Core.Services
{
    public class CategoryService : ICategoryService
    {
        IPersistence<Category> _categoryPersistence;

        public CategoryService(IPersistence<Category> categoryPersistence) 
        {
            _categoryPersistence = categoryPersistence;
        }

        public void AddCategory(Category category)
        {
            CategoryValidator.ValidateCategoryInput(category, _categoryPersistence.GetAll().ToList());
            _categoryPersistence.Add(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryPersistence.GetAll();
        }

        public Category GetCategoryByName(string categoryName)
        {
            return _categoryPersistence.GetByName(categoryName);
        }

        public void RemoveCategory(Category category)
        {
            CategoryValidator.ValidateCategoryExists(category, _categoryPersistence.GetAll().ToList());
            _categoryPersistence.Remove(category);
        }
    }
}
