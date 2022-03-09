using AutoMapper;
using TicketSystem.Common.Models.Dto;
using TicketSystem.Common.Models.Entity;

namespace TicketSystem.Common.AutoMapper
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Issue, IssueDto>();
        }
    }
}
