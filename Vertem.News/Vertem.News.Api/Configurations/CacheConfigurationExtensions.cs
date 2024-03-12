using Microsoft.EntityFrameworkCore;

namespace Vertem.News.Api.Configurations
{
    public static class CacheConfigurationExtensions
    {
        public static void AddCacheConfig(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddDistributedMemoryCache();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Cache");
                //options.InstanceName = $"CacheOficinaTech{env.EnvironmentName}";
            });
        }
    }
}
