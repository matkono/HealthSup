using HealthSup.Application.DataContracts.v1.Responses.Doctor;
using HealthSup.Domain.Entities;

namespace HealthSup.Application.Mappers
{
    public static class DoctorMapper
    {
        public static DoctorResponse ToDataContract(this Doctor doctorModel)
            => new DoctorResponse()
            {
                Name = doctorModel.Name,
                Crm = doctorModel.Crm,
                Phone = doctorModel.Phone,
                UserId = doctorModel.User.Id
            };
    }
}
