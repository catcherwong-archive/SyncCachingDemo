namespace Lib
{
    using System;

    public interface ICachePublisher
    {
        void Notify(NotifyType notifyType ,string cacheKey, object cacheValue, TimeSpan expiration);
    }
}
