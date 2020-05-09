using Cardiompp.Application.DataContracts.Responses;

namespace Cardiompp.Application.DataContracts.v1.Responses.Doctor
{
    public class GetDoctorResponse<T>: BaseResponse<T>
    {
        public GetDoctorResponse(T response) : base(response)
        {
        }
    }
}
