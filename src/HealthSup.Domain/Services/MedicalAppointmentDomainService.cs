using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
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
            var medicalAppointment = await _unitOfWork.MedicalAppointmentRepository.GetById(id);

            return medicalAppointment;
        }

        public async Task<MedicalAppointment> Create
        (
            int patientId, 
            int diseaseId
        )
        {
            var decisionTree = await _unitOfWork.DecisionTreeRepository.GetCurrentByDiseaseId(diseaseId);
            var initialNode = await _unitOfWork.NodeRepository.GetInitialByDecisionTreeId(decisionTree.Id);
            var patient = await _unitOfWork.PatientRepository.GetById(patientId);
            var medicalAppointmentStatus = new MedicalAppointmentStatus() { Id = (int)MedicalAppointmentStatusEnum.InProgress };

            var medicalAppointment = new MedicalAppointment() 
            {
                IsDiagnostic = false,
                Patient = patient,
                DecisionTree = decisionTree,
                CurrentNode = initialNode,
                Status = medicalAppointmentStatus
            };

            return await _unitOfWork.MedicalAppointmentRepository.Create(medicalAppointment);
        }
    }
}
