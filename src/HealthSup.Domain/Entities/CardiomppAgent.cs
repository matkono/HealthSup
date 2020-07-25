namespace HealthSup.Domain.Entities
{
    public class HealthSupAgent: DomainResponse
    {
        public HealthSupAgent
        (
            int id,
            string name,
            string password
        ) 
        {
            Id = id;
            Name = name;
            Password = password;
        }

        public HealthSupAgent() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }
    }
}
