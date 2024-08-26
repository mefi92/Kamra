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

        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }        

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
