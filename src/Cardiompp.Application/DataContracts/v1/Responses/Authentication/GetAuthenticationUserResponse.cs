using Cardiompp.Application.DataContracts.Responses;

namespace Cardiompp.Application.DataContracts.v1.Responses.Authentication
{
    public class GetAuthenticationUserResponse : BaseResponse<AuthenticationUserResponse>
    {
        public GetAuthenticationUserResponse(AuthenticationUserResponse response) : base(response)
        {
        }
    }
}
