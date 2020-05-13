using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using Cardiompp.Domain.Entities;

namespace Cardiompp.Application.Mappers
{
    public static class DoctorMapper
    {
        public static GetDoctorByCrmResponse ToGetByCrmDataContract(this Doctor doctor)
            => new GetDoctorByCrmResponse()
            {
                Name = doctor.Name,
                Crm = doctor.Crm,
                Phone = doctor.Phone,
                Email = doctor.Email,
                IsActive = doctor.IsActive,
                Address = doctor.Address?.ToDataContract()
            };

        public static GetDoctorByEmailAndPasswordResponse ToGetByEmailAndPasswordDataContact(this Doctor doctor)
            => new GetDoctorByEmailAndPasswordResponse()
            {
                Crm = doctor.Crm,
                Email = doctor.Email,
                IsActive = doctor.IsActive
            };
    }
}
