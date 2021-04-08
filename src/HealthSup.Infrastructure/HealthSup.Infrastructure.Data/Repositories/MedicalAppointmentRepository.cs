using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
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
                medicalAppointment.CurrentNode = node;
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
            int currenteNodeId
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.UpdateLastNode);

            return await UnitOfWork.Connection.ExecuteAsync
            (
                query,
                new { id, currenteNodeId },
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

        public async Task<int> UpdateIsDiagnostic
        (
            int id,
            bool isDiagnostic
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.UpdateIsDiagnostic);

            return await UnitOfWork.Connection.ExecuteAsync
            (
                query,
                new { id, isDiagnostic },
                UnitOfWork.Transaction
            );
        }

        public async Task<PagedResult<List<MedicalAppointment>>> ListPagedByPatientId
        (
            int patientId,
            int pageNumber,
            int pageSize
        )
        {
            MedicalAppointment MapFromQuery
            (
                MedicalAppointment medicalAppointment,
                Patient patient,
                DecisionTree decisionTree,
                Disease disease,
                Node node,
                MedicalAppointmentStatus status
            )
            {
                decisionTree.Disease = disease;

                medicalAppointment.Patient = patient;
                medicalAppointment.DecisionTree = decisionTree;
                medicalAppointment.CurrentNode = node;
                medicalAppointment.Status = status;

                return medicalAppointment;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.ListPagedByPatientId);
            var countQuery = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.CountByPatientId);

            var result = await UnitOfWork.Connection.QueryAsync<MedicalAppointment, Patient, DecisionTree, Disease, Node, MedicalAppointmentStatus, MedicalAppointment>(
                                                                query,
                                                                MapFromQuery,
                                                                new { patientId, pageNumber, pageSize },
                                                                UnitOfWork.Transaction);

            var count = UnitOfWork.Connection.ExecuteScalar<int>(countQuery, new { patientId }, UnitOfWork.Transaction);

            var toReturn = new PagedResult<List<MedicalAppointment>>(result.ToList(), pageNumber, pageSize, count);

            return toReturn;
        }

        public async Task<MedicalAppointment> Create
        (
            MedicalAppointment medicalAppointment
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.Create);
            var parameters = new DynamicParameters();
            parameters.Add("@patientId", medicalAppointment.Patient.Id);
            parameters.Add("@decisionTreeId", medicalAppointment.DecisionTree.Id);
            parameters.Add("@currenteNodeId", medicalAppointment.CurrentNode.Id);
            parameters.Add("@medicalAppointmentStatusId", medicalAppointment.Status.Id);

            var result = await UnitOfWork.Connection.QueryAsync<int>(query,
                                                      parameters,
                                                      UnitOfWork.Transaction);

            return await GetById(result.Single());
        }
    }
}
