using TicketSystem.Common.Models.Dto;

namespace TicketSystem.Service.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDto> Query(int roleID);
    }
}
