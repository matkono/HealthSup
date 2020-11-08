using HealthSup.Application.DataContracts.v1.Responses.Decision;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class DecisionMapper
    {
        public static DecisionResponse ToDataContract(this Decision decision)
            => new DecisionResponse()
            {
                Id = decision.Id,
                IsInitial = decision.IsInitial,
                NodeType = decision.NodeType?.ToDataContract(),
                DecisionTree = decision.DecisionTree?.ToDataContract(),
                DecisionId = decision.DecisionId,
                Code = decision.Code,
                Title = decision.Title,
                IsDiagnostic = decision.IsDiagnostic
            };
    }
}
