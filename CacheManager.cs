using StackExchange.Redis;
using System;
using Newtonsoft.Json;

namespace Alyas.Foundation.RedisCaching
{
    public class CacheManager : ICacheManager
    {
        private readonly IDatabase _redisCache;
        private readonly IServer _redisServer;

        public CacheManager(IRedisCacheProvider redisCacheProvider)
        {
            _redisCache = redisCacheProvider.GetRedisCache();
            _redisServer = redisCacheProvider.GetRedisServer();
        }


        public T GetCacheValue<T>(string key)
        {
            if (_redisCache == null)
            {
                return default;
            }
            var res = _redisCache.StringGet(key);
            return string.IsNullOrEmpty(res) ? default : JsonConvert.DeserializeObject<T>(res);
        }
        public void SetCacheValue<T>(string key, T value)
        {
            _redisCache?.StringSet(key, JsonConvert.SerializeObject(value), TimeSpan.FromHours(24));
        }

        public void Remove(string key)
        {
            _redisCache?.KeyDelete(key);
        }

        public void RemoveByPattern(string keyPattern)
        {
            if (_redisCache == null)
            {
                return;
            }
            foreach (var key in _redisServer.Keys(pattern: keyPattern))
            {
                _redisCache.KeyDelete(key);
            }
        }
    }
}