using Cardiompp.Application.Services;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Repositories;
using Cardiompp.Domain.Services;
using Cardiompp.Domain.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Authentication.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Hash.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Services.Authentication;
using Cardiompp.Infrastructure.CrossCutting.Services.Hash;
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
            services.AddScoped<IDoctorApplicationService, DoctorApplicationService>();
            services.AddScoped<IDoctorDomainService, DoctorDomainService>();
            services.AddScoped<IAuthenticationApplicationService, AuthenticationApplicationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IHashService, HashService>();
        }

        private static void ConfigureUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(serviceProvider => new SqlConnection
            {
                ConnectionString = configuration.GetConnectionString("HealthSup")
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureExternalServices(this IServiceCollection services)
        {

        }
    }
}
