namespace HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment
{
    public class ListPagedByPatientIdRequest
    {
        public Pagination Pagination { get; set; }

        public int PatientId { get; set; }
    }
}
