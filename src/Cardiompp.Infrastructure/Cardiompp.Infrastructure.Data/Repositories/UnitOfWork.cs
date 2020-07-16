﻿using HealthSup.Domain.Repositories;
using System.Data;

namespace HealthSup.Infrastructure.Data.Repositories
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
        private IHealthSupAgentRepository _HealthSupAgentRepository;
        private IUserRepository _userRepository;

        public IDoctorRepository DoctorRepository => _doctorRepository ??= new DoctorRepository(this);

        public IHealthSupAgentRepository HealthSupAgentRepository => _HealthSupAgentRepository ??= new HealthSupAgentRepository(this);

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(this);

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
