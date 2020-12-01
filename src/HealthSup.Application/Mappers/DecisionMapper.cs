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
                Decision = new DecisionBase() 
                {
                    Id = decision.DecisionId,
                    Title = decision.Title,
                    Code = decision.Code,
                    IsDiagnostic = decision.IsDiagnostic
                }
            };
    }
}
