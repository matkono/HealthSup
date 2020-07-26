namespace HealthSup.Domain.Entities
{
    public class PossibleAnswer
    {
        public PossibleAnswer
        (
            int id,
            int code,
            string value
        )
        {
            Id = id;
            Code = code;
            Value = value;
        }

        public PossibleAnswer() { }

        public int Id { get; private set; }

        public int Code { get; private set; }

        public string Value { get; private set; }
    }
}
