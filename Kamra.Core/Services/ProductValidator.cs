using Kamra.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Services
{
    public static class ProductValidator
    {
        public static void ValidateProductInput(Product product, List<Product> products)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty", nameof(product));

            if (product.Category == null)
                throw new ArgumentNullException("Product category cannot be null", nameof(product.Category));

            if (!string.IsNullOrWhiteSpace(product.Barcode))
            {
                if (products.Any(p => p.Barcode == product.Barcode))
                    throw new ArgumentException("Product with this barcode already exists", nameof(product.Barcode));
            }
        }

        public static void ValidateCategoryInput(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("Category cannot be null", nameof(category));

            if (string.IsNullOrWhiteSpace(category.Name))
                throw new ArgumentException("Category name cannot be empty", nameof(category.Name));
        }

        public static void ValidateCategoryName(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category name cannot be empty", nameof(categoryName));
        }

        public static void ValidateBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                throw new ArgumentException("Barcode cannot be empty", nameof(barcode));
        }
    }
}
