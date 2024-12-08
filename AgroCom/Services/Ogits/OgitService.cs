//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using AgroCom.Brokers.Storages;
using AgroCom.Models.Foundations.Ogits;

namespace AgroCom.Services.Ogits
{
    public class OgitService : IOgitService
    {
        private readonly IStorageBroker storageBroker;

        public OgitService(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;
        
        public async ValueTask<Ogit> RemoveOgitByIdAsync(int ogitId)
        {
            Ogit maybeOgit=
                await this.storageBroker.SelectOgitByIdAsync(ogitId);

            return await this.storageBroker.DeleteOgitAsync(maybeOgit);
        }
    }
}
