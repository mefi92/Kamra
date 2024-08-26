namespace Kamra.Core.Models
{
    // place? eg: fridge, pentry
    // felbontás napja => Data of opening , optional
    // csomagolás típusa, optional
    // mentett receptek amikben megtalálható 
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
    }
}
