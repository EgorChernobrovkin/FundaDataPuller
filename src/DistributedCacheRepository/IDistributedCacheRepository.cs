using System;
using System.Threading.Tasks;

namespace CacheRepository
{
    public interface IDistributedCacheRepository
    {
        Task Update<T>(string key, T data, TimeSpan? expiration = null);

        Task<bool> DoesExist(string key);

        Task<T> Get<T>(string key);
    }
}
