using HealthSup.Domain.Repositories;
using System.Data;

namespace HealthSup.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork
        (
            IDbConnection dbConnection
        )
        {
            Connection = dbConnection;
            Connection.Open();
        }

        private IDoctorRepository _doctorRepository;
        private IHealthSupAgentRepository _healthSupAgentRepository;
        private IUserRepository _userRepository;
        private IMedicalAppointmentRepository _medicalAppointmentRepository;
        private INodeRepository _nodeRepository;
        private IQuestionRepository _questionRepository;
        private IActionRepository _actionRepository;
        private IDecisionRepository _decisionRepository;
        private IPossibleAnswerRepository _possibleAnswerRepository;
        private IDecisionTreeRuleRepository _decisionTreeRuleRepository;
        private IMedicalAppointmentMovementRepository _medicalAppointmentMovementRepository;
        private IPossibleAnswerGroupRepository _possibleAnswerGroupRepository;
        private IAnswerRepository _answerRepository;
        private IPatientRepository _patientRepository;
        private IDiseaseRepository _diseaseRepository;
        private IDecisionTreeRepository _decisionTreeRepository;

        public IDoctorRepository DoctorRepository => _doctorRepository ??= new DoctorRepository(this);

        public IHealthSupAgentRepository HealthSupAgentRepository => _healthSupAgentRepository ??= new HealthSupAgentRepository(this);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(this);

        public IMedicalAppointmentRepository MedicalAppointmentRepository => _medicalAppointmentRepository ??= new MedicalAppointmentRepository(this);

        public INodeRepository NodeRepository => _nodeRepository ??= new NodeRepository(this);

        public IQuestionRepository QuestionRepository => _questionRepository ??= new QuestionRepository(this);

        public IActionRepository ActionRepository => _actionRepository ??= new ActionRepository(this);

        public IDecisionRepository DecisionRepository => _decisionRepository ??= new DecisionRepository(this);

        public IPossibleAnswerRepository PossibleAnswerRepository => _possibleAnswerRepository ??= new PossibleAnswerRepository(this);

        public IDecisionTreeRuleRepository DecisionTreeRuleRepository => _decisionTreeRuleRepository ??= new DecisionTreeRuleRepository(this);

        public IMedicalAppointmentMovementRepository MedicalAppointmentMovementRepository => _medicalAppointmentMovementRepository ??= new MedicalAppointmentMovementRepository(this);

        public IPossibleAnswerGroupRepository PossibleAnswerGroupRepository => _possibleAnswerGroupRepository ??= new PossibleAnswerGroupRepository(this);

        public IAnswerRepository AnswerRepository => _answerRepository ??= new AnswerRepository(this);

        public IPatientRepository PatientRepository => _patientRepository ??= new PatientRepository(this);

        public IDiseaseRepository DiseaseRepository => _diseaseRepository ??= new DiseaseRepository(this);

        public IDecisionTreeRepository DecisionTreeRepository => _decisionTreeRepository ?? new DecisionTreeRepository(this);

        public void Begin
        (
            IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted
        )
        {
            Transaction?.Dispose();

            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            Transaction = Connection.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            Transaction?.Commit();
        }

        public void Rollback()
        {
            Transaction?.Rollback();
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Connection?.Dispose();
            Transaction = null;
        }
    }
}
