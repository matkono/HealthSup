using Cardiompp.Application.DataContracts.Responses;

namespace Cardiompp.Application.DataContracts.v1.Responses.Authentication
{
    public class GetAuthenticationResponse: BaseResponse<AuthenticationResponse>
    {
        public GetAuthenticationResponse(AuthenticationResponse response) : base(response)
        {
        }
    }
}
