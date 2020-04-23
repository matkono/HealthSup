namespace Cardiompp.Domain.Entities
{
    public class Address
    {
        public Address(
            int id, 
            string region,
            string cep
        ) 
        {
            Id = id;
            Region = region;
            Cep = cep;
        }

        public Address() { }

        public int Id { get; private set; }

        public string Region { get; private set; }

        public string Cep { get; private set; }
    }
}
