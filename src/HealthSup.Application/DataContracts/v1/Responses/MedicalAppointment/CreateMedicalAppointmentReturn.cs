using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class CreateMedicalAppointmentReturn : BaseResponse<MedicalAppointmentResponse>
    {
        public CreateMedicalAppointmentReturn(MedicalAppointmentResponse data) : base(data)
        {
        }
    }
}
