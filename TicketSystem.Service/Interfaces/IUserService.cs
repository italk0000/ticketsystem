using TicketSystem.Common.Models.Dto;
using TicketSystem.Common.Models.Request;
using TicketSystem.Common.Models.Response;

namespace TicketSystem.Service.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<LoginDto>> Authenticate(LoginRequest request);
    }
}
