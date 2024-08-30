using Kamra.Core.Models;
using Kamra.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Validators
{
    public static class CategoryValidator
    {
        public static void ValidateCategoryInput(Category category, List<Category> categories)
        {
            if (category == null)
                throw new ArgumentNullException(ValidationMessages.CategoryCannotBeNull, nameof(category));

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException(ValidationMessages.CategoryCannotBeEmpty, nameof(category.Name));

            if (categories.Any(c => c.Name == category.Name))
            {
                throw new ArgumentException(ValidationMessages.CategoryAlreadyExists, nameof(category));
            }
        }

        public static void ValidateCategoryName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException(ValidationMessages.CategoryCannotBeEmpty, nameof(categoryName));
        }

        public static void ValidateCategoryExists(Category category, List<Category> categories)
        {
            if (!categories.Contains(category))
                throw new InvalidOperationException(ValidationMessages.CategoryNotExists);
        }
    }
}
