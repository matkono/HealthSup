using HealthSup.Application.DataContracts.Responses;
using HealthSup.Application.DataContracts.v1.Requests.Doctor;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Authentication.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Hash.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class DoctorApplicationService : IDoctorApplicationService
    {
        public DoctorApplicationService
        (
            IDoctorDomainService doctorServiceDomain,
            IHashService hashCrossCuttingService,
            IAuthenticationService authenticationCrossCuttingService
        )
        {
            DoctorServiceDomain = doctorServiceDomain ?? throw new ArgumentNullException(nameof(doctorServiceDomain));
            HashService = hashCrossCuttingService ?? throw new ArgumentNullException(nameof(hashCrossCuttingService));
            AuthenticationService = authenticationCrossCuttingService ?? throw new ArgumentNullException(nameof(authenticationCrossCuttingService));
        }

        private IDoctorDomainService DoctorServiceDomain;

        private IHashService HashService;

        private IAuthenticationService AuthenticationService;
    }
}
