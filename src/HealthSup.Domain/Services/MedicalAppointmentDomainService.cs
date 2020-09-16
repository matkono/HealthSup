using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services
{
    public class MedicalAppointmentDomainService : IMedicalAppointmentDomainService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicalAppointmentDomainService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MedicalAppointment> GetById
        (
            int id
        )
        {
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById
            (
                id
            );

            return medicalAppointment;
        }

        public async Task UpdatelastNode
        (
            int id,
            int lastNodeId
        )
        {
            await _unitOfWork.MedicalAppointmentRepository.UpdateLastNode(id, lastNodeId);
        }
    }
}
