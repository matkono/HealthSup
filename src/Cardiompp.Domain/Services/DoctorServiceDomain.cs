using Cardiompp.Domain.Entities;
using Cardiompp.Domain.Enums;
using Cardiompp.Domain.Repositories;
using Cardiompp.Domain.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Services
{
    public class DoctorServiceDomain : IDoctorServiceDomain
    {
        public DoctorServiceDomain
        (
            IUnitOfWork unitOfWork,
            IHashServiceDomain hashService
        )
        {
            _unitOfWork = unitOfWork;
            HashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
        }

        private readonly IUnitOfWork _unitOfWork;

        IHashServiceDomain HashService { get; set; }

        public async Task<Doctor> GetByCrm(string crm)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByCrm(crm);

            return doctor;
        }

        public async Task<Doctor> GetByEmailAndPassword(string email, string password)
        {
            var passwordMd5 = HashService.GetMd5Hash(password);
            var doctor = await _unitOfWork.DoctorRepository.GetByEmailAndPassword(email, passwordMd5);

            if (doctor == null) {
                doctor = new Doctor();
                doctor.AddError
                (
                    (int)ValidationErrorCodeEnum.EmailOrPasswordInvalid,
                    "Email or password incorrect.",
                    null
                );
            }

            return doctor;
        }

        public async Task UpdatePassword(int doctorId, string newPassword)
        {
            var newPasswordMd5 = HashService.GetMd5Hash(newPassword);
            await _unitOfWork.DoctorRepository.UpdatePassword(doctorId, newPasswordMd5);
        }
    }
}
