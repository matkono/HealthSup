using Cardiompp.Application.DataContracts.v1.Requests.Login;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services.Contracts
{
    public interface IAuthenticationService
    {
        public Task<bool> AuthenticateAsync(string name, string password);
    }
}
