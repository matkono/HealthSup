using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using Cardiompp.Domain.Entities;

namespace Cardiompp.Application.Mappers
{
    public static class DoctorMapper
    {
        public static DoctorResponse ToDataContract(this Doctor doctor)
            => new DoctorResponse()
            {
                Name = doctor.Name,
                Crm = doctor.Crm,
                Phone = doctor.Phone,
                Address = doctor.Address?.ToDataContract()
            };
    }
}
