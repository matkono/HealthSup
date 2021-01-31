using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.MedicalAppointment
{
    public class ListPagedByPatientIdReturn : BaseResponse<ListPagedByPatientIdResponse>
    {
        public ListPagedByPatientIdReturn(ListPagedByPatientIdResponse data) : base(data)
        {
        }
    }
}
