using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    public class ListPagedPatientsResponse
    {
        public List<PatientResponse> Patients { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }
    }
}
