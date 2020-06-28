using Cardiompp.Application.DataContracts.v1.Requests.Authenticate;
using Cardiompp.Application.DataContracts.v1.Responses.Authentication;
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

        private IHashCrossCuttingService HashCrossCuttingService { get; set; }

        private IAuthenticationCrossCuttingService AuthenticationCrossCuttingService;

        public async Task<GetAuthenticationAgentResponse> AuthenticateAsync(AuthenticateAgentRequest authenticateRequest)
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
    }
}
