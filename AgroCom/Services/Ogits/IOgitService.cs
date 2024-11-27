using AgroCom.Models.Foundations.Ogits;

namespace AgroCom.Services.Ogits
{
    public interface IOgitService
    {
        ValueTask<Ogit> RemoveProductByIdAsync(int ogitId);
    }
}
