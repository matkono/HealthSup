using Cardiompp.Application.DataContracts.Responses;
using Cardiompp.Application.DataContracts.v1.Requests.Doctor;
using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using Cardiompp.Application.Mappers;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Services.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
{
    public class DoctorApplicationService : IDoctorApplicationService
    {
        public DoctorApplicationService
        (
            IDoctorDomainService doctorServiceDomain
        )
        {
            DoctorServiceDomain = doctorServiceDomain ?? throw new ArgumentNullException(nameof(doctorServiceDomain));
        }

        IDoctorDomainService DoctorServiceDomain;

        public async Task<DoctorResponse<GetDoctorByEmailAndPasswordResponse>> GetByEmailAndPassword
        (
            GetDoctorByEmailAndPasswordRequest loginRequest
        ) 
        {
            var doctor = await DoctorServiceDomain.GetByEmailAndPassword
            (
                loginRequest.Email,
                loginRequest.Password
            );

            if(doctor.Errors != null && doctor.Errors.Any()) 
            {
                var response = new DoctorResponse<GetDoctorByEmailAndPasswordResponse>(null);

                foreach (var error in doctor.Errors)
                    response.AddError(error.Code, error.Message, error.Field);

                return response;

            }

            return new DoctorResponse<GetDoctorByEmailAndPasswordResponse>(doctor?.ToGetByEmailAndPasswordDataContact());
        }

        public async Task<BaseResponse> UpdatePassword
        (
            UpdatePasswordRequest updatePasswordRequest
        ) 
        {
            var baseResponse = new BaseResponse();

            var doctor = await DoctorServiceDomain.GetByEmailAndPassword
            (
                updatePasswordRequest.Email, 
                updatePasswordRequest.Password
            );

            if (doctor.Errors != null && doctor.Errors.Any())
            {
                foreach (var error in doctor.Errors)
                    baseResponse.AddError(error.Code, error.Message, error.Field);

                return baseResponse;
            }

            await DoctorServiceDomain.UpdatePassword(doctor.Id, updatePasswordRequest.NewPassword);

            return baseResponse;
        }
    }
}
