namespace HealthSup.Infrastructure.CrossCutting.Authentication.DTO
{
    public class AgentDTO
    {
        public AgentDTO
        (
            int id,
            string name,
            string key,
            string password
        )
        {
            Id = id;
            Name = name;
            Key = key;
            Password = password;
        }

        public AgentDTO() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Key { get; private set; }

        public string Password { get; private set; }
    }
}
