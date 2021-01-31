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

        public async Task<List<Patient>> ListPaged
        (
            int pageNumber,
            int pageSize
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.Patient.ListPagedPatients);

            var result = await UnitOfWork.Connection.QueryAsync<Patient>(
                                                                query,
                                                                new { pageNumber, pageSize },
                                                                UnitOfWork.Transaction);

            return result.ToList();
        }
    }
}
