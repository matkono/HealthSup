using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class NodeMapper
    {
        public static NodeResponse ToDataContract(this Node node)
            => new NodeResponse()
            {
                Id = node.Id,
                IsInitial = node.IsInitial,
                NodeType = node.NodeType?.ToDataContract(),
                DecisionTree = node.DecisionTree?.ToDataContract()
            };
    }
}
