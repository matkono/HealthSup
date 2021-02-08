using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Disease
{
    public class ListDiseasesPagedReturn : BaseResponse<ListDiseasesPagedResponse>
    {
        public ListDiseasesPagedReturn(ListDiseasesPagedResponse data) : base(data)
        {
        }
    }
}
