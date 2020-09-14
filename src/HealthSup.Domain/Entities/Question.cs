namespace HealthSup.Domain.Entities
{
    public class Question: Node
    {
        public Question
        (
            int nodeId,
            bool isInitial,
            NodeType nodeType,
            DecisionTree decisionTree,
            int questionId,
            int code,
            string title,
            QuestionType questionType
        ): base(nodeId, isInitial, nodeType, decisionTree)
        {
            QuestionId = questionId;
            Code = code;
            Title = title;
            QuestionType = questionType;
        }

        public Question() { }

        public int QuestionId { get; private set; }

        public int Code { get; private set; }

        public string Title { get; private set; }

        public QuestionType QuestionType { get; private set; }

        public void SetQuestionType
        (
            QuestionType questionType
        )
        {
            QuestionType = questionType;
        }
    }
}
