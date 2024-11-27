using AgroCom.Models.Foundations.Products;
using Microsoft.EntityFrameworkCore;

namespace AgroCom.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Product> Products { get; set; }
    }
}
