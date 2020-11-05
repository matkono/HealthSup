using Dapper;
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

            foreach (var answer in answers) 
            {
                var individualQuery = new StringBuilder();
                individualQuery.Append(ScriptManager.GetByName(ScriptManager.FileNames.Answer.Insert));
                individualQuery.Replace("@dateAnswered", $"'{answer.Date}'");
                individualQuery.Replace("@questionId", answer.Question.Id.ToString());
                individualQuery.Replace("@possibleAnswerId", answer.PossibleAnswer.Id.ToString());
                individualQuery.Replace("@doctorId", answer.Doctor.Id.ToString());
                individualQuery.Replace("@medicalAppointmentId", answer.MedicalAppointment.Id.ToString());

                query.Append(individualQuery);
            }

            return await UnitOfWork.Connection.ExecuteAsync(query.ToString(),
                                                      UnitOfWork.Transaction);
        }
    }
}
