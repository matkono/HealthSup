using HealthSup.Application.DataContracts.v1.Responses.QuestionType;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class QuestionTypeMapper
    {
        public static QuestionTypeResponse ToDataContract(this QuestionType questionType)
            => new QuestionTypeResponse()
            {
                Id = questionType.Id,
                Code = questionType.Code,
                Name = questionType.Name
            };
    }
}
