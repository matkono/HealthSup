namespace HealthSup.Domain.Entities
{
    public class HealthSupAgent: BaseEntity
    {
        public HealthSupAgent
        (
            int id,
            string name,
            string keyAgent,
            string password
        ) 
        {
            Id = id;
            Name = name;
            KeyAgent = keyAgent;
            Password = password;
        }

        public HealthSupAgent() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string KeyAgent { get; private set; }

        public string Password { get; private set; }
    }
}
