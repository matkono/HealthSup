using Cardiompp.Application.DataContracts.v1.Requests.Authenticate;
using Cardiompp.Application.DataContracts.v1.Responses.Authentication;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService
        (
            IAuthenticationServiceDomain authenticationServiceDomain
        )
        {
            AuthenticationServiceDomain = authenticationServiceDomain ?? throw new ArgumentNullException(nameof(authenticationServiceDomain));
        }

        private IAuthenticationServiceDomain AuthenticationServiceDomain;

        public async Task<GetAuthenticationResponse> AuthenticateAsync(AuthenticateRequest authenticateRequest)
        {
            var cardiomppAgent = await AuthenticationServiceDomain.AuthenticateAsync
            (
                authenticateRequest.AgentName,
                authenticateRequest.Password
            );


            if (cardiomppAgent.Errors != null && cardiomppAgent.Errors.Any())
            {
                var response = new GetAuthenticationResponse(null);

                foreach (var error in cardiomppAgent.Errors)
                    response.AddError(error.Code, error.Message, error.Field);

                return response;

            }
                
            var authenticationResponse = new AuthenticationResponse
            (
                AuthenticationServiceDomain.BuildToken()
            );
            
            return new GetAuthenticationResponse(authenticationResponse);
        }
    }
}
