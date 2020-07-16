namespace Cardiompp.Application.DataContracts.v1.Responses.Authentication
{
    public class AuthenticationAgentResponse
    {
        public AuthenticationAgentResponse
        (
            string token
        ) 
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
