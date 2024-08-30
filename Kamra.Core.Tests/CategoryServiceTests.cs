using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using Kamra.Core.Services;
using Kamra.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Tests
{
    [TestClass]
    public class CategoryServiceTests
    {
        private ICategoryService categoryService;

        private Category foodCategory;
        private Category drinkCategory;

        [TestInitialize]
        public void Init()
        {
            categoryService = new CategoryService();

            foodCategory = new Category { Id = 1, Name = "Food" };
            drinkCategory = new Category { Id = 2, Name = "Drink" };
        }        

        [TestMethod]
        public void AddCategory_ShouldAddCategorySuccessfully()
        {
            categoryService.AddCategory(foodCategory);

            var categories = categoryService.GetAllCategories();

            Assert.IsNotNull(categories);
            Assert.AreEqual(1, categories.Count());
            Assert.AreEqual("Food", categories.First().Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCategory_NullInput_ShouldThrowArgumentNullException()
        {
            categoryService.AddCategory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCategory_NullOrEmptyCategoryName_ShouldThrowArgumentException()
        {
            var category = new Category { Id = 1, Name = "" };
            categoryService.AddCategory(category);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCategory_CategoryDuplication_ShouldThrowArgumentException()
        {
            categoryService.AddCategory(foodCategory);
            categoryService.AddCategory(new Category { Id = 2, Name = "Food" });
        }

        [TestMethod]
        public void GetAllCategories_ShouldReturnAllCategories()
        {
            categoryService.AddCategory(foodCategory);
            categoryService.AddCategory(drinkCategory);

            var categories = categoryService.GetAllCategories();

            Assert.AreEqual(2, categories.Count());
            Assert.AreEqual("Food", categories.First().Name);
            Assert.AreEqual("Drink", categories.Last().Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetAllCategories_ShouldReturnImmutableCollection()
        {
            categoryService.AddCategory(foodCategory);
            categoryService.AddCategory(drinkCategory);

            var categories = categoryService.GetAllCategories();
            (categories as IList<Category>).Add(new Category());
        }

        [TestMethod]
        public void RemoveCategory_ShouldRemoveCategorySuccessfully()
        {
            categoryService.AddCategory(foodCategory);
            categoryService.AddCategory(drinkCategory);

            categoryService.RemoveCategory(foodCategory);

            var categories = categoryService.GetAllCategories();
            Assert.AreEqual(1, categories.Count());
            Assert.AreEqual("Drink", categories.First().Name);

            categoryService.RemoveCategory(drinkCategory);
            categories = categoryService.GetAllCategories();
            Assert.AreEqual(0, categories.Count());
        }

        [TestMethod]
        public void GetCategoryByName_ShouldReturnCategorySuccessfully()
        {
            categoryService.AddCategory(foodCategory);

            var addedCategory = categoryService.GetCategoryByName("Food");

            Assert.AreEqual(foodCategory, addedCategory);
        }

        [TestMethod]
        [ExpectedException (typeof(InvalidOperationException))]
        public void RemoveCategory_NotExists_ShouldThrowInvalidOperationException()
        {            
            categoryService.RemoveCategory(foodCategory);
        }
    }
}
