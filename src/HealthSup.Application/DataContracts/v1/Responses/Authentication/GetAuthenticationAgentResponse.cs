using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Authentication
{
    public class GetAuthenticationAgentResponse: BaseResponse<AuthenticationAgentResponse>
    {
        public GetAuthenticationAgentResponse
        (
            AuthenticationAgentResponse response
        ) : base(response)
        {
        }
    }
}
