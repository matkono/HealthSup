using HealthSup.Application.DataContracts.v1.Requests.Patient;
using HealthSup.Application.DataContracts.v1.Responses.Patient;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Repositories;
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

        public async Task<ListPagedPatientsReturn> ListPaged(ListPagedPatientsRequest argument)
        {
            var patients = await _unitOfWork.PatientRepository.ListPaged(argument.Pagination.PageNumber, argument.Pagination.PageSize);

            return new ListPagedPatientsReturn(patients.ToDataContract());
        }
    }
}
