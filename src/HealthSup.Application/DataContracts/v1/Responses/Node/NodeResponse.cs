namespace HealthSup.Application.DataContracts.v1.Responses.Node
{
    public class NodeResponse
    {
        public int Id { get; private set; }

        public bool IsInitial { get; private set; }

        public NodeTypeResponse NodeType { get; private set; }
    }
}
