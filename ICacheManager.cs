namespace Alyas.Foundation.RedisCaching
{
    public interface ICacheManager
    {
        T GetCacheValue<T>(string key);
        void SetCacheValue<T>(string key, T value);
        void Remove(string key);
        void RemoveByPattern(string keyPattern);
    }
}
