using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TripSample.Infrastructure
{
    public static class RedisCacheServiceCollection
    {
        public static IServiceCollection AddRedisCacheService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = "127.0.0.1:6379";
                opt.InstanceName = "redis-local-test";
            }
        );

            return services;
        }
    }
}
