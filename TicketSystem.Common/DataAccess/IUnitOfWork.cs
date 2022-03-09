using System.Data;

namespace TicketSystem.Common.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection DbConnection { get; }

        IDbTransaction Transaction { get; }

        void Begin();

        void Commit();

        void Rollback();
    }
}
