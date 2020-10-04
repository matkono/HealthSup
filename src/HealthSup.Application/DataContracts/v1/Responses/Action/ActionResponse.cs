using HealthSup.Application.DataContracts.v1.Responses.Node;

namespace HealthSup.Application.DataContracts.v1.Responses.Action
{
    public class ActionResponse: NodeResponse
    {
        public int ActionId { get; set; }

        public int Code { get; set; }

        public string Title { get; set; }
    }
}
