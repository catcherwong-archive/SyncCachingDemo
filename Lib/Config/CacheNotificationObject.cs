namespace Lib
{
    using System;

    public class CacheNotificationObject
    {        
        public string CacheKey { get; set; }
        public object CacheValue { get; set; }
        public TimeSpan Expiration { get; set; }
    }
}
