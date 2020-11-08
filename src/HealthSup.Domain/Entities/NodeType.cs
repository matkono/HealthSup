namespace HealthSup.Domain.Entities
{
    public class NodeType
    {
        public NodeType
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

        public NodeType() { }

        public int Id { get; private set; }

        public int Code { get; private set; }

        public string Name { get; private set; }
    }
}
