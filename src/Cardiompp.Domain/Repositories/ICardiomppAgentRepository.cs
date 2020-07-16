using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Repositories
{
    public interface ICardiomppAgentRepository
    {
        Task<AgentDTO> GetByKeyAndPassword(string key, string password);
    }
}
