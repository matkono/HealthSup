using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Node
{
    public class GetNextNodeReturn : BaseResponse<NodeResponse>
    {
        public GetNextNodeReturn
        (
            NodeResponse data
        ) : base(data)
        {
        }
    }
}
