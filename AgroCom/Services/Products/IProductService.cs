using AgroCom.Models.Foundations.Products;

namespace AgroCom.Services.Products
{
    public interface IProductService
    {
        ValueTask<Product> RemoveProductByIdAsync(int productId);
    }
}
