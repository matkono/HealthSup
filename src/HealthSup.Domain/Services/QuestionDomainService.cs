using HealthSup.Domain.Entities;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services
{
    public class QuestionDomainService : IQuestionDomainService
    {
        public QuestionDomainService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public Task<Question> GetInitialByDisease
        (
            int diseaseId
        )
        {
            return  _unitOfWork.QuestionRepository.GetInitialByDisease(diseaseId);
        }
    }
}
