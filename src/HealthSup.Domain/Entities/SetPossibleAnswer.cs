using System.Security.Cryptography;

namespace HealthSup.Domain.Entities
{
    public class SetPossibleAnswer: BaseEntity
    {
        public SetPossibleAnswer
        (
            int id,
            string description
        )
        {
            Id = id;
            Description = description;
        }

        public SetPossibleAnswer() { }

        public int Id { get; private set; }

        public string Description { get; private set;}
    }
}
