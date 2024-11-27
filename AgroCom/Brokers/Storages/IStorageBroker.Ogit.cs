using AgroCom.Models.Foundations.Ogits;

namespace AgroCom.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Ogit> InsertOgitAsync(Ogit ogit);
        ValueTask<IQueryable<Ogit>> SelectAllOgitsAsync();
        ValueTask<Ogit> SelectOgitByIdAsync(int ogitId);
        ValueTask<Ogit> UpdateOgitAsync(Ogit ogit);
        ValueTask<Ogit> DeleteOgitAsync(Ogit ogit);
    }
}
