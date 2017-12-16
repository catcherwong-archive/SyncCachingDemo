namespace Lib
{
    using StackExchange.Redis;

    public class RedisCacheSubscriber : ICacheSubscriber
    {
        private readonly ILocalCacheProvider _localCache;
        private readonly ISerializer _serialize;

        public RedisCacheSubscriber(ILocalCacheProvider localCache , ISerializer serialize)
        {            
            this._localCache = localCache;
            this._serialize = serialize;
        }

        public void Subscribe(string channel, NotifyType notifyType)
        {
            switch (notifyType)
            {
                case NotifyType.Add:
                    RedisPubSubConfig.Connection.GetSubscriber().Subscribe(channel, CacheAddAction);
                    break;
                case NotifyType.Update:
                    RedisPubSubConfig.Connection.GetSubscriber().Subscribe(channel, CacheUpdateAction);
                    break;
                case NotifyType.Delete:
                    RedisPubSubConfig.Connection.GetSubscriber().Subscribe(channel, CacheUpdateAction);
                    break;
            }
        }

        private void CacheDeleteAction(RedisChannel channel, RedisValue message)
        {
            var deleteNotification = _serialize.Deserialize<CacheNotificationObject>(message);

            _localCache.Remove(deleteNotification.CacheKey);
        }

        private void CacheUpdateAction(RedisChannel channel, RedisValue message)
        {
            var updateNotification = _serialize.Deserialize<CacheNotificationObject>(message);

            _localCache.Remove(updateNotification.CacheKey);
            _localCache.Set(updateNotification.CacheKey, updateNotification.CacheValue, updateNotification.Expiration);
        }

        private void CacheAddAction(RedisChannel channel, RedisValue message)
        {
            var addNotification = _serialize.Deserialize<CacheNotificationObject>(message);

            _localCache.Set(addNotification.CacheKey, addNotification.CacheValue, addNotification.Expiration);
        }

    }
}
