using System.ComponentModel.DataAnnotations;

namespace HealthSup.Application.DataContracts.v1.Requests.MedicalAppointment
{
    public class GetInitialQuestionRequest
    {
        [Required(ErrorMessage = "DiseaseId is required to get initial question.")]
        public int DiseaseId { get; set; }
    }
}
