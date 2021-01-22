using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    public class ListPagedPatientsReturn : BaseResponse<ListPagedPatientsResponse>
    {
        public ListPagedPatientsReturn(ListPagedPatientsResponse data) : base(data)
        {
        }
    }
}
