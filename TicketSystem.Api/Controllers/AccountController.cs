using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Common.Models.Dto;
using TicketSystem.Common.Models.Request;
using TicketSystem.Common.Models.Response;
using TicketSystem.Service.Interfaces;

namespace TicketSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ApiResponse<LoginDto>> Login(LoginRequest request)
        {
            return await _service.Authenticate(request);
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse<LoginDto>> Post(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
