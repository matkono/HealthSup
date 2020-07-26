using HealthSup.Infrastructure.CrossCutting.Ioc.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthSup.WebApi.Configurations
{
    internal static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDI
        (
            this IServiceCollection services, 
            IConfiguration configuration
        )
        {
            services.ConfigureDependencyInjection(configuration);

            return services;
        }
    }
}
