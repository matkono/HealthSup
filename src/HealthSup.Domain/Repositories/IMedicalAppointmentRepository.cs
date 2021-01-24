using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IMedicalAppointmentRepository
    {
        Task<MedicalAppointment> GetById
        (
            int id
        );

        public Task<int> UpdateLastNode
        (
            int id,
            int lastNodeId
        );

        public Task<int> UpdateStatus
        (
            int id,
            int statusId
        );

        public Task<int> UpdateIsDiagnostic
        (
            int id,
            bool isDiagnostic
        );

        Task<List<MedicalAppointment>> ListPagedByPatientId
        (
            int patientId,
            uint pageNumber,
            uint pageSize
        );
    }
}
