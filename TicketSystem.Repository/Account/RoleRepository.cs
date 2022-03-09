using TicketSystem.Common.DataAccess;
using TicketSystem.Repository.Interfaces;

namespace TicketSystem.Repository.Account
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
