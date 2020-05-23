using Cardiompp.Application.DataContracts.Responses;
using Cardiompp.Application.DataContracts.v1.Requests.Doctor;
using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using Cardiompp.Application.Mappers;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Cardiompp.Domain.Enums;

namespace Cardiompp.Application.Services
{
    public class DoctorService : IDoctorService
    {
        public DoctorService
        (
            IUnitOfWork unitOfWork,
            IHashService hashService
        )
        {
            _unitOfWork = unitOfWork;
            HashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
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
            var passwordMd5 = HashService.GetMd5Hash(loginRequest.Password);
            var doctor = await _unitOfWork.DoctorRepository.GetByEmailAndPassword(loginRequest.Email, passwordMd5);

            return new DoctorResponse<GetDoctorByEmailAndPasswordResponse>(doctor?.ToGetByEmailAndPasswordDataContact());
        }

        public async Task<BaseResponse> UpdatePassword
        (
            UpdatePasswordRequest updatePasswordRequest
        ) 
        {
            var passwordMd5 = HashService.GetMd5Hash(updatePasswordRequest.Password);
            var newPasswordMd5 = HashService.GetMd5Hash(updatePasswordRequest.NewPassword);
            var doctor = await _unitOfWork.DoctorRepository.GetByEmailAndPassword(updatePasswordRequest.Email, passwordMd5);
            var baseResponse = new BaseResponse();

            if (doctor == null)
            {
                baseResponse.Success = false;
                baseResponse.AddError
                (
                    (int) ValidationErrorCodeEnum.EmailOrPasswordInvalid,
                    "Email or password incorrect.",
                    null
                );

                return baseResponse;
            }

            var repositoryResponse = await _unitOfWork.DoctorRepository.UpdatePassword(doctor.Id, newPasswordMd5) > 0;

            if (repositoryResponse)
            {
                baseResponse.Success = true;
            }

            return baseResponse;
        }
    }
}
