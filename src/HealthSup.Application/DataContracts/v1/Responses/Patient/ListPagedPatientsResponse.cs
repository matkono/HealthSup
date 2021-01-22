using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    public class ListPagedPatientsResponse
    {
        public List<PatientResponse> Patients { get; set; }
    }
}
