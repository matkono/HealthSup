using HealthSup.Application.DataContracts.v1.Responses.Disease;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class DiseaseMapper
    {
        public static DiseaseResponse ToDataContract(this Disease disease)
            => new DiseaseResponse()
            {
                Id = disease.Id,
                Name = disease.Name
            };
    }
}
