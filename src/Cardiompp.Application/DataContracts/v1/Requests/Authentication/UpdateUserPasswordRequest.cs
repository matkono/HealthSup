using HealthSup.Application.DataContracts.v1.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace HealthSup.Application.DataContracts.v1.Requests.Doctor
{
    public class UpdateUserPasswordRequest
    {
        [Required(ErrorMessage = "Email is required to reset password.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required to reset password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "New password is required to reset password.")]
        [NotEqual(nameof(Password))]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required to reset password.")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords mismatch")]
        public string ConfirmNewPassword { get; set; }
    }
}
