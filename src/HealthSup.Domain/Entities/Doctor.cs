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

        public int Id { get; set; }

        public string Name { get; set; }

        public string Crm { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public User User { get; set; }
    }
}
