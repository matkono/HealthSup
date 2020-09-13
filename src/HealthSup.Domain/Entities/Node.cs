using HealthSup.Domain.Enums;

namespace HealthSup.Domain.Entities
{
    public class Node : BaseEntity
    {
        public Node
        (
            int id,
            bool isInitial,
            NodeType nodeType,
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

        public NodeType NodeType { get; private set; }

        public DecisionTree DecisionTree { get; private set; }

        public void SetNodeType
        (
            NodeType nodeType
        )
        {
            NodeType = nodeType;
        }

        public void SetDecisionTree
        (
            DecisionTree decisionTree
        )
        {
            DecisionTree = decisionTree;
        }
    }
}
