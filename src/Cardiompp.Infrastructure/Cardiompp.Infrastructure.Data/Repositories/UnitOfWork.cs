using Cardiompp.Domain.Repositories;
using System.Data;

namespace Cardiompp.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        public UnitOfWork(IDbConnection dbConnection)
        {
            Connection = dbConnection;
            Connection.Open();
        }

        private IDoctorRepository _doctorRepository;
        private ICardiomppAgentRepository _cardiomppAgentRepository;

        public IDoctorRepository DoctorRepository => _doctorRepository ??= new DoctorRepository(this);
        public ICardiomppAgentRepository CardiomppAgentRepository => _cardiomppAgentRepository ??= new CardiomppAgentRepository(this);

        public void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
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
