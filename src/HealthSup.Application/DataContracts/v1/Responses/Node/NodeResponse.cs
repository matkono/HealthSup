using HealthSup.Application.DataContracts.v1.Responses.DecisionTree;

namespace HealthSup.Application.DataContracts.v1.Responses.Node
{
    public class NodeResponse
    {
        public int Id { get; set; }

        public bool IsInitial { get; set; }

        public NodeTypeResponse NodeType { get; set; }

        public DecisionTreeResponse DecisionTree { get; set; }
    }
}
