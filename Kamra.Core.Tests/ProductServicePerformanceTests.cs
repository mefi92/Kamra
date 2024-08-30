using Kamra.Core.Services;
using Kamra.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Kamra.Core.Tests
{
    public class ProductServicePerformanceTests
    {
        private ProductService productService;
        private List<Product> products;

        [GlobalSetup]
        public void Setup()
        {
            productService = new ProductService();
            products = new List<Product>();

            var category = new Category { Id = 1, Name = "Food" };
            for (int i = 0; i < 10000; i++)
            {
                products.Add(new Product { Id = i, Name = $"Product {i}", Category = category, ExpirationDate = DateTime.Now.AddDays(i), Quantity = i });
            }

            foreach (var product in products)
            {
                productService.AddProduct(product);
            }
        }

        [Benchmark]
        public void SearchProducts_PerformanceTest()
        {
            productService.SearchProducts("Product 9999");
        }

        [Benchmark]
        public void FilterProductsByExpirationDate_PerformanceTest()
        {
            var filterDate = DateTime.Now.AddDays(5000);
            productService.FilterProductsByExpirationDate(filterDate);
        }
    }

    


}
