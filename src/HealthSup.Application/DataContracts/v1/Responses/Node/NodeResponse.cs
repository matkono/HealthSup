using HealthSup.Application.DataContracts.v1.Responses.DecisionTree;
using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.Node
{
    [DataContract]
    public class NodeResponse
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public bool IsInitial { get; private set; }

        [DataMember]
        public NodeTypeResponse NodeType { get; private set; }

        [DataMember]
        public DecisionTreeResponse DecisionTree { get; private set; }
    }
}
