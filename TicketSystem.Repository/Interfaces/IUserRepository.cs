using TicketSystem.Common.Models.Entity;

namespace TicketSystem.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Query(string username);
    }
}
