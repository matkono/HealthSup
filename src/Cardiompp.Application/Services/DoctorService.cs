using Cardiompp.Application.DataContracts.v1.Responses.Doctor;
using Cardiompp.Application.Services.Contracts;
using Cardiompp.Domain.Repositories;
using Cardiompp.Application.Mappers;
using System.Threading.Tasks;

namespace Cardiompp.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetDoctorResponse> GetByCrm(string crm)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetByCrm(crm);

            return new GetDoctorResponse(doctor?.ToDataContract());
        }
    }
}
