using System.Security.Cryptography;

namespace HealthSup.Domain.Entities
{
    public class PossibleAnswerGroup: BaseEntity
    {
        public PossibleAnswerGroup
        (
            int id,
            string description
        )
        {
            Id = id;
            Description = description;
        }

        public PossibleAnswerGroup() { }

        public int Id { get; set; }

        public string Description { get; set;}
    }
}
