using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    public class ListPagedReturn : BaseResponse<ListPagedResponse>
    {
        public ListPagedReturn(ListPagedResponse data) : base(data)
        {
        }
    }
}
