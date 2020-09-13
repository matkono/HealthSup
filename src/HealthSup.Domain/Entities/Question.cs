namespace HealthSup.Domain.Entities
{
    public class Question: Node
    {
        public Question
        (
            int questionId,
            int code,
            string title,
            QuestionType questionType
        )
        {
            QuestionId = questionId;
            Code = code;
            Title = title;
            QuestionType = questionType;
        }

        public int QuestionId { get; private set; }

        public int Code { get; private set; }

        public string Title { get; private set; }

        public QuestionType QuestionType { get; private set; }
    }
}
