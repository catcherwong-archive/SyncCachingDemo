namespace Lib
{
    using System;
    using StackExchange.Redis;

    public static class RedisCacheConfig
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            ConfigurationOptions config = new ConfigurationOptions();
            config.EndPoints.Add("192.168.0.109");
            config.Password = "";
            config.Ssl = false;
            config.AbortOnConnectFail = false;
            config.ConnectRetry = 5;
            config.ConnectTimeout = 1000;
            return ConnectionMultiplexer.Connect(config);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

    }
}
