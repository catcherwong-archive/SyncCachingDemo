namespace Lib
{
    using System;

    public class PublishCacheProvider : IPublishCacheProvider
    {
        private readonly IRemoteCacheProvider _remoteCache;
        private readonly ICachePublisher _cachePublisher;

        public PublishCacheProvider(IRemoteCacheProvider remoteCache, ICachePublisher publisher)
        {
            this._remoteCache = remoteCache;
            this._cachePublisher = publisher;
        }

        public void Add(string cacheKey, object cacheValue, TimeSpan expiration, bool isNeedToNotify = false)
        {
            _remoteCache.Set(cacheKey, cacheValue, expiration);

            if (isNeedToNotify)
                _cachePublisher.Notify(NotifyType.Add, cacheKey, cacheValue, expiration);
        }

        public void Delete(string cacheKey)
        {
            _remoteCache.Remove(cacheKey);

            _cachePublisher.Notify(NotifyType.Delete, cacheKey, null, TimeSpan.Zero);
        }

        public void Update(string cacheKey, object cacheValue, TimeSpan expiration)
        {
            _remoteCache.Remove(cacheKey);
            _remoteCache.Set(cacheKey, cacheValue, expiration);

            _cachePublisher.Notify(NotifyType.Update, cacheKey, cacheValue, expiration);
        }
    }
}
