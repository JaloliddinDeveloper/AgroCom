namespace AgroCom.Models.Foundations.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public string ProductPicture { get; set; }
    }
}
