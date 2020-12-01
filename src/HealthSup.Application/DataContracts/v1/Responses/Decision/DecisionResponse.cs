using HealthSup.Application.DataContracts.v1.Responses.Node;

namespace HealthSup.Application.DataContracts.v1.Responses.Decision
{
    public class DecisionResponse: NodeResponse
    {
        public DecisionBase Decision { get; set; }
    }
}
