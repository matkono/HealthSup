using HealthSup.Application.DataContracts.Responses;
using HealthSup.Application.DataContracts.v1.Requests.Authenticate;
using HealthSup.Application.DataContracts.v1.Requests.Doctor;
using HealthSup.Application.DataContracts.v1.Responses.Authentication;
using System.Threading.Tasks;

namespace HealthSup.Application.Services.Contracts
{
    public interface IAuthenticationApplicationService
    {
        public Task<GetAuthenticationAgentResponse> AuthenticateAgentAsync
        (
            AuthenticationAgentRequest authenticateAgentRequest
        );

        public Task<GetAuthenticationUserResponse> AuthenticateUserAsync
        (
            AuthenticationUserRequest authenticateUserRequest
        );

        public Task<BaseResponse> UpdatePassword
        (
            UpdateUserPasswordRequest updatePasswordRequest
        );
    }
}
