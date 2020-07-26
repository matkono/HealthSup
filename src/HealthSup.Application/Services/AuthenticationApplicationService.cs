using HealthSup.Application.DataContracts.Responses;
using HealthSup.Application.DataContracts.v1.Requests.Authenticate;
using HealthSup.Application.DataContracts.v1.Requests.Doctor;
using HealthSup.Application.DataContracts.v1.Responses.Authentication;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Infrastructure.CrossCutting.Authentication.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Hash.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        public AuthenticationApplicationService
        (
            IHashService hashService,
            IAuthenticationService authenticationService
        )
        {
            HashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            AuthenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        private readonly IHashService HashService;

        private readonly IAuthenticationService AuthenticationService;

        public async Task<GetAuthenticationAgentResponse> AuthenticateAgentAsync
        (
            AuthenticationAgentRequest authenticateRequest
        )
        {
            var passwordMd5 = HashService.GetMd5Hash(authenticateRequest.Password);
            var agent = await AuthenticationService.AuthenticateAgentAsync
            (
                authenticateRequest.AgentKey,
                passwordMd5
            );
            
            if (agent == null)
            {
                var response = new GetAuthenticationAgentResponse(null);

                response.AddError
                (
                    (int)ValidationErrorCodeEnum.AgentNameOrPasswordInvalid,
                    "AgentKey or password is incorrect.",
                    null
                );

                return response;
            }
                
            var authenticationResponse = new AuthenticationAgentResponse
            (
                AuthenticationService.BuildToken()
            );
            
            return new GetAuthenticationAgentResponse(authenticationResponse);
        }

        public async Task<GetAuthenticationUserResponse> AuthenticateUserAsync
        (
            AuthenticationUserRequest authenticateUserRequest
        )
        {
            var passwordMd5 = HashService.GetMd5Hash(authenticateUserRequest.Password);
            var user = await AuthenticationService.AuthenticateUserAsync
            (
                authenticateUserRequest.Email,
                passwordMd5
            );

            if (user == null)
            {
                var response = new GetAuthenticationUserResponse(null);

                response.AddError
                (
                    (int)ValidationErrorCodeEnum.EmailOrPasswordInvalid,
                    "Email or password is incorrect.",
                    null
                );

                return response;
            }

            return new GetAuthenticationUserResponse(user.ToDataContract());
        }

        public async Task<BaseResponse> UpdatePassword
        (
            UpdateUserPasswordRequest updatePasswordRequest
        )
        {
            var baseResponse = new BaseResponse();

            var passwordMd5 = HashService.GetMd5Hash(updatePasswordRequest.Password);
            var user = await AuthenticationService.AuthenticateUserAsync
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

            var newPasswordMd5 = HashService.GetMd5Hash(updatePasswordRequest.NewPassword);

            await AuthenticationService.UpdatePassword(user.Id, newPasswordMd5);

            return baseResponse;
        }
    }
}
