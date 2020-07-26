namespace HealthSup.Domain.Entities
{
    public class Doctor: DomainResponse
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
            Address address
        ) 
        {
            Id = id;
            Name = name;
            Crm = crm;
            Phone = phone;
            Email = email;
            Password = password;
            IsActive = isActive;
            Address = address;
        }

        public Doctor() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Crm { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public bool IsActive { get; private set; }

        public Address Address { get; private set; }

        public void SetAddress
        (
            Address address
        ) 
        {
            Address = address;
        }
    }
}
