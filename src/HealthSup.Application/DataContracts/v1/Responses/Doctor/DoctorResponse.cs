using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Doctor
{
    public class DoctorResponse
    {
        public string Name { get; set; }

        public string Crm { get; set; }

        public string Phone { get; set; }

        public int UserId { get; set; }
    }
}
