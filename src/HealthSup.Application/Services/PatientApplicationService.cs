using HealthSup.Application.DataContracts.v1.Requests;
using HealthSup.Application.DataContracts.v1.Responses.Patient;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
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

        public async Task<ListPatientsPagedReturn> ListPaged(Pagination pagination)
        {
            var pageNumber = pagination.PageNumber;
            var pageSize = pagination.PageSize > ApplicationConstants.MaxPageSize ? ApplicationConstants.MaxPageSize : pagination.PageSize;

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
