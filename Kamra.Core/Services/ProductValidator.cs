﻿using Kamra.Core.Models;
using Kamra.Core.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Services
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

        
    }
}
