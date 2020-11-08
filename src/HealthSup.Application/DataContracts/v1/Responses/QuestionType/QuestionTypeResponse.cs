using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.QuestionType
{
    [DataContract]
    public class QuestionTypeResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Code { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
