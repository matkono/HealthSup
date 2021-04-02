using HealthSup.Application.DataContracts.v1.Responses.Patient;
using PatientContract = HealthSup.Application.DataContracts.v1.Requests.Patient.Patient;
using PatientModel = HealthSup.Domain.Entities.Patient;

namespace HealthSup.Application.Mappers
{
    public static class PatientMapper
    {
        public static PatientResponse ToDataContract(this PatientModel patientModel)
            => new PatientResponse()
            {
                Id = patientModel != null? patientModel.Id: 0,
                Name = patientModel != null ? patientModel.Name : string.Empty,
                Registration = patientModel != null ? patientModel.Registration : string.Empty,
                Address = patientModel.Address?.ToDataContract()
            };

        public static PatientModel ToModel(this PatientContract patientContract)
            => new PatientModel
            {
                Name = patientContract.Name,
                Registration = patientContract.Registration,
                Address = patientContract.Address.ToModel()
            };
    }
}
