namespace HealthSup.Domain.Entities
{
    public class Doctor: BaseEntity
    {
        public Doctor
        (
            int id,
            string name,
            string crm,
            string phone,
            string email,
            string password,
            bool isActive,
            User user
        ) 
        {
            Id = id;
            Name = name;
            Crm = crm;
            Phone = phone;
            Email = email;
            Password = password;
            IsActive = isActive;
            User = user;
        }

        public Doctor() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Crm { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public bool IsActive { get; private set; }

        public User User { get; private set; }

        public void SetUser
        (
            User user
        )
        {
            User = user;
        }
    }
}
