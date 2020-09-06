using HealthSup.Application.DataContracts.Responses;
using HealthSup.Application.DataContracts.v1.Responses.Node;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class GetMedicalAppointmentLastNodeReturn : BaseResponse<NodeResponse>
    {
        public GetMedicalAppointmentLastNodeReturn
        (
            NodeResponse data
        ) : base(data)
        {
        }
    }
}
