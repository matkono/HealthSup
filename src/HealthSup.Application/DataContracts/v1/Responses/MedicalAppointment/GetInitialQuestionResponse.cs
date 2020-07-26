using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class GetInitialQuestionResponse: BaseResponse<InitialQuestionResponse>
    {
        public GetInitialQuestionResponse
        (
            InitialQuestionResponse response
        ) : base(response)
        {
        }
    }
}
