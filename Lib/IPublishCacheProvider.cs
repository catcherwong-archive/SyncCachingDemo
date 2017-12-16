namespace Lib
{
    using System;

    public interface IPublishCacheProvider
    {
        void Add(string cacheKey, object cacheValue, TimeSpan expiration, bool isNeedToNotify = false);

        void Update(string cacheKey, object cacheValue, TimeSpan expiration);

        void Delete(string cacheKey);
    }
}
