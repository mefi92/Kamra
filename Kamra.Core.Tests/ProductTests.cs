namespace Kamra.Core.Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Product_CanBeCreated()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "Milk",
                Category = "Food",
                Description = "Test",
                ExpirationDate = DateTime.Now.AddDays(10),
                Quantity = 5
            };

            Assert.AreEqual(1, product.Id );
            Assert.AreEqual("Milk", product.Name );
            Assert.AreEqual("Food", product.Category );
            Assert.AreEqual("Test", product.Description );
            Assert.IsTrue(product.ExpirationDate > DateTime.Now );
            Assert.AreEqual(5, product.Quantity);
        }
    }
}