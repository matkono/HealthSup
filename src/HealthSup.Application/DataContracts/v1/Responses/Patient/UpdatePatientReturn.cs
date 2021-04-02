using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    public class UpdatePatientReturn : BaseResponse<PatientResponse>
    {
        public UpdatePatientReturn(PatientResponse data) : base(data)
        {
        }
    }
}
