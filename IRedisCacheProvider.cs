using StackExchange.Redis;

namespace Alyas.Foundation.RedisCaching
{
    public interface IRedisCacheProvider
    {
        IDatabase GetRedisCache();
        IServer GetRedisServer();
    }
}
