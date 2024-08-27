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
        private ProductService productService;

        [TestInitialize]
        public void Init()
        {
            productService = new ProductService();
            testProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "Milk", Category = "Food", ExpirationDate = DateTime.Now.AddDays(10), Quantity = 5 },
                new Product { Id = 2, Name = "Bread", Category = "Food", ExpirationDate = DateTime.Now.AddDays(20), Quantity = 10 }
            };
        }

        [TestMethod]
        public void AddProduct_ShouldAddProcutSuccessfully()
        {
            var testProduct = testProducts[0];

            productService.AddProduct(testProduct);

            var addedProduct = productService.GetProductByName("Milk");

            Assert.IsNotNull(addedProduct);
            Assert.AreSame(testProduct, addedProduct);
            Assert.AreEqual(testProduct.Name, addedProduct.Name);
            Assert.AreEqual(testProduct.Quantity, addedProduct.Quantity);
        }

        [TestMethod]
        public void RemoveProduct_ShouldRemoveProductSuccessfully()
        {
            var testProduct = testProducts[0];
            productService.AddProduct(testProducts[0]);

            productService.RemoveProduct(testProduct);
            var addedProduct = productService.GetProductByName("Milk");

            Assert.IsNull(addedProduct);
        }

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            productService.AddProduct(testProducts[0]);
            productService.AddProduct(testProducts[1]);

            var addedProducts = productService.GetAllProducts();

            Assert.AreEqual(2, addedProducts.Count());
        }
    }
}
