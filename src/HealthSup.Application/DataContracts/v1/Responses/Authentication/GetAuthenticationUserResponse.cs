using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Authentication
{
    public class GetAuthenticationUserResponse : BaseResponse<AuthenticationUserResponse>
    {
        public GetAuthenticationUserResponse
        (
            AuthenticationUserResponse response
        ) : base(response)
        {
        }
    }
}
