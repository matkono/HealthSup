using Cardiompp.Domain.Repositories;
using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;
using Cardiompp.Infrastructure.Data.Scripts;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace Cardiompp.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public UserRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<UserDTO> GetByEmailAndPassword(string email, string password)
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.UserDTO.GetByEmailAndPassword);

            var result = await UnitOfWork.Connection.QueryAsync<UserDTO>(
                                                                query,
                                                                new { email, password },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }
    }
}
