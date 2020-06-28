using Cardiompp.Application.DataContracts.v1.Requests.Authenticate;
using Cardiompp.Application.DataContracts.v1.Responses.Authentication;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services.Contracts
{
    public interface IAuthenticationApplicationService
    {
        public Task<GetAuthenticationAgentResponse> AuthenticateAgentAsync(AuthenticationAgentRequest authenticateAgentRequest);

        public Task<GetAuthenticationUserResponse> AuthenticateUserAsync(AuthenticationUserRequest authenticateUserRequest);
    }
}
