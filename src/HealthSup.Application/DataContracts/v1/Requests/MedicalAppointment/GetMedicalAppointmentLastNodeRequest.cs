using System.ComponentModel.DataAnnotations;

namespace HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment
{
    public class GetMedicalAppointmentLastNodeRequest
    {
        [Required(ErrorMessage = "MedicalAppointmentId is required to get last node of medical appointment.")]
        public int MedicalAppointmentId { get; set; }
    }
}
