using Dapper;
using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Infrastructure.Data.Scripts;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<List<Answer>> ListByQuestionId
        (
            int questionId
        )
        {
            Answer MapFromQuery
            (
                Answer answer,
                Question question,
                PossibleAnswer possibleAnswer,
                Doctor doctor, 
                MedicalAppointment medicalAppointment
            )
            {
                answer.Question = question;
                answer.PossibleAnswer = possibleAnswer;
                answer.Doctor = doctor;
                answer.MedicalAppointment = medicalAppointment;

                return answer;
            };

            var query = ScriptManager.GetByName(ScriptManager.FileNames.Answer.ListByQuestionId);

            var result = await UnitOfWork.Connection.QueryAsync<Answer, Question, PossibleAnswer, Doctor, MedicalAppointment, Answer>(
                                                                query,
                                                                MapFromQuery,
                                                                new { questionId },
                                                                UnitOfWork.Transaction);

            return result.ToList();
        }

        public async Task<int> DeleteMany
        (
            List<Answer> deleteList
        )
        {
            var query = new StringBuilder();

            var parameters = new DynamicParameters();

            for (var index = 0; index < deleteList.Count; index++)
            {
                var individualQuery = new StringBuilder();

                individualQuery.Append(ScriptManager.GetByName(ScriptManager.FileNames.Answer.DeleteById));
                individualQuery.Replace("@id", $"@id{index}");

                parameters.Add($"@id{index}", deleteList[index].Id, DbType.Int32);

                query.Append(individualQuery);
            }

            return await UnitOfWork.Connection.ExecuteAsync(query.ToString(),
                                                      parameters,
                                                      UnitOfWork.Transaction);
        }
    }
}
