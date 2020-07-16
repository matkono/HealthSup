using Cardiompp.Domain.Repositories;
using Cardiompp.Domain.Services.Contracts;
using Cardiompp.Infrastructure.CrossCutting.Hash.Services.Contracts;

namespace Cardiompp.Domain.Services
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
