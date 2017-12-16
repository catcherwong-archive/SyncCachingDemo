namespace Lib
{
    using System;

    public class RedisCacheProvider : IRemoteCacheProvider
    {
        private readonly ISerializer _serializer;
        public RedisCacheProvider(ISerializer serializer)
        {
            this._serializer = serializer;
        }

        public object Get(string cacheKey, Func<object> dataRetriever, TimeSpan expiration)
        {
            var result = RedisCacheConfig.Connection.GetDatabase().StringGet(cacheKey);

            if (result.HasValue)
                return _serializer.Deserialize(result);

            var obj = dataRetriever.Invoke();

            if (obj != null)
                this.Set(cacheKey, obj, expiration);

            return obj;
        }

        public void Remove(string cacheKey)
        {
            RedisCacheConfig.Connection.GetDatabase().KeyDelete(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue, TimeSpan expiration)
        {
            var value = _serializer.Serialize(cacheValue);
            RedisCacheConfig.Connection.GetDatabase().StringSet(cacheKey, value, expiration);
        }
    }
}
