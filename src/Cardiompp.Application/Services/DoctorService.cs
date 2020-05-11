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
            Md5HashService = md5HashService ?? throw new ArgumentNullException(nameof(md5HashService));
        }

        private readonly IUnitOfWork _unitOfWork;

        IHashService Md5HashService { get; set; }

        public async Task<GetDoctorResponse<GetDoctorByCrmResponse>> GetByCrm(string crm)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByCrm(crm);

            return new GetDoctorResponse<GetDoctorByCrmResponse>(doctor?.ToGetByCrmDataContract());
        }

        public async Task<GetDoctorResponse<GetDoctorByEmailAndPasswordResponse>> GetByEmailAndPassword
        (
            GetDoctorByEmailAndPasswordRequest loginRequest
        ) 
        {
            var passwordMd5 = Md5HashService.GetHash(loginRequest.Password);
            var doctor = await _unitOfWork.DoctorRepository.GetByEmailAndPassword(loginRequest.Email, passwordMd5);

            return new GetDoctorResponse<GetDoctorByEmailAndPasswordResponse>(doctor?.ToGetByEmailAndPasswordDataContact());
        }
    }
}
