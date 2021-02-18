using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    public class GetPatientByRegistrationReturn:  BaseResponse<PatientResponse>
    {
        public GetPatientByRegistrationReturn(PatientResponse data) : base(data)
        {
        }
    }
}
