using HealthSup.Application.DataContracts.v1.Requests.Disease;
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
            ListPagedRequest argument
        )
        {
            var pageNumber = argument.Pagination.PageNumber;
            var pageSize = argument.Pagination.PageSize > ApplicationConstants.MaxPageSize ? ApplicationConstants.MaxPageSize : argument.Pagination.PageSize;

            var diseases = await _unitOfWork.DiseaseRepository.ListPaged(pageNumber, pageSize);

            return new ListDiseasesPagedReturn(diseases.ToDataContract());
        }
    }
}
