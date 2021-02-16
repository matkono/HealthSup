namespace HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment
{
    public class CreateMedicalAppointmentRequest
    {
        public int PatientId { get; set; }

        public int DiseaseId { get; set; }
    }
}
