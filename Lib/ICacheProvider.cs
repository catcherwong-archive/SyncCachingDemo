namespace Lib
{
    using System;

    public interface ICacheProvider
    {
        void Set(string cacheKey, object cacheValue, TimeSpan expiration);

        object Get(string cacheKey, Func<object> dataRetriever, TimeSpan expiration);

        void Remove(string cacheKey);
    }
}
