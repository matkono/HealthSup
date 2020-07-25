using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using HealthSup.Infrastructure.CrossCutting.Hash.Services.Contracts;

namespace HealthSup.Domain.Services
{
    public class DoctorDomainService : IDoctorDomainService
    {
        public DoctorDomainService
        (
            IUnitOfWork unitOfWork,
            IHashService hashService
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;
    }
}
