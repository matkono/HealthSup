using HealthSup.Application.DataContracts.v1.Responses.Patient;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class PatientMapper
    {
        public static PatientResponse ToDataContract(this Patient patient)
            => new PatientResponse()
            {
                Id = patient.Id,
                Name = patient.Name,
                Registration = patient.Registration
            };
    }
}
