using HealthSup.Application.DataContracts.v1.Responses.Node;

namespace HealthSup.Application.DataContracts.v1.Responses.Action
{
    public class ActionResponse: NodeResponse
    {
        public ActionBase Action { get; set; }
    }
}
