using Kamra.Core.Interfaces;
using Kamra.Core.Models;
using Kamra.Core.Validators;

namespace Kamra.Core.Services
{
    public class ProductService : IProductService
    {        
        IPersistence<Product> _productPersistence;

        public ProductService(IPersistence<Product> productPersistence) 
        {
            _productPersistence = productPersistence;
        }

        public void AddProduct(Product product)
        {
            ProductValidator.ValidateProductInput(product, _productPersistence.GetAll().ToList());
            _productPersistence.Add(product);
        }        

        public Product GetProductByName(string name)
        {
            return _productPersistence.GetByName(name);
        }

        public void RemoveProduct(Product product)
        {
            ProductValidator.ValidateProductExists(product, _productPersistence.GetAll().ToList());
            _productPersistence.Remove(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productPersistence.GetAll();
        }

        public void AssignStoragePlace(Product product, StoragePlace storagePlace)
        {
            var existingProduct = GetProductByName(product.Name);
            if (existingProduct != null)
            {
                existingProduct.StoragePlace = storagePlace;
                UpdateProductAndModificationDate(product, existingProduct);
            }
        }

        public void TrackOpeningDate(Product product, DateTime dateOfOpening)
        {
            var existingProduct = GetProductByName(product.Name);
            if (existingProduct != null)
            {
                existingProduct.DateOfOpening = dateOfOpening;
                UpdateProductAndModificationDate(product, existingProduct);
            }
        }

        public void AssignCategory(Product product, Category category)
        {
            var existingProduct = GetProductByName(product.Name);
            if (existingProduct != null)
            {
                existingProduct.Category = category;
                UpdateProductAndModificationDate(product, existingProduct);
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            CategoryValidator.ValidateCategoryName(categoryName);

            return _productPersistence.GetAll().Where(p => p.Category.Name == categoryName).ToList().AsReadOnly();
        }

        public Product GetProductByBarcode(string barcode)
        {
            ProductValidator.ValidateBarcode(barcode);

            return _productPersistence.GetAll().FirstOrDefault(p => p.Barcode == barcode);
        }                

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return _productPersistence.GetAll().Where(p =>
                p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Product> FilterProductsByExpirationDate(DateTime date)
        {
            return _productPersistence.GetAll().Where(p => p.ExpirationDate <= date);
        }

        private void UpdateProductAndModificationDate(Product newProduct, Product oldProduct)
        {
            _productPersistence.Update(oldProduct);
            newProduct.UpdateLastModifiedDate();
        }
    }
}
