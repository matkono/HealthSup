using System.Collections.Generic;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class ListMedicalAppointmentsPagedByPatientIdResponse
    {
        public List<MedicalAppointmentResponse> MedicalAppointments { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRows { get; set; }
    }
}
