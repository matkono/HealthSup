using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> GetByEmailAndPassword(string email, string password);

        Task UpdatePassword(int doctorId, string newPassword);
    }
}
