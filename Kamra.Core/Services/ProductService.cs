using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using Kamra.Core.Validators;

namespace Kamra.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IPersistence<Product> _productPersistence;

        public ProductService(IPersistence<Product> productPersistence)
        {
            _productPersistence = productPersistence;
        }

        public void AddProduct(Product product)
        {
            ProductValidator.ValidateProductInput(product, GetAllProducts().ToList());
            _productPersistence.Add(product);
        }

        public Product GetProductByName(string name)
        {
            return _productPersistence.GetByName(name);
        }

        public void RemoveProduct(Product product)
        {
            ProductValidator.ValidateProductExists(product, GetAllProducts().ToList());
            _productPersistence.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productPersistence.GetAll();
        }

        public void AssignStoragePlace(Product product, StoragePlace storagePlace)
        {
            UpdateProductField(product, p => p.StoragePlace = storagePlace);
        }

        public void TrackOpeningDate(Product product, DateTime dateOfOpening)
        {
            UpdateProductField(product, p => p.DateOfOpening = dateOfOpening);
        }

        public void AssignCategory(Product product, Category category)
        {
            UpdateProductField(product, p => p.Category = category);
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            CategoryValidator.ValidateCategoryName(categoryName);
            return GetAllProducts().Where(p => p.Category.Name == categoryName).ToList().AsReadOnly();
        }

        public Product GetProductByBarcode(string barcode)
        {
            ProductValidator.ValidateBarcode(barcode);
            return GetAllProducts().FirstOrDefault(p => p.Barcode == barcode);
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return GetAllProducts().Where(p =>
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Product> FilterProductsByExpirationDate(DateTime date)
        {
            return GetAllProducts().Where(p => p.ExpirationDate <= date);
        }

        private void UpdateProductField(Product product, Action<Product> updateAction)
        {
            if (product != null)
            {
                updateAction(product);
                product.UpdateLastModifiedDate();
                _productPersistence.Update(product);
            }
        }

        public void UpdateProductQuantity(Product product, int newQuantity)
        {
            UpdateProductField(product, p => p.Quantity = newQuantity);
        }

        public IEnumerable<Product> GetExpiringSoonProducts(int daysBeforeExpiration = 7)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAvailableProducts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetOutOfStockProducts()
        {
            throw new NotImplementedException();
        }
    }
}
