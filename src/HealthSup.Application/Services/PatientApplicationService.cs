using HealthSup.Application.DataContracts.v1.Requests.Patient;
using HealthSup.Application.DataContracts.v1.Responses.Patient;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.CrossCutting.Constants;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class PatientApplicationService : IPatientApplicationService
    {
        public PatientApplicationService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<ListPatientsPagedReturn> ListPaged(ListPatientPagedRequest argument)
        {
            var pageNumber = argument.Pagination.PageNumber;
            var pageSize = argument.Pagination.PageSize > ApplicationConstants.MaxPageSize ? ApplicationConstants.MaxPageSize : argument.Pagination.PageSize;

            var patients = await _unitOfWork.PatientRepository.ListPaged(pageNumber, pageSize);

            return new ListPatientsPagedReturn(patients.ToDataContract());
        }

        public async Task<GetPatientByRegistrationReturn> GetByRegistration
        (
            string registration
        )
        {
            var patient = await _unitOfWork.PatientRepository.GetByRegistration(registration);

            return new GetPatientByRegistrationReturn(patient.ToDataContract());
        }
    }
}
