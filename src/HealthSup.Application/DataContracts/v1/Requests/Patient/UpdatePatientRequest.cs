using Microsoft.AspNetCore.Mvc;

namespace HealthSup.Application.DataContracts.v1.Requests.Patient
{
    public class UpdatePatientRequest
    {
        [FromQuery]
        public int PatientId { get; set; }

        [FromBody]
        public Address.Address? Address { get; set;}
    }
}
