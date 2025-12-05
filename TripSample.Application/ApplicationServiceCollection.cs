using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TripSample.Application.Interfaces;
using TripSample.Application.Services;

namespace TripSample.Application
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IBusLocationService, BusLocationService>();
            services.AddScoped<IJourneyService, JourneyService>();

            return services;
        }
    }
}
