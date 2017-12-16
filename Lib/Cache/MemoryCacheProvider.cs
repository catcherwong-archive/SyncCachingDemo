namespace Lib
{
    using System;
    using Microsoft.Extensions.Caching.Memory;

    public class MemoryCacheProvider : ILocalCacheProvider
    {
        private IMemoryCache _cache;

        public MemoryCacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public object Get(string cacheKey, Func<object> dataRetriever, TimeSpan expiration)
        {
            var result = _cache.Get(cacheKey);

            if (result != null)
                return result;

            var obj = dataRetriever.Invoke();

            if (obj != null)
                this.Set(cacheKey, obj, expiration);

            return obj;
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue, TimeSpan expiration)
        {
            _cache.Set(cacheKey, cacheValue, expiration);
        }
    }
}
