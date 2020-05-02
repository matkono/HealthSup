namespace Cardiompp.Application.DataContracts.v1.Responses.Authentication
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse
        (
            string token
        ) 
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
