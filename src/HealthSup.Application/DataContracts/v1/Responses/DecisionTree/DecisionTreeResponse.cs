using HealthSup.Application.DataContracts.v1.Responses.Disease;
using System.Runtime.Serialization;

namespace HealthSup.Application.DataContracts.v1.Responses.DecisionTree
{
    public class DecisionTreeResponse
    {
        public int Id { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public bool IsCurrent { get; set; }

        public DiseaseResponse Disease { get; set; }
    }
}
