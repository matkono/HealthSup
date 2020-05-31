namespace Cardiompp.Domain.Entities
{
    public class CardiomppAgent: DomainResponse
    {
        public CardiomppAgent
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

        public CardiomppAgent() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Password { get; private set; }
    }
}
