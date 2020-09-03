using HealthSup.Domain.Enums;

namespace HealthSup.Domain.Entities
{
    public class Node : BaseEntity
    {
        public Node
        (
            int id,
            bool isInitial,
            NodeTypeEnum nodeType,
            DecisionTree decisionTree
        )
        {
            Id = id;
            IsInitial = isInitial;
            NodeType = nodeType;
            DecisionTree = decisionTree;
        }

        public Node() { }

        public int Id { get; private set; }

        public bool IsInitial { get; private set; }

        public NodeTypeEnum NodeType { get; private set; }

        public DecisionTree DecisionTree { get; private set; }
    }
}
