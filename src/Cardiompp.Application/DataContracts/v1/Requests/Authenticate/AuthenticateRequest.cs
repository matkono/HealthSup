using System.ComponentModel.DataAnnotations;

namespace Cardiompp.Application.DataContracts.v1.Requests.Login
{
    public class AuthenticateRequest
    {
        [Required]
        public string AgentName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
