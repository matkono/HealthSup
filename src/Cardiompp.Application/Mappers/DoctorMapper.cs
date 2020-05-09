using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using Cardiompp.Domain.Entities;

namespace Cardiompp.Application.Mappers
{
    public static class DoctorMapper
    {
        public static GetDoctorByCrmResponse ToDataContract(this Doctor doctor)
            => new GetDoctorByCrmResponse()
            {
                Name = doctor.Name,
                Crm = doctor.Crm,
                Phone = doctor.Phone,
                Email = doctor.Email,
                IsActive = doctor.IsActive,
                Address = doctor.Address?.ToDataContract()
            };
    }
}
