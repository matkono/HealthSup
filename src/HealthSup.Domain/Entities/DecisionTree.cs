using Microsoft.IdentityModel.Tokens;

namespace HealthSup.Domain.Entities
{
    public class DecisionTree: BaseEntity
    {
        public DecisionTree(
            int id,
            string description,
            string version,
            bool isCurrent,
            Disease disease
        )
        {
            Id = id;
            Description = description;
            Version = version;
            IsCurrent = isCurrent;
            Disease = disease;
        }

        public DecisionTree() { }

        public int Id { get; private set; }

        public string Description { get; private set; }

        public string Version { get; private set; }

        public bool IsCurrent { get; private set; }

        public Disease Disease { get; private set; }
    }
}
