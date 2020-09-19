using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services
{
    public class NodeDomainService : INodeDomainService
    {
        public NodeDomainService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<Node> GetInitialByDecisionTreeId
        (
            int decisionTreeId
        )
        {
            var initialNode = await _unitOfWork.NodeRepository.GetInitialByDecisionTreeId
            (
                decisionTreeId
            );

            switch (initialNode.NodeType.Id)
            {
                case (int)NodeTypeEnum.Action:
                    var action = await _unitOfWork.ActionRepository.GetByNodeId(initialNode.Id);
                    return action;

                default:
                    var question = await _unitOfWork.QuestionRepository.GetByNodeId(initialNode.Id);
                    question.SetPossibleAnswers(await _unitOfWork.PossibleAnswerRepository.ListByQuestionId(question.Id));
                    return question;
            }
        }

        public async Task<Node> GetById
        (
            int id
        )
        {
            return  await _unitOfWork.NodeRepository.GetById(id);
        }
    }
}
