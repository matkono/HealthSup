using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Repositories;
using Cardiompp.Application.Mappers;
using System.Threading.Tasks;
using Cardiompp.Application.DataContracts.v1.Requests.Doctor;
using System;

namespace Cardiompp.Application.Services
{
    public class DoctorService : IDoctorService
    {
        public DoctorService
        (
            IUnitOfWork unitOfWork,
            IHashService md5HashService
        )
        {
            _unitOfWork = unitOfWork;
            HashService = md5HashService ?? throw new ArgumentNullException(nameof(md5HashService));
        }

        private readonly IUnitOfWork _unitOfWork;

        IHashService HashService { get; set; }

        public async Task<DoctorResponse<GetDoctorByCrmResponse>> GetByCrm(string crm)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByCrm(crm);

            return new DoctorResponse<GetDoctorByCrmResponse>(doctor?.ToGetByCrmDataContract());
        }

        public async Task<DoctorResponse<GetDoctorByEmailAndPasswordResponse>> GetByEmailAndPassword
        (
            GetDoctorByEmailAndPasswordRequest loginRequest
        ) 
        {
            var passwordMd5 = HashService.GetHash(loginRequest.Password);
            var doctor = await _unitOfWork.DoctorRepository.GetByEmailAndPassword(loginRequest.Email, passwordMd5);

            return new DoctorResponse<GetDoctorByEmailAndPasswordResponse>(doctor?.ToGetByEmailAndPasswordDataContact());
        }

        public async Task<bool> UpdatePassword
        (
            UpdatePasswordRequest updatePasswordRequest
        ) 
        {
            var passwordMd5 = HashService.GetHash(updatePasswordRequest.Password);
            var newPasswordMd5 = HashService.GetHash(updatePasswordRequest.NewPassword);
            var doctor = await _unitOfWork.DoctorRepository.GetByEmailAndPassword(updatePasswordRequest.Email, passwordMd5);

            if(doctor == null)
                return false;

            return await _unitOfWork.DoctorRepository.UpdatePassword(doctor.Id, newPasswordMd5) > 0;
        }
    }
}
