using Cardiompp.Application.DataContracts.v1.Requests.Authenticate;
using Cardiompp.Application.DataContracts.v1.Responses.Authentication;
using Cardiompp.Application.Mappers;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Enums;
using Cardiompp.Infrastructure.CrossCutting.Authentication.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Hash.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
{
    public class AuthenticationApplicationService : IAuthenticationApplicationService
    {
        public AuthenticationApplicationService
        (
            IHashCrossCuttingService hashCrossCuttingService,
            IAuthenticationCrossCuttingService authenticationCrossCuttingService
        )
        {
            HashCrossCuttingService = hashCrossCuttingService ?? throw new ArgumentNullException(nameof(hashCrossCuttingService));
            AuthenticationCrossCuttingService = authenticationCrossCuttingService ?? throw new ArgumentNullException(nameof(authenticationCrossCuttingService));
        }

        private readonly IHashCrossCuttingService HashCrossCuttingService;

        private readonly IAuthenticationCrossCuttingService AuthenticationCrossCuttingService;

        public async Task<GetAuthenticationAgentResponse> AuthenticateAgentAsync(AuthenticationAgentRequest authenticateRequest)
        {
            var passwordMd5 = HashCrossCuttingService.GetMd5Hash(authenticateRequest.Password);
            var agent = await AuthenticationCrossCuttingService.AuthenticateAgentAsync
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
                AuthenticationCrossCuttingService.BuildToken()
            );
            
            return new GetAuthenticationAgentResponse(authenticationResponse);
        }

        public async Task<GetAuthenticationUserResponse> AuthenticateUserAsync(AuthenticationUserRequest authenticateUserRequest)
        {
            var passwordMd5 = HashCrossCuttingService.GetMd5Hash(authenticateUserRequest.Password);
            var user = await AuthenticationCrossCuttingService.AuthenticateUserAsync
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
    }
}
