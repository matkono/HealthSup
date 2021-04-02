using System.Collections.Generic;

namespace HealthSup.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public Patient() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Registration { get; set; }

        public Address Address { get; set; }

        public List<MedicalAppointment> MedicalAppointments { get; set; }

        public void SetAddress(Address address) 
        {
            Address = address;
        }
    }
}
