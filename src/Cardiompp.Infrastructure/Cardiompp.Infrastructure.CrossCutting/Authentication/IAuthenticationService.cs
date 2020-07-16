using Cardiompp.Infrastructure.CrossCutting.Authentication.DTO;
using System.Threading.Tasks;

namespace Cardiompp.Infrastructure.CrossCutting.Authentication.Services.Contracts
{
    public interface IAuthenticationService
    {
        public Task<AgentDTO> AuthenticateAgentAsync(string name, string password);

        public Task<UserDTO> AuthenticateUserAsync(string email, string password);

        public Task UpdatePassword(int doctorId, string newPassword);

        public string BuildToken();
    }
}
