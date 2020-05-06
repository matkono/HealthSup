using System.ComponentModel.DataAnnotations;

namespace Cardiompp.Application.DataContracts.v1.Requests.Login
{
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "Agent name is required to auhenticate.")]
        public string AgentName { get; set; }

        [Required(ErrorMessage = "Password is required to auhenticate.")]
        public string Password { get; set; }
    }
}
