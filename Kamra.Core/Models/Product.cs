namespace Kamra.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public StoragePlace StoragePlace { get; set; }
        public DateTime? DateOfOpening { get; set; }
        public string PackagingType { get; set; }
        public string Barcode { get; set; }
        public List<Recipe> Recipes { get; set; }
        public DateTime CreatingDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        
        public Product() 
        { 
            CreatingDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }

        public void UpdateLastModifiedDate()
        {
            LastModifiedDate = DateTime.Now;
        }

    }
}
