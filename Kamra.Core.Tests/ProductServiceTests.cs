using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kamra.Core.Services;
using Moq;
using Karma.Persistence;

namespace Kamra.Core.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private IPersistence<Product> productPersistence;
        private IProductService productService;        

        private List<Product> testProducts;
        
        private Category foodCategory;
        private Category drinkCategory;



        [TestInitialize]
        public void Init()
        {
            productPersistence = new InMemoryPersistence<Product>();
            productService = new ProductService(productPersistence);

            foodCategory = new Category { Id = 1, Name = "Food" };
            drinkCategory = new Category { Id = 1, Name = "Drink" };
            testProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "Milk", Category = drinkCategory, ExpirationDate = DateTime.Now.AddDays(10), Quantity = 5 },
                new Product { Id = 2, Name = "Bread", Category = foodCategory, ExpirationDate = DateTime.Now.AddDays(20), Quantity = 10 },
                new Product { Id = 2, Name = "Bread roll", Category = foodCategory, ExpirationDate = DateTime.Now.AddDays(5), Quantity = 10 }
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddProduct_NullInput_ShouldThrowArgumentNullException()
        {
            productService.AddProduct(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddProduct_EmptyStringInput_ShouldThrowArgumentException()
        {
            var testProduct = new Product { Id = 1, Name = "", Category = foodCategory, ExpirationDate = DateTime.Now.AddDays(10), Quantity = 5 };

            productService.AddProduct(testProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddProduct_WhiteSpaceStringInput_ShouldThrowArgumentException()
        {
            var testProduct = new Product { Id = 1, Name = " ", Category = foodCategory, ExpirationDate = DateTime.Now.AddDays(10), Quantity = 5 };

            productService.AddProduct(testProduct);
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
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveProdct_NotExists_ShouldThrowInvalidOperationException()
        {
            var testProduct = testProducts[0];
            productService.RemoveProduct(testProduct);
        }

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            productService.AddProduct(testProducts[0]);
            productService.AddProduct(testProducts[1]);

            var addedProducts = productService.GetAllProducts();

            Assert.AreEqual(2, addedProducts.Count());
        }

        [TestMethod]
        public void AssignStoragePlace_ShouldUpdateProductStoragePlace()
        {
            var testProduct = testProducts[0];
            productService.AddProduct(testProduct);
            var newStoragePlace = new StoragePlace { Name = "Pantry" };

            productService.AssignStoragePlace(testProduct, newStoragePlace);

            var updatedProduct = productService.GetProductByName("Milk");
            Assert.AreEqual("Pantry", updatedProduct.StoragePlace.Name);
        }

        [TestMethod]
        public void TrackOpeningDate_ShouldSetDateOfOpeningCorrectly()
        {
            var testProduct = testProducts[0];
            var openingDate = new DateTime(2024, 8, 27);
            productService.AddProduct(testProduct);

            productService.TrackOpeningDate(testProduct, openingDate);

            var updatedProduct = productService.GetProductByName("Milk");
            Assert.AreEqual(openingDate, updatedProduct.DateOfOpening);
        }

        [TestMethod]
        public void GetProductsByCategory_ShouldReturnCorrectProducts()
        {
            var mockCategoryService = new Mock<ICategoryService>();
            var drinkCategory = new Category { Id = 2, Name = "Drink" };
            var foodCategory = new Category { Id = 1, Name = "Food" };

            mockCategoryService.Setup(c => c.GetCategoryByName("Food")).Returns(foodCategory);
            mockCategoryService.Setup(c => c.GetCategoryByName("Drink")).Returns(drinkCategory);

            var milk = new Product { Name = "Milk", Category = drinkCategory };
            var bread = new Product { Name = "Bread", Category = foodCategory };

            productService.AddProduct(milk);
            productService.AddProduct(bread);

            var foodProducts = productService.GetProductsByCategory("Food");
            Assert.AreEqual(1, foodProducts.Count());
            Assert.AreEqual("Bread", foodProducts.First().Name);

            var drinkProducts = productService.GetProductsByCategory("Drink");
            Assert.AreEqual(1, drinkProducts.Count());
            Assert.AreEqual("Milk", drinkProducts.First().Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddProduct_ProductWithNullCategory_ShouldThrowArgumentNullException()
        {
            var product = new Product { Name = "Milk", Category = null };
            productService.AddProduct(product);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetProductsByCategory_NullOrEmptyCategoryName_ShouldThrowArgumentException()
        {
            productService.GetProductsByCategory("");
        }

        [TestMethod]
        public void GetProductByBarcode_ShouldReturnCorrectProduct()
        {
            var testProduct = new Product { Name = "Bread", Category = foodCategory, Barcode = "12345" };
            productService.AddProduct(testProduct);

            var addedProduct = productService.GetProductByBarcode("12345");

            Assert.IsNotNull(addedProduct);
            Assert.AreEqual("Bread", addedProduct.Name);
        }

        [TestMethod]
        public void AddProduct_WithDuplicateBarcode_ShouldThrowArgumentException()
        {
            var product1 = new Product { Name = "Banana", Category = foodCategory, Barcode = "12345" };
            var product2 = new Product { Name = "Bread", Category = foodCategory, Barcode = "12345" };

            productService.AddProduct(product1);

            Assert.ThrowsException<ArgumentException>(() => productService.AddProduct(product2));
        }

        [TestMethod]
        public void AddProduct_WithoutBarcode_ShouldAddProductSuccessfully()
        {
            var product = new Product { Name = "Milk", Category = foodCategory, Barcode = null };

            productService.AddProduct(product);

            var addedProduct = productService.GetProductByName("Milk");
            Assert.IsNotNull(addedProduct);
            Assert.IsNull(addedProduct.Barcode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetProductByBarcode_EmptyBarcode_ShouldThrowArgumentException()
        {
            productService.GetProductByBarcode("");
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetAllProducts_ShouldReturnImmutableCollection()
        {            
            productService.AddProduct(testProducts[0]);
            productService.AddProduct(testProducts[1]);

            var products = productService.GetAllProducts();
            (products as IList<Product>).Add(new Product());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetProductsByCategory_ShouldReturnImmutableCollection()
        {
            productService.AddProduct(testProducts[0]);
            productService.AddProduct(testProducts[1]);

            var productsByCategory = productService.GetProductsByCategory("Food");
            (productsByCategory as IList<Product>).Add(new Product());
        }

        [TestMethod]
        public void AssignCategory_ShouldAssignProductToCategorySuccessfully()
        {
            var product = new Product { Id = 4, Name = "Juice", Category = new Category { Name = "NA" }, ExpirationDate = DateTime.Now.AddDays(15), Quantity = 7 };
            var newCategory = new Category { Name = "Beverages" };
            productService.AddProduct(product);

            productService.AssignCategory(product, newCategory);

            var assignedProduct = productService.GetProductByName("Juice");
            Assert.IsNotNull(assignedProduct);
            Assert.AreEqual("Beverages", assignedProduct.Category.Name);
        }

        [TestMethod]
        public void SearchProducts_ByName_ShouldReturnMatchingProducts()
        {
            productService.AddProduct(testProducts[0]);
            productService.AddProduct(testProducts[1]);

            var searchResults = productService.SearchProducts("Milk");

            Assert.AreEqual(1, searchResults.Count());
            Assert.AreEqual("Milk", searchResults.First().Name);
        }

        [TestMethod]
        public void SearchProducts_ByCategory_ShouldReturnMatchingProducts()
        {
            productService.AddProduct(testProducts[0]);
            productService.AddProduct(testProducts[1]);
            productService.AddProduct(testProducts[2]);

            var searchResults = productService.SearchProducts("Food");

            Assert.AreEqual(2, searchResults.Count());
            Assert.IsTrue(searchResults.All(p => p.Category.Name == "Food"));
        }

        [TestMethod]
        public void FilterProductsByExpirationDate_ShouldReturnProductsExpiringSoon()
        {
            productService.AddProduct(testProducts[0]);
            productService.AddProduct(testProducts[1]);
            productService.AddProduct(testProducts[2]);

            var filterDate = DateTime.Now.AddDays(15);

            var filteredProducts = productService.FilterProductsByExpirationDate(filterDate);

            Assert.AreEqual(2, filteredProducts.Count());
            Assert.IsTrue(filteredProducts.All(p => p.ExpirationDate <= filterDate));
        }

        [TestMethod]
        public void FilterProductsByExpirationDate_ShouldReturnEmptyForFutureDate()
        {
            productService.AddProduct(testProducts[0]);
            productService.AddProduct(testProducts[1]);
            productService.AddProduct(testProducts[2]);
            
            var filterDate = DateTime.Now.AddDays(-1);

            var filteredProducts = productService.FilterProductsByExpirationDate(filterDate);

            Assert.AreEqual(0, filteredProducts.Count());
        }

        [TestMethod]
        public void ProductCreation_ShouldInitializeDatesWithinTolerance()
        {
            DateTime timeNow = DateTime.Now;
            productService.AddProduct(testProducts[0]);
            
            var addedProduct = productService.GetProductByName("Milk");

            TimeSpan tolerance = TimeSpan.FromMilliseconds(100);

            Assert.IsTrue(Math.Abs((addedProduct.CreatingDate - timeNow).TotalMilliseconds) <= tolerance.TotalMilliseconds, "The CreatingDate is not within the acceptable tolerance.");
            Assert.IsTrue(Math.Abs((addedProduct.LastModifiedDate - timeNow).TotalMilliseconds) <= tolerance.TotalMilliseconds, "The LastModifiedDate is not within the acceptable tolerance.");
        }
    }
}
