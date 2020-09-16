namespace HealthSup.Domain.Entities
{
    public class PossibleAnswer: BaseEntity
    {
        public PossibleAnswer
        (
            int id,
            int code,
            string title,
            SetPossibleAnswer setPossibleAnswer
        )
        {
            Id = id;
            Code = code;
            Title = title;
            SetPossibleAnswer = setPossibleAnswer;
        }

        public PossibleAnswer() { }

        public int Id { get; private set; }

        public int Code { get; private set; }

        public string Title { get; private set; }

        public SetPossibleAnswer SetPossibleAnswer { get; private set; }

        public void SetSetPossibleAnswer
        (
            SetPossibleAnswer setPossibleAnswer
        )
        {
            SetPossibleAnswer = setPossibleAnswer;
        }
    }
}
