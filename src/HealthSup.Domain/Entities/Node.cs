using HealthSup.Domain.Enums;

namespace HealthSup.Domain.Entities
{
    public class Node
    {
        public Node
        (
            int id,
            NodeTypeEnum nodeType,
            Disease disease
        )
        {
            Id = id;
            NodeType = nodeType;
            Disease = disease;
        }

        public Node() { }

        public int Id { get; private set; }

        public NodeTypeEnum NodeType { get; private set; }

        public Disease Disease { get; private set; }
    }
}
