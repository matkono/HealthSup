using System.Collections.Generic;

namespace HealthSup.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public Patient
        (
            string name,
            string registration,
            Address address
        )
        {
            Name = name;
            Registration = registration;
            Address = address;
        }

        public Patient() { }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Registration { get; private set; }

        public Address Address { get; private set; }

        public List<MedicalAppointment> MedicalAppointments { get; private set; }

        public void SetAddress(Address address) 
        {
            Address = address;
        }
    }
}
