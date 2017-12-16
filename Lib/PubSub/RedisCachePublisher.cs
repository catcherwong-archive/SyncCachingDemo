namespace Lib
{
    using System;

    public class RedisCachePublisher : ICachePublisher
    {

        private readonly ISerializer _serialize;

        public RedisCachePublisher(ISerializer serialize)
        {
            this._serialize = serialize;
        }

        public void Notify(NotifyType notifyType, string cacheKey, object cacheValue, TimeSpan expiration)
        {

            string channelName = string.Empty;

            switch (notifyType)
            {
                case NotifyType.Add:
                    channelName = "CacheAdd";
                    break;
                case NotifyType.Update:
                    channelName = "CacheUpdate";
                    break;
                case NotifyType.Delete :
                    channelName = "CacheDelete";
                    break;
            }

            CacheNotificationObject args = new CacheNotificationObject()
            {
                CacheKey = cacheKey,
                CacheValue = cacheValue,
                Expiration = expiration
            };

            if(!string.IsNullOrWhiteSpace(channelName))
                RedisPubSubConfig.Connection.GetSubscriber().Publish(channelName, _serialize.Serialize(args));            
        }              
    }
}
