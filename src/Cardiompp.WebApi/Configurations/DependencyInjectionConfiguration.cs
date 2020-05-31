using Cardiompp.Infrastructure.CrossCutting.Ioc.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cardiompp.WebApi.Configurations
{
    internal static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDependencyInjection(configuration);

            return services;
        }
    }
}
