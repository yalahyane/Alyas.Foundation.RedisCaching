using System;
using System.Configuration;
using System.Linq;
using StackExchange.Redis;

namespace Alyas.Foundation.RedisCaching
{
    public class RedisCacheProvider : IRedisCacheProvider
    {
        private static ConfigurationOptions Options =>
            ConfigurationOptions.Parse(ConfigurationManager.ConnectionStrings["redis.sessions"].ConnectionString);
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            Options.AllowAdmin = true;
            Options.SyncTimeout = 60000;
            Options.ConnectRetry = 5;

            return ConnectionMultiplexer.Connect(Options);

        });

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        private readonly IDatabase _redisCache = Connection?.GetDatabase();
        private readonly IServer _redisServer = Connection?.GetServer(Options.EndPoints.FirstOrDefault());

        public IDatabase GetRedisCache()
        {
            return _redisCache;
        }

        public IServer GetRedisServer()
        {
            return _redisServer;
        }
    }
}