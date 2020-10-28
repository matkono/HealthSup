using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class MedicalAppointmentFlowRepository : IMedicalAppointmentFlowRepository
    {
        public MedicalAppointmentFlowRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<int> InsetMovement
        (
            MedicalAppointmentMovement medicalAppointmentFlow
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointmentFlow.Insert);

            var parameters = new
            {
                fromNodeId = medicalAppointmentFlow.FromNode.Id,
                toNodeId = medicalAppointmentFlow.ToNode.Id,
                medicalAppointmentId = medicalAppointmentFlow.MedicalAppointment.Id
            };

            return await UnitOfWork.Connection.ExecuteAsync(query,
                                                      parameters,
                                                      UnitOfWork.Transaction);
        }
    }
}
