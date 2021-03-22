using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    public class CreatePatientReturn : BaseResponse<PatientResponse>
    {
        public CreatePatientReturn(PatientResponse data) : base(data)
        {
        }
    }
}
