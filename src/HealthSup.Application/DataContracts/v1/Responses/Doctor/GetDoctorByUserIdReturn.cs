using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Doctor
{
    public class GetDoctorByUserIdReturn : BaseResponse<DoctorResponse>
    {
        public GetDoctorByUserIdReturn(DoctorResponse data) : base(data)
        {
        }
    }
}
