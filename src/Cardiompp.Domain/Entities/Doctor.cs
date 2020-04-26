namespace Cardiompp.Domain.Entities
{
    public class Doctor
    {
        public Doctor(
            int id,
            string name,
            string crm,
            string phone,
            Address address
        ) 
        {
            Id = id;
            Name = name;
            Crm = crm;
            Phone = phone;
            Address = address;
        }

        public Doctor() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Crm { get; private set; }

        public string Phone { get; private set; }

        public Address Address { get; private set; }

        public void SetAddress(Address address) 
        {
            Address = address;
        }
    }
}
