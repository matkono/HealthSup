using HealthSup.Application.DataContracts.v1.Responses.Patient;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class PatientMapper
    {
        public static PatientResponse ToDataContract(this Patient patient)
            => new PatientResponse()
            {
                Id = patient != null? patient.Id: 0,
                Name = patient != null ? patient.Name : string.Empty,
                Registration = patient != null ? patient.Registration : string.Empty,
                Address = patient.Address?.ToDataContract()
            };
    }
}
