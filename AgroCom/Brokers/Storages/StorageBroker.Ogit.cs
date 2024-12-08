using AgroCom.Models.Foundations.Ogits;
using Microsoft.EntityFrameworkCore;

namespace AgroCom.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Ogit> Ogits { get; set; }

        public async ValueTask<Ogit> InsertOgitAsync(Ogit ogit) =>
           await InsertAsync(ogit);

        public async ValueTask<IQueryable<Ogit>> SelectAllOgitsAsync() =>
           await SelectAllAsync<Ogit>();

        public async ValueTask<Ogit> SelectOgitByIdAsync(int ogitId) =>
            await SelectAsync<Ogit>(ogitId);

        public async ValueTask<Ogit> UpdateOgitAsync(Ogit ogit) =>
            await UpdateAsync(ogit);

        public async ValueTask<Ogit> DeleteOgitAsync(Ogit ogit) =>
            await DeleteAsync(ogit);

        public async ValueTask<IQueryable<Ogit>> SelectAllOgitsSuyuqAsync()
        {
            var query = Ogits.Where(tur => tur.OgitType == OgitType.Suyuq);
            return await ValueTask.FromResult(query);
        }

        public async ValueTask<IQueryable<Ogit>> SelectAllOgitsQuyuqAsync()
        {
            var query = Ogits.Where(tur => tur.OgitType == OgitType.Kukun);
            return await ValueTask.FromResult(query);
        }
    }
}
