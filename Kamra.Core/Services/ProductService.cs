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
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            ProductInputValidation(product);

            products.Add(product);
        }

        private static void ProductInputValidation(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty", nameof(product));
        }

        public Product GetProductByName(string name)
        {
            return products.FirstOrDefault(p => p.Name == name);
        }

        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
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
    }
}
