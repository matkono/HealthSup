﻿using Dapper;
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
                Node node
            )
            {
                medicalAppointment.Patient = patient;

                medicalAppointment.DecisionTree = decisionTree;

                medicalAppointment.LastNode = node;

                return medicalAppointment;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.GetById);

            var result = await UnitOfWork.Connection.QueryAsync<MedicalAppointment, Patient, DecisionTree, Node, MedicalAppointment>(
                                                                query,
                                                                MapFromQuery,
                                                                new { medicalAppointmentId },
                                                                UnitOfWork.Transaction);

            return result.FirstOrDefault();
        }

        public async Task UpdateLastNode
        (
            int id,
            int lastNodeId
        )
        {
            var query = ScriptManager.GetByName(ScriptManager.FileNames.MedicalAppointment.UpdateLastNode);

            await UnitOfWork.Connection.ExecuteAsync
            (
                query,
                new { id, lastNodeId },
                UnitOfWork.Transaction
            );
        }
    }
}
