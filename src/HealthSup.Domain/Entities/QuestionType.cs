namespace HealthSup.Domain.Entities
{
    public class QuestionType
    {
        public QuestionType
        (
            int id, 
            int code,
            string name
        )
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public QuestionType() { }

        public int Id { get; private set; }

        public int Code { get; private set; }

        public string Name { get; private set; }
    }
}
