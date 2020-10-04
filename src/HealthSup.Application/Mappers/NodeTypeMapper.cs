using HealthSup.Application.DataContracts.v1.Responses.Node;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class NodeTypeMapper
    {
        public static NodeTypeResponse ToDataContract(this NodeType node)
            => new NodeTypeResponse()
            {
                Id = node.Id,
                Code = node.Code,
                Name = node.Name
            };
    }
}
