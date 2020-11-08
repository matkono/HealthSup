using HealthSup.Application.DataContracts.v1.Responses.DecisionTree;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class DecisionTreeMapper
    {
        public static DecisionTreeResponse ToDataContract(this DecisionTree decisionTree)
            => new DecisionTreeResponse()
            {
                Id = decisionTree.Id,
                Description = decisionTree.Description,
                Version = decisionTree.Version,
                Disease = decisionTree.Disease?.ToDataContract()
            };
    }
}
