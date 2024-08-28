﻿using Kamra.Core.Interfaces;
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
        private Category foodCategory;
        private Category drinkCategory;

        [TestInitialize]
        public void Init()
        {
            productService = new ProductService();

            foodCategory = new Category { Id = 1, Name = "Food" };
            drinkCategory = new Category { Id = 1, Name = "Drink" };
            testProducts = new List<Product>()
            {
                new Product { Id = 1, Name = "Milk", Category = drinkCategory, ExpirationDate = DateTime.Now.AddDays(10), Quantity = 5 },
                new Product { Id = 2, Name = "Bread", Category = foodCategory, ExpirationDate = DateTime.Now.AddDays(20), Quantity = 10 }
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
        public void AddCategory_ShouldAddCategorySuccessfully()
        {
            productService.AddCategory(foodCategory);

            var testProduct = new Product { Name = "Milk", Category = foodCategory };
            productService.AddProduct(testProduct);

            var addedProduct = productService.GetProductByName("Milk");
            Assert.AreEqual("Food", addedProduct.Category.Name);
        }

        [TestMethod]
        public void GetProductsByCategory_ShouldReturnCorrectProducts()
        {
            var drinkCategory = new Category { Id = 2, Name = "Drink" };

            productService.AddCategory(foodCategory);
            productService.AddCategory(drinkCategory);

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
        public void AddCategory_NullOrEmptyCategoryName_ShouldThrowArgumentException()
        {
            var category = new Category { Id = 1, Name = "" };
            productService.AddCategory(category);
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
        [ExpectedException(typeof(ArgumentException))]
        public void AddCategory_CategoryDuplication_ShouldThrowArgumentException()
        {
            Category foodCategory1 = new Category { Id = 1, Name = "Food" };
            Category foodCategory2 = new Category { Id = 2, Name = "Food" };

            productService.AddCategory(foodCategory1);
            productService.AddCategory(foodCategory2);
        }

        [TestMethod]
        public void GetAllCategories_ShouldReturnAllCategories()
        {
            productService.AddCategory(foodCategory);
            productService.AddCategory(drinkCategory);

            var addedCategories = productService.GetAllCategories();

            Assert.AreEqual (2, addedCategories.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetAllCategories_ShouldReturnImmutableCollection()
        {
            productService.AddCategory(foodCategory);
            productService.AddCategory(drinkCategory);

            var addedCategories = productService.GetAllCategories();
            (addedCategories as IList<Category>).Add(new Category());
        }
    }
}
