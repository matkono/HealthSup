using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;
using System.Threading.Tasks;

namespace Cardiompp.Infrastructure.CrossCutting.Authentication.Services.Contracts
{
    public interface IAuthenticationCrossCuttingService
    {
        public Task<AgentDTO> AuthenticateAgentAsync(string name, string password);

        public Task<UserDTO> AuthenticateUserAsync(string email, string password);

        public string BuildToken();
    }
}
