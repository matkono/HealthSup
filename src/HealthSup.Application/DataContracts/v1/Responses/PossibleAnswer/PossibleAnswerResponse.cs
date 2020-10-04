using HealthSup.Application.DataContracts.v1.Responses.PossibleAnswerGroup;
using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.PossibleAnswer
{
    [DataContract]
    public class PossibleAnswerResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public PossibleAnswerGroupResponse PossibleAnswerGroup { get; set; }
    }
}
