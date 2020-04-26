using System;
using System.Data;

namespace Cardiompp.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);

        void Commit();

        void Rollback();

        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        IDoctorRepository DoctorRepository { get; }
    }
}
