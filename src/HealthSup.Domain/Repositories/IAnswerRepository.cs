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
    }
}
