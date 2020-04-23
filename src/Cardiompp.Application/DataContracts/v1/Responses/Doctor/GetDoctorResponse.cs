using Cardiompp.Application.DataContracts.Responses;

namespace Cardiompp.Application.DataContracts.v1.Responses.Doctor
{
    public class GetDoctorResponse: BaseResponse<DoctorResponse>
    {
        public GetDoctorResponse(DoctorResponse response) : base(response)
        {
        }
    }
}
