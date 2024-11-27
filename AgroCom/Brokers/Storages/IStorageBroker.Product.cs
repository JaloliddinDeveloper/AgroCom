using AgroCom.Models.Foundations.Products;

namespace AgroCom.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Product> InsertProductAsync(Product product);
        ValueTask<IQueryable<Product>> SelectAllProductsAsync();
        ValueTask<Product> SelectProductByIdAsync(int productId);
        ValueTask<Product> UpdateProductAsync(Product product);
        ValueTask<Product> DeleteProductAsync(Product product);

        ValueTask<IQueryable<Product>> SelectAllProductsGerbesetlarAsync();
        ValueTask<IQueryable<Product>> SelectAllProductsFungisetlarAsync();
    }
}
