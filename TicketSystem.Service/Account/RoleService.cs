using AutoMapper;
using TicketSystem.Common.Models.Dto;
using TicketSystem.Repository.Interfaces;
using TicketSystem.Service.Interfaces;

namespace TicketSystem.Service.Account
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Task<RoleDto> Query(int roleID)
        {
            throw new NotImplementedException();
        }
    }
}
