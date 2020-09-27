using HealthSup.Application.DataContracts.v1.Responses.Disease;
using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.DecisionTree
{
    [DataContract]
    public class DecisionTreeResponse
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string Version { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public DiseaseResponse Disease { get; private set; }
    }
}
