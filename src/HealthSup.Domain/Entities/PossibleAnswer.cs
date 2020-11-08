namespace HealthSup.Domain.Entities
{
    public class PossibleAnswer: BaseEntity
    {
        public PossibleAnswer
        (
            int id,
            int code,
            string title,
            PossibleAnswerGroup possibleAnswerGroup
        )
        {
            Id = id;
            Code = code;
            Title = title;
            PossibleAnswerGroup = possibleAnswerGroup;
        }

        public PossibleAnswer() { }

        public int Id { get; private set; }

        public int Code { get; private set; }

        public string Title { get; private set; }

        public PossibleAnswerGroup PossibleAnswerGroup { get; private set; }

        public void SetPossibleAnswerGroup
        (
            PossibleAnswerGroup possibleAnswerGroup
        )
        {
            PossibleAnswerGroup = possibleAnswerGroup;
        }
    }
}
