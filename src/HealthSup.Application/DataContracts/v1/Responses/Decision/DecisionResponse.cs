using HealthSup.Application.DataContracts.v1.Responses.Node;

namespace HealthSup.Application.DataContracts.v1.Responses.Decision
{
    public class DecisionResponse: NodeResponse
    {
        public int DecisionId { get; set; }

        public int Code { get; set; }

        public string Title { get; set; }

        public bool IsDiagnostic { get; set; }
    }
}
