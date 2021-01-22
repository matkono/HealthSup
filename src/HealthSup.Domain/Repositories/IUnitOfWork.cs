using System;
using System.Data;

namespace HealthSup.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin
        (
            IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted
        );

        void Commit();

        void Rollback();

        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        IDoctorRepository DoctorRepository { get; }

        IHealthSupAgentRepository HealthSupAgentRepository { get; }

        IUserRepository UserRepository { get; }

        IMedicalAppointmentRepository MedicalAppointmentRepository { get; }

        INodeRepository NodeRepository { get; }

        IQuestionRepository QuestionRepository { get; }

        IActionRepository ActionRepository { get; }

        IDecisionRepository DecisionRepository { get; }

        IPossibleAnswerRepository PossibleAnswerRepository { get; }
        
        IDecisionTreeRuleRepository DecisionTreeRuleRepository { get; }

        IMedicalAppointmentMovementRepository MedicalAppointmentMovementRepository { get; }

        IPossibleAnswerGroupRepository PossibleAnswerGroupRepository { get; }

        IAnswerRepository AnswerRepository { get; }

        IPatientRepository PatientRepository { get; }
    }
}
