namespace Cardiompp.Application.DataContracts.v1.Responses.Authentication
{
    public class AuthenticationUserResponse
    {
        public AuthenticationUserResponse
        (
            string email,
            bool isActive
        )
        {
            Email = email;
            IsActive = isActive;
        }

        public AuthenticationUserResponse(){ }

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
