using AgroCom.Brokers.Storages;
using AgroCom.Models.Foundations.Products;

namespace AgroCom.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;

        public ProductService(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;
       
        public async ValueTask<Product> RemoveProductByIdAsync(int productId)
        {
           Product maybeProduct=
                await this.storageBroker.SelectProductByIdAsync(productId);

            return await this.storageBroker.DeleteProductAsync(maybeProduct);
        }
    }
}
