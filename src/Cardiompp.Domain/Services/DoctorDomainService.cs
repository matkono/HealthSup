using Cardiompp.Domain.Repositories;
using Cardiompp.Domain.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Hash.Services.Contracts;
using System.Threading.Tasks;

namespace Cardiompp.Domain.Services
{
    public class DoctorDomainService : IDoctorDomainService
    {
        public DoctorDomainService
        (
            IUnitOfWork unitOfWork,
            IHashCrossCuttingService hashCrossCuttingService
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task UpdatePassword(int doctorId, string newPassword)
        {
            await _unitOfWork.DoctorRepository.UpdatePassword(doctorId, newPassword);
        }
    }
}
