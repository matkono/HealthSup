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
            IHashCrossCuttingService hashCrossCuttingService,
            IAuthenticationCrossCuttingService authenticationCrossCuttingService
        )
        {
            DoctorServiceDomain = doctorServiceDomain ?? throw new ArgumentNullException(nameof(doctorServiceDomain));
            HashCrossCuttingService = hashCrossCuttingService ?? throw new ArgumentNullException(nameof(hashCrossCuttingService));
            AuthenticationCrossCuttingService = authenticationCrossCuttingService ?? throw new ArgumentNullException(nameof(authenticationCrossCuttingService));
        }

        private IDoctorDomainService DoctorServiceDomain;

        private IHashCrossCuttingService HashCrossCuttingService;

        private IAuthenticationCrossCuttingService AuthenticationCrossCuttingService;

        public async Task<BaseResponse> UpdatePassword
        (
            UpdatePasswordRequest updatePasswordRequest
        ) 
        {
            var baseResponse = new BaseResponse();

            var passwordMd5 = HashCrossCuttingService.GetMd5Hash(updatePasswordRequest.Password);
            var user = await AuthenticationCrossCuttingService.AuthenticateUserAsync
            (
                updatePasswordRequest.Email,
                passwordMd5
            );

            if (user == null)
            {
                baseResponse.AddError
                (
                    (int)ValidationErrorCodeEnum.EmailOrPasswordInvalid,
                    "Email or password is incorrect.",
                    null
                );

                return baseResponse;
            }

            var newPasswordMd5 = HashCrossCuttingService.GetMd5Hash(updatePasswordRequest.NewPassword);

            await DoctorServiceDomain.UpdatePassword(user.Id, newPasswordMd5);

            return baseResponse;
        }
    }
}
