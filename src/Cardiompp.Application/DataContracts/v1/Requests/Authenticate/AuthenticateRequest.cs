namespace Cardiompp.Application.DataContracts.v1.Requests.Login
{
    public class AuthenticateRequest
    {
        public string AgentName { get; set; }

        public string Password { get; set; }
    }
}
