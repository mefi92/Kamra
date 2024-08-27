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
            products.Add(product);
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
            throw new NotImplementedException();
        }

        public void AssignStoragePlace(Product product, DateTime dateOfOpening)
        {
            throw new NotImplementedException();
        }
    }
}
