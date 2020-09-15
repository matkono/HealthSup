namespace HealthSup.Domain.Entities
{
    public class Action: Node
    {
        public Action
        (
            int nodeId,
            bool isInitial,
            NodeType nodeType,
            DecisionTree decisionTree,
            int actionId, 
            int code,
            string title
        ) : base(nodeId, isInitial, nodeType, decisionTree)
        {
            ActionId = actionId;
            Code = code;
            Title = title;
        }

        public Action() { }

        public int ActionId { get; private set; }

        public int Code { get; private set; }

        public string Title { get; private set; }
    }
}
