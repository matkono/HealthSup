using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Linq;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class MedicalAppointmentMovementRepository : IMedicalAppointmentMovementRepository
    {
        public MedicalAppointmentMovementRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<int> InsetMovement
        (
            MedicalAppointmentMovement medicalAppointmentMovement
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointmentMovement.Insert);

            var parameters = new
            {
                fromNodeId = medicalAppointmentMovement.FromNode.Id,
                toNodeId = medicalAppointmentMovement.ToNode.Id,
                medicalAppointmentId = medicalAppointmentMovement.MedicalAppointment.Id
            };

            return await UnitOfWork.Connection.ExecuteAsync(query,
                                                      parameters,
                                                      UnitOfWork.Transaction);
        }

        public async Task<MedicalAppointmentMovement> GetByFromNodeId
        (
            int medicalAppointmentId,
            int fromNodeId
        )
        {
            MedicalAppointmentMovement MapFromQuery
            (
                MedicalAppointmentMovement medicalAppointmentMovement,
                Node fromNode,
                Node toNode,
                MedicalAppointment medicalAppointment
            )
            {
                medicalAppointmentMovement.FromNode = fromNode;

                medicalAppointmentMovement.ToNode = toNode;

                medicalAppointmentMovement.MedicalAppointment  = medicalAppointment;

                return medicalAppointmentMovement;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointmentMovement.GetByFromNodeId);

            var result = await UnitOfWork.Connection.QueryAsync<MedicalAppointmentMovement, Node, Node, MedicalAppointment, MedicalAppointmentMovement>(
                                                                query,
                                                                MapFromQuery,
                                                                new { medicalAppointmentId, fromNodeId },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<MedicalAppointmentMovement> GetByToNodeId
        (
            int medicalAppointmentId,
            int toNodeId
        )
        {
            MedicalAppointmentMovement MapFromQuery
            (
                MedicalAppointmentMovement medicalAppointmentMovement,
                Node fromNode,
                Node toNode,
                MedicalAppointment medicalAppointment
            )
            {
                medicalAppointmentMovement.FromNode = fromNode;

                medicalAppointmentMovement.ToNode = toNode;

                medicalAppointmentMovement.MedicalAppointment = medicalAppointment;

                return medicalAppointmentMovement;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointmentMovement.GetByToNodeId);

            var result = await UnitOfWork.Connection.QueryAsync<MedicalAppointmentMovement, Node, Node, MedicalAppointment, MedicalAppointmentMovement>(
                                                                query,
                                                                MapFromQuery,
                                                                new { medicalAppointmentId, fromNodeId },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task<int> DeleteById
        (
            int id
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointmentMovement.DeleteById);

            return await UnitOfWork.Connection.ExecuteAsync(query,
                                                      new { id },
                                                      UnitOfWork.Transaction);
        }
    }
}
