using HealthSup.Application.DataContracts.v1.Requests;
using HealthSup.Application.DataContracts.v1.Responses.Disease;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.CrossCutting.Constants;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class DiseaseApplicationService : IDiseaseApplicationService
    {
        public DiseaseApplicationService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<ListDiseasesPagedReturn> ListPaged
        (
            Pagination pagination
        )
        {
            var pageNumber = pagination.PageNumber;
            var pageSize = pagination.PageSize > ApplicationConstants.MaxPageSize ? ApplicationConstants.MaxPageSize : pagination.PageSize;

            var diseases = await _unitOfWork.DiseaseRepository.ListPaged(pageNumber, pageSize);

            return new ListDiseasesPagedReturn(diseases.ToDataContract());
        }
    }
}
