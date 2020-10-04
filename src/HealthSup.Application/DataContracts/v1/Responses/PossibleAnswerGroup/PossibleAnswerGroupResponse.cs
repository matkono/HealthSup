using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.PossibleAnswerGroup
{
    [DataContract]
    public class PossibleAnswerGroupResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
