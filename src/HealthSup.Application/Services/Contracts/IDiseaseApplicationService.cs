using HealthSup.Application.DataContracts.v1.Requests;
using HealthSup.Application.DataContracts.v1.Responses.Disease;
using System.Threading.Tasks;

namespace HealthSup.Application.Services.Contracts
{
    public interface IDiseaseApplicationService
    {
        public Task<ListDiseasesPagedReturn> ListPaged
        (
            Pagination pagination
        );
    }
}
