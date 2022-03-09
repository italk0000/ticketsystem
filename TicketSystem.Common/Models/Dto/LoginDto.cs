using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TicketSystem.Common.Models.Dto
{
    public class LoginDto
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; } = JwtBearerDefaults.AuthenticationScheme;

        public UserDto User { get; set; }
    }
}
