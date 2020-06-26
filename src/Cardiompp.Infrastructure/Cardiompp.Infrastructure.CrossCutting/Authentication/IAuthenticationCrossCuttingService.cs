using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;
using System.Threading.Tasks;

namespace Cardiompp.Infrastructure.CrossCutting.Authentication.Services.Contracts
{
    public interface IAuthenticationCrossCuttingService
    {
        public Task<AgentDTO> AuthenticateAsync(string name, string password);

        public string BuildToken();
    }
}
