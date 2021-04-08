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

        public int Id { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public bool? IsCurrent { get; set; }

        public Disease Disease { get; set; }

        public void SetDisease
        (
            Disease disease
        )
        {
            Disease = disease;
        }
    }
}
