using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kamra.Core.Services;

namespace Kamra.Core.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private List<Product> testProducts;
        private ProductService service;

        [TestInitialize]
        public void Init()
        {
            service = new ProductService();
            testProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "Milk", Category = "Food", ExpirationDate = DateTime.Now.AddDays(10), Quantity = 5 },
                new Product { Id = 2, Name = "Bread", Category = "Food", ExpirationDate = DateTime.Now.AddDays(20), Quantity = 10 }
            };
        }

        [TestMethod]
        public void AddProduct_ShouldAddProcutSuccessfully()
        {
            var product = testProducts[0];

            service.AddProduct(product);

            var addedProduct = service.GetProductByName("Milk");

            Assert.IsNotNull(addedProduct);
            Assert.AreSame(product, addedProduct);
            Assert.AreEqual(product.Name, addedProduct.Name);
            Assert.AreEqual(product.Quantity, addedProduct.Quantity);
        }


    }
}
