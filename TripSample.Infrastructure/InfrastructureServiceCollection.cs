using Microsoft.Extensions.DependencyInjection;
using TripSample.Infrastructure.Client;
using Microsoft.Extensions.Configuration;

namespace TripSample.Infrastructure
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ObiletApiOptions>(configuration.GetSection("ObiletApi"));

            services.AddHttpClient<IObiletApiClient, ObiletApiClient>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            return services;
        }
    }
}
