using Cardiompp.Domain.Entities;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Services.Contracts
{
    public interface IAuthenticationDomainService
    {
        public Task<CardiomppAgent> AuthenticateAsync(string name, string password);

        public string BuildToken();
    }
}
