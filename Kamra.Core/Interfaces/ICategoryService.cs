using Kamra.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Interfaces
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void RemoveCategory(Category category);
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryByName(string categoryName);
    }
}
