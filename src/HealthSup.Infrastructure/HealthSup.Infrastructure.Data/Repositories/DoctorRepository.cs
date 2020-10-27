using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public DoctorRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<Doctor> GetById
        (
            int id
        )
        {
            Doctor MapFromQuery
            (
                Doctor doctor,
                User user
            )
            {
                doctor.SetUser(user);

                return doctor;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Doctor.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<Doctor, User, Doctor>(
                                                                query,
                                                                MapFromQuery,
                                                                new { id },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }
    }
}
