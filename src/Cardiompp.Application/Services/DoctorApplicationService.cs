using Cardiompp.Application.DataContracts.Responses;
using Cardiompp.Application.DataContracts.v1.Requests.Doctor;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Enums;
using Cardiompp.Domain.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Authentication.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Hash.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
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
