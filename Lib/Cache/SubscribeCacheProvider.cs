//using System;
//namespace Lib
//{
//    public class SubscribeCacheProvider : ISubscribeCacheProvider
//    {
//        private readonly ILocalCacheProvider _localCache;
//        private readonly ICacheSubscriber _cacheSubscriber;

//        public SubscribeCacheProvider(ILocalCacheProvider cache, ICacheSubscriber sub)
//        {
//            this._localCache = cache;
//            this._cacheSubscriber = sub;

//            _cacheSubscriber.CacheAdd += OnCacheAdd;
//            _cacheSubscriber.CacheUpdate += OnCacheUpdate;
//            _cacheSubscriber.CacheDelete += OnCacheDelete;
//        }

//        private void OnCacheDelete(object sender, CacheNotificationObject e)
//        {
//            Remove(e.CacheKey);
//        }

//        private void OnCacheUpdate(object sender, CacheNotificationObject e)
//        {
//            Remove(e.CacheKey);
//            Set(e.CacheKey, e.CacheValue, e.Expiration);
//        }

//        private void OnCacheAdd(object sender, CacheNotificationObject e)
//        {
//            Set(e.CacheKey, e.CacheValue, e.Expiration);
//        }

//        public object Get(string cacheKey, Func<object> dataRetriever, TimeSpan expiration)
//        {
//            return _localCache.Get(cacheKey, dataRetriever, expiration);
//        }

//        public void Remove(string cacheKey)
//        {
//            _localCache.Remove(cacheKey);
//        }

//        public void Set(string cacheKey, object cacheValue, TimeSpan expiration)
//        {
//            _localCache.Set(cacheKey, cacheValue, expiration);
//        }
//    }
//}
