namespace HealthSup.Domain.Entities
{
    public class DecisionTreeRule
    {
        public DecisionTreeRule
        (
            int id,
            Node fromNode,
            Node toNode,
            PossibleAnswerGroup possibleAnswerGroup
        )
        {
            Id = id;
            FromNode = fromNode;
            ToNode = toNode;
            PossibleAnswerGroup = possibleAnswerGroup;
        }

        public DecisionTreeRule() { }

        public int Id { get; private set;}

        public Node FromNode { get; private set; }

        public Node ToNode { get; private set; }

        public PossibleAnswerGroup PossibleAnswerGroup { get; private set;}

        public void SetFromNode
        (
            Node fromNode
        )
        {
            FromNode = fromNode;
        }

        public void SetToNode
        (
            Node toNode
        )
        {
            ToNode = toNode;
        }

        public void SetPossibleAnswerGroup
        (
            PossibleAnswerGroup possibleAnswerGroup
        )
        {
            PossibleAnswerGroup = possibleAnswerGroup;
        }
    }
}
