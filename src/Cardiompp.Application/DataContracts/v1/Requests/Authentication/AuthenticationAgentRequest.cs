using System.ComponentModel.DataAnnotations;

namespace Cardiompp.Application.DataContracts.v1.Requests.Authenticate
{
    public class AuthenticationAgentRequest
    {
        [Required(ErrorMessage = "Agent key is required to auhenticate.")]
        public string AgentKey { get; set; }

        [Required(ErrorMessage = "Password is required to auhenticate.")]
        public string Password { get; set; }
    }
}
