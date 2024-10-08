﻿using Kamra.Core.Models;

namespace Kamra.Core.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void RemoveProduct(Product product);
        Product GetProductByName(string name);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string categoryName);
        void AssignStoragePlace(Product product, StoragePlace storagePlace);
        void TrackOpeningDate(Product product, DateTime dateOfOpening);
        void MarkAsFavorite(Product product, bool isFavorite);
        void ArchiveProduct(Product product, bool isArchived);
        Product GetProductByBarcode(string barcode);
        IEnumerable<Product> SearchProducts(string searchTerm);
        IEnumerable<Product> FilterProductsByExpirationDate(DateTime date);
        void AssignCategory(Product product, Category category);
        void UpdateProductQuantity(Product product, int newQuantity);
        IEnumerable<Product> GetExpiringSoonProducts(int daysBeforeExpiration = 7);
        IEnumerable<Product> GetAvailableProducts();
        IEnumerable<Product> GetOutOfStockProducts();
        void IncreaseProductQuantity(Product product, int amount);
        void DecreaseProductQuantity(Product product, int amount);

    }
}
