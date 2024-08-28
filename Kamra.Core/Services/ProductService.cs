using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Services
{
    public class ProductService : IProductService
    {
        private List<Category> _categories = new List<Category>();
        private List<Product> _products = new List<Product>();

        public void AddProduct(Product product)
        {
            ProductValidator.ValidateProductInput(product, _products);

            _products.Add(product);
        }        

        public Product GetProductByName(string name)
        {
            return _products.FirstOrDefault(p => p.Name == name);
        }

        public void RemoveProduct(Product product)
        {
            _products.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products.AsReadOnly();
        }

        public void AssignStoragePlace(Product product, StoragePlace storagePlace)
        {
            var existingProduct = GetProductByName(product.Name);
            if (existingProduct != null)
            {
                existingProduct.StoragePlace = storagePlace;
            }
        }

        public void TrackOpeningDate(Product product, DateTime dateOfOpening)
        {
            var existingProduct = GetProductByName(product.Name);
            if (existingProduct != null)
            {
                existingProduct.DateOfOpening = dateOfOpening;
            }
        }

        public void AddCategory(Category category)
        {
            ProductValidator.ValidateCategoryInput(category, _categories);
            _categories.Add(category);
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            ProductValidator.ValidateCategoryName(categoryName);

            return _products.Where(p => p.Category.Name == categoryName).ToList().AsReadOnly();
        }

        public Product GetProductByBarcode(string barcode)
        {
            ProductValidator.ValidateBarcode(barcode);

            return _products.FirstOrDefault(p => p.Barcode == barcode);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categories.AsReadOnly();
        }

        public void RemoveCategory(Category category)
        {
            _categories.Remove(category);
        }

        public void AssignCategory(Product product, Category category)
        {
            var existingProduct = GetProductByName(product.Name);
            if (existingProduct != null)
            {
                existingProduct.Category = category;
            }
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            ProductValidator.ValidateSearchTerm(searchTerm);

            return _products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                        p.Category.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Product> FilterProductsByExpirationDate(DateTime date)
        {
            return _products.Where(p => p.ExpirationDate <= date);
        }
    }
}
