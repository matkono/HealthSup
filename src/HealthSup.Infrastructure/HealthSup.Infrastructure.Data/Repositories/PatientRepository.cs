using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public PatientRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<PagedResult<List<Patient>>> ListPaged
        (
            int pageNumber,
            int pageSize
        )
        {
            var listQuery = ScriptManager.GetByName(ScriptManager.FileNames.Patient.ListPagedPatients);
            var countQuery = ScriptManager.GetByName(ScriptManager.FileNames.Patient.CountPatients);

            var result = await UnitOfWork.Connection.QueryAsync<Patient>(
                                                                listQuery,
                                                                new { pageNumber, pageSize },
                                                                UnitOfWork.Transaction);

            var count = UnitOfWork.Connection.ExecuteScalar<int>(countQuery, UnitOfWork.Transaction);

            var toReturn = new PagedResult<List<Patient>>(result.ToList(), pageNumber, pageSize, count);

            return toReturn;
        }
    }
}
