using HealthSup.Application.DataContracts.v1.Responses.PossibleAnswer;
using HealthSup.Domain.Entities;
using System.Collections.Generic;

namespace HealthSup.Application.Mappers
{
    public static class PossibleAnswerMapper
    {
        public static PossibleAnswerResponse ToDataContract(this PossibleAnswer possibleAnswer)
            => new PossibleAnswerResponse()
            {
                Id = possibleAnswer.Id,
                Code = possibleAnswer.Code,
                Title = possibleAnswer.Title,
                PossibleAnswerGroup = possibleAnswer.PossibleAnswerGroup.ToDataContract()
            };
    }
}
