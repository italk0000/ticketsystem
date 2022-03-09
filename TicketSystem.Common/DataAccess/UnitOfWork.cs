using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace TicketSystem.Common.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection DbConnection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        private bool _disposed;

        public UnitOfWork(IConfiguration configuration)
        {
            DbConnection = new MySqlConnection(configuration.GetConnectionString("MySql"));
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public void Begin()
        {
            DbConnection.Open();
            Transaction = DbConnection.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
            Transaction.Dispose();
            Transaction = null;
        }

        public void Rollback()
        {
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Transaction != default)
                    {
                        Transaction.Dispose();
                        Transaction = default;
                    }
                    if (DbConnection != default)
                    {
                        DbConnection.Dispose();
                        DbConnection = default;
                    }
                }

                _disposed = true;
            }
        }
    }
}
