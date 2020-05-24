using Cardiompp.Application.Services;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Repositories;
using Cardiompp.Domain.Services;
using Cardiompp.Domain.Services.Contracts;
using Cardiompp.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace Cardiompp.Infrastructure.CrossCutting.Ioc.Ioc
{
    public static class DependencyInjectorConfiguration
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureUnitOfWork(configuration);
            services.ConfigureServices();
            services.ConfigureExternalServices();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorServiceApplication>();
            services.AddScoped<IDoctorServiceDomain, DoctorServiceDomain>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationServiceDomain, AuthenticationServiceDomain>();
            services.AddScoped<IHashServiceDomain, HashServiceDomain>();
        }

        private static void ConfigureUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(serviceProvider => new SqlConnection
            {
                ConnectionString = configuration.GetConnectionString("Cardiompp")
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureExternalServices(this IServiceCollection services)
        {

        }
    }
}
