using Cardiompp.Application.DataContracts.Responses;

namespace Cardiompp.Application.DataContracts.v1.Responses.Authentication
{
    public class GetAuthenticationAgentResponse: BaseResponse<AuthenticationAgentResponse>
    {
        public GetAuthenticationAgentResponse(AuthenticationAgentResponse response) : base(response)
        {
        }
    }
}
