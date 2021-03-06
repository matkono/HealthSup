namespace HealthSup.Domain.Entities
{
    public class Address: BaseEntity
    {
        public Address(
            int id, 
            string neighborhood,
            string cep,
            string city
        ) 
        {
            Id = id;
            Neighborhood = neighborhood;
            Cep = cep;
            City = city;
        }

        public Address() { }

        public int Id { get; private set; }

        public string Neighborhood { get; private set; }

        public string Cep { get; private set; }

        public string City { get; private set; }
    }
}
