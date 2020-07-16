using System.ComponentModel.DataAnnotations;

namespace HealthSup.Application.DataContracts.v1.Requests.Authenticate
{
    public class AuthenticationUserRequest
    {
        [Required(ErrorMessage = "Email is required to auhenticate.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required to auhenticate.")]
        public string Password { get; set; }
    }
}
