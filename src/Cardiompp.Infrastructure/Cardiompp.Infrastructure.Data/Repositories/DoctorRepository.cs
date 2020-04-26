using Cardiompp.Domain.Entities;
using Cardiompp.Domain.Repositories;
using Cardiompp.Infrastructure.Data.Scripts;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace Cardiompp.Infrastructure.Data.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public DoctorRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Doctor> GetByCrm(string crm)
        {
            Doctor MapFromQuery(Doctor doctor, Address address)
            {
                doctor.SetAddress(address);
                return doctor;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Doctor.GetByCrm);

            var result = await UnitOfWork.Connection.QueryAsync<Doctor, Address, Doctor>(
                                                                query,
                                                                MapFromQuery,
                                                                new { crm },
                                                                UnitOfWork.Transaction,
                                                                splitOn: "Id");

            return result.FirstOrDefault();
        }

    }
}
