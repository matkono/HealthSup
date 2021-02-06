using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Patient
{
    public class ListPatientsPagedReturn : BaseResponse<ListPatientsPagedResponse>
    {
        public ListPatientsPagedReturn(ListPatientsPagedResponse data) : base(data)
        {
        }
    }
}
