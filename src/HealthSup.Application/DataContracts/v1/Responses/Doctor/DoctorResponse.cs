using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Doctor
{
    public class DoctorResponse<T>: BaseResponse<T>
    {
        public DoctorResponse
        (
            T response
        ) : base(response)
        {
        }
    }
}
