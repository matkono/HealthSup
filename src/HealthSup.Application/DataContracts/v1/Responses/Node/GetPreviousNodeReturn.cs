using HealthSup.Application.DataContracts.Responses;

namespace HealthSup.Application.DataContracts.v1.Responses.Node
{
    public class GetPreviousNodeReturn : BaseResponse<NodeResponse>
    {
        public GetPreviousNodeReturn
        (
            NodeResponse data
        ) : base(data)
        {
        }
    }
}
