using EasyCaching.Disk;
using Microsoft.Extensions.DependencyInjection;

namespace CacheRepository
{
    public static class DistributedCacheRepositoryModule
    {
        public static void AddDistributedCacheRepository(this IServiceCollection services, string dbPath)
        {
            services.AddEasyCaching(options =>
            {
                options.UseDisk(config =>
                {
                    config.DBConfig = new DiskDbOptions() {BasePath = dbPath};
                });
            });
            services.AddSingleton<IDistributedCacheRepository, FileStorageDistributedCacheRepository>();
        }
    }
}
