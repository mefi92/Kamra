using Kamra.Core.Models;

namespace Kamra.Core.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product);
        Product GetProductByName(string name);
        void RemoveProduct(Product product);
        IEnumerable<Product> GetAllProducts();
    }
}
