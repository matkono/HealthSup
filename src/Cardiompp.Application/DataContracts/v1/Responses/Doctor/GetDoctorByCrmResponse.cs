using Cardiompp.Application.DataContracts.v1.Responses.Address;

namespace Cardiompp.Application.DataContracts.v1.Responses.Doctor
{
    public class GetDoctorByCrmResponse
    {
        public string Name { get; set; }

        public string Crm { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; } 

        public AddressResponse Address { get; set; }
    }
}
