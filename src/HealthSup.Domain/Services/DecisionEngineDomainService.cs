using HealthSup.Domain.Entities;
using HealthSup.Domain.Enums;
using HealthSup.Domain.Exception;
using HealthSup.Domain.Repositories;
using HealthSup.Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthSup.Domain.Services
{
    public class DecisionEngineDomainService : IDecisionEngineDomainService
    {
        public DecisionEngineDomainService
        (
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
        }

        private readonly IUnitOfWork _unitOfWork;

        public async Task<Node> ResolveNextNode
        (
            int medicalAppointmentId,
            int doctorId,
            int questionId,
            int possibleAnswerGroupId,
            DateTime date,
            List<int> PossibleAnswersId
        )
        {
            var question = await _unitOfWork.QuestionRepository.GetById(questionId);
            var decisionTreeRule = await _unitOfWork.DecisionTreeRuleRepository.GetByFromNodeIdAndPossibleAnswerIdAsync(question.Id, possibleAnswerGroupId);
            var medicalAppointment = new MedicalAppointment() { Id = medicalAppointmentId };

            var answers = new List<Answer>();

            foreach (var possibleAnswerId in PossibleAnswersId) 
            {
                answers.Add(new Answer() 
                {
                    Date = date,
                    Question = question,
                    PossibleAnswer = new PossibleAnswer() { Id = possibleAnswerId },
                    Doctor = new Doctor() { Id = doctorId },
                    MedicalAppointment = new MedicalAppointment() { Id = medicalAppointmentId }
                });
            }

            var medicalAppointmentMoviment = new MedicalAppointmentMovement() 
            {
                FromNode = decisionTreeRule.FromNode,
                ToNode = decisionTreeRule.ToNode,
                MedicalAppointment = medicalAppointment
            };

            await _unitOfWork.AnswerRepository.InsertManyAsync(answers);
            await _unitOfWork.MedicalAppointmentMovementRepository.InsetMovement(medicalAppointmentMoviment);
            await _unitOfWork.MedicalAppointmentRepository.UpdateLastNode(medicalAppointment.Id, decisionTreeRule.ToNode.Id);

            var node = await _unitOfWork.NodeRepository.GetById(decisionTreeRule.ToNode.Id);

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
