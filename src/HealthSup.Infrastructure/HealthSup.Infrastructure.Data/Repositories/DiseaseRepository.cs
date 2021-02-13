using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class DiseaseRepository: IDiseaseRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public DiseaseRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<PagedResult<List<Disease>>> ListPaged
        (
            int pageNumber,
            int pageSize
        )
        {
            var listQuery = ScriptManager.GetByName(ScriptManager.FileNames.Disease.ListPaged);
            var countQuery = ScriptManager.GetByName(ScriptManager.FileNames.Disease.CountDiseases);

            var result = await UnitOfWork.Connection.QueryAsync<Disease>(
                                                                listQuery,
                                                                new { pageNumber, pageSize },
                                                                UnitOfWork.Transaction);

            var count = UnitOfWork.Connection.ExecuteScalar<int>(countQuery, UnitOfWork.Transaction);

            var toReturn = new PagedResult<List<Disease>>(result.ToList(), pageNumber, pageSize, count);

            return toReturn;
        }
    }
}
