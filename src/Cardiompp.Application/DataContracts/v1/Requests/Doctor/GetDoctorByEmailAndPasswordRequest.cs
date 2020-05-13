using System.ComponentModel.DataAnnotations;

namespace Cardiompp.Application.DataContracts.v1.Requests.Doctor
{
    public class GetDoctorByEmailAndPasswordRequest
    {
        [Required(ErrorMessage = "Email is required to login.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required to login.")]
        public string Password { get; set; }
    }
}
