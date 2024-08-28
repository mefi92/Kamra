using Kamra.Core.Models;

namespace Kamra.Core.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product);
        Product GetProductByName(string name);
        Product GetProductByBarcode(string barcode);
        IEnumerable<Product> GetProductsByCategory(string categoryName);
        void RemoveProduct(Product product);
        void AssignStoragePlace(Product product, StoragePlace storagePlace);
        void TrackOpeningDate(Product product, DateTime dateOfOpening);        
        IEnumerable<Product> GetAllProducts();
        void AddCategory(Category category);
        void RemoveCategory(Category category);    
        IEnumerable<Category> GetAllCategories();
    }
}
