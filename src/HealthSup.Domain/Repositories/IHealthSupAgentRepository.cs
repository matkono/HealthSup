using HealthSup.Infrastructure.CrossCutting.Authentication.DTO;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IHealthSupAgentRepository
    {
        Task<AgentDTO> GetByKeyAndPassword
        (
            string key, 
            string password
        );
    }
}
