using HealthSup.Application.DataContracts.v1.Responses.PossibleAnswerGroup;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class PossibleAnswerGroupMapper
    {
        public static PossibleAnswerGroupResponse ToDataContract(this PossibleAnswerGroup possibleAnswerGroup)
            => new PossibleAnswerGroupResponse()
            {
                Id = possibleAnswerGroup.Id,
                Description = possibleAnswerGroup.Description
            };
    }
}
