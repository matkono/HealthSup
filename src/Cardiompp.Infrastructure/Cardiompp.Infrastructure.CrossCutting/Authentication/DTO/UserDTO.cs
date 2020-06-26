namespace Cardiompp.Infrastructure.CrossCutting.Authentication.DTO
{
    public class UserDTO
    {
        public UserDTO(
            int id,
            string email,
            string password,
            bool isActive
        )
        {
            Id = id;
            Email = email;
            Password = password;
            IsActive = isActive;
        }

        public int Id { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public bool IsActive { get; private set; }
    }
}
