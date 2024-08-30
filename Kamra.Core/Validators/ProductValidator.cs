using Kamra.Core.Models;
using Kamra.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Validators
{
    public static class ProductValidator
    {
        public static void ValidateProductInput(Product product, List<Product> existingProducts)
        {
            ValidateIsProductNull(product);
            ValidateProductName(product.Name);
            ValidateProductCategory(product.Category);
            ValidateProductBarcode(product.Barcode, existingProducts);
        }

        private static void ValidateIsProductNull(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
        }

        private static void ValidateProductBarcode(string barcode, List<Product> existingProducts)
        {
            if (!string.IsNullOrWhiteSpace(barcode))
            {
                if (existingProducts.Any(p => p.Barcode == barcode))
                    throw new ArgumentException(ValidationMessages.BarcodeAlreadyExists, nameof(barcode));
            }
        }

        private static void ValidateProductName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ValidationMessages.ProductNameCannotBeEmpty, nameof(name));
        }

        private static void ValidateProductCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(ValidationMessages.ProductCannotBeNull, nameof(category));
        }

        public static void ValidateBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                throw new ArgumentException(ValidationMessages.BarcodeCannotBeEmpty, nameof(barcode));
        }

        public static void ValidateSearchTerm(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException(ValidationMessages.SearchTermCannotBeEmpty, nameof(searchTerm));
        }


    }
}
