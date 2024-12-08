//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using AgroCom.Models.Foundations.Ogits;

namespace AgroCom.Services.Ogits
{
    public interface IOgitService
    {
        ValueTask<Ogit> RemoveOgitByIdAsync(int ogitId);
    }
}
