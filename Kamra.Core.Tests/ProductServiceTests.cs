using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductService> mockProductService;
        private List<Product> testProducts;

        [TestInitialize]
        public void Init()
        {
            mockProductService = new Mock<IProductService>();
            testProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "Milk", Category = "Food", ExpirationDate = DateTime.Now.AddDays(10), Quantity = 5 },
                new Product { Id = 2, Name = "Bread", Category = "Food", ExpirationDate = DateTime.Now.AddDays(20), Quantity = 10 }
            };
        }        
    }
}
