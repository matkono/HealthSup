using HealthSup.Application.Services;
using HealthSup.Application.Services.Contracts;
using HealthSup.Application.Validators;
using HealthSup.Application.Validators.Contracts;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services;
using HealthSup.Domain.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Authentication.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Hash.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Services.Authentication;
using HealthSup.Infrastructure.CrossCutting.Services.Hash;
using HealthSup.Infrastructure.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace HealthSup.Infrastructure.CrossCutting.Ioc.Ioc
{
    public static class DependencyInjectorConfiguration
    {
        public static void ConfigureDependencyInjection
        (
            this IServiceCollection services, 
            IConfiguration configuration
        )
        {
            services.ConfigureUnitOfWork(configuration);
            services.ConfigureServices();
            services.ConfigureExternalServices();
        }

        private static void ConfigureServices
        (
            this IServiceCollection services
        )
        {
            #region Validator

            services.AddScoped<IAnswerQuestionValidator, AnswerQuestionValidator>();
            services.AddScoped<IConfirmActionValidator, ConfirmActionValidator>();
            services.AddScoped<IGetPreviousNodeValidator, GetPreviousNodeValidator>();
            services.AddScoped<IConfirmDecisionValidator, ConfirmDecisionValidator>();

            #endregion

            #region CrossCutting

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IHashService, HashService>();

            #endregion

            #region ApplicationService

            services.AddScoped<IDoctorApplicationService, DoctorApplicationService>();
            services.AddScoped<IMedicalAppointmentApplicationService, MedicalAppointmentApplicationService>();
            services.AddScoped<IAuthenticationApplicationService, AuthenticationApplicationService>();
            services.AddScoped<IDecisionEngineApplicationService, DecisionEngineApplicationService>();

            #endregion

            #region DomainService

            services.AddScoped<IDoctorDomainService, DoctorDomainService>();
            services.AddScoped<IMedicalAppointmentDomainService, MedicalAppointmentDomainService>();
            services.AddScoped<IDecisionEngineDomainService, DecisionEngineDomainService>();
            services.AddScoped<INodeDomainService, NodeDomainService>();

            #endregion            
        }

        private static void ConfigureUnitOfWork
        (
            this IServiceCollection services, 
            IConfiguration configuration
        )
        {
            services.AddScoped<IDbConnection>(serviceProvider => new SqlConnection
            {
                ConnectionString = configuration.GetConnectionString("HealthSup")
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void ConfigureExternalServices
        (
            this IServiceCollection services
        )
        {

        }
    }
}
