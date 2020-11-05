using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class MedicalAppointmentRepository : IMedicalAppointmentRepository
    {
        private IUnitOfWork UnitOfWork { get; }

        public MedicalAppointmentRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<MedicalAppointment> GetById
        (
            int medicalAppointmentId
        )
        {
            MedicalAppointment MapFromQuery
            (
                MedicalAppointment medicalAppointment,
                Patient patient,
                DecisionTree decisionTree,
                Node node,
                MedicalAppointmentStatus status
            )
            {
                medicalAppointment.Patient = patient;
                medicalAppointment.DecisionTree = decisionTree;
                medicalAppointment.LastNode = node;
                medicalAppointment.Status = status;

                return medicalAppointment;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<MedicalAppointment, Patient, DecisionTree, Node, MedicalAppointmentStatus, MedicalAppointment>(
                                                                query,
                                                                MapFromQuery,
                                                                new { medicalAppointmentId },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<int> UpdateLastNode
        (
            int id,
            int lastNodeId
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.UpdateLastNode);

            return await UnitOfWork.Connection.ExecuteAsync
            (
                query,
                new { id, lastNodeId },
                UnitOfWork.Transaction
            );
        }

        public async Task<int> UpdateStatus
        (
            int id,
            int statusId
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.UpdateStatus);

            return await UnitOfWork.Connection.ExecuteAsync
            (
                query,
                new { id, statusId },
                UnitOfWork.Transaction
            );
        }
    }
}
