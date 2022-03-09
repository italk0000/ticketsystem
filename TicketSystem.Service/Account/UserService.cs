using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using TicketSystem.Common.Models.Dto;
using TicketSystem.Common.Models.Entity;
using TicketSystem.Common.Models.Request;
using TicketSystem.Common.Models.Response;
using TicketSystem.Repository.Interfaces;
using TicketSystem.Service.Interfaces;

namespace TicketSystem.Service.Account
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LoginDto>> Authenticate(LoginRequest request)
        {
            var user = await _userRepository.Query(request.Username);

            var response = new LoginDto
            {
                AccessToken = GenerateJWT(user),
                User = _mapper.Map<UserDto>(user),
            };

            return new ApiResponse<LoginDto>(data: response);
        }

        private string GenerateJWT(User user)
        {
            var issuedAt = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var expiresTime = DateTime.UtcNow.AddDays(7);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.UserID.ToString()),
                new Claim(JwtClaimTypes.IdentityProvider, "ticketsystem"),
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.SessionId, Guid.NewGuid().ToString()),
                new Claim(JwtClaimTypes.IssuedAt, issuedAt.ToString(), ClaimValueTypes.Integer64),
            };

            foreach (var item in user.Roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, item.RoleID.ToString(), ClaimValueTypes.Integer64));
            }

            // TODO: Get from appsettings
            var secret = "AnAppleADayKeepsTheDoctorAway";
            var issuer = "TicketSystem";
            var audience = "TicketSystem.Api";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(issuer: issuer
                , audience: audience
                , claims: claims
                , expires: expiresTime
                , signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
