using HealthSup.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IAnswerRepository
    {
        public Task<int> InsertManyAsync
        (
            List<Answer> answers
        );

        public Task<List<Answer>> ListByQuestionId
        (
            int questionId
        );
    }
}
