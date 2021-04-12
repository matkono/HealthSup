namespace HealthSup.Application.DataContracts.v1.Responses.Authentication
{
    public class AuthenticationUserResponse
    {
        public AuthenticationUserResponse
        (
            int id,
            string email,
            bool isActive
        )
        {
            Id = id;
            Email = email;
            IsActive = isActive;
        }

        public AuthenticationUserResponse(){ }

        public int Id { get; set;}

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
