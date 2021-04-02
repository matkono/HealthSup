using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services
{
    public class PatientDomainService: IPatientDomainService
    {
        public PatientDomainService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<Patient> Create
        (
            Patient patient
        )
        {
            var address = await _unitOfWork.AddressRepository.GetByCep(patient.Address.Cep);

            if (address == null)
                address = await _unitOfWork.AddressRepository.Create(patient.Address);

            patient.Address = address;
            return await _unitOfWork.PatientRepository.Create(patient);
        }

        public async Task<Patient> Update
        (
            Patient patient
        )
        {
            if (patient.Address != null) 
            {
                var address = await _unitOfWork.AddressRepository.GetByCep(patient.Address.Cep);

                if (address == null)
                    address = await _unitOfWork.AddressRepository.Create(patient.Address);

                patient.Address = address;
            }
            
            return await _unitOfWork.PatientRepository.Update(patient);
        }
    }
}
