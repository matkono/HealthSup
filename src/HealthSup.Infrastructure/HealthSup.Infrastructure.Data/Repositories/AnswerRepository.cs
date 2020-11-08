﻿using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        public AnswerRepository
        (
            IUnitOfWork unitOfWork
        )
        {
            UnitOfWork = unitOfWork;
        }

        private IUnitOfWork UnitOfWork { get; }

        public async Task<int> InsertManyAsync
        (
            List<Answer> answers
        )
        {
            var query = new StringBuilder();
            
            var parameters = new DynamicParameters();

            for (var index = 0; index < answers.Count; index ++) 
            {
                var individualQuery = new StringBuilder();

                individualQuery.Append(ScriptManager.GetByName(ScriptManager.FileNames.Answer.Insert));
                individualQuery.Replace("@dateAnswered", $"@dateAnswered{index}");
                individualQuery.Replace("@questionId", $"@questionId{index}");
                individualQuery.Replace("@possibleAnswerId", $"@possibleAnswerId{index}");
                individualQuery.Replace("@doctorId", $"@doctorId{index}");
                individualQuery.Replace("@medicalAppointmentId", $"@medicalAppointmentId{index}");

                parameters.Add($"@dateAnswered{index}", answers[index].Date, DbType.DateTime);
                parameters.Add($"@questionId{index}", answers[index].Question.Id, DbType.Int32);
                parameters.Add($"@possibleAnswerId{index}", answers[index].PossibleAnswer.Id, DbType.Int32);
                parameters.Add($"@doctorId{index}", answers[index].Doctor.Id, DbType.Int32);
                parameters.Add($"@medicalAppointmentId{index}", answers[index].MedicalAppointment.Id, DbType.Int32);

                query.Append(individualQuery);
            }

            return await UnitOfWork.Connection.ExecuteAsync(query.ToString(),
                                                      parameters,
                                                      UnitOfWork.Transaction);
        }
    }
}