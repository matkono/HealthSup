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

        public int Id { get; set; }

        public int Code { get; set; }

        public string Title { get; set; }

        public PossibleAnswerGroup PossibleAnswerGroup { get; set; }
    }
}
