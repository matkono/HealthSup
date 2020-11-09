using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Exception;
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

        public async Task<Node> GetCurrentNode
        (
            MedicalAppointment medicalAppointment
        )
        {
            var node = await _unitOfWork.NodeRepository.GetById(medicalAppointment.CurrentNode.Id);

            return await LoadNodeDetails(node.Id, node.NodeType.Id);
        }

        private async Task<Node> LoadNodeDetails
        (
            int nodeId,
            int nodeTypeId
        )
        {
            switch (nodeTypeId)
            {
                case (int)NodeTypeEnum.Action:
                    var action = await _unitOfWork.ActionRepository.GetByNodeId(nodeId);
                    return action;

                case (int)NodeTypeEnum.Question:
                    var question = await _unitOfWork.QuestionRepository.GetByNodeId(nodeId);
                    question.SetPossibleAnswers(await _unitOfWork.PossibleAnswerRepository.ListByQuestionId(question.Id));
                    return question;

                case (int)NodeTypeEnum.Decision:
                    var decision = await _unitOfWork.DecisionRepository.GetByNodeId(nodeId);
                    return decision;

                default:
                    throw new InvalidNodeTypeException("NodeType is invalid.");
            }
        }
    }
}
