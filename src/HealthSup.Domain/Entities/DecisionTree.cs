namespace HealthSup.Domain.Entities
{
    public class DecisionTree: BaseEntity
    {
        public DecisionTree(
            int id,
            string description,
            string version,
            Disease disease
        )
        {
            Id = id;
            Description = description;
            Version = version;
            Disease = disease;
        }

        public DecisionTree() { }

        public int Id { get; private set; }

        public string Description { get; private set; }

        public string Version { get; private set; }

        public Disease Disease { get; private set; }
    }
}
