using Cardiompp.Application.DataContracts.v1.Requests.Login;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Repositories;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AuthenticateAsync(string name, string password)
        {
            var isAuthenticate = false;
            var cardiomppAgent = await _unitOfWork.CardiomppAgentRepository.GetByNameAndPassword(name, password);

            if (cardiomppAgent != null)
                isAuthenticate = true;

            return isAuthenticate;
        }
    }
}
