using AgroCom.Models.Foundations.Ogits;
using Microsoft.EntityFrameworkCore;

namespace AgroCom.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Ogit> Ogits { get; set; }
    }
}
