namespace HealthSup.Domain.Entities
{
    public class Decision : Node
    {
        public Decision
        (
            int nodeId,
            bool isInitial,
            NodeType nodeType,
            DecisionTree decisionTree,
            int decisionId,
            int code,
            string title,
            bool isDiagnostic
        ) : base(nodeId, isInitial, nodeType, decisionTree)
        {
            DecisionId = decisionId;
            Code = code;
            Title = title;
            IsDiagnostic = isDiagnostic;
        }

        public Decision() { }

        public int DecisionId { get; private set; }

        public int Code { get; private set; }

        public string Title { get; private set; }

        public bool IsDiagnostic { get; set; }
    }
}
