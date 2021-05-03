using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyCaching.Core;

namespace CacheRepository
{
    internal class FileStorageDistributedCacheRepository: IDistributedCacheRepository
    {
        private readonly IEasyCachingProvider _provider;

        public FileStorageDistributedCacheRepository(IEasyCachingProvider provider)
        {
            _provider = provider;
        }
        
        public async Task Update<T>(string key, T data, TimeSpan? expiration = null)
        {
            await _provider.RemoveAsync(key);
            await _provider.SetAsync(key, data, expiration ?? TimeSpan.FromMinutes(10));
        }

        public Task<bool> DoesExist(string key)
        {
            return _provider.ExistsAsync(key);
        }

        public async Task<T> Get<T>(string key)
        {
            var res = await _provider.GetAsync<T>(key);
            return res.Value;
        }
    }
}
