using HealthSup.Application.DataContracts.v1.Responses.Doctor;
using HealthSup.Application.Mappers;
using HealthSup.Application.Services.Contracts;
using HealthSup.Domain.Repositories;
using System.Threading.Tasks;

namespace HealthSup.Application.Services
{
    public class DoctorApplicationService : IDoctorApplicationService
    {
        public DoctorApplicationService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<GetDoctorByUserIdReturn> GetByUserId(int userId)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByUserId(userId);

            if (doctor == null)
                return new GetDoctorByUserIdReturn(null);

            return new GetDoctorByUserIdReturn(doctor.ToDataContract());
        }
    }
}
