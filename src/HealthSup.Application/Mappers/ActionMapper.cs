using HealthSup.Application.DataContracts.v1.Responses.Action;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class ActionMapper
    {
        public static ActionResponse ToDataContract(this Action action)
            => new ActionResponse()
            {
                Id = action.Id,
                IsInitial = action.IsInitial,
                NodeType = action.NodeType?.ToDataContract(),
                DecisionTree = action.DecisionTree?.ToDataContract(),
                Action = new ActionBase() 
                {
                    Id = action.ActionId,
                    Code = action.Code,
                    Title = action.Title
                }
            };
    }
}
