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
            ProductInputValidation(product);

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
            return _products;
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

        private static void ProductInputValidation(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty", nameof(product));
        }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }
    }
}
