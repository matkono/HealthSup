using Cardiompp.Application.DataContracts.v1.Responses.Address;

namespace Cardiompp.Application.DataContracts.v1.Responses.Doctor
{
    public class DoctorResponse
    {
        public string Name { get; set; }

        public string Crm { get; set; }

        public string Phone { get; set; }

        public AddressResponse Address { get; set; }
    }
}
