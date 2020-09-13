namespace HealthSup.Domain.Entities
{
    public class QuestionType: BaseEntity
    {
        public int Id { get; private set; }

        public int Code { get; private set; }

        public string Name { get; private set; }
    }
}
